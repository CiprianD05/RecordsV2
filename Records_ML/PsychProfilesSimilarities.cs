
using RecordsRepositories.Interfaces;
using Microsoft.ML;
using AutoMapper;

using RecordsDTOs.CitizensDTOs;
using RecordsDTOs.SimilaritiesDTOs;

using RecordsModels;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;


namespace Records_ML
{

    public class PsychProfilesSimilarities:IPsychProfilesSimilarities
    {
        private readonly IPsychologicalProfileRepo psychRepo;
        private readonly ICitizenRepo citizenRepo;
        private IMapper mapper;
        private MLContext mlContext;

        public PsychProfilesSimilarities(IPsychologicalProfileRepo psychRepo, ICitizenRepo citizenRepo, IMapper mapper)
        {

            this.psychRepo = psychRepo;
            this.citizenRepo = citizenRepo;
            this.mapper = mapper;
            this.mlContext = new MLContext();
        }

        public async Task<List<SimilaritiesReadDTO>> CompareProfiles(Citizen targetCitizen)
        {
            var allPsychProfiles = await LoadPsychologicalProfilesFromDatabase();

            // Remove the target citizen's psych profiles from the list
            var otherPsychProfiles = allPsychProfiles.Where(profile => profile.CitizenId != targetCitizen.Id).ToList();

            // Create the MLContext
            var mlContext = new MLContext();

            // Prepare the data for ML.NET
            var data = otherPsychProfiles.Select(profile => new
            {
                Features = profile.Summary,
                Label = 0 // Label 0 for others
            }).ToList();

            // Retrieve the psych profiles of the target citizen
            var targetPsychProfiles = await GetPsychProfilesByCitizenId(targetCitizen.Id);
            data.AddRange(targetPsychProfiles.Select(profile => new
            {
                Features = profile.Summary,
                Label = 1 // Label 1 for target citizen
            }));

            // Convert the data to IDataView
            var dataView = mlContext.Data.LoadFromEnumerable(data);

            // Configure the ML pipeline
            var pipeline = mlContext.Transforms.Text.FeaturizeText("Features", new Microsoft.ML.Transforms.Text.TextFeaturizingEstimator.Options
            {
                OutputTokensColumnName = "Tokens",
                CaseMode = Microsoft.ML.Transforms.Text.TextNormalizingEstimator.CaseMode.Lower,
                KeepDiacritics = false,
                KeepPunctuations = false,
                StopWordsRemoverOptions = new Microsoft.ML.Transforms.Text.StopWordsRemovingEstimator.Options(),
            })
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Label"));

            // Fit the model
            var model = pipeline.Fit(dataView);

            // Create a prediction engine
            var engine = mlContext.Model.CreatePredictionEngine<PredictionData, PredictionResult>(model);

            // Prepare the target citizen data for prediction
            var targetData = new PredictionData
            {
                Features = string.Join(" ", targetPsychProfiles.Select(profile => profile.Summary))
            };

            // Predict the similarity scores for all citizens
            List<SimilaritiesReadDTO> predictions = new List<SimilaritiesReadDTO>();

            foreach (var profile in otherPsychProfiles)
            {
                var otherData = new PredictionData
                {
                    Features = profile.Summary
                };

                var targetFeatures = ParseFeatures(targetData.Features, otherPsychProfiles.Count);
                var otherFeatures = ParseFeatures(otherData.Features, otherPsychProfiles.Count);


                // Compute the cosine similarity
                var cosineSimilarity = CosineSimilarity(targetFeatures, otherFeatures);

                var citizen = await GetCitizenById(profile.CitizenId);

                var similarityReadDTO = new SimilaritiesReadDTO
                {
                    Score = cosineSimilarity,
                    Citizen = new CitizenReadDTO
                    {
                        Id = citizen.Id,
                        FirstName = citizen.FirstName,
                        LastName = citizen.LastName,
                        SocialSecurityNumber = citizen.SocialSecurityNumber,
                        PassportNumber = citizen.PassportNumber
                    }
                };

                predictions.Add(similarityReadDTO);
            }

            return predictions;
        }

        // Load the psychological profiles from the database (replace with your implementation)
        private async Task<List<PsychologicalProfile>> LoadPsychologicalProfilesFromDatabase()
        {
            
            return (List<PsychologicalProfile>)await psychRepo.Get();
        }

        private async Task<List<PsychologicalProfile>> GetPsychProfilesByCitizenId(int citizenId)
        {
            
            return (List<PsychologicalProfile>)await psychRepo.GetAllPsychologicalProfiles(citizenId);
        }

        // Load the citizens from the database (replace with your implementation)
        private async Task<List<Citizen>> LoadCitizensFromDatabase()
        {
            var citizenList = (List<Citizen>)await citizenRepo.GetAllCitizens();

            
            // Implement the logic to load citizens from the database
            return citizenList;
        }
        private async Task< Citizen> GetCitizenById(int citizenId)
        {
            return await citizenRepo.GetAllCitizenById(citizenId);
        }
        private Vector<double> ParseFeatures(string featuresString, int dimension)
        {
            var featureValues = featuresString.Split(' ');
            var featureList = new List<double>();

            foreach (var featureValue in featureValues)
            {
                if (double.TryParse(featureValue, out double feature))
                {
                    featureList.Add(feature);
                }
                else
                {
                    // Handle invalid or non-numeric values
                }
            }

            // Pad or truncate the feature list to match the expected dimensionality
            if (featureList.Count < dimension)
            {
                featureList.AddRange(Enumerable.Repeat(0.0, dimension - featureList.Count));
            }
            else if (featureList.Count > dimension)
            {
                featureList = featureList.Take(dimension).ToList();
            }

            return Vector<double>.Build.DenseOfEnumerable(featureList);
        }
        private float CosineSimilarity(Vector<double> vectorA, Vector<double> vectorB)
        {
            var dotProduct = vectorA.DotProduct(vectorB);
            var magnitudeA = vectorA.L2Norm();
            var magnitudeB = vectorB.L2Norm();

            if (magnitudeA == 0 || magnitudeB == 0)
            {
                return 0f;
            }

            return (float)(dotProduct / (magnitudeA * magnitudeB));
        }
    }
    public class PredictionData
    {
        public string Features { get; set; }
        public int Label { get; set; }
    }

    public class PredictionResult
    {
        public float Score { get; set; }
    }

    
}
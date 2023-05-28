
using RecordsRepositories.Interfaces;
using Microsoft.ML;
using AutoMapper;
using RecordsDTOs.SimilaritiesDTOs;
using RecordsDTOs.CitizensDTOs;
using RecordsModels;


namespace Records_ML
{

    public class PsychProfilesSimilarities:IPsychProfilesSimilarities
    {
        private readonly IPsychologicalProfileRepo psychRepo;
        private readonly ICitizenRepo citizenRepo;
        private IMapper mapper;

        public PsychProfilesSimilarities(IPsychologicalProfileRepo psychRepo, ICitizenRepo citizenRepo, IMapper mapper)
        {

            this.psychRepo = psychRepo;
            this.citizenRepo = citizenRepo;
            this.mapper = mapper;

        }

        public async Task<List<SimilaritiesReadDTO>> CompareProfiles(Citizen targetCitizen) 
        {
            
            var mlContext = new MLContext();

            // Load the psychological profiles and citizens from your database
            var profiles =await  LoadPsychologicalProfilesFromDatabase();
            var citizens = await LoadCitizensFromDatabase();

            // Create the training data using the target citizen's profiles
            var targetCitizenId = targetCitizen.Id; 
            var targetProfiles = profiles.Where(p => p.CitizenId == targetCitizenId).ToList();

            // Create the training dataset
            var trainingData = mlContext.Data.LoadFromEnumerable(targetProfiles);

            // Define the similarity pipeline
            var similarityPipeline = mlContext.Transforms.Conversion.MapValueToKey("CitizenId")
                .Append(mlContext.Transforms.Text.FeaturizeText("Features", nameof(PsychologicalProfile.Summary)))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.Transforms.Concatenate("Features"))
                .Append(mlContext.Transforms.NormalizeMinMax("Features"))
                .Append(mlContext.Transforms.Conversion.MapKeyToValue("CitizenId"))
                .Append(mlContext.Transforms.SelectColumns("Score", "Features", nameof(PsychologicalProfile.CitizenId)));

            // Fit the similarity pipeline to the training data
            var trainedPipeline = similarityPipeline.Fit(trainingData);

            // Create the test dataset using the rest of the population's profiles
            var testProfiles = profiles.Where(p => p.CitizenId != targetCitizenId).ToList();
            var testData = mlContext.Data.LoadFromEnumerable(testProfiles);

            // Apply the trained similarity pipeline to the test data
            var transformedData = trainedPipeline.Transform(testData);

            // Get the similarity scores and citizens from the transformed data
            return (List<SimilaritiesReadDTO>)mlContext.Data.CreateEnumerable<SimilaritiesReadDTO>(transformedData, reuseRowObject: false).OrderByDescending(sm => sm.Score).Take(5);

            

        }
        // Load the psychological profiles from the database (replace with your implementation)
        private async Task<List<PsychologicalProfile>> LoadPsychologicalProfilesFromDatabase()
        {
            
            return (List<PsychologicalProfile>)await psychRepo.Get();
        }

        // Load the citizens from the database (replace with your implementation)
        private async Task<List<CitizenReadDTO>> LoadCitizensFromDatabase()
        {
            var citizenList = (List<Citizen>)await citizenRepo.GetAllCitizens();

            
            // Implement the logic to load citizens from the database
            return (List<CitizenReadDTO>)mapper.Map<IEnumerable<CitizenReadDTO>>(citizenList);
        }


    }
}
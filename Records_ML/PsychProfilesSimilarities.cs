
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
            var targetPsychProfilesList=await GetPsychProfilesByCitizenId(targetCitizen.Id);
            var targetPsychPorfile=(from l in targetPsychProfilesList select l).FirstOrDefault();

            var allPsychProfiles = await LoadPsychologicalProfilesFromDatabase();
            // Remove the target citizen's psych profiles from the list
            var otherPsychProfiles = allPsychProfiles.Where(profile => profile.CitizenId != targetCitizen.Id).ToList();

            // Create the MLContext
            var mlContext = new MLContext();

            var data = mlContext.Data.LoadFromEnumerable(otherPsychProfiles);

            var pipeline = mlContext.Transforms.Text.NormalizeText("NormalizedText", "Summary", keepPunctuations: false)
                .Append(mlContext.Transforms.Text.TokenizeIntoWords("Tokens", "NormalizedText"))
                .Append(mlContext.Transforms.Conversion.MapValueToKey("Tokens"))
                .Append(mlContext.Transforms.Text.ProduceNgrams("Tokens"))
                .Append(mlContext.Transforms.Text.LatentDirichletAllocation("Features", "Tokens", numberOfTopics: otherPsychProfiles.Count));

            var transform = pipeline.Fit(data);

            var predictionEngine=mlContext.Model.CreatePredictionEngine<PsychologicalProfile,PredictionResult>(transform);

            var similarities = new List<SimilaritiesReadDTO>();


            foreach (var profile in otherPsychProfiles)
            {
                var prediction = predictionEngine.Predict(profile);
                similarities.Add(new SimilaritiesReadDTO
                {
                    Score = prediction.Features[0], // Modify this line based on your requirements
                    Citizen = new CitizenReadDTO // Modify this line based on your requirements
                    {
                        // Populate the properties of the CitizenReadDTO object based on the Citizen object
                        Id = profile.Citizen.Id,
                        FirstName = profile.Citizen.FirstName,
                        LastName = profile.Citizen.LastName,
                        SocialSecurityNumber = profile.Citizen.SocialSecurityNumber,
                        PassportNumber = profile.Citizen.PassportNumber
                    }
                });
            }

            return similarities;





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
        
       
    }
   

    public class PredictionResult
    {
        public float[] Features { get; set; }
    }

    
}
using RestSharp;
using APITraining.Session3.DataModels;
using APITraining.Session3.Resources;
using APITraining.Session3.Tests.TestData;

namespace APITraining.Session3.Helpers
{
    internal class PetHelper
    {
        /// <summary>
        /// Send POST request to add new pet
        /// </summary>

        public static async Task<PetModel> AddNewPet(RestClient restClient)
        {
            var newPetData = GeneratePet.pet();

            var postRestRequest = new RestRequest(Endpoints.CreateOrUpdatePet());

            // Send POST request to add new pet

            postRestRequest.AddJsonBody(newPetData);
            var postRestResponse = await restClient.ExecutePostAsync<PetModel>(postRestRequest);

            // Return created pet data
            return newPetData;
        }

        /// <summary>
        /// Send GET request to find pet by Id
        /// </summary>

        public static async Task<RestResponse<PetModel>> FindPetById(RestClient restClient, long petId)
        {
            var getRestRequest = new RestRequest(Endpoints.FindOrDeletePetById(petId));

            // Send GET request to find pet by Id

            var getRestResponse = await restClient.ExecuteGetAsync<PetModel>(getRestRequest);

            // Return response pet
            return getRestResponse;
        }
    }
}

using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APITraining.Session3
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
            await restClient.ExecutePostAsync<PetModel>(postRestRequest);

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

using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITraining.Session3
{
    internal class Endpoints
    {

        public static readonly string BaseURL = "https://petstore.swagger.io/v2/";

        public static readonly string PetEndpoint = "pet";

        public static string FindOrDeletePetById(long petId) => $"{BaseURL}/{PetEndpoint}/{petId}";

        public static string CreateOrUpdatePet() => $"{BaseURL}/{PetEndpoint}";

    }
}

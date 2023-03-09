using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APITraining.Session2._1
{
    [TestClass]
    public class HTTPClientTests
    {
        private static HttpClient httpClient;

        private static readonly string BaseURL = "https://petstore.swagger.io/v2/";

        private static readonly string PetEndpoint = "pet";

        private static string GetURL(string endpoint) => $"{BaseURL}{endpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<PetModel> cleanUpList = new List<PetModel>();

        [TestInitialize]
        public void TestInitialize()
        {
            httpClient = new HttpClient();
        }

        [TestCleanup]
        public async Task TestCleanUp()
        {
            foreach (var data in cleanUpList)
            {
                var httpResponse = await httpClient.DeleteAsync(GetURL($"{PetEndpoint}/{data.Id}"));
            }
        }

        [TestMethod]
        public async Task PutMethod()
        {

            #region create pet data

            // Create Json Object

            CategoryModel categoryData = new CategoryModel()
            {
                Id = 0,
                Name = "string"
            };

            var RandomInt64 = new Random();

            PetModel petData = new PetModel()
            {
                Id = RandomInt64.NextInt64(),
                Category = categoryData,
                Name = "Blue",
                PhotoUrls = new string[] { "string" },
                Tags = new CategoryModel[] { categoryData },
                Status = "available"
            };

            // Serialize Content
            var request = JsonConvert.SerializeObject(petData);
            var postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Post Request
            await httpClient.PostAsync(GetURL(PetEndpoint), postRequest);

            #endregion

            #region get Id of the created data

            // Get Request
            var getResponse = await httpClient.GetAsync(GetURI($"{PetEndpoint}/{petData.Id}"));

            // Deserialize Content
            var listData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

            #endregion

            #region send put request to update data

            // Update value of status
            petData = new PetModel()
            {
                Id = listData.Id,
                Category = listData.Category,
                Name = listData.Name,
                PhotoUrls = listData.PhotoUrls,
                Tags = listData.Tags,
                Status = "sold"
            };

            // Serialize Content
            request = JsonConvert.SerializeObject(petData);
            postRequest = new StringContent(request, Encoding.UTF8, "application/json");

            // Send Put Request
            var httpResponse = await httpClient.PutAsync(GetURL($"{PetEndpoint}"), postRequest);

            // Get Status Code
            var statusCode = httpResponse.StatusCode;

            #endregion

            #region get updated data

            // Get Request
            getResponse = await httpClient.GetAsync(GetURI($"{PetEndpoint}/{petData.Id}"));

            // Deserialize Content
            listData = JsonConvert.DeserializeObject<PetModel>(getResponse.Content.ReadAsStringAsync().Result);

            #endregion

            #region cleanup data

            // Add data to cleanup list
            cleanUpList.Add(listData);

            #endregion

            #region assertion

            // Assertion
            Assert.AreEqual(HttpStatusCode.OK, statusCode, "Status code is not equal to 200");
            Assert.AreEqual(petData.Status, listData.Status, "Status is not matching");

            #endregion

        }
    }
}

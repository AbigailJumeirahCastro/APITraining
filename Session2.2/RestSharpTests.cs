using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace APITraining.Session2._2
{
    [TestClass]
    public class RestSharpTests
    {
        private static RestClient restClient;

        private static readonly string BaseURL = "https://petstore.swagger.io/v2/";

        private static readonly string PetEndpoint = "pet";

        private static string GetURL(string endpoint) => $"{BaseURL}{endpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<PetModel> cleanUpList = new List<PetModel>();

        [TestInitialize]

        public async Task TestInitialize()
        {
            restClient = new RestClient();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in cleanUpList)
            {
                var restRequest = new RestRequest(GetURI($"{PetEndpoint}/{data.Id}"));
                await restClient.DeleteAsync(restRequest);
            }
        }

        [TestMethod]
        public async Task PostMethod()
        {
            #region create pet data

            // Create JSON Object

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

            // Send Post Request
            var postRestRequest = new RestRequest(GetURI(PetEndpoint)).AddJsonBody(petData);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);

            // Add data to cleanup list
            cleanUpList.Add(petData);

            #endregion

            #region assert post request is successful

            // Assertion

            Assert.AreEqual(HttpStatusCode.OK, postRestResponse.StatusCode, "Status code for post is not equal to 200");

            #endregion

            #region get pet data

            var getRestRequest = new RestRequest(GetURI($"{PetEndpoint}/{petData.Id}"));
            var getRestResponse = await restClient.ExecuteGetAsync<PetModel>(getRestRequest);

            #endregion

            #region assert pet data if values are correct

            // Assertion
            Assert.AreEqual(HttpStatusCode.OK, getRestResponse.StatusCode, "Status code for get is not equal to 200");
            Assert.AreEqual(petData.Name, getRestResponse.Data.Name, "Name did not match");
            Assert.AreEqual(petData.Category.Id, getRestResponse.Data.Category.Id, "Category.Id did not match");
            Assert.AreEqual(petData.Category.Name, getRestResponse.Data.Category.Name, "Category.Name did not match");
            CollectionAssert.AreEqual(petData.PhotoUrls, getRestResponse.Data.PhotoUrls, "PhotoUrls did not match");
            if (petData.Tags.Length == getRestResponse.Data.Tags.Length) {
                for (int ctr = 0; ctr < petData.Tags.Length; ctr++) {
                    Assert.AreEqual(petData.Tags[ctr].Id, getRestResponse.Data.Tags[ctr].Id, $"Tags.Id[{ctr}] did not match");
                    Assert.AreEqual(petData.Tags[ctr].Name, getRestResponse.Data.Tags[ctr].Name, $"Tags.Name[{ctr}] did not match");
                }
            } else {
                Assert.Fail("Tags did not match");
            }         
            Assert.AreEqual(petData.Status, getRestResponse.Data.Status, "Status did not match");

            #endregion

        }

    }
}

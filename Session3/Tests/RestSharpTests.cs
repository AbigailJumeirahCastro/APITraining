using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using APITraining.Session3.DataModels;
using APITraining.Session3.Helpers;
using APITraining.Session3.Resources;

namespace APITraining.Session3.Tests
{
    [TestClass]
    public class RestSharpTests : BaseTest
    {
        private readonly List<PetModel> cleanUpList = new List<PetModel>();

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in cleanUpList)
            {
                var restRequest = new RestRequest(Endpoints.FindOrDeletePetById(data.Id));
                await restClient.DeleteAsync(restRequest);
            }
        }

        [TestMethod]
        public async Task PostMethod()
        {
            // Add new pet
            PetModel petData = await PetHelper.AddNewPet(restClient);
            cleanUpList.Add(petData);

            // Get pet data
            var getRestResponse = await PetHelper.FindPetById(restClient, petData.Id);

            // Assert Values
            Assert.AreEqual(HttpStatusCode.OK, getRestResponse.StatusCode, "Status code for get is not equal to 200");
            Assert.AreEqual(petData.Name, getRestResponse.Data.Name, "Name did not match");
            Assert.AreEqual(petData.Category.Id, getRestResponse.Data.Category.Id, "Category.Id did not match");
            Assert.AreEqual(petData.Category.Name, getRestResponse.Data.Category.Name, "Category.Name did not match");
            CollectionAssert.AreEqual(petData.PhotoUrls, getRestResponse.Data.PhotoUrls, "PhotoUrls did not match");
            if (petData.Tags.Length == getRestResponse.Data.Tags.Length)
            {
                for (int ctr = 0; ctr < petData.Tags.Length; ctr++)
                {
                    Assert.AreEqual(petData.Tags[ctr].Id, getRestResponse.Data.Tags[ctr].Id, $"Tags.Id[{ctr}] did not match");
                    Assert.AreEqual(petData.Tags[ctr].Name, getRestResponse.Data.Tags[ctr].Name, $"Tags.Name[{ctr}] did not match");
                }
            }
            else
            {
                Assert.Fail("Tags did not match");
            }
            Assert.AreEqual(petData.Status, getRestResponse.Data.Status, "Status did not match");

        }

    }
}

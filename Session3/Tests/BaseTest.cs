using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;

namespace APITraining.Session3.Tests
{

    public class BaseTest
    {
        public RestClient restClient { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            restClient = new RestClient();
        }

    }

}

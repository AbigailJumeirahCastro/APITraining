using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APITraining.Session3
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

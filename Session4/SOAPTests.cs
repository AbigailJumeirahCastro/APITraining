using CIServiceReference;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace APITraining.Session4
{
    [TestClass]
    public class SOAPTests
    {
        public CountryInfoServiceSoapTypeClient ciSoapClient { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            ciSoapClient = new CountryInfoServiceSoapTypeClient(CountryInfoServiceSoapTypeClient.EndpointConfiguration.CountryInfoServiceSoap);
        }

        [TestMethod]
        public void TestListOfCountryNamesByCodeAscendingOrder()
        {
            // Make API call to get country list
            var countryList = ciSoapClient.ListOfCountryNamesByCode();
            var countryCodes = countryList.Select(x => x.sISOCode).ToList();

            // Generate sorted country codes for comparison
            var sortedCountryCodes = new List<string>(countryCodes);
            sortedCountryCodes.Sort();

            // Assert if country list is by ascending order of country code
            CollectionAssert.AreEqual(countryCodes, sortedCountryCodes, "The country names are not in ascending order of country code.");

        }

        [TestMethod]
        public void TestInvalidCountryCodeReturnsCountryNotFound()
        {
            // Declare an invalid country code
            var invalidCountryCode = "XX";

            // Make API call with the invalid country code
            var countryName = ciSoapClient.CountryName(invalidCountryCode);

            // Assert that API call returns "Country not found in the database"
            Assert.AreEqual("Country not found in the database", countryName, "The API call did not return 'Country not found in the database'.");
        }

        [TestMethod]
        public void TestCountryNameMatchesFromBothAPIs()
        {
            // Make API call to get country list
            var countryList = ciSoapClient.ListOfCountryNamesByCode();

            // Get the last country from the list
            var lastCountry = countryList.Last();

            // Get the country name from last country
            var countryNameFromList = lastCountry.sName;

            // Make the API call with the country code from last country
            var countryCode = lastCountry.sISOCode;
            var countryName = ciSoapClient.CountryName(countryCode);

            // Assert that country names from both APIs are the same
            Assert.AreEqual(countryNameFromList, countryName, "The country name from ListOfCountryNamesByCode() and CountryName() APIs did not match.");
        }
    }
}

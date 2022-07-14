using UnReaL.Models;
using UnReaL.Repository;

namespace UnReaL.Tests
{
    public class BijectionTests
    {
        private IBijectionService _service;

        [SetUp]
        public void Setup() =>_service = new BijectionService();

        [Test]
        public void IsBijectionServiceAccessible_ReturnTrue() 
            => Assert.That(_service, Is.Not.EqualTo(null));

        [Test]
        public void IsEncodedStringShorter_ReturnTrue()
        {
            var testUrl = new ShortURL
            {
                Id = 1234,
                Url = "https://en.wikipedia.org/wiki/Bijection#:~:text=In%20mathematics%2C%20a%20bijection%2C%20also,with%20exactly%20one%20element%20of"
            };
            var encodedVal = $"https://www.UnReaL.com/{_service.Encode(testUrl.Id)}";

            Assert.That(encodedVal, Has.Length.LessThan(testUrl.Url.Length));
        }

        [Test]
        public void IsDecodedUrlAValidId_ReturnTrue()
        {
            var testUrl = new ShortURL
            {
                Id = 1234,
                Url = "https://ww.google.com"
            };
            var encodedVal = _service.Encode(testUrl.Id);
            var decodedId = _service.Decode(encodedVal);

            Assert.That(decodedId, Is.EqualTo(testUrl.Id));
        }
    }
}
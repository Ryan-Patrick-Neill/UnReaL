using Microsoft.EntityFrameworkCore;
using UnReaL.Database;
using UnReaL.Models;
using UnReaL.Repository;

namespace UnReaL.Tests
{
    public class UnReaLServiceTests
    {
        private IBijectionService _bijectionService;
        private IUnReaLService _service;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<UnReaLContext>()
                .UseInMemoryDatabase(databaseName: "Db").Options;
            var context = new UnReaLContext(options);

            _bijectionService = new BijectionService();
            _service = new UnReaLService(context, _bijectionService);
        }

        [Test]
        public void IsUnReaLServiceAccessible_ReturnTrue()
            => Assert.That(_service, Is.Not.EqualTo(null));

        [Test]
        public void CanSaveUrl_ReturnTrue()
        {
            var testUrl = new ShortURL
            {
                Id = 0,
                Url = "https://www.google.com"
            };

            testUrl.Id = _service.Save(testUrl);

            Assert.That(_service.GetById(testUrl.Id).Url, Is.EqualTo(testUrl.Url));
        }

        [Test]
        public void CanSaveDuplicateUrl_ReturnsFalse()
        {
            var testUrl = new ShortURL
            {
                Id = 0,
                Url = "https://www.bbc.co.uk/news"
            };

            testUrl.Id = _service.Save(testUrl);
            _service.Save(testUrl);

            var result = _service.GetById(testUrl.Id+1);

            Assert.That(result, Is.Null);
        }
    }
}

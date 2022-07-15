using UnReaL.Repository;

namespace UnReaL.Tests
{
    public class URLValidationServiceTests
    {
        private IURLValidationService _service;

        [SetUp]
        public void Setup() => _service = new URLValidationService();

        [Test]
        public void IsValidationServiceAccessible_ReturnTrue()
            => Assert.That(_service, Is.Not.EqualTo(null));

        [Test]
        public void ValidSimpleUrlPassesValidation_ReturnTrue()
        {
            var errorString = _service.ValidateInput("https://www.google.com");

            Assert.That(string.IsNullOrEmpty(errorString));
        }

        [Test]
        public void ValidComplexUrlPassesValidation_ReturnTrue()
        {
            var errorString = _service.ValidateInput("https://www.google.com/maps/place/Belfast+City+Hall/@54.596658,-5.9289734,17.15z/data=!4m5!3m4!1s0x486108562c8242a1:0xa923f9ba0ada408!8m2!3d54.5965028!4d-5.9300752");

            Assert.That(string.IsNullOrEmpty(errorString));
        }

        [Test]
        public void EmptyStringFailsValidation_ReturnTrue()
        {
            var errorString = _service.ValidateInput(string.Empty);

            Assert.That(errorString, Does.Contain("Input field is empty."));
        }

        [Test]
        public void StringOfWhiteSpaceFailsValidation_ReturnTrue()
        {
            var errorString = _service.ValidateInput("            ");

            Assert.That(errorString, Does.Contain("Input field is empty."));
        }

        [Test]
        public void RFC3986ReservedCharsFailValidation_ReturnTrue()
        {
            var errorString = _service.ValidateInput(":/? #[]@");

            Assert.That(errorString, Does.Contain("Invalid characters detected."));
        }

        [Test]
        public void RandomStringFailsValidation_ReturnTrue()
        {
            var errorString = _service.ValidateInput("zHlFROt4MI");

            Assert.That(errorString, Does.Contain("Malformed URL detected."));
        }

        [Test]
        public void InvalidUrlFailsValidation_ReturnTrue()
        {
            var errorString = _service.ValidateInput("httpwww.google.biz-fwrdslash-mapz");

            Assert.That(errorString, Is.Not.Empty);
        }
    }
}

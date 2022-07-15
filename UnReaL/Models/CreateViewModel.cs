using UnReaL.Repository;

namespace UnReaL.Models
{
    public class CreateViewModel
    {
        private readonly IURLValidationService _service = new URLValidationService();

        public ShortURL ShortURL { get; set; } = new ShortURL();
        public string ErrorString { get; set; } = string.Empty;

        public string ValidateInput(string input) => _service.ValidateInput(input);
    }
}

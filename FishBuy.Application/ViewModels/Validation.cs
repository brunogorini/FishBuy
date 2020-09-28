using System.Collections.Generic;
using System.Linq;

namespace FishBuy.Application
{
    public abstract class Validation
    {
        private List<string> _validationMessages { get; set; }

        private List<string> ValidationMessages
        {
            get { return _validationMessages ?? (_validationMessages = new List<string>()); }
        }

        protected void ClearValidationMessage()
        {
            ValidationMessages.Clear();
        }

        protected void AddMessage(string message)
        {
            ValidationMessages.Add(message);
        }

        public string GetValidationMessages()
        {
            return string.Join(". ", ValidationMessages);
        }

        public List<string> GetValidationMessagesList()
        {
            return ValidationMessages;
        }

        public abstract void Validate();

        public bool IsValid
        {
            get { return !ValidationMessages.Any(); }
        }
    }
}

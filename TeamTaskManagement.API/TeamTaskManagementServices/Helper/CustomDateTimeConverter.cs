using Newtonsoft.Json.Converters;

namespace TeamTaskManagement.Services.Helper.Configuration
{
    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            base.DateTimeFormat = "dd/MM/yyyy";
        }
    }
}

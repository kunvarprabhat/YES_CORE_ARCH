
namespace YES.Dtos.API
{
    public class APIException : ApplicationException
    {
        public APIException()
        {
        }

        public APIException(string message)
            : base(message)
        {
        }

        public APIException(string message, Exception? inner)
            : base(message, inner)
        {
        }

        //Overriding the HelpLink Property
        public override string HelpLink
        {
            get
            {
                return "Get More Information from here: https://sitelink.com/customPageForErrorDescription";
            }
        }
    }
}

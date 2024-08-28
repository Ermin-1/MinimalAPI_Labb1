using System.Net;

namespace Library_API.Models
{
    public class APIResponse
    {

        //tom lista redo för att fånga upp felmeddelanden
        public APIResponse()
        {
            ErrorMessages = new List<string>();
        }

        public bool IsSuccess { get; set; }
        public Object Result { get; set; }
        public HttpStatusCode  Statuscode { get; set; }
        public List<string> ErrorMessages { get; set; }

    }
}

using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/")]
    public class WelcomeController : ControllerBase
    {
        private WelcomeMessage _message ;
        [HttpGet]
        public string SayHello() =>_message.Message;
        public WelcomeController(WelcomeMessage welcomeMessage)
        {
                _message = welcomeMessage;
        }
    }
}
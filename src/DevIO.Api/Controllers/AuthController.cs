using DevIO.Business.Interfaces;

namespace DevIO.Api.Controllers
{
    public class AuthController : MainController
    {
        public AuthController(INotifier notifier) : base(notifier)
        {
        }
    }
}

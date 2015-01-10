namespace AccountSystem.Web.Controllers
{
    using System.Web.Mvc;

    using AccountSystem.Data;

    [Authorize(Roles="Admin, AccountHolder")]
    public class BaseController : Controller
    {
        protected ApplicationDbContext context;

        public BaseController()
        {
            this.context = new ApplicationDbContext();
        }
	}
}
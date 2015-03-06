namespace AccountSystem.Web.Mailers
{
    using Mvc.Mailer;

    public interface IUserMailer
    {
			MvcMailMessage Invoice(string email);
	}
}
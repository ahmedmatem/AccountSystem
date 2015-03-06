namespace AccountSystem.Web.Mailers
{
    using Mvc.Mailer;

    public class UserMailer : MailerBase, IUserMailer 	
	{
		public UserMailer()
		{
			MasterName="_Layout";
		}
		
		public virtual MvcMailMessage Invoice(string email)
		{
			//ViewBag.Data = someObject;
			return Populate(x =>
			{
				x.Subject = "Invoice";
				x.ViewName = "Invoice";
                x.To.Add(email);
			});
		}
 	}
}
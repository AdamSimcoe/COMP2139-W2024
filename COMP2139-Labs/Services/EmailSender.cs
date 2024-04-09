using Microsoft.AspNetCore.Identity.UI.Services;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace COMP2139_Labs.Services
{
	public class EmailSender : IEmailSender
	{
		private readonly string _sendGridKey;

		public EmailSender(IConfiguration configuration)
		{
			_sendGridKey = configuration["SendGrid:ApiKey"];
		}

		public async Task SendEmailAsync(string email, string subject, string htmlMessage)
		{
			var client = new SendGridClient(_sendGridKey);
			var msg = new SendGridMessage()
			{
				From = new EmailAddress("101442161@georgebrown.ca", "COMP 2139 Labs"),
				Subject = subject,
				PlainTextContent = htmlMessage,
				HtmlContent = htmlMessage
			};
			msg.AddTo(new EmailAddress(email));

			var response = await client.SendEmailAsync(msg);
			
			/*
			var from = new EmailAddress("101442161@georgebrown.ca", "COMP 2139 Labs");
			var to = new EmailAddress(email);
			var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlMessage);
			await client.SendEmailAsync(msg); */
		}
	}
}

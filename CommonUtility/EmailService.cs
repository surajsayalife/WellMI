using System.Net.Mail;
using WellMI.Auth;
using WellMI.Models;

namespace WellMI.CommonUtility
{
    public class EmailService
    {
        public string _environment = string.Empty;
        public EmailService (  )
        {

            //_environment = environment;

        }
        public bool SendEmail ( string toEmail, string subject, string body, List<string> EmailToBcc = null )
        {
            var SMTPServer = Environment.GetEnvironmentVariable ( "SMTPServer" ) != null ? Environment.GetEnvironmentVariable ( "SMTPServer" ) : "smtppro.zoho.com";
            var SMTPport = Convert.ToInt32 ( Environment.GetEnvironmentVariable ( "SMTPport" ) != null ? Environment.GetEnvironmentVariable ( "SMTPport" ) : "587" );
            var From = Environment.GetEnvironmentVariable ( "FromAddress" ) != null ? Environment.GetEnvironmentVariable ( "FromAddress" ) : "saya-noreply@saya.life";
            var FromPassword = Environment.GetEnvironmentVariable ( "FromPassword" ) != null ? Environment.GetEnvironmentVariable ( "FromPassword" ) : "ivD3CaK0Y1z5";
            var FromName = Environment.GetEnvironmentVariable ( "FromDisplayName" ) != null ? Environment.GetEnvironmentVariable ( "FromDisplayName" ) : "SayaLife WebSite";
            var SMTPEnableSSL = true;
            var SMTPUseDefaultCredentials = false;

            MailAddress fromEmail = new MailAddress ( From, FromName );
            MailMessage messages = new MailMessage ( fromEmail.ToString (), toEmail );
            messages.Subject = subject;
            messages.Body = body;
            messages.IsBodyHtml = true;

            if ( EmailToBcc != null )
            {
                foreach ( string bcc in EmailToBcc )
                {
                    messages.Bcc.Add ( new MailAddress ( bcc ) );
                }
            }

            SmtpClient client = new SmtpClient ( SMTPServer, SMTPport );
            client.EnableSsl = true;

            if ( !SMTPEnableSSL )
            {
                client.EnableSsl = SMTPEnableSSL.ToString ().ToLower () == "true" ? true : false;
            }
            if ( !SMTPUseDefaultCredentials )
            {
                client.UseDefaultCredentials = SMTPUseDefaultCredentials.ToString ().ToLower () == "true" ? true : false;
            }
            client.Credentials = new System.Net.NetworkCredential ( From, FromPassword );
            try
            {
                client.Send ( messages );

                bool result = InsertEmail ( From, FromName, toEmail, string.Empty, subject, body, EmailToBcc, true );

                return result;
            }
            catch ( Exception e )
            {
                bool result = InsertEmail ( From, FromName, toEmail, string.Empty, subject, body, EmailToBcc, false );

                string message = e.Message;
                string messag1e = e.InnerException.Message;
                return false;
            }
        }


        private bool InsertEmail ( string from, string FromName, string toEmail, string toName, string subject, string body, List<string> EmailToBcc, bool isSent )
        {
            using ( var context = new UserContext ( _environment ) )
            {
                string emailToBCC = EmailToBcc != null ? string.Join ( ";", EmailToBcc ) : "Null";

                var emailHistory = new EmailHistory
                {
                    From = from,
                    FromName = FromName,
                    To = toEmail,
                    ToName = toName,
                    Subject = subject,
                    Body = body,
                    Bcc = emailToBCC,
                    CreatedOn = DateTime.Now,
                    IsSent = isSent

                };

                 context.EmailHistory.Add ( emailHistory );               
                int rowsAffected = context.SaveChanges ();

                return rowsAffected > 0;
            }
        }

    }
}

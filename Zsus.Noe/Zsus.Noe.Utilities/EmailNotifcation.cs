using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using NServiceBus.Logging;

namespace Zsus.Noe.Utilities
{
    public class EmailNotifcationFactory
    {
        private static EmailNotifcation _inst = null;

        public static EmailNotifcation GetEmailNotifcation()
        {
            if (_inst != null)
            {
                return _inst;
            }
            else
            {
                _inst = new EmailNotifcation();
                return _inst;
            }
        }
    }

    public class EmailNotifcation : IEmailNotification<EmailParameters>
    {
        public void SendNeedsApproval(EmailParameters parameters)
        {
            string to = @"curtis.fell@workflowstudios.com";
            string from = @"curtis.fell@gmail.com";
            string subject = @"NOE Needs Approval Processing";
            string body = @"<p>NOE requires approval processing, please select this <a href='http://wfsw7pro01:3000/noe-approval?noeid=" + parameters.NoeId + "&sagaid=" + parameters.SagaId + "&flag=1'>link</a> to process this NOE approval request.";
            Send(to, from, subject, body);
        }

        public void SendNeedsNegotiation(EmailParameters parameters)
        {
            string to = @"curtis.fell@workflowstudios.com";
            string from = @"curtis.fell@gmail.com";
            string subject = @"NOE Needs Negotiation Processing";
            string body = @"<p>NOE requires negotiation processing, please select this <a href='http://wfsw7pro01:3000/noe-negotiation?noeid=" + parameters.NoeId + "&sagaid=" + parameters.SagaId + "&flag=1'>link</a> to process this NOE negotiation request.";
            Send(to, from, subject, body);
        }

        public void SendNeedsOffering(EmailParameters parameters)
        {
            string to = @"curtis.fell@workflowstudios.com";
            string from = @"curtis.fell@gmail.com";
            string subject = @"NOE Needs Offer Processing";
            string body = @"<p>NOE requires offer processing, please select this <a href='http://wfsw7pro01:3000/noe-offering?noeid=" + parameters.NoeId + "&sagaid=" + parameters.SagaId + "&flag=1'>link</a> to process this NOE offering request.";
            Send(to, from, subject, body);
        }

        private void Send(string to, string from, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

            mail.From = new MailAddress(from);
            mail.To.Add(to);
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(@"", @"");
            SmtpServer.EnableSsl = true;
            try { SmtpServer.Send(mail); }
            catch(Exception e)
            {
                int i = 0;
            }

        }
    }
}

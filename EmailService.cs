using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using PrescribingSystem.Data;
using PrescribingSystem.Models;


namespace PrescribingSystem.Services
{
    public class EmailService
    {
        private readonly SmtpSettings _settings;

        public EmailService(IOptions<SmtpSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendMailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress("Ibhayi Pharmacy-Group 09", _settings.Username));
            email.To.Add(MailboxAddress.Parse(to));  // make sure 'to' is not null/empty
            email.Subject = subject;
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = body };

            using var smtp = new SmtpClient();
            try
            {
                // First try STARTTLS on 587
                await smtp.ConnectAsync(_settings.Server, 587, SecureSocketOptions.StartTls);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[EmailService] Port 587 failed: {ex.Message}. Retrying with 465...");

                // If 587 fails, fallback to SSL on 465
                await smtp.ConnectAsync(_settings.Server, 465, SecureSocketOptions.SslOnConnect);
            }
            await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task SendPrescriptionDispensedEmailAsync(ApplicationUser customer, int prescriptionId)
        {
            if (customer == null || string.IsNullOrWhiteSpace(customer.Email)) return;

            string subject = "Your Prescription is Ready For Collection - GROUP 09";
            string body = $@"
                 <h3>Prescription Ready for Collection -Group 09</h3>
                  <p>Hi {customer.FirstName} {customer.LastName},</p>
                  <p>Your prescription #{prescriptionId} has been dispensed and is ready for collection.</p>
                  
                  <p>Thank you for using Ibhayi Pharmacy service!</p> ";

            await SendMailAsync(customer.Email, subject, body);
        }

        // New method for multiple prescriptions
        public async Task SendMultiplePrescriptionDispensedEmailAsync(ApplicationUser customer, IEnumerable<int> prescriptionIds)
        {
            if (customer == null || string.IsNullOrWhiteSpace(customer.Email)) return;

            string subject = "Your Prescriptions are Ready For Collection - GROUP 09";
            string scriptList = string.Join(", ", prescriptionIds.Select(id => $"#{id}"));

            string body = $@"
        <h3>Prescriptions Ready for Collection - Group 09</h3>
        <p>Hi {customer.FirstName} {customer.LastName},</p>
        <p>Your prescription(s) {scriptList} have been dispensed and are ready for collection.</p>
        <p>Thank you for using Ibhayi Pharmacy service!</p>";

            await SendMailAsync(customer.Email, subject, body);
        }

        public async Task SendPrescriptionPartiallyDispensedEmailAsync(ApplicationUser customer, int prescriptionId)
        {
            if (customer == null || string.IsNullOrWhiteSpace(customer.Email)) return;

            //string medsList = string.Join(", ", dispensedMedications);

            string subject = "Your Prescription Update - GROUP 09";
            string body = $@"
        <h3>Prescription Update</h3>
        <p>Hi {customer.FirstName} {customer.LastName},</p>
        <p>Items in your prescription #{prescriptionId} are ready for collection.</p>
        <p><b>Medication(s) ready: Please login the website for more details.</p>
        <p>Thank you for using Ibhayi Pharmacy service!</p>";

            await SendMailAsync(customer.Email, subject, body);
        }

        public async Task SendReadyForCollectionEmailAsync(ApplicationUser customer, int prescriptionId)
        {
            if (customer == null || string.IsNullOrWhiteSpace(customer.Email)) return;

            string subject = "Prescription Order Update - GROUP 09";
            string body = $@"
        <h3>Prescription Order Update</h3>
        <p>Hi {customer.FirstName} {customer.LastName},</p>
        <p>Your order had been processed and ready for collection.</p>
        <p>Thank you for using Ibhayi Pharmacy service!</p>";
            await SendMailAsync(customer.Email, subject, body);
        }
    }
}


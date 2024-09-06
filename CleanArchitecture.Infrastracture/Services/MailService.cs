using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Application.Services;
using System.Net;
using System.Net.Mail;

namespace CleanArcihtecture.Infrastructure.Services;

public sealed class MailService : IMailService
{
    public async Task SendMailAsync(List<string> emails, string subject, string body, List<Attachment> attachments = null)
    {
        SendEmailModel sendEmailModel = new()
        {
            Body = body,
            Attachments = attachments,
            Emails = emails,
            Email = "",
            Html = true,
            Password = "",
            Port = 587,
            Smtp = "",
            SSL = true,
            Subject = subject,
        };
        using MailMessage mail = new MailMessage();
        mail.From = new MailAddress(sendEmailModel.Email);
        foreach (string email in sendEmailModel.Emails)
        {
            mail.To.Add(email);
        }

        mail.Subject = sendEmailModel.Subject;
        mail.Body = sendEmailModel.Body;
        mail.IsBodyHtml = sendEmailModel.Html;
        if (sendEmailModel.Attachments != null)
        {
            foreach (Attachment attachment in sendEmailModel.Attachments)
            {
                mail.Attachments.Add(attachment);
            }
        }

        using SmtpClient smtp = new SmtpClient(sendEmailModel.Smtp);
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = new NetworkCredential(sendEmailModel.Email, sendEmailModel.Password);
        smtp.EnableSsl = sendEmailModel.SSL;
        smtp.Port = sendEmailModel.Port;
        await smtp.SendMailAsync(mail);
    }
}

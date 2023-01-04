using System.Text;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using ToDoListMVC.Application.Interfaces;
using ToDoListMVC.Application.ViewModels.ToDoTask;

namespace ToDoListMVC.Application.Services
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _config;
        private readonly IToDoTaskService _toDoTaskService;

        public MailService(IConfiguration config 
            ,IToDoTaskService toDoTaskService
            )
        {
            _config = config;
            _toDoTaskService = toDoTaskService;
        }

        public async Task SendNotificationMailAsync()
        {
            var mailConfig = new MailConfiguration();
            _config.GetSection("MailConfiguration").Bind(mailConfig);

            var tomorrowDate = DateTime.Now.AddDays(1);

            var toDoTasks = _toDoTaskService.GetToDoTasksForSendMail(tomorrowDate);

            if (toDoTasks.Count == 0)
                return;

            var emailBody = GenerateEmailBody(toDoTasks);

            var email = new MimeMessage
            {
                Sender = new MailboxAddress(mailConfig.DisplayName, mailConfig.From),
                Subject = "Tasks deadlines",
                Body = new BodyBuilder
                {
                    HtmlBody = emailBody
                }.ToMessageBody()
            };

            var toEmail = "test@test.com";

            email.To.Add(new MailboxAddress(mailConfig.DisplayName, toEmail));
            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(mailConfig.Host, mailConfig.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(mailConfig.UserName, mailConfig.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        private string GenerateEmailBody(List<ToDoTaskVm> toDoTasks)
        {
            var sb = new StringBuilder();
            sb.Append("Tasks deadlines are approaching <br/>");
            sb.Append("<table>");
            sb.Append("<tr><th>Name</th><th>Description</th><th>Due date</th></tr>");
            foreach (var toDoTask in toDoTasks)
            {
                var dateOnly = DateOnly.FromDateTime(toDoTask.DueDate!.Value);

                sb.Append($"<tr><td>{toDoTask.Name}</td>" +
                          $"<td>{toDoTask.Description}</td>" +
                          $"<td>{dateOnly}</td></tr>");
            }
            sb.Append("</table>");
            var emailBody = sb.ToString();
            return emailBody;
        }
    }

    public class MailConfiguration
    {
        public string From { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
    }
}

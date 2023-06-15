namespace Divena_CMS.Models.ViewModels
{
    public class EmailModel
    {

        public EmailModel(string to, string subject, string message, bool isBodyHtml)
        {
            To = to;
            Subject = subject;
            Message = message;

        }

        public string To { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }

    }
}

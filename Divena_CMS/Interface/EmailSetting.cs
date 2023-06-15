namespace Divena_CMS.Interface
{
    public class EmailSetting
    {
        public string MailServer { get; set; }
        public int MailPort { get; set; }
        public string SenderName { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public string Receiver { get; set; }
        public string Alias { get; set; }
        public bool EnableSsl { get; set; }

        

    }
}

namespace Project.Api.Helpers
{
    public class EmailConfig
    {
        public string EmailSender { get; set; }
        public int Port { get; set; }
        public string Key { get; set; }
        public string SmtpClient { get; set; }
        public string EmailConfirmReturnPath { get; set; }
        public string NewPasswordReturnPath { get; set; }
    }
}
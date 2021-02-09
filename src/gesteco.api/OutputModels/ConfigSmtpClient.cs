namespace gesteco.api.OutputModels {
    public class ConfigSmtpClient {

        public int Port { get; set; }
        public string AddressFrom { get; set; }
        public string Password { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string SmtpClient { get; set; }
        public string FileName { get; set; }
    }
}

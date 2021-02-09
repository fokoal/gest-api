using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gesteco.api.Configuration {
    public class SendMailOptions {

        public const string MailSend = "MailSend";

        public string Subject { get; set; }
        public string Body { get; set; }
        public string FileName { get; set; }
    }
}

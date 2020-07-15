using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactUsHandler.Models
{
    public class UserMessageModel
    {
        public string Name {get; set;}
        public string Email {get; set;}
        public string Message {get; set;}
        public string CaptchaToken {get; set;}
    }
}

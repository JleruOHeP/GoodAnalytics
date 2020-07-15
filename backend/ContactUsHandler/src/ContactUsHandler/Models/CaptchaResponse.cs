using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactUsHandler.Models
{
    public class CaptchaResponse
    {
        public bool Success {get; set;} // whether this request was a valid reCAPTCHA token for your site
        public float Score {get; set;} // the score for this request (0.0 - 1.0)
        public string Action {get; set;} // the action name for this request (important to verify)
    }
}

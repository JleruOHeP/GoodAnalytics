using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using System.Text;
using System.Net.Http;

using Newtonsoft.Json;

using ContactUsHandler.Models;

namespace ContactUsHandler.Services
{
    public class CaptchaService
    {
        public async Task<bool> ValidateCaptcha(string token)
        {
            var captchaResponse = "";
            using (var httpClient = new HttpClient())
            {
                var secret = "";
                var captchaUrl = $"https://www.google.com/recaptcha/api/siteverify?secret={secret}&response={token}";
                
                var response = await httpClient.GetAsync(captchaUrl);
                var resp = await response.Content.ReadAsStringAsync();
                
                var result = JsonConvert.DeserializeObject<CaptchaResponse>(resp);
                return result.Success && result.Score > 0.5;
            }
        }
    }
}

using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class SMSManager
    {
        public static bool SendSMS(string MobileNumber, string SMSText)
        {

            // Send SMS 
            var URL = "https://apps.idlc.com/IDLCRepo/api/v1/IDLCSMS/Send";
            var client = new RestClient(URL);
            var request = new RestRequest("/", Method.Post);
            request.AddHeader("api_key", "AhSfIeS9gyc3ULMN9l0uzpQsyXDwiTZK");
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new
            {
                MobileNo = MobileNumber,
                SmsText = SMSText,
                SmsAuthKey = "wQlKO7wRCydNGXPodDfcvIsKCa35kNhZIhoka9vzkkIxxNWij9agJWQ9ZbhyFzqO",
            });
            var restResponse = client.ExecuteAsync(request);

            return true;

        }
    }
}

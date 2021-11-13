using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Identity2.Services
{
    public class SmsService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            string AccountSid = "ACe114144dee8614dfad10ea1a1ba68918";
            string AuthToken = "646fcec5c71ec7d5f41f835d23d5bf52";

            TwilioClient.Init(AccountSid, AuthToken);

            var messageToSend = MessageResource.Create(
                from: new PhoneNumber("+14094077274"),
                to: message.Destination,
                body: message.Body);

            return Task.FromResult(0);
        }
    }
}

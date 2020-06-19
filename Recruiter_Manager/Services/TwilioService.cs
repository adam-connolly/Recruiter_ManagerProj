using Recruiter_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Recruiter_Manager.Services
{
    public class TwilioService
    {
        public static void SendTextMessage(Customer customer, Appointment appointment)
        {
            string accountSid = APIKeys.TwilioAccountSid;
            string authToken = APIKeys.TwilioAuthToken;

            TwilioClient.Init(accountSid, authToken);

            var message = MessageResource.Create(
                body: $"{appointment.AppointmentName} on {appointment.AppointmentDate}. Your appointment details: {appointment.AppointmentDetails}",
                from: new Twilio.Types.PhoneNumber(APIKeys.TwilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber(customer.PhoneNumber)
                );
        }
    }
}

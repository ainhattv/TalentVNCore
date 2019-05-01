using Floxdc.ExponentServerSdk;
using Floxdc.ExponentServerSdk.Response;
using System;
using System.Threading.Tasks;

namespace TalentVN.SchoolCMS.Services
{
    public class NotificationService : INotificationService
    {
        public async Task<PushResponse> SendNotification(string expoToken)
        {
            string ExpoToken = expoToken;
            var client = new PushClient();
            var notification = new PushMessage(expoToken, title: "A new message from your friend");

            try
            {
                return await client.Publish(notification);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}

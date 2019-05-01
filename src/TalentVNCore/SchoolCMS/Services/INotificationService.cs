using Floxdc.ExponentServerSdk.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TalentVN.SchoolCMS.Services
{
    public interface INotificationService
    {
        Task<PushResponse> SendNotification(string expoToken);
    }
}

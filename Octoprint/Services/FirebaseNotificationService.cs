using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Iid;
using OctoPrintCore.Services;

namespace Octoprint.Services
{
    public class FirebaseNotificationService : IFirebaseNotificationsService
    {
        private readonly IAccountService _accountService;

        public FirebaseNotificationService(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public async Task<string> Register(string id)
        {
            string token = "";
            await Task.Run(() => {
                var instanceid = FirebaseInstanceId.Instance;
                token = instanceid.GetToken(Application.Context.GetString(Resource.String.gcm_defaultSenderId), Firebase.Messaging.FirebaseMessaging.InstanceIdScope);
                _accountService.registerFCMToken(id, token);
            });
            return token;
        }

        public async Task Unregister(string id)
        {
            await Task.Run(() => {
                var instanceid = FirebaseInstanceId.Instance;
                instanceid.DeleteInstanceId();
                _accountService.unregisterFCMToken(id);
            });
        }
    }
}
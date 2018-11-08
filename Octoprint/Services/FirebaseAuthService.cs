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
using OctoPrintCore.Services;
using Firebase.Auth;

namespace Octoprint.Services
{
    public class FirebaseAuthService : IFirebaseAuth
    {

        public async Task<bool> Connect(string login, string password)
        {
            var user = await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(login, password);
            if (user != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Disconnect()
        {
            Firebase.Auth.FirebaseAuth.Instance.SignOut();
            return true;
        }

        public async Task<string> GetToken()
        {
            var token = await Firebase.Auth.FirebaseAuth.Instance.CurrentUser.GetIdTokenAsync(false);
            return token.Token;
        }

        public async Task<bool> IsConnected()
        {
            if (Firebase.Auth.FirebaseAuth.Instance.CurrentUser != null)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Subscribe(string login, string password)
        {
            var user = await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(login, password);
            if (user != null)
            {
                return true;
            }
            return false;
        }
    }
}
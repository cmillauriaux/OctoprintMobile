using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace OctoPrintCore
{
    public class UserProperties
    {
        public static string GetRemoteIP()
        {
            return Preferences.Get("remote_ip", "");
        }

        public static void SetRemoteIP(string ip)
        {
            Preferences.Set("remote_ip", ip);
        }

        public static string GetLocalIP()
        {
            return Preferences.Get("local_ip", "");
        }

        public static void SetLocalIP(string ip)
        {
            Preferences.Set("local_ip", ip);
        }

        public static string GetUser()
        {
            return Preferences.Get("user", "");
        }

        public static void SetUser(string user)
        {
            Preferences.Set("user", user);
        }

        public static string GetPassword()
        {
            return Preferences.Get("password", "");
        }

        public static void SetPassword(string password)
        {
            Preferences.Set("password", password);
        }

        public static string GetApiKey()
        {
            return Preferences.Get("apikey", "");
        }

        public static void SetApiKey(string apikey)
        {
            Preferences.Set("apikey", apikey);
        }

        public static void Clear()
        {
            Preferences.Clear();
        }
    }
}

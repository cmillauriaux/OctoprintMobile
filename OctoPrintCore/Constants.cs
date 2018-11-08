using System;
using System.Collections.Generic;
using System.Text;

namespace OctoPrintCore
{
    public class Constants
    {
        public static string BASE_URL = "http://";
        public static string BASE_SERVER_URL = "http://192.168.1.186:5000/octomobile-2aecc/us-central1";

        public static string API_VERSION = "/api/version";
        public static string API_CONNECTION = "/api/connection";
        public static string API_CONNECT = "/api/connection";
        public static string API_DISCONNECT = "/api/connection";
        public static string API_LOGIN = "/api/login";
        public static string API_JOB = "/api/job";

        public static string SERVER_API_REGISTER_FCM_TOKEN = "/registerFCMToken";
        public static string SERVER_API_UNREGISTER_FCM_TOKEN = "/unregisterFCMToken";
        public static string SERVER_API_GET_PROFILE = "/getProfile";
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class UpdateUserInfo
    {
        public string username { get; set; }
        public string user_password { get; set; }
        public string user_email { get; set; }

        public byte[] user_profilepic { get; set; }

    }
}
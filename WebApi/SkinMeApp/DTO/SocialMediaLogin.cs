using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class SocialMediaLogin
    {
        public int appUser_id { get; set; }
        public string user_firstName { get; set; }
        public string email { get; set; }

        public byte[] picture { get; set; }

        

    }
}
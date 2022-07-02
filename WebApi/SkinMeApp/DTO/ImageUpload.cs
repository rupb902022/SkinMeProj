using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace SkinMeApp.DTO
{
    public class ImageUpload
    {
        public byte[] picture { get; set; } // upload profile pic

        public byte[] user_picsprocess { get; set; } // upload skin pic for maslul 

        internal Image FromFile(byte[] picture)
        {
            throw new NotImplementedException();
        }
    }
}
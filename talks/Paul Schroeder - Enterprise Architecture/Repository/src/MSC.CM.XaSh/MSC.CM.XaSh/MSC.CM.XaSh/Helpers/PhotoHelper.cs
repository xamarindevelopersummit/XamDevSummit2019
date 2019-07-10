using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MSC.CM.XaSh.Helpers
{
    public static class PhotoHelper
    {
        public static async Task<bool> CheckPickPhoto()
        {
            if (await CrossMedia.Current.Initialize())
            {
                if (CrossMedia.Current.IsPickPhotoSupported)
                {
                    return true;
                }
            }

            return false;
        }

        public static async Task<bool> CheckTakePhoto()
        {
            if (await CrossMedia.Current.Initialize())
            {
                if (CrossMedia.Current.IsTakePhotoSupported)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
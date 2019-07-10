using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSC.CM.Xam.ModelObj.CM
{
    public partial class UserProfile : ObservableObject
    {
        public string DisplayName { get { return $"{FirstName} {LastName}"; } }

        public bool DoesUserTweet
        {
            get { return string.IsNullOrEmpty(TwitterUrl) ? false : true; }
        }
    }
}
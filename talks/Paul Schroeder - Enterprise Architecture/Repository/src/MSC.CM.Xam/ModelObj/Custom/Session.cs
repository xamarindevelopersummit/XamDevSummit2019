using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSC.CM.Xam.ModelObj.CM
{
    public partial class Session : ObservableObject
    {
        public string DateDisplay
        {
            get { return StartTime != null ? ((DateTime)StartTime).ToLocalTime().ToString("dd MMM yyyy") : string.Empty; }
        }

        public bool HasLikes
        {
            get { return SessionLikes.Count > 0; }
        }

        public string SpeakerList
        {
            get
            {
                string returnMe = string.Empty;
                if (SessionSpeakers != null)
                {
                    foreach (var s in SessionSpeakers)
                    {
                        if (s.UserProfile != null)
                        {
                            returnMe += $"{s.UserProfile.DisplayName} ";
                        }
                    }
                }
                return returnMe;
            }
        }

        public string StartEndTimeDisplay
        {
            get { return (StartTime != null && EndTime != null) ? $"{((DateTime)StartTime).ToLocalTime().ToString("h:mm tt")} - {((DateTime)EndTime).ToLocalTime().ToString("h:mm tt")}" : string.Empty; }
        }
    }
}
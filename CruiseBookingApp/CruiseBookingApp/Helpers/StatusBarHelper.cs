using System;
using Xamarin.Forms;

namespace CruiseBookingApp.Helpers
{
    public class StatusBarHelper
    {
        public const string TranslucentStatusChangeMessage = "TranslucentStatusChange";

        static readonly StatusBarHelper _instance = new StatusBarHelper();

        public static StatusBarHelper Instance => _instance;

        protected StatusBarHelper() { }

        public void MakeTranslucentStatusBar(bool translucent)
        {
            MessagingCenter.Send(this, TranslucentStatusChangeMessage, translucent);
        }
    }
}

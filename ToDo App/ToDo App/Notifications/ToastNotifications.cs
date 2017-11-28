using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

namespace Notifications
{
    /// <summary>
    /// Provides functionality for creating ToastNotifications
    /// </summary>
    public class ToastNotifications
    {
        /// <summary>
        /// Creates a ToastNotification with a given message
        /// </summary>
        /// <param name="msg">Message to show in the toast notification</param>
        /// <returns>ToastNotification with given message</returns>
        public static ToastNotification CreateNotification(string msg)
        {
            string title = "Reminder!";
            //string logo = "ms-appdata:///local/Andrew.jpg";
            string logo = "ms-appdata:///local/Assets/bell-icon.png";

            // Construct the visuals of the toast
            string toastVisual =
            $@"<visual>
              <binding template='ToastGeneric'>
                <text>{title}</text>
                <text>{msg}</text>
                <image src='{logo}' placement='appLogoOverride' hint-crop='circle'/>
              </binding>
            </visual>";

            string toastXmlString =
            $@"<toast>
                {toastVisual}
            </toast>";

            // Parse to XML
            XmlDocument toastXml = new XmlDocument();
            toastXml.LoadXml(toastXmlString);

            // Generate toast
            var toast = new ToastNotification(toastXml);
            return toast;
        }
    }
}

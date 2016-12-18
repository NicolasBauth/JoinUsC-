using JoinUs.Factory;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace JoinUs.AppToastCenter
{
    public static class ToastCenter
    {
        public static void Notify(ToastContent content)
        {
            XmlDocument xmlContent = content.GetXml();
            ToastNotification notification = new ToastNotification(xmlContent);
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }

        public static void InformativeNotify(string title, string textToDisplay)
        {
            ToastContent content = ToastFactory.CreateStandardAppToastContent(title, textToDisplay);
            Notify(content);
        }
    }
}

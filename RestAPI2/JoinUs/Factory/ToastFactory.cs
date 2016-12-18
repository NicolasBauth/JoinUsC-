using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JoinUs.Factory
{
    public static class ToastFactory
    {
        public static ToastContent CreateStandardAppToastContent(string toastTitle,string textToDisplay)
        {
            ToastContent content = new ToastContent()
            {
                Launch = "triggered-Toast",
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = toastTitle
                            },

                            new AdaptiveText()
                            {
                                Text = textToDisplay
                            }
                        },
                        AppLogoOverride = new ToastGenericAppLogo()
                        {
                            Source = "Assets/Joynus.png"
                        }
                    }

                },
                Audio = new ToastAudio()
                {
                    Src = new Uri("ms-winsoundevent:Notification.Reminder")
                }
            };
            return content;
        }

        
    }
}

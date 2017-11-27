using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Windows.UI.Core;
using Windows.UI.Notifications;

namespace Notifications
{
    public class NotificationSender
    {
        IEnumerable<TodoViewModel> Todos { get; }
        ToastNotifier Notifier;

        public NotificationSender(IEnumerable<TodoViewModel> Todos)
        {
            this.Todos = Todos;
            Notifier = ToastNotificationManager.CreateToastNotifier();
        }

        async void CheckAndDeliverNotificationsAsync()
        {
            //Debug.WriteLine("Checked for Notifications");
            foreach (TodoViewModel t in Todos)
            {
                if (!t.Recycled && t.HasReminder && t.Reminder <= DateTime.Now)
                {
                    var Toast = ToastNotifications.CreateNotification(t.Note);
                    Notifier.Show(Toast);
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
                    () => { t.HasReminder = false; });
                    //TODO: update t.HasReminder in the DataService as well
                    //might be automatically updated?
                }
            }
        }

        public void Start()
        {
            var startTimeSpan = TimeSpan.Zero;
            var periodTimeSpan = TimeSpan.FromSeconds(5);
            
            var timer = new System.Threading.Timer((e) =>
            {
                CheckAndDeliverNotificationsAsync();
            }, null, startTimeSpan, periodTimeSpan);

            //var t = new Windows.UI.Xaml.DispatcherTimer();
            //t.Interval = periodTimeSpan;
            //t.Tick += T_Tick;
        }
    }
}

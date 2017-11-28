using Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;
using Windows.System.Threading;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.Xaml;

namespace Notifications
{
    /// <summary>
    /// Class implementing notification sending functionality
    /// </summary>
    public class NotificationSender
    {
        /// <summary>
        /// List of Todos to watch for notifications
        /// </summary>
        IEnumerable<TodoViewModel> Todos { get; }
        ToastNotifier Notifier;

        /// <summary>
        /// Constructor for NotificationSender that watches a list of 
        /// Todos
        /// </summary>
        /// <param name="Todos">Todos list to watch</param>
        public NotificationSender(IEnumerable<TodoViewModel> Todos)
        {
            this.Todos = Todos;
            Notifier = ToastNotificationManager.CreateToastNotifier();
        }

        async

        /// <summary>
        /// Iterate over list of Todos, delivering those that are overdue and
        /// not recycled.
        /// </summary>
        void CheckAndDeliverNotificationsAsync()
        {
            Debug.WriteLine("Checked for Notifications");
            foreach (TodoViewModel t in Todos)
            {
                if (!t.Recycled && t.HasReminder && t.Reminder <= DateTime.Now)
                {
                    var Toast = ToastNotifications.CreateNotification(t.Note);
                    Notifier.Show(Toast);
                    //runs the UI updating code on the Dispatcher Thread because it will otherwise throw errors
                    await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(
                        CoreDispatcherPriority.High, 
                        () => { t.HasReminder = false; });
                }
            }
        }

        /// <summary>
        /// Begins sending notifications
        /// </summary>
        public void Start()
        {
            TimeSpan period = TimeSpan.FromSeconds(30);

            ThreadPoolTimer PeriodicTimer = ThreadPoolTimer.CreatePeriodicTimer((source) =>
            {
                CheckAndDeliverNotificationsAsync();
            }, period);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using Notifications;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ToDo_App
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ToDoListViewModel Todos { get; set; }

        public MainPage()
        {
            Debug.WriteLine("constructed");
            this.InitializeComponent();
            Todos = new ToDoListViewModel();
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RecycledView));
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (!string.IsNullOrWhiteSpace(sender.Text))
            {
                Todos.Filter = (x => (bool) x.Note?.ToLower().Contains(sender.Text?.ToLower()));
            }
            else
            {
                Todos.Filter = (x => true);
            }
        }

        private void toggleSwitch1_Toggled(object sender, RoutedEventArgs e)
        {
            var result = (sender as ToggleSwitch).IsOn;
            Todos.SelectedTodo.HasReminder = result;
            if (result && Todos.SelectedTodo.Reminder <= DateTime.Now)
            {
                //if notification is in the past, update it
                Todos.SelectedTodo.Reminder = DateTime.Now.AddMinutes(5);
            }
        }
    }
}

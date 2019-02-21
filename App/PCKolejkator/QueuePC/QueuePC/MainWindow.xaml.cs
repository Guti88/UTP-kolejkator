using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QueuePC
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
        string currentID;
        string currentQueueStatus;
        List<String> IDs;
        List<Queue> queueStatuses;
        Student currentStudent;

        public MainWindow()
        {
            InitializeComponent();
            ResetView();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 500);
            dispatcherTimer.Start();
            Thread updateThread = new Thread(UpdateThread);
            updateThread.Start();
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
            ServiceCommands.EditQueueStatus("1", "zamknieta");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
        }

        /// <summary>
        /// Updates buttons depending on queue statuses to prevent lockdown and sending twice the same request
        /// </summary>
        private void UpdateButtons()
        {
            //TODO check if the list is empty and gray out delete button
            switch (currentQueueStatus)
            {
                case "otwarta":
                    {
                        btn_pause.IsEnabled = true;
                        img_Pause.Opacity = 255;
                        img_Play.Opacity = 0;
                        btn_start_queue.IsEnabled = false;
                        btn_stop_queue.IsEnabled = true;
                        btn_delete.IsEnabled = true;

                    }
                    break;
                case "zamknieta":
                    {
                        btn_pause.IsEnabled = false;
                        img_Pause.Opacity = 0;
                        img_Play.Opacity = 0;
                        btn_start_queue.IsEnabled = true;
                        btn_stop_queue.IsEnabled = false;
                        btn_delete.IsEnabled = false;
                    }
                    break;
                case "wstrzymana":
                    {
                        btn_pause.IsEnabled = true;
                        img_Pause.Opacity = 0;
                        img_Play.Opacity = 255;
                        btn_start_queue.IsEnabled = false;
                        btn_stop_queue.IsEnabled = true;
                        btn_delete.IsEnabled = false;
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Method used to set queue status to local variable currentQueueStatus for convenience of using it in this class
        /// </summary>
        private void CheckQueueStatus()
        {
            ServiceCommands.GetQueueStatus();
            queueStatuses = ServiceCommands.queueStatuses;
            if(queueStatuses[0].idQueue == "1")
            {
                if(queueStatuses[0].status == "otwarta")
                {
                    currentQueueStatus = "otwarta";
                }
                else if (queueStatuses[0].status == "zamknieta")
                {
                    currentQueueStatus = "zamknieta";
                }
                else
                {
                    currentQueueStatus = "wstrzymana";
                }
            }
        }

        /// <summary>
        /// Method used to updated info labels based data received about upcoming student from the queue
        /// </summary>
        private void UpdateView()
        {
            //Console.WriteLine(currentStudent.name);
            if(currentStudent != null)
            {
                lb_Name.Content = currentStudent.name;
                lb_Surname.Content = currentStudent.surname;
                lb_Field.Content = currentStudent.field;
                if (currentStudent.isDaily)
                {
                    lb_isDaily.Content = "Dzienne";
                }
                else 
                {
                    lb_isDaily.Content = "Zaoczne";
                }
                if (currentStudent.isMaster)
                {
                    lb_isMaster.Content = "Magisterskie";
                }
                else
                {
                    lb_isMaster.Content = "Inżynierskie";
                }
            }
            listBox.ItemsSource = IDs;
        }

        /// <summary>
        /// Method used to update queue status and getting current queue and filling local list with ID's.
        /// It's get called from another thread to avoid hanging of application
        /// </summary>
        private void UpdateThread()
        {
            while(true)
            {
                CheckQueueStatus();
                ServiceCommands.GetQueue("1");
                IDs = ServiceCommands.IDs;
                if(IDs.Any())
                {
                    currentID = IDs.ElementAt(0);
                }
                currentStudent = ServiceCommands.GetStudentDetails<Student>(currentID);
            }
        }

        /// <summary>
        /// This method is called when pause button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_pause_Click(object sender, RoutedEventArgs e)
        {
            if(currentQueueStatus == "wstrzymana")
            {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                ServiceCommands.EditQueueStatus("1", "otwarta");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
            }
            else
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
                ServiceCommands.EditQueueStatus("1", "wstrzymana");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.

        }

        /// <summary>
        /// This method is called when start button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_start_queue_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
            ServiceCommands.EditQueueStatus("1", "otwarta");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
        }

        /// <summary>
        /// This method is called when stop button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_stop_queue_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
            ServiceCommands.EditQueueStatus("1", "zamknieta");
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
        }

        /// <summary>
        /// This method is called when delete button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
            ServiceCommands.DeleteFromQueueByIdAsync(currentID);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed. Consider applying the 'await' operator to the result of the call.
            UpdateView();
        }

        /// <summary>
        /// This method is used for updating buttons and view. It get called every 1 second.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            UpdateView();
            UpdateButtons();
        }

        /// <summary>
        /// Method used to reset view to its initial state
        /// </summary>
        private void ResetView()
        {
            btn_pause.IsEnabled = false;
            img_Pause.Opacity = 0;
            img_Play.Opacity = 0;
            btn_start_queue.IsEnabled = true;
            btn_stop_queue.IsEnabled = false;
            btn_delete.IsEnabled = false;
            lb_Name.Content = "";
            lb_Surname.Content = "";
            lb_Field.Content = "";
            lb_isDaily.Content = "";
            lb_isMaster.Content = "";
        }

    }
}

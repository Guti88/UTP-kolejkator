using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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


		public MainWindow()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            dispatcherTimer.Start();
            UpdateView();
        }

        private void UpdateView()
        {
            ServiceCommands.GetQueue("1");
            IDs = ServiceCommands.IDs;
            currentID = IDs.ElementAt(0);
            Console.WriteLine(currentID);
            //Console.WriteLine(currentID);
            var currentStudent = ServiceCommands.GetStudentDetails<Student>(currentID);
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
            listBox.ItemsSource = IDs;
        }

        private void Btn_pause_Click(object sender, RoutedEventArgs e)
        {
            ServiceCommands.EditQueueStatus("1");
        }

        private void Btn_start_queue_Click(object sender, RoutedEventArgs e)
        {
            ServiceCommands.EditQueueStatus("1");
        }

        private void Btn_stop_queue_Click(object sender, RoutedEventArgs e)
        {
            ServiceCommands.EditQueueStatus("1");
        }

        private void Btn_delete_Click(object sender, RoutedEventArgs e)
        {
            ServiceCommands.DeleteFromQueueByIdAsync(currentID);
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {


        }
        
    }
}

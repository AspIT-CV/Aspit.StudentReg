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
using Aspit.StudentReg.DataAccess;
using Aspit.StudentReg.Entities;

namespace Aspit.StudentReg.GUI.Terminal
{
    /// <summary>
    /// Interaction logic for CheckInOrOutPromt.xaml
    /// </summary>
    public partial class CheckInOrOutPromt : UserControl
    {
        private MainWindow parent;
        StudentsRepository studentsRepository;
        Student student;
        public CheckInOrOutPromt(Student studentarg, MainWindow parentArg)
        {
            parent = parentArg;
            student = studentarg;
            InitializeComponent();
            studentsRepository = new StudentsRepository(RepositoryBase.RetrieveConnectionString());
            

            TopLine.Text = "Hej " + student.Name + "!";
            

            if (studentsRepository.IsCheckedIn(student))
            {
                //Student is currently checked in
                if(student.AttendanceRegistrations.MeetingTime.Date == DateTime.Now.Date)
                {
                    //Student has checked in today
                    OnlyShowCheckOut();
                    UnderTopLine.Text = "Vi ses!";

                } else
                {
                    //Student has not checked in today
                    OnlyShowCheckIn();
                    UnderTopLine.Text = "Du glemte at checke ud sidste gang!";
                }
            } else
            {
                //Student is currently checked out
                OnlyShowCheckIn();
                UnderTopLine.Text = "Velkommen tilbage!";
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            parent.Content = new StartScreenUserControl(parent);
        }

        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            studentsRepository.CheckIn(student, DateTime.Now);
            parent.Content = new CheckedInUserControl(student, parent, true);
        }

        private void CheckIn_Click(object sender, RoutedEventArgs e)
        {
            studentsRepository.CheckOut(student, DateTime.Now);
            parent.Content = new CheckedInUserControl(student, parent, false);
        }

        private void OnlyShowCheckOut()
        {
            CheckOut.Visibility = Visibility.Hidden;
            CheckIn.SetValue(Grid.ColumnSpanProperty, 2);
            CheckIn.SetValue(Grid.ColumnProperty, 0);
        }
        private void OnlyShowCheckIn()
        {
            CheckIn.Visibility = Visibility.Hidden;
            CheckOut.SetValue(Grid.ColumnSpanProperty, 2);
            CheckOut.SetValue(Grid.ColumnProperty, 0);
        }
    }
}

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
            
            //Set TopLine text
            TopLine.Text = "Hej " + student.Name + "!";
            
            //Check if student is currently checked in
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
        /// <summary>
        /// Handle click of the Cancel button
        /// Send user back to <see cref="StartScreenUserControl"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            parent.Content = new StartScreenUserControl(parent);
        }

        /// <summary>
        /// Handels click on check out button
        /// Checks out user and redirects to <see cref="CheckedInUserControl"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckOut_Click(object sender, RoutedEventArgs e)
        {
            //Check out user
            studentsRepository.CheckIn(student, DateTime.Now);

            //Redirect
            parent.Content = new CheckedInUserControl(student, parent, true);
        }

        /// <summary>
        /// Handels click on check in button
        /// Checks in user and redirects to <see cref="CheckedInUserControl"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckIn_Click(object sender, RoutedEventArgs e)
        {
            //Check in user
            studentsRepository.CheckOut(student, DateTime.Now);

            //Redirect
            parent.Content = new CheckedInUserControl(student, parent, false);
        }

        /// <summary>
        /// Make GUI only show check out button
        /// </summary>
        private void OnlyShowCheckOut()
        {
            CheckOut.Visibility = Visibility.Hidden;
            CheckIn.SetValue(Grid.ColumnSpanProperty, 2);
            CheckIn.SetValue(Grid.ColumnProperty, 0);
        }
        /// <summary>
        /// Make GUI only show check in button
        /// </summary>
        private void OnlyShowCheckIn()
        {
            CheckIn.Visibility = Visibility.Hidden;
            CheckOut.SetValue(Grid.ColumnSpanProperty, 2);
            CheckOut.SetValue(Grid.ColumnProperty, 0);
        }
    }
}

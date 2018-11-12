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

namespace Aspit.StudentReg.Gui.Desktop
{
    /// <summary>
    /// Interaction logic for StudentRegistrationViewer.xaml
    /// </summary>
    public partial class StudentRegistrationsViewer: UserControl
    {
        /// <summary>
        /// Repository for <see cref="Student"/>s
        /// </summary>
        private StudentsRepository studentsRepository;

        /// <summary>
        /// Repository for <see cref="AttendanceRegistration"/>s
        /// </summary>
        AttendanceRegistrationsRepository registrationsRepository;

        /// <summary>
        /// A list of all <see cref="AttendanceRegistration"/>s
        /// </summary>
        private List<AttendanceRegistration> registrations;

        /// <summary>
        /// The student this <see cref="StudentRegistrationsViewer"/> is showing data for
        /// </summary>
        private Student showingStudent;

        /// <summary>
        /// Intializes a new <see cref="StudentRegistrationsViewer"/>
        /// </summary>
        public StudentRegistrationsViewer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoke when the page should change to the StudentViewer
        /// </summary>
        public RoutedEventHandler GoBack{get; set; }

        /// <summary>
        /// Intializes this StudentRegistrationsViewer with the given parameters
        /// </summary>
        /// <param name="repository">The repository used to get data</param>
        /// <param name="student">The student to get <see cref="AttendanceRegistration"/>s from</param>
        public StudentRegistrationsViewer Intialize(AttendanceRegistrationsRepository repository, StudentsRepository studentsRepository, Student student)
        {
            this.studentsRepository = studentsRepository;
            registrationsRepository = repository;
            showingStudent = student;
            UpdateRegistrationDataGrid();
            StudentsNameLabel.Content = "(" + student.UniLogin + ") " + student.Name + "s tidsregistreringer:";

            return this;
        }

        private static DockPanel RegistrationView(AttendanceRegistration registration)
        {
            DockPanel registrationDockPanel = new DockPanel() {LastChildFill = false };

            if(registration.Date != DateTime.Now.Date && (registration.LeavingTime.TimeOfDay == default || registration.MeetingTime.TimeOfDay == default) )
            {
                registrationDockPanel.Background = Brushes.PaleVioletRed;
            }

            Label meetingTime = new Label() { Margin = new Thickness(30, 0, 5, 0), Content = /*registration.MeetingTime.TimeOfDay == default ? "--:--:--" :*/ registration.MeetingTime.TimeOfDay.ToString() };
            DockPanel.SetDock(meetingTime, Dock.Left);
            registrationDockPanel.Children.Add(meetingTime);
            Label leavingTime = new Label() { Margin = new Thickness(5, 0, 30, 0), Content = /*registration.LeavingTime.TimeOfDay == default ? "--:--:--" :*/ registration.LeavingTime.TimeOfDay.ToString() };
            DockPanel.SetDock(leavingTime, Dock.Right);
            registrationDockPanel.Children.Add(leavingTime);

            return registrationDockPanel;
        }

        /// <summary>
        /// Updates the <see cref="AttendanceRegistration"/> data grid
        /// </summary>
        private void UpdateRegistrationDataGrid()
        {
            registrations = registrationsRepository.GetUsersRegistrations(showingStudent);
            registrations.Sort(AttendanceRegistration.Compare);

            RegistrationsListBox.Items.Clear();
            foreach(AttendanceRegistration registration in registrations)
            {
                RegistrationsListBox.Items.Add(RegistrationView(registration));
            }
        }

        /// <summary>
        /// Invoked when the back button is clicked
        /// </summary>
        private void BackButton_Clicked(object sender, RoutedEventArgs e)
        {
            GoBack?.Invoke(sender, e);
        }

        /// <summary>
        /// Invoked when the RegistrationsListBox selection changed
        /// </summary>
        private void RegistrationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(RegistrationsListBox.SelectedIndex != -1)
            {
                RegistrationViewerControl.IsEnabled = true;
                RegistrationViewerControl.AttendanceRegistration = registrations[RegistrationsListBox.SelectedIndex];
            }
            else
            {
                RegistrationViewerControl.IsEnabled = false;
            }
        }

        /// <summary>
        /// Invoked when the save button is clicked
        /// </summary>
        private void SaveButton_Clicked(object sender, RoutedEventArgs e)
        {
            if(!showingStudent.AttendanceRegistrations.IsDefault() && showingStudent.AttendanceRegistrations.Id == RegistrationViewerControl.AttendanceRegistration.Id)
            {
                if(RegistrationViewerControl.AttendanceRegistration.LeavingTime.TimeOfDay != default)
                {
                    showingStudent.AttendanceRegistrations = default;
                    studentsRepository.UpdateStudent(showingStudent);
                }
            }

            registrationsRepository.Update(RegistrationViewerControl.AttendanceRegistration);
            UpdateRegistrationDataGrid();
            RegistrationsListBox.SelectedIndex = -1;
        }
    }
}

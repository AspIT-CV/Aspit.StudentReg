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

        /// <summary>
        /// Updates the <see cref="AttendanceRegistration"/> data grid
        /// </summary>
        public void UpdateRegistrationDataGrid()
        {
            registrations = registrationsRepository.GetUsersRegistrations(showingStudent);
            RegistrationDataGrid.ItemsSource = (from registration in registrations
                                                let Id = registration.Id
                                                let Dato = $"{registration.Date.Day}/{registration.Date.Month}/{registration.Date.Year}"
                                                let Tjekind = DateTime.Equals(registration.MeetingTime, default) ? "-" : registration.MeetingTime.TimeOfDay.ToString()
                                                let Tjekud = DateTime.Equals(registration.MeetingTime, default) ? "-" : registration.LeavingTime.TimeOfDay.ToString()
                                                select new
                                                {
                                                    Id,
                                                    Dato,
                                                    Tjekind,
                                                    Tjekud
                                                });

            if(RegistrationDataGrid.Columns.Count != 0)
            {
                RegistrationDataGrid.Columns[0].Visibility = Visibility.Collapsed;
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
        /// Invoked when the RegistrationsDataGrid selection changed
        /// </summary>
        private void RegistrationDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(RegistrationDataGrid.SelectedIndex != -1)
            {
                RegistrationViewerControl.IsEnabled = true;
                RegistrationViewerControl.AttendanceRegistration = registrations[RegistrationDataGrid.SelectedIndex];
            }
            else
            {
                RegistrationViewerControl.IsEnabled = false;
            }
        }

        private void SaveButton_Clicked(object sender, RoutedEventArgs e)
        {
            if(!showingStudent.AttendanceRegistrations.IsDefault() && showingStudent.AttendanceRegistrations.Id == RegistrationViewerControl.AttendanceRegistration.Id)
            {
                if(RegistrationViewerControl.AttendanceRegistration.LeavingTime != default)
                {
                    showingStudent.AttendanceRegistrations = default;
                    studentsRepository.UpdateStudent(showingStudent);
                }
            }

            registrationsRepository.Update(RegistrationViewerControl.AttendanceRegistration);
            UpdateRegistrationDataGrid();
            RegistrationDataGrid.SelectedIndex = -1;
        }
    }
}

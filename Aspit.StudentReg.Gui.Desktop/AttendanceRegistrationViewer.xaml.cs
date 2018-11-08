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
using Aspit.StudentReg.Entities;

namespace Aspit.StudentReg.Gui.Desktop
{
    /// <summary>
    /// Interaction logic for AttendanceRegistrationViewer.xaml
    /// </summary>
    public partial class AttendanceRegistrationViewer: UserControl
    {
        private AttendanceRegistration attendanceRegistration;

        /// <summary>
        /// Intializes a new <see cref="AttendanceRegistrationViewer"/>
        /// </summary>
        public AttendanceRegistrationViewer()
        {
            InitializeComponent();
            CheckInOutDate.DisplayDateEnd = DateTime.Now;
            ValidateInformation();
        }

        /// <summary>
        /// The AttendanceRegistration to change
        /// </summary>
        public AttendanceRegistration AttendanceRegistration
        {
            get
            {
                return attendanceRegistration;
            }
            set
            {
                attendanceRegistration = value;
                if(attendanceRegistration.IsDefault())
                {
                    MeetingTimePicker.Time = default;
                    LeavingTimePicker.Time = default;
                    CheckInOutDate.SelectedDate = null;
                    IsNew = true;
                }
                else
                {
                    MeetingTimePicker.Time = attendanceRegistration.MeetingTime.TimeOfDay;
                    LeavingTimePicker.Time = attendanceRegistration.LeavingTime.TimeOfDay;
                    CheckInOutDate.SelectedDate = attendanceRegistration.Date;
                    IsNew = false;
                }
                ValidateInformation();
            }
        }

        /// <summary>
        /// Tells if the <see cref="Entities.AttendanceRegistration"/> is new
        /// </summary>
        public bool IsNew
        {
            get;
            private set;
        }

        /// <summary>
        /// Enables or disables this user control
        /// </summary>
        public new bool IsEnabled
        {
            get
            {
                return base.IsEnabled;
            }
            set
            {
                base.IsEnabled = value;
                AttendanceRegistration = default;
                ErrorLabel.Content = "";
            }
        }

        /// <summary>
        /// Makes new button visible or invisible
        /// </summary>
        public bool NewButtonVisible
        {
            get
            {
                return !(NewButton.Visibility == Visibility.Collapsed);
            }
            set
            {
                NewButton.Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        /// <summary>
        /// Invoked when the save button is clicked
        /// </summary>
        public RoutedEventHandler SaveButtonClicked{ get; set; }

        /// <summary>
        /// Invoked when the day datepicker's date selection has changed
        /// </summary>
        private void RegistrationDate_Changed(object sender, SelectionChangedEventArgs e)
        {
            ValidateInformation();
        }

        /// <summary>
        /// Invoked when any one of the timepicker's time has changed
        /// </summary>
        private void RegistrationInformation_Changed(object sender, TextChangedEventArgs e)
        {
            ValidateInformation();
        }

        /// <summary>
        /// Validates the information in this AttendanceRegistrationViewer
        /// </summary>
        private void ValidateInformation()
        {
            SaveButton.IsEnabled = false;
            InformationLabel.Content = "";

            if(CheckInOutDate.SelectedDate is null)
            {
                ErrorLabel.Content = "Der er ikke valgt nogen dag.";
                return;
            }
            if(!MeetingTimePicker.IsValidTime)
            {
                ErrorLabel.Content = "Tjekind tidspunktet er tomt.";
                return;
            }
            if (!LeavingTimePicker.IsValidTime)
            {
                ErrorLabel.Content = "Tjekud tidspunktet er tomt.";
                return;
            }
            if(MeetingTimePicker.Time >= LeavingTimePicker.Time)
            {
                ErrorLabel.Content = "Tjekind tidspunktet kan ikke være efter tjekud";
                return;
            }

            InformationLabel.Content = "Tid i alt: " + (LeavingTimePicker.Time - MeetingTimePicker.Time);
            ErrorLabel.Content = "";
            SaveButton.IsEnabled = true;
        }

        /// <summary>
        /// Invoked when the save button is clicked
        /// </summary>
        private void SaveButton_Clicked(object sender, RoutedEventArgs e)
        {
            attendanceRegistration = new AttendanceRegistration
            {
                Id = AttendanceRegistration.Id,
                MeetingTime = CheckInOutDate.SelectedDate.Value + MeetingTimePicker.Time,
                LeavingTime = CheckInOutDate.SelectedDate.Value + LeavingTimePicker.Time
            };
            SaveButtonClicked?.Invoke(sender, e);
        }

        /// <summary>
        /// Invoked when the New button is clicked
        /// </summary>
        private void NewButton_Clicked(object sender, RoutedEventArgs e)
        {
            AttendanceRegistration = default;
        }
    }
}

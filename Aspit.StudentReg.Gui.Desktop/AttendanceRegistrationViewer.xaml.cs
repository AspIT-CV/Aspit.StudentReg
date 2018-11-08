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

namespace Aspit.StudentReg.Gui.Desktop
{
    /// <summary>
    /// Interaction logic for AttendanceRegistrationViewer.xaml
    /// </summary>
    public partial class AttendanceRegistrationViewer: UserControl
    {
        /// <summary>
        /// Intializes a new <see cref="AttendanceRegistrationViewer"/>
        /// </summary>
        public AttendanceRegistrationViewer()
        {
            InitializeComponent();
            CheckInOutDate.DisplayDateEnd = DateTime.Now;
        }

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

            if(CheckInOutDate.SelectedDate is null)
            {
                ErrorLabel.Content = "Dagen er ikke valgt";
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
    }
}

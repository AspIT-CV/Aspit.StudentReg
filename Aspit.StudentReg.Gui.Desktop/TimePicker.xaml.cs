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
    /// Interaction logic for TimePicker.xaml
    /// </summary>
    public partial class TimePicker: UserControl
    {
        /// <summary>
        /// Stops events from being invoked if true
        /// </summary>
        private bool eventLock;

        /// <summary>
        /// Intializes a new <see cref="TimePicker"/>
        /// </summary>
        public TimePicker()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Gets invoked everytime a textbox in this TimePicker changes
        /// </summary>
        public TextChangedEventHandler TimeChanged { get; set; }

        /// <summary>
        /// This TimePicker contains a valid time if true
        /// </summary>
        public bool IsValidTime
        {
            get
            {
                return (!string.IsNullOrEmpty(HoursTextBox.Text)
                    && !string.IsNullOrEmpty(MinutesTextBox.Text)
                    && !string.IsNullOrEmpty(SecondsTextBox.Text));
            }
        }

        /// <summary>
        /// Returns the time chosen in this TimePicker
        /// </summary>
        public TimeSpan Time
        {
            get
            {
                if(IsValidTime)
                {
                    return new TimeSpan(
                        Convert.ToInt32(HoursTextBox.Text), 
                        Convert.ToInt32(MinutesTextBox.Text), 
                        Convert.ToInt32(SecondsTextBox.Text));
                }
                else
                {
                    throw new InvalidOperationException("TimePicker doesnt contain a valid time");
                }
            }
            set
            {
                if(value == default)
                {
                    HoursTextBox.Text = "";
                    MinutesTextBox.Text = "";
                    SecondsTextBox.Text = "";
                }
                else
                {
                    HoursTextBox.Text = value.Hours.ToString();
                    MinutesTextBox.Text = value.Minutes.ToString();
                    SecondsTextBox.Text = value.Seconds.ToString();
                }
            }
        }

        /// <summary>
        /// Gets invoked when HoursTextBox text changed
        /// </summary>
        private void HoursTextBox_Changed(object sender, TextChangedEventArgs e)
        {
            if(!eventLock)
            {
                OnlyAcceptNumberBox(HoursTextBox, 23);
                TimeChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Gets invoked when MinutesTextBox text changed
        /// </summary>
        private void MinutesTextBox_Changed(object sender, TextChangedEventArgs e)
        {
            if(!eventLock)
            {
                OnlyAcceptNumberBox(MinutesTextBox, 59);
                TimeChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Gets invoked when SecondsTextBox text changed
        /// </summary>
        private void SecondsTextBox_Changed(object sender, TextChangedEventArgs e)
        {
            if(!eventLock)
            {
                OnlyAcceptNumberBox(SecondsTextBox, 59);
                TimeChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Removes all none numbers from a textbox and makes sure the number isnt too high
        /// </summary>
        /// <param name="textBox">The textbox to change</param>
        /// <param name="maximumValue">The maximum number the textbox can contain</param>
        private void OnlyAcceptNumberBox(TextBox textBox, int maximumValue)
        {
            eventLock = true;
            List<char> numbers = textBox.Text.ToCharArray().ToList();
            numbers.RemoveAll((letter) => !char.IsNumber(letter));
            if(numbers.Count >= 1)
            {
                int Number = Convert.ToInt32(string.Concat(numbers));
                textBox.Text = Math.Min(Number, maximumValue).ToString();
                textBox.CaretIndex = textBox.Text.Length;
            }
            else
            {
                textBox.Text = "";
            }
            eventLock = false;
        }
    }
}

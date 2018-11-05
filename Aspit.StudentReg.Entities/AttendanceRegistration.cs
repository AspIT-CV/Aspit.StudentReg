using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspit.StudentReg.Entities
{
    /// <summary>
    /// Represents an attendance registration
    /// </summary>
    public struct AttendanceRegistration
    {
        /// <summary>
        /// The id of the AttendanceRegistration
        /// </summary>
        private int id;

        /// <summary>
        /// The time the user signed in
        /// </summary>
        private DateTime meetingTime;

        /// <summary>
        /// The time the user signed out
        /// </summary>
        private DateTime leavingTime;

        /// <summary>
        /// Gets or sets the AttendanceRegistration's Id
        /// </summary>
        public int Id
        {
            get
            {
                return id;
            }

            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException("Id cannot be less than 0.");
                }
                id = value;
            }
        }

        /// <summary>
        /// Gets or sets the AttendanceRegistration's MeetingTime
        /// </summary>
        public DateTime MeetingTime
        {
            get
            {
                return meetingTime;
            }

            set
            {
                if(value != null && value > DateTime.Now)
                {
                    throw new ArgumentException("MeetingTime cannot be in the future.");
                }
                meetingTime = value;
            }
        }

        /// <summary>
        /// Gets or sets the AttendanceRegistration's LeavingTime
        /// </summary>
        public DateTime LeavingTime
        {
            get
            {
                return leavingTime;
            }

            set
            {
                if(value != null && value > DateTime.Now)
                {
                    throw new ArgumentException("LeavingTime cannot be in the future.");
                }
                leavingTime = value;
            }
        }

        /// <summary>
        /// Gets the AttendanceRegistration's date
        /// </summary>
        public DateTime Date
        {
            get
            {
                if(LeavingTime.Date != meetingTime.Date)
                {
                    throw new InvalidOperationException("Couldn't get date from LeavingTime and MeetingTime since they are 2 different dates.");
                }

                return meetingTime.Date;
            }
        }

        //TODO create a duration calculate method
    }
}

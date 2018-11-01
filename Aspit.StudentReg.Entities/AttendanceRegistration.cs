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
    class AttendanceRegistration
    {
        private int id;
        private int userForeignKey;
        private DateTime meetingTime;
        private DateTime leaveTime;
        private DateTime date;

        public AttendanceRegistration(int id, int userForeignKey, DateTime meetingTime, DateTime leaveTime, DateTime date)
        {
            Id = id;
            UserForeignKey = userForeignKey;
            MeetingTime = meetingTime;
            LeaveTime = leaveTime;
            Date = date;
        }

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

        public int UserForeignKey
        {
            get
            {
                return userForeignKey;
            }

            set
            {
                if(value < 0)
                {
                    throw new ArgumentOutOfRangeException("UserForeignKey cannot be less than 0");
                }
                userForeignKey = value;
            }
        }

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

        public DateTime LeaveTime
        {
            get
            {
                return leaveTime;
            }

            set
            {
                if(value != null && value > DateTime.Now)
                {
                    throw new ArgumentException("LeaveTime cannot be in the future.");
                }
                leaveTime = value;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                if(value != null && value > DateTime.Now)
                {
                    throw new ArgumentException("Date cannot be in the future.");
                }
                date = value;
            }
        }
    }
}

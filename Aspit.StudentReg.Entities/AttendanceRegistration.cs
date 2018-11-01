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
    class AttendanceRegistrations
    {
        private int id;
        private int userForeignKey;
        private DateTime meetingTime;
        private DateTime leaveTime;
        private DateTime date;

        public AttendanceRegistrations(int id, int userForeignKey, DateTime meetingTime, DateTime leaveTime, DateTime date)
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
                //TODO check for valid value
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
                //TODO check for valid value
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
                //TODO check for valid value
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
                //TODO check for valid value
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
                //TODO check for valid value
                date = value;
            }
        }
    }
}

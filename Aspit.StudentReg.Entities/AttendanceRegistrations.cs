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

        public int Id
        {
            get
            {
                return id;
            }

            set
            {
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
                date = value;
            }
        }
    }
}

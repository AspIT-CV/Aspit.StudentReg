﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aspit.StudentReg.Entities
{
    /// <summary>
    /// Represents an attendance registration
    /// </summary>
    public class AttendanceRegistration
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
        private DateTime leaveTime;

        /// <summary>
        /// Intializes a new AttendanceRegistration using the given parameters.
        /// </summary>
        /// <param name="id">the registration id</param>
        /// <param name="meetingTime">the time the user signed in</param>
        /// <param name="leaveTime">the time the user signed out</param>
        /// <param name="date">the day this attendance is about</param>
        public AttendanceRegistration(int id, DateTime meetingTime, DateTime leaveTime)
        {
            Id = id;
            MeetingTime = meetingTime;
            LeaveTime = leaveTime;
        }

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
        /// Gets or sets the AttendanceRegistration's LeaveTime
        /// </summary>
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

        /// <summary>
        /// Gets the AttendanceRegistration's date
        /// </summary>
        public DateTime Date
        {
            get
            {
                if(LeaveTime.Date != meetingTime.Date)
                {
                    throw new InvalidOperationException("Couldn't get date from LeaveTime and MeetingTime since they are 2 different dates.");
                }

                return meetingTime.Date;
            }
        }

        //TODO create a duration calculate method
    }
}
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
                return new DateTime(meetingTime.Ticks / 10000000 * 10000000);
            }

            set
            {
                if(value > DateTime.Now)
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
                return new DateTime(leavingTime.Ticks / 10000000 * 10000000);
            }

            set
            {
                if(value > DateTime.Now)
                {
                    throw new ArgumentException("LeavingTime cannot be in the future.");
                }
                leavingTime = new DateTime(value.Ticks / 1000000 * 1000000);
            }
        }

        /// <summary>
        /// Gets the AttendanceRegistration's date
        /// </summary>
        public DateTime Date
        {
            get
            {
                if(DateTime.Equals(leavingTime, default) && DateTime.Equals(meetingTime, default))
                {
                    throw new InvalidOperationException("Couldn't calculate Date from LeavingTime and MeetingTime because they are both default.");
                }
                if(leavingTime != default && meetingTime == default)
                {
                    return leavingTime.Date;
                }
                else if(meetingTime != default && leavingTime == default)
                {
                    return meetingTime.Date;
                }
                if(LeavingTime.Date != meetingTime.Date)
                {
                    throw new InvalidOperationException("Couldn't get date from LeavingTime and MeetingTime since they are 2 different dates.");
                }
                else
                {
                    return meetingTime.Date;
                }

            }
        }

        /// <summary>
        /// Gets the AttendanceRegistration's timespan
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                DateTime canCalculateDate = Date;
                if(DateTime.Equals(leavingTime, default) || DateTime.Equals(meetingTime, default))
                {
                    throw new InvalidOperationException("Couldn't calculate Duration from LeavingTime and MeetingTime.");
                }

                TimeSpan output = leavingTime - meetingTime;

                return leavingTime - meetingTime;
            }
        }

        /// <summary>
        /// Checks if this AttendanceRegistration is equal to a default one
        /// </summary>
        /// <returns>True if this is equal to default</returns>
        public bool IsDefault()
        {
            AttendanceRegistration defaultRegistration = default;
            return Equals(defaultRegistration);
        }

        /// <summary>
        /// Checks if this attendanceRegistration is equal to the given object
        /// </summary>
        /// <param name="obj">The object to check</param>
        /// <returns>returns true if they are equal</returns>
        public override bool Equals(object obj)
        {
            if(!(obj is AttendanceRegistration registration))
            {
                throw new ArgumentException("Cannot check if object is equal to attendanceRegistration");
            }
            return (LeavingTime == registration.LeavingTime
                && MeetingTime == registration.MeetingTime);
        }

        /// <summary>
        /// Returns the hascode for this instance
        /// </summary>
        /// <returns>Returns the hascode for this instance</returns>
        public override int GetHashCode()
        {
            return id;
        }

        /// <summary>
        /// Checks if the given attendanceRegistration are equal
        /// </summary>
        /// <param name="left">attendanceRegistration 1</param>
        /// <param name="right">attendanceRegistration 2</param>
        /// <returns>Returns true if they are equal</returns>
        public static bool operator ==(AttendanceRegistration left, AttendanceRegistration right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Checks if the given attendanceRegistration are not equal
        /// </summary>
        /// <param name="left">attendanceRegistration 1</param>
        /// <param name="right">attendanceRegistration 2</param>
        /// <returns>Returns true if they are not equal</returns>
        public static bool operator !=(AttendanceRegistration left, AttendanceRegistration right)
        {
            return !(left == right);
        }
    }
}

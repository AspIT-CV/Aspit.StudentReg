using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aspit.StudentReg.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspit.StudentReg.Entities;

namespace Aspit.StudentReg.Tests
{
    [TestClass()]
    public class AttendanceRegistrationsTests
    {
        [TestMethod()]
        public void DurationTest()
        {
            //Tests if duration is calculated correct
            AttendanceRegistration registration = new AttendanceRegistration
            {
                Id = 1,
                LeavingTime = new DateTime(9, 9, 9, 14, 30, 0),
                MeetingTime = new DateTime(9, 9, 9, 9, 0, 0)
            };
            //14:30 - 9:00 = 5:30h = 330m
            Assert.AreEqual(330, registration.Duration.TotalMinutes);

            //Tests if duration throws an error if dates arent on the same date
            registration = new AttendanceRegistration
            {
                Id = 1,
                LeavingTime = new DateTime(2, 2, 2, 2, 0, 0),
                MeetingTime = new DateTime(9, 9, 9, 9, 0, 0)
            };
            Assert.ThrowsException<InvalidOperationException>(() => registration.Duration);
        }

        [TestMethod()]
        public void DateTest()
        {
            //Tests if date is calculated correct
            AttendanceRegistration registration = new AttendanceRegistration
            {
                Id = 1,
                LeavingTime = new DateTime(2018, 6, 11, 23, 59, 59),
                MeetingTime = new DateTime(2018, 6, 11, 9, 0, 0)
            };
            Assert.AreEqual(new DateTime(2018,6,11), registration.Date);

            //Tests if date throws an error if dates arent on the same date
            registration = new AttendanceRegistration
            {
                Id = 1,
                LeavingTime = new DateTime(2, 2, 2, 2, 0, 0),
                MeetingTime = new DateTime(9, 9, 9, 9, 0, 0)
            };
            Assert.ThrowsException<InvalidOperationException>(() => registration.Date);
        }

        [TestMethod()]
        public void EqualsTest()
        {
            //Tests if 2 dates are the same
            AttendanceRegistration registration1 = new AttendanceRegistration
            {
                Id = 1,
                LeavingTime = new DateTime(2018, 6, 11, 23, 59, 59),
                MeetingTime = new DateTime(2018, 6, 11, 9, 0, 0)
            };
            AttendanceRegistration registration2 = new AttendanceRegistration
            {
                Id = 2,
                LeavingTime = new DateTime(2018, 6, 11, 23, 59, 59),
                MeetingTime = new DateTime(2018, 6, 11, 9, 0, 0)
            };
            Assert.IsTrue(registration1.Equals(registration2));

            //Tests if 2 dates arent the same
            registration2 = new AttendanceRegistration
            {
                Id = 2,
                LeavingTime = new DateTime(2018, 6, 10, 23, 59, 59),
                MeetingTime = new DateTime(2018, 6, 10, 9, 0, 0)
            };
            Assert.IsFalse(registration1.Equals(registration2));

            //Tests if equals throws an error if the given parameter isnt a datetime
            Assert.ThrowsException<ArgumentException>(() => registration1.Equals(5));
        }

        [TestMethod()]
        public void IsDefaultTest()
        {
            //Tests if a date is default
            AttendanceRegistration registration = default;
            Assert.IsTrue(registration.IsDefault());

            //Tests if a date isnt default
            registration = new AttendanceRegistration
            {
                Id = 2,
                LeavingTime = new DateTime(2018, 6, 10, 23, 59, 59),
                MeetingTime = new DateTime(2018, 6, 10, 9, 0, 0)
            };
            Assert.IsFalse(registration.IsDefault());
        }

        [TestMethod()]
        public void Compare()
        {
            //Test if a registration with a higher date outputs -1
            AttendanceRegistration registration1 = new AttendanceRegistration
            {
                Id = 1,
                MeetingTime = new DateTime(2018, 6, 10, 9, 0, 0),
                LeavingTime = new DateTime(2018, 6, 10, 15, 0, 0)
            };
            AttendanceRegistration registration2 = new AttendanceRegistration
            {
                Id = 2,
                MeetingTime = new DateTime(2018, 6, 9, 8, 0, 0),
                LeavingTime = new DateTime(2018, 6, 9, 9, 0, 0)
            };
            Assert.AreEqual(-1, AttendanceRegistration.Compare(registration1, registration2));

            //Test if a registration with a lower time outputs 1
            registration1 = new AttendanceRegistration
            {
                Id = 1,
                MeetingTime = new DateTime(2018, 6, 9, 7, 0, 0),
                LeavingTime = new DateTime(2018, 6, 9, 8, 0, 0)
            };
            Assert.AreEqual(1, AttendanceRegistration.Compare(registration1, registration2));
        }
    }
}

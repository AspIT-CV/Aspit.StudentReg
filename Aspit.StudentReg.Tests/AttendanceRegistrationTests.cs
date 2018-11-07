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
            AttendanceRegistration registration = new AttendanceRegistration
            {
                Id = 1,
                LeavingTime = new DateTime(9, 9, 9, 14, 30, 0),
                MeetingTime = new DateTime(9, 9, 9, 9, 0, 0)
            };
            //14:30 - 9:00 = 5:30h = 330m
            Assert.AreEqual(330, registration.Duration.TotalMinutes);

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
            AttendanceRegistration registration = new AttendanceRegistration
            {
                Id = 1,
                LeavingTime = new DateTime(2018, 6, 11, 23, 59, 59),
                MeetingTime = new DateTime(2018, 6, 11, 9, 0, 0)
            };

            Assert.AreEqual(new DateTime(2018,6,11), registration.Date);

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

            registration2 = new AttendanceRegistration
            {
                Id = 2,
                LeavingTime = new DateTime(2018, 6, 10, 23, 59, 59),
                MeetingTime = new DateTime(2018, 6, 10, 9, 0, 0)
            };
            Assert.IsFalse(registration1.Equals(registration2));
            Assert.ThrowsException<ArgumentException>(() => registration1.Equals(5));
        }

        [TestMethod()]
        public void IsDefaultTest()
        {
            AttendanceRegistration registration = default;
            Assert.IsTrue(registration.IsDefault());

            registration = new AttendanceRegistration
            {
                Id = 2,
                LeavingTime = new DateTime(2018, 6, 10, 23, 59, 59),
                MeetingTime = new DateTime(2018, 6, 10, 9, 0, 0)
            };
            Assert.IsFalse(registration.IsDefault());
        }
    }
}

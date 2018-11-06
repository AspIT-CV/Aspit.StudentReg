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
        public void Duration()
        {
            AttendanceRegistration registration = new AttendanceRegistration
            {
                Id = 1,
                LeavingTime = new DateTime(9, 9, 9, 14, 30, 0),
                MeetingTime = new DateTime(9, 9, 9, 9, 0, 0)
            };

            //14:30 - 9:00 = 5:30h = 330m
            Assert.AreEqual(330, registration.Duration.TotalMinutes);
        }
    }
}

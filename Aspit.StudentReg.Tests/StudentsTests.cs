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
    [TestClass]
    public class StudentsTests
    {
        Student student;
        int id = 1;
        string name = " per Bosen ";
        string expectedName = "Per Bosen";
        string uniLogin = "perx234k";

        public StudentsTests()
        {
            student = new Student(id, name, uniLogin);
        }

        [TestMethod]
        public void Initialization()
        {
            Assert.AreNotEqual(null, student);
        }

        [TestMethod]
        public void StudentValueTest()
        {
            Assert.AreEqual(id, student.Id);
            Assert.AreEqual(expectedName, student.Name);
            Assert.AreEqual(uniLogin, student.UniLogin);
        }

        [TestMethod]
        public void StudentAttendanceRegistrationsAssignment()
        {
            AttendanceRegistration registration = new AttendanceRegistration
            {
                Id = 1,
                LeavingTime = new DateTime(2018, 6, 11, 23, 59, 59),
                MeetingTime = new DateTime(2018, 6, 11, 9, 0, 0)
            };

            student.AttendanceRegistrations = registration;
            Assert.AreEqual(registration, student.AttendanceRegistrations);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Unilogin is invalid")]
        public void StudentUniLoginError()
        {
            uniLogin = "PerBo";
            student = new Student(id, name, uniLogin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
        "Name is invalid")]
        public void StudentNameError()
        {
            name = "!Per0Bo";
            student = new Student(id, name, uniLogin);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException),
        "Id cannot be less than 0")]
        public void StudentIdError()
        {
            id = -1;
            student = new Student(id, name, uniLogin);
        }
    }
}
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Aspit.StudentReg.Entities;

namespace Aspit.StudentReg.Tests
{
    [TestClass]
    public class StudentsTests
    {
        [TestMethod]
        public void Initialization()
        {
            //Arrange
            Student s;

            int id = 1;

            string name = "per Bosen";
            string expectedName = "Per Bosen";

            string uniLogin = "perx234k";

            //Act

            s = new Student(id, name, uniLogin);

            //Assert

            Assert.AreEqual(id, s.Id);
            Assert.AreEqual(expectedName, s.Name);
            Assert.AreEqual(uniLogin, s.UniLogin);
        }
    }
}

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
    public class StudentsRepositoryTests
    {

        public static StudentsRepository CreateRepository()
        {
            return new StudentsRepository(RepositoryBase.RetrieveConnectionString());
        }

        [TestMethod()]
        public void GetAllTest()
        {
            StudentsRepository StudentsRepositorys = new StudentsRepository(RepositoryBase.RetrieveConnectionString());
            List<Student> list = StudentsRepositorys.GetAll();

            Assert.AreNotEqual(0, list.Count);
        }

        [TestMethod()]
        public void GetFromIdTest()
        {
            StudentsRepository repository = CreateRepository();
            Student student = new Student(0, "bla", "blax2345");

            repository.CreateStudent(student);

            Student databaseStudent = repository.GetFromId(student.Id);
            Assert.AreEqual(student.Name, databaseStudent.Name);
            Assert.AreEqual(student.UniLogin, databaseStudent.UniLogin);
        }

        [TestMethod()]
        public void UpdateTest()
        {
            StudentsRepository repository = CreateRepository();
            Student student = new Student(0, "bla", "blax2345");

            repository.CreateStudent(student);

            student.Name = "bla blas";
            student.UniLogin = "blas1346";

            repository.UpdateStudent(student);

            Student databaseStudent = repository.GetFromId(student.Id);

            Assert.AreEqual(student.Name, databaseStudent.Name);
            Assert.AreEqual(student.UniLogin, databaseStudent.UniLogin);
        }
    }
}

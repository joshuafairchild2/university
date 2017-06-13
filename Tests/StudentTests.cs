using Xunit;
using System;
using System.Collections.Generic;

namespace University.Objects
{
  [Collection("University")]

  public class StudentTests : IDisposable
  {
    public StudentTests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=university_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Student_DatabaseEmptyOnload()
    {
      List<Student> testList = Student.GetAll();
      List<Student> controlList = new List<Student>{};

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void Student_Save_SaveToDatabase()
    {
      Student newStudent = new Student("David", new DateTime(2015, 05, 12));
      newStudent.Save();

      Student testStudent = Student.GetAll()[0];
      Assert.Equal(newStudent, testStudent);
    }

    [Fact]
    public void Student_Equal_StudentAreEqual()
    {
      Student newStudent = new Student("David", new DateTime(2015, 05, 12), 1);
      Student testStudent = new Student("David", new DateTime(2015, 05, 12), 1);

      Assert.Equal(newStudent, testStudent);
    }
    public void Dispose()
    {
      Student.DeleteAll();
    }
  }
}

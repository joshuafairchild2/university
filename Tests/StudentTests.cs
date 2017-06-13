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

    [Fact]
    public void Student_Find_FindsStudentInDB()
    {
      Student controlStudent = new Student("David", new DateTime(2015, 05, 12));
      controlStudent.Save();

      Student testStudent = Student.Find(controlStudent.GetId());

      Assert.Equal(controlStudent, testStudent);
    }

    [Fact]
    public void Student_AddCourse_AddCourseToStudent()
    {
      Student newStudent = new Student("David", new DateTime(2015, 05, 12));
      newStudent.Save();

      Course firstCourse = new Course("Computer Science", "CS101");
      firstCourse.Save();
      Course secondCourse = new Course("Computer Science 2", "CS102");
      secondCourse.Save();

      newStudent.AddCourse(firstCourse);
      newStudent.AddCourse(secondCourse);

      List<Course> studentCourses = newStudent.GetCourses();
      List<Course> controlCourses = new List<Course>{firstCourse, secondCourse};

      Assert.Equal(controlCourses, studentCourses);
    }

    [Fact]
    public void Student_Delete_DeleteSingleStudentFromDB()
    {
      Student newStudent = new Student("David", new DateTime(2015, 05, 12));
      newStudent.Save();

      newStudent.Delete();

      List<Student> testList = Student.GetAll();
      List<Student> controlList = new List<Student>{};

      Assert.Equal(controlList, testList);
    }

    public void Dispose()
    {
      Student.DeleteAll();
    }
  }
}

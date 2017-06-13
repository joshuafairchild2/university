using Xunit;
using System;
using System.Collections.Generic;

namespace University.Objects
{
  [Collection("University")]
  public class CourseTests : IDisposable
  {
    public CourseTests()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=university_test;Integrated Security=SSPI;";
    }

    [Fact]
    public void Course_GetAll_DatabaseEmptyOnload()
    {
      List<Course> testList = Course.GetAll();
      List<Course> controlList = new List<Course>{};

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void Course_Save_SaveToDatabase()
    {
      Course newCourse = new Course("Computer Science", "CS101");
      newCourse.Save();

      Course testCourse = Course.GetAll()[0];
      Assert.Equal(newCourse, testCourse);
    }

    [Fact]
    public void Course_Equals_CourseEqualsCourse()
    {
      Course controlCourse = new Course("Computer Science", "CS101", 1);
      Course testCourse = new Course("Computer Science", "CS101", 1);

      Assert.Equal(controlCourse, testCourse);
    }

    [Fact]
    public void Course_Find_FindsCourseInDB()
    {
      Course controlCourse = new Course("Computer Science", "CS101");
      controlCourse.Save();

      Course testCourse = Course.Find(controlCourse.GetId());

      Assert.Equal(controlCourse, testCourse);
    }

    [Fact]
    public void Course_AddStudent_AddsStudentToCourse()
    {
      Course newCourse = new Course("Computer Science", "CS101");
      newCourse.Save();
      Student newStudent1 = new Student("David", new DateTime(2015, 05, 12));
      newStudent1.Save();
      Student newStudent2 = new Student("John", new DateTime(2016, 05, 22));
      newStudent2.Save();

      newCourse.AddStudent(newStudent1);
      newCourse.AddStudent(newStudent2);

      List<Student> testList = newCourse.GetStudents();
      List<Student> controlList = new List<Student>{newStudent1, newStudent2};

      Assert.Equal(controlList, testList);
    }

    [Fact]
    public void Course_Delete_DeleteCourse()
    {
      Course newCourse = new Course("Computer Science", "CS101");
      newCourse.Save();

      newCourse.Delete();

      List<Course> newList = Course.GetAll();
      List<Course> controlList = new List<Course>{};

      Assert.Equal(controlList, newList);
    }

    public void Dispose()
    {
      Course.DeleteAll();
    }
  }
}

using System;
using System.Collections.Generic;
using Nancy;
using University.Objects;
// using System.Data;
// using System.Data.SqlClient;

namespace University
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        return View["index.cshtml"];
      };

      Get["/students"] = _ => {
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };

      Get["/students/new"] = _ => {
        return View["new_student.cshtml"];
      };

      Post["/students"] = _ => {
        Student newStudent = new Student(Request.Form["student-name"], DateTime.Now);
        newStudent.Save();
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };
    }
  }
}

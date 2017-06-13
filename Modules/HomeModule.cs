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

      Get["/courses"] = _ => {
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };

      Get["/courses/new"] = _ => {
        return View["new_course.cshtml"];
      };

      Post["/courses"] = _ => {
        Course newCourse = new Course(Request.Form["course-name"], Request.Form["course-number"]);
        newCourse.Save();
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };

      Get["/courses/{courseId}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Course selectedCourse = Course.Find(parameters.courseId);
        List<Student> students = selectedCourse.GetStudents();
        List<Student> allStudents = Student.GetAll();
        model.Add("selected-course", selectedCourse);
        model.Add("students", students);
        model.Add("all-students", allStudents);
        return View["course.cshtml", model];
      };

      Get["/students/{studentId}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Student selectedStudent = Student.Find(parameters.studentId);
        List<Course> courses = selectedStudent.GetCourses();
        List<Course> allCourses = Course.GetAll();
        model.Add("selected-student", selectedStudent);
        model.Add("courses", courses);
        model.Add("all-courses", allCourses);
        return View["student.cshtml", model];
      };

      Post["/courses/{courseId}"] = parameters => {
        Dictionary<string, object> model = new Dictionary<string, object>{};
        Course selectedCourse = Course.Find(parameters.courseId);
        selectedCourse.AddStudent(Student.Find(Request.Form["selected-student"]));
        List<Student> students = selectedCourse.GetStudents();
        List<Student> allStudents = Student.GetAll();
        model.Add("selected-course", selectedCourse);
        model.Add("students", students);
        model.Add("all-students", allStudents);
        return View["course.cshtml", model];
      };

      Delete["/students/{student_id}/delete"] = parameters => {
        Student foundStudent = Student.Find(parameters.student_id);
        foundStudent.Delete();
        List<Student> allStudents = Student.GetAll();
        return View["students.cshtml", allStudents];
      };

      Delete["/courses"] = _ => {
        Course.DeleteAll();
        List<Course> allCourses = Course.GetAll();
        return View["courses.cshtml", allCourses];
      };
    }
  }
}

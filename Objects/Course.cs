using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace University.Objects
{
  public class Course
  {
    private int _id;
    private string _name;
    private string _courseNumber;

    public Course(string name, string courseNumber, int id = 0)
    {
      _name = name;
      _courseNumber = courseNumber;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public void SetId(int newId)
    {
      _id = newId;
    }
    public string GetName()
    {
      return _name;
    }
    public void SetName(string newName)
    {
      _name = newName;
    }
    public string GetCourseNumber()
    {
      return _courseNumber;
    }
    public void SetCourseNumber(string newCourseNumber)
    {
      _courseNumber = newCourseNumber;
    }

    public override bool Equals(System.Object otherCourse)
    {
      if(!(otherCourse is Course))
      {
        return false;
      }
      else
      {
        Course newCourse = (Course) otherCourse;
        bool nameEquality = this.GetName() == newCourse.GetName();
        bool idEquality = this.GetId() == newCourse.GetId();
        bool numberEquality = this.GetCourseNumber() == newCourse.GetCourseNumber();
        return (nameEquality && idEquality && numberEquality);
      }
    }

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM courses;", conn);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public static List<Course> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      List<Course> courses = new List<Course>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string courseNumber = rdr.GetString(2);
        Course newCourse = new Course(name, courseNumber, id);
        courses.Add(newCourse);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return courses;
    }

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO courses (name, number) OUTPUT INSERTED.ID values (@CourseName, @CourseNumber)", conn);

      SqlParameter nameParameter = new SqlParameter("@CourseName", this.GetName());
      SqlParameter numberParameter = new SqlParameter("@CourseNumber", this.GetCourseNumber());

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(numberParameter);

      SqlDataReader rdr = cmd.ExecuteReader();
      while(rdr.Read())
      {
        this._id = rdr.GetInt32(0);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Course Find(int idToFind)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM courses WHERE id = @CourseId", conn);
      SqlParameter idParam = new SqlParameter("@CourseId", idToFind);
      cmd.Parameters.Add(idParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      int id = 0;
      string name = null;
      string courseNumber = null;

      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        courseNumber = rdr.GetString(2);
      }

      Course foundCourse  = new Course(name, courseNumber, id);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundCourse;
    }

    public void AddStudent(Student studentToAdd)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId);", conn);
      SqlParameter studentIdParam = new SqlParameter("@StudentId", studentToAdd.GetId());
      SqlParameter courseIdParam = new SqlParameter("@CourseId", this.GetId());
      cmd.Parameters.Add(studentIdParam);
      cmd.Parameters.Add(courseIdParam);

      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public List<Student> GetStudents()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT students.* FROM courses JOIN students_courses ON (courses.id = students_courses.course_id) JOIN students ON (students.id = students_courses.student_id) WHERE courses.id = @CourseId;", conn);
      SqlParameter courseIdParam = new SqlParameter("@CourseId", this.GetId());
      cmd.Parameters.Add(courseIdParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      List<Student> students = new List<Student>{};
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        DateTime enrollDate = rdr.GetDateTime(2);
        Student newStudent = new Student(name, enrollDate, id);
        students.Add(newStudent);
      }

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return students;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM courses WHERE id = @CourseId;", conn);
      SqlParameter idParam = new SqlParameter("@CourseId", this.GetId());
      cmd.Parameters.Add(idParam);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }
  }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace University.Objects
{
  public class Student
  {
    private int _id;
    private string _name;
    private DateTime _enrollDate;

    public Student(string name, DateTime enrollDate, int id = 0)
    {
      _name = name;
      _enrollDate = enrollDate;
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
    public DateTime GetEnrollDate()
    {
      return _enrollDate;
    }
    public void SetEnrollDate(DateTime newEnrollDate)
    {
      _enrollDate = newEnrollDate;
    }

    public override bool Equals(System.Object otherStudent)
    {
      if(!(otherStudent is Student))
      {
        return false;
      }
      else
      {
        Student newStudent = (Student) otherStudent;
        bool nameEquality = this.GetName() == newStudent.GetName();
        bool idEquality = this.GetId() == newStudent.GetId();
        bool enrollDateEquality = this.GetEnrollDate() == newStudent.GetEnrollDate();
        return (nameEquality && idEquality && enrollDateEquality);
      }
    }

    public static List<Student> GetAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students", conn);
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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO students (name, enroll_date) OUTPUT INSERTED.id VALUES (@StudentName, @EnrollDate)", conn);

      SqlParameter nameParameter = new SqlParameter("@StudentName", this.GetName());
      SqlParameter enrollParameter = new SqlParameter("@EnrollDate", this.GetEnrollDate());

      cmd.Parameters.Add(nameParameter);
      cmd.Parameters.Add(enrollParameter);

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

    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM students; DELETE FROM students_courses;", conn);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public static Student Find(int idToFind)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM students WHERE id = @StudentId", conn);
      SqlParameter idParam = new SqlParameter("@StudentId", idToFind);
      cmd.Parameters.Add(idParam);

      SqlDataReader rdr = cmd.ExecuteReader();

      int id = 0;
      string name = null;
      DateTime enrollDate = default(DateTime);

      while(rdr.Read())
      {
        id = rdr.GetInt32(0);
        name = rdr.GetString(1);
        enrollDate = rdr.GetDateTime(2);
      }

      Student foundStudent  = new Student(name, enrollDate, id);

      if(rdr != null)
      {
        rdr.Close();
      }
      if(conn != null)
      {
        conn.Close();
      }

      return foundStudent;
    }

    public void AddCourse(Course newCourse)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO students_courses (student_id, course_id) VALUES (@StudentId, @CourseId);", conn);

      SqlParameter studentParam = new SqlParameter("@StudentId", this.GetId());
      SqlParameter courseParam = new SqlParameter("@CourseId", newCourse.GetId());

      cmd.Parameters.Add(studentParam);
      cmd.Parameters.Add(courseParam);
      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }

    public List<Course> GetCourses()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT courses.* FROM students JOIN students_courses ON (students.id = students_courses.student_id) JOIN courses ON (courses.id = students_courses.course_id) WHERE students.id = @StudentId;", conn);

      SqlParameter studentParameter = new SqlParameter("@StudentId", this.GetId());
      cmd.Parameters.Add(studentParameter);

      List<Course> courses = new List<Course>{};
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        string course = rdr.GetString(2);
        Course newCourse = new Course(name, course, id);
        courses.Add(newCourse);
      }

      if(conn != null)
      {
        conn.Close();
      }
      if(rdr != null)
      {
        rdr.Close();
      }

      return courses;
    }

    public void Delete()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("DELETE FROM students WHERE id = @StudentId; DELETE FROM students_courses WHERE student_id = @StudentId;", conn);
      SqlParameter idParam = new SqlParameter("@StudentId", this.GetId());
      cmd.Parameters.Add(idParam);

      cmd.ExecuteNonQuery();

      if(conn != null)
      {
        conn.Close();
      }
    }
  }
}

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

    
    //methods go here
  }
}

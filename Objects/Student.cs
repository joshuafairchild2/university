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
    //methods go here
  }
}

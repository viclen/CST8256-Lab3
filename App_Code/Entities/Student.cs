using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Student
/// </summary>
public class Student
{
    public string ID { get; set; }
    public string Name { get; set; }
    public int Grade { get; set; }

    public string CourseNumber { get; set; }

    public Student()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Student(string i, string n, string g)
    {
        this.ID = i;
        this.Name = n;
        this.Grade = Int32.Parse(g);
    }
}
using System;
using System.Collections;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Course
/// </summary>
public class Course
{
    public string CourseNumber { get; set; }
    public string CourseName { get; set; }
    private ArrayList StudentList { get; set; }

    public Course()
    {
        StudentList = new ArrayList();
    }

    public Course(string CourseNumber, string CourseName)
    {
        this.CourseNumber = CourseNumber;
        this.CourseName = CourseName;
        StudentList = new ArrayList();
    }

    public void AddStudent(Student s)
    {
        StudentList.Add(s);
    }

    public ArrayList GetStudents()
    {
        return StudentList;
    }

    override
    public string ToString()
    {
        string str = CourseNumber + " - '" + CourseName + "' | Students: [ ";

        foreach (Student s in StudentList)
        {
            str += StudentList.ToString() + " ,";
        }

        str += " ]";

        return str;
    }
}
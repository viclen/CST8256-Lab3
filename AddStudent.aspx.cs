using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Addstudents : System.Web.UI.Page
{
    public string order = "ascending";

    public class SortById : IComparer
    {
        private String order = "ascending";
        public SortById(String order)
        {
            this.order = order;
        }

        int IComparer.Compare(Object x, Object y)
        {
            Student a = (Student)x;
            Student b = (Student)y;

            if (this.order.Equals("descending"))
            {
                a = (Student)y;
                b = (Student)x;
            }

            return ((new CaseInsensitiveComparer()).Compare(a.ID, b.ID));
        }
    }

    public class SortByName : IComparer
    {
        private String order = "ascending";
        public SortByName(String order)
        {
            this.order = order;
        }

        int IComparer.Compare(Object x, Object y)
        {
            Student a = (Student)x;
            Student b = (Student)y;

            if (this.order.Equals("descending"))
            {
                a = (Student)y;
                b = (Student)x;
            }

            if (a.Name.Split(' ')[1].Equals(b.Name.Split(' ')[1]))
            {
                return ((new CaseInsensitiveComparer()).Compare(a.Name, b.Name));
            }

            return ((new CaseInsensitiveComparer()).Compare(a.Name.Split(' ')[1], b.Name.Split(' ')[1]));
        }
    }

    public class SortByGrade : IComparer
    {
        private String order = "ascending";
        public SortByGrade(String order)
        {
            this.order = order;
        }

        int IComparer.Compare(Object x, Object y)
        {
            Student a = (Student)x;
            Student b = (Student)y;

            if (this.order.Equals("descending"))
            {
                a = (Student)y;
                b = (Student)x;
            }

            if (a.Grade == b.Grade)
            {
                return ((IComparer)new SortByName(this.order)).Compare(x, y);
            }

            return a.Grade < b.Grade ? 1 : -1;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        LinkButton btnHome = (LinkButton)Master.FindControl("btnHome");
        btnHome.Click += (object s, EventArgs ev) =>
        {
            Response.Redirect("Default.aspx");
        };

        BulletedList topMenu = (BulletedList)Master.FindControl("topMenu");
        topMenu.Click += (object s, BulletedListEventArgs ev) =>
        {
            switch (ev.Index)
            {
                case 0:
                    Response.Redirect("AddCourse.aspx");
                    break;
                case 1:
                    Response.Redirect("AddStudent.aspx");
                    break;
            }
        };
        if (topMenu.Items.Count < 2)
        {
            topMenu.Items.Add("Add Courses");
            topMenu.Items.Add("Add Students");
        }

        if (Session["courselist"] != null)
        {
            ArrayList courses = (ArrayList)Session["courselist"];

            if(drpCourseSelection.Items.Count < courses.Count){
                for(int i=0; i<courses.Count; i++)
                {
                    drpCourseSelection.Items.Add(new ListItem(((Course)courses[i]).CourseName, i + ""));
                }
            }
        }

        if (Session["selectedcourse"] != null && drpCourseSelection.SelectedValue=="-1")
        {
            drpCourseSelection.SelectedValue = Session["selectedcourse"].ToString();
            orderList(null, null);
        }
    }

    public void btnSubmit_Click(object sender, EventArgs e)
    {
        Page.Validate();

        ArrayList courses = (ArrayList)Session["courselist"];

        if (!drpCourseSelection.SelectedValue.Equals("-1"))
        {
            if (Session["selectedcourse"] != null)
            {
                if (Page.IsValid)
                {
                    int number = int.Parse(Session["selectedcourse"].ToString());
                    Course course = (Course)courses[number];
                    course.AddStudent(new Student(studentID.Text, studentName.Text, studentGrade.Text));
                    courses[number] = course;
                    Session["courselist"] = courses;
                }

                Response.Redirect("AddStudent.aspx");
            }
        }
    }

    public void orderList(object sender, EventArgs e)
    {
        ArrayList courselist = (ArrayList)Session["courselist"];

        if (Session["selectedcourse"] != null && !Session["selectedcourse"].Equals("-1"))
        {
            Course course = (Course)courselist[int.Parse(Session["selectedcourse"].ToString())];

            TableRow header = tblStudents.Rows[0];
            tblStudents.Rows.Clear();
            tblStudents.Rows.Add(header);

            ArrayList list = course.GetStudents();

            if(Session["sort"] != null && (string)Session["sort"] == Request.Params["sort"])
            {
                if (Session["order"] != null && Session["order"].ToString() == "ascending")
                {
                    this.order = "descending";
                    Session["order"] = this.order;
                }
                else
                {
                    this.order = "ascending";
                    Session["order"] = this.order;
                }
            }
            else
            {
                this.order = "ascending";
                Session["order"] = this.order;
            }

            Session["sort"] = Request.Params["sort"];

            IComparer comparer;
            switch (Request.Params["sort"])
            {
                case "id":
                    comparer = new SortById(this.order);
                    list.Sort(comparer);
                    break;
                case "name":
                    comparer = new SortByName(this.order);
                    list.Sort(comparer);
                    break;
                case "grade":
                    comparer = new SortByGrade(this.order);
                    list.Sort(comparer);
                    break;
            }

            foreach (Student student in list)
            {
                TableRow row = new TableRow();

                TableCell cell = new TableCell();
                cell.Text = student.ID;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = student.Name;
                row.Cells.Add(cell);

                cell = new TableCell();
                cell.Text = student.Grade + "";
                row.Cells.Add(cell);

                tblStudents.Rows.Add(row);
            }
        }
    }

    protected void drpCourseSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["selectedcourse"] = drpCourseSelection.SelectedValue;

        orderList(null, null);
    }
}
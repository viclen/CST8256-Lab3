using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Addcourses : System.Web.UI.Page
{
    public ArrayList courseList;
    private string order = "ascending";

    public class SortByNumber : IComparer
    {
        private String order = "ascending";
        public SortByNumber(String order)
        {
            this.order = order;
        }

        int IComparer.Compare(Object x, Object y)
        {
            Course a = (Course)x;
            Course b = (Course)y;

            if (this.order.Equals("descending"))
            {
                a = (Course)y;
                b = (Course)x;
            }

            return ((new CaseInsensitiveComparer()).Compare(a.CourseNumber, b.CourseNumber));
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
            Course a = (Course)x;
            Course b = (Course)y;

            if (this.order.Equals("descending"))
            {
                a = (Course)y;
                b = (Course)x;
            }

            return ((new CaseInsensitiveComparer()).Compare(a.CourseName, b.CourseName));
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["courselist"] == null)
        {
            this.courseList = new ArrayList();
            Session["courselist"] = this.courseList;
        }
        else
        {
            this.courseList = (ArrayList)Session["courselist"];
        }

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

        loadTable();
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        this.courseList = (ArrayList)Session["courselist"];

        Course course = new Course(courseNumber.Text, courseName.Text);

        this.courseList.Add(course);
        Session["courselist"] = this.courseList;

        Response.Redirect("AddCourse.aspx");
    }

    private void loadTable()
    {
        ArrayList list = (ArrayList)Session["courselist"];

        if (Session["sort"] != null && Session["sort"].ToString() == Request.Params["sort"])
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
            case "number":
                comparer = new SortByNumber(this.order);
                list.Sort(comparer);
                break;
            case "name":
                comparer = new SortByName(this.order);
                list.Sort(comparer);
                break;
        }

        foreach (Course course in list)
        {
            TableRow row = new TableRow();

            TableCell cell = new TableCell();
            cell.Text = course.CourseNumber;
            row.Cells.Add(cell);

            cell = new TableCell();
            cell.Text = course.CourseName;
            row.Cells.Add(cell);

            tblCourses.Rows.Add(row);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
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
    }
}
<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeFile="AddCourse.aspx.cs" Inherits="Addcourses" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="App_Themes/SiteStyles.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h1>Add New Course</h1>

    <table>
        <tr>
            <td>
                <label for="courseNumber">Course Number</label>
            </td>
            <td>
                <asp:TextBox ID="courseNumber" runat="server" CssClass="form-control" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="courseNumberValidator" runat="server" ControlToValidate="courseNumber"
                        CssClass="error" Text="Number is required." />
            </td>
        </tr>
        <tr>
            <td>
                <label for="courseName">Course Name</label>
            </td>
            <td>
                <asp:TextBox ID="courseName" runat="server" CssClass="form-control" />
            </td>
            <td>
                <asp:RequiredFieldValidator ID="courseNameValidator1" runat="server" ControlToValidate="courseName"
                        CssClass="error" Text="Name is required." />
            </td>
        </tr>
    </table>
    <div>
        <asp:Button runat="server" CssClass="btn btn-primary" ID="btnSubmit" OnClick="btnSubmit_Click" Text="Submit Course Information" />
    </div>
    <div>
        <h3>The following courses are currently in the system:</h3>
    </div>
    <asp:Table runat="server" ID="tblCourses" CssClass="table">
        <asp:TableRow>
            <asp:TableHeaderCell><a href="AddCourse.aspx?sort=number">Number</a></asp:TableHeaderCell>
            <asp:TableHeaderCell><a href="AddCourse.aspx?sort=name">Name</a></asp:TableHeaderCell>
        </asp:TableRow>
    </asp:Table>
    
</asp:Content>


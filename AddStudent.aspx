<%@ Page Title="" Language="C#" MasterPageFile="~/ACMasterPage.master" AutoEventWireup="true" CodeFile="AddStudent.aspx.cs" Inherits="Addstudents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="App_Themes/SiteStyles.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
            <table>
                <tr>
                    <td>
                        <label>Course</label>
                    </td>
                    <td>
                        <asp:DropDownList ID="drpCourseSelection" runat="server" CssClass="form-control"
                            OnSelectedIndexChanged="drpCourseSelection_SelectedIndexChanged" AutoPostBack="true">
                            <asp:ListItem Value="-1">Select a course ... </asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:RangeValidator id="RangeValidator1"
                            ControlToValidate="drpCourseSelection"
                            MinimumValue="0"
                            MaximumValue="100"
                            Type="Double"
                            EnableClientScript="false"
                            Text="The course must be selected."
                            runat="server"
                            CssClass="error" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Student ID</label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="studentID" CssClass="form-control" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="studentIDValidator" runat="server" ControlToValidate="studentID"
                                CssClass="error" Text="Student ID is required." />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Student Name</label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="studentName" CssClass="form-control" />
                    </td>
                    <td>
                        <asp:RegularExpressionValidator
                            ID="studentNameValidator"
                            ValidationExpression="[a-zA-Z]+\s+[a-zA-Z]+"
                            ControlToValidate="studentName"
                            CssClass="error"
                            Display="Dynamic"
                            ErrorMessage="Must be in first_name last_name!"
                            runat="server"/>
                        <asp:RequiredFieldValidator ID="studentNameValidator2" runat="server" ControlToValidate="studentName"
                                CssClass="error" Text="Student name is required." />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Grade (0-100)</label>
                    </td>
                    <td>
                        <asp:TextBox runat="server" ID="studentGrade" CssClass="form-control" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="studentGradeValidator" runat="server" ControlToValidate="studentGrade"
                                CssClass="error" Text="Student grade is required." />
                        <asp:RangeValidator MinimumValue="0" MaximumValue="100" Type="Double" CssClass="error" ID="rangeValidator" ControlToValidate="studentGrade"
                            runat="server" Text="Grade must be between 0 to 100." />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:Button runat="server" Text="Add to Course" OnClick="btnSubmit_Click" CssClass="btn btn-primary" />
        </div><br />
        <div>
            Following student records have been added:
        </div><br />
        <div>
            <asp:Table runat="server" ID="tblStudents" CssClass="table">
                <asp:TableRow>
                    <asp:TableHeaderCell><a href="AddStudent.aspx?sort=id">ID</a></asp:TableHeaderCell>
                    <asp:TableHeaderCell><a href="AddStudent.aspx?sort=name">Name</a></asp:TableHeaderCell>    
                    <asp:TableHeaderCell><a href="AddStudent.aspx?sort=grade">Grade</a></asp:TableHeaderCell> 
                </asp:TableRow>
            </asp:Table>
        </div>
</asp:Content>


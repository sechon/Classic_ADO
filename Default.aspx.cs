using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class _Default : System.Web.UI.Page
{
    AuthorsAndBooks gs = new AuthorsAndBooks();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            FillDropDownList();
    }
    protected void AuthorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillGridView();
    }

    protected void FillDropDownList()
    {
        DataTable table = null; ;
        try
        {
            table = gs.GetAuthors();
        }
        catch (Exception ex)
        {
            ErrorLabel.Text = ex.Message;
        }
        AuthorDropDownList.DataSource = table;
        AuthorDropDownList.DataTextField = "AuthorName";
        AuthorDropDownList.DataValueField = "AuthorKey";
        AuthorDropDownList.DataBind();
    }

    protected void FillGridView()
    {
        int authorKey = int.Parse(AuthorDropDownList.SelectedValue.ToString());
        DataTable tbl = null;
        try
        {
            tbl = gs.GetAuthorBooks(authorKey);

        }
        catch (Exception ex)
        {
            ErrorLabel.Text = ex.Message;
        }
        BooksGridView.DataSource = tbl;
        BooksGridView.DataBind();
    }
}
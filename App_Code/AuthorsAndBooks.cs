using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
/// <summary>
/// Summary description for AuthorsAndBooks
/// </summary>
public class AuthorsAndBooks
{
    SqlConnection connect;
    public AuthorsAndBooks()
    {
        connect = new SqlConnection(ConfigurationManager.
            ConnectionStrings["BookReviewDBConnectionString"].ToString());
    }

    public DataTable GetAuthors()
    {
        string sql = "Select AuthorKey, AuthorName from Author";
        DataTable tbl;
        SqlCommand cmd = new SqlCommand(sql, connect);
        try
        {
            tbl = ProcessQuery(cmd);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return tbl;

    }
    public DataTable GetAuthorBooks(int authorKey)
    {
        string sql = "SELECT * FROM Book INNER JOIN AuthorBook ON Book.BookKey = AuthorBook.BookKey WHERE AuthorKey = @AuthorKey";
        SqlCommand cmd = new SqlCommand(sql, connect);
        cmd.Parameters.Add("@AuthorKey", authorKey);

        DataTable tbl;
        try
        {
            tbl = ProcessQuery(cmd);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return tbl;

    }

    private DataTable ProcessQuery(SqlCommand cmd)
    {
        DataTable table = new DataTable();
        SqlDataReader reader;
        try
        {
            connect.Open();
            reader = cmd.ExecuteReader();
            table.Load(reader);
            connect.Close();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return table;
    }
}
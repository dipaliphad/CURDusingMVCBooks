using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace CURDusingMVCBooks.DAL
{
    public class BooksDAL
    {
        private readonly IConfiguration configuration;
        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader dr;

    public BooksDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string str = this.configuration.GetConnectionString("defaultConnection");
            con = new SqlConnection(str);
        }
        public List<books>GetAllBooks()
        {
            List<books> books = new List<books>();
            cmd=new SqlCommand("select * from books",con);
            con.Open();
            dr = cmd.ExecuteReader();
            if(dr.HasRows)
            {
                while(dr.Read())
                {
                    books books1 = new books();
                    books1.Id = Convert.ToInt32(dr["id"]);
                    books1.Name = Convert.ToString(dr["Publisher_Name"]);
                    books1.booksPrice = Convert.ToDecimal(dr["booksPrice"]);
                    books.Add(books1);
                }
            }
            con.Close();
            return books;
        }
        public books GetBooksById(int id)
        {
            books books1=new books();
            string qry = "select * from books where id=@id";
            cmd=new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@id",id);
            con.Open(); 
            dr=cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while(dr.Read())
                {
                    books1.Id=Convert.ToInt32(dr["id"]);
                    books1.Publisher_Name = dr["Publisher_Name"].ToString();
                    books1.booksPrice = Convert.ToDecimal(dr["booksPrice"]);
                }
            }
            con.Close();
            return books1;
        }
        public int Addbooks(books books1)
        {
            string qry = "insert into books values(@Publisher_Name,@booksPrice)";
            cmd = new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@Publisher_Name", books1.Publisher_Name);
            cmd.Parameters.AddWithValue("@booksPrice", books1.booksPrice);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int Updatebooks(books books1)
        {
            string qry = "update books set name=@Publisher_Name,@booksPrice";
            cmd = new SqlCommand(qry, con);
            cmd.Parameters.AddWithValue("@Publisher_Name", books1.Publisher_Name);
            cmd.Parameters.AddWithValue("@booksPrice", books1.booksPrice);
            cmd.Parameters.AddWithValue("@id", books1.Id);
            con.Open();
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
        public int Deletebooks(int id)
        {
            string qry = "delete from books where id=@id";
            cmd= new SqlCommand(qry,con);
            cmd.Parameters.AddWithValue("@id",id);
            con.Open();
            int result=cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}

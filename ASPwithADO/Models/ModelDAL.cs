using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace ASPwithADO.Models
{
    public class ModelDAL
    {
      
        string strConnect = "server=.;database=SchoolDB;Trusted_Connection=true";

        public IEnumerable<Student> GetStudents()
        {
            List<Student> listStudent = new List<Student>();
            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                // Dung cau truy van
                //string sqlSelect = "Select * From Student";
                //SqlCommand sqlCommand = new SqlCommand(sqlSelect, sqlConnection);

                //Dung Store Procedure
                SqlCommand sqlCommand = new SqlCommand("ViewStudents", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                SqlDataReader rd = sqlCommand.ExecuteReader();

                while (rd.Read())
                {
                    Student student = new Student
                    {
                        Id = int.Parse(rd["Id"].ToString()),
                        Name = rd["Name"].ToString(),
                        Address = rd["Address"].ToString(),
                        Gender = rd["Gender"].ToString(),
                        Birthday =rd["Birthday"].ToString()
                    };

                    listStudent.Add(student);
                }
                sqlConnection.Close();
            }
            return listStudent;
        }


        public void PostStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(strConnect))
            {
                // Dung cau truy van
                //string sqlInsert = "INSERT Student VALUES(@Name,@Address,@Gender,@Birthday)";
                //SqlCommand sqlCommand = new SqlCommand(sqlInsert, connection);

                //Dung Store Procedure
                SqlCommand sqlCommand = new SqlCommand("AddStudent", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                sqlCommand.Parameters.AddWithValue("@Address", student.Address);
                sqlCommand.Parameters.AddWithValue("@Gender", student.Gender);
                sqlCommand.Parameters.AddWithValue("@Birthday", student.Birthday);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }

        }

        public void Delete(int id)
        {
            try
            { // Test Github
                SqlConnection connection = new SqlConnection(strConnect);

                //string sqlDelete = "DELETE from Student WHERE Id=@studId";
                //SqlCommand sqlCommand = new SqlCommand(sqlDelete, connection);

                SqlCommand sqlCommand = new SqlCommand("DeleteStudent", connection);
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@studId", id);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();



            }
            catch (Exception)
            {

                throw;

            }
        }


        public void UpdateStudent(Student student)
        {
            using (SqlConnection connection = new SqlConnection(strConnect))
            {
                string sqlUpdate = "UPDATE Student SET Name = @Name,Address = @Address,Gender = @Gender,Birthday = @Birthday WHERE Id =@Id";
                SqlCommand sqlCommand = new SqlCommand(sqlUpdate, connection);

                //SqlCommand sqlCommand = new SqlCommand("AddStudent", connection);
                //sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@Id", student.Id);
                sqlCommand.Parameters.AddWithValue("@Name", student.Name);
                sqlCommand.Parameters.AddWithValue("@Address", student.Address);
                sqlCommand.Parameters.AddWithValue("@Gender", student.Gender);
                sqlCommand.Parameters.AddWithValue("@Birthday", student.Birthday);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }

        }

       

         public Student FindId(int id)  
        {
            Student student = new Student();
            using (SqlConnection con = new SqlConnection(strConnect))  
            {  
                string sqlQuery = "SELECT * FROM Student WHERE Id=@Id ";  
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();  
                SqlDataReader rdr = cmd.ExecuteReader();  
                while (rdr.Read())  
                {
                    
                    {
                        student.Id = int.Parse(rdr["Id"].ToString());
                        student.Name = rdr["Name"].ToString();
                        student.Address = rdr["Address"].ToString();
                        student.Gender = rdr["Gender"].ToString();
                        student.Birthday = rdr["Birthday"].ToString();
                    }

                  
                }

                con.Close();
            }

            return student;
              
        }

        //Version có nullable int ? 
        //public Student FindId(int? id)
        //{
        //    Student student = new Student();
        //    using (SqlConnection con = new SqlConnection(strConnect))
        //    {
        //        string sqlQuery = "SELECT * FROM Student WHERE Id=@Id ";
        //        SqlCommand cmd = new SqlCommand(sqlQuery, con);
        //        cmd.Parameters.AddWithValue("@Id", id);
        //        con.Open();
        //        SqlDataReader rdr = cmd.ExecuteReader();
        //        while (rdr.Read())
        //        {

        //            {
        //                student.Id = int.Parse(rdr["Id"].ToString());
        //                student.Name = rdr["Name"].ToString();
        //                student.Address = rdr["Address"].ToString();
        //                student.Gender = rdr["Gender"].ToString();
        //                student.Birthday = rdr["Birthday"].ToString();
        //            }


        //        }

        //        con.Close();
        //    }

        //    return student;

        //}





        //public void findAll(TextBox textBox, DataGridView dataGridView)
        //{
        //    setConnect();
        //    string sql = "select * from tbAccount where AccountName like'%" + textBox.Text + "%'";
        //    dataAdapter = new SqlDataAdapter(sql, sqlConnection);
        //    dataSet = new DataSet();
        //    dataAdapter.Fill(dataSet, "tbAccount");

        //    dataGridView.DataSource = dataSet.Tables["tbAccount"];
        //}

    }
}

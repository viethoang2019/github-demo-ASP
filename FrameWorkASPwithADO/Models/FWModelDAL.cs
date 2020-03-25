using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace FrameWorkASPwithADO.Models
{
    public class FWModelDAL
    {
        string strConnect = "server=.;database=SchoolDB;Trusted_Connection=true";

        public IEnumerable<FWStudent> GetStudents()
        {
            List<FWStudent> listStudent = new List<FWStudent>();
            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                // Dung cau truy van
                string sqlSelect = "Select * From Student";
                SqlCommand sqlCommand = new SqlCommand(sqlSelect, sqlConnection);

                //Dung Store Procedure
                //SqlCommand sqlCommand = new SqlCommand("ViewStudents", sqlConnection);
                // sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                SqlDataReader rd = sqlCommand.ExecuteReader();

                while (rd.Read())
                {
                    FWStudent student = new FWStudent
                    {
                        Id = int.Parse(rd["Id"].ToString()),
                        Name = rd["Name"].ToString(),
                        Address = rd["Address"].ToString(),
                        Gender = rd["Gender"].ToString(),
                        Birthday = rd["Birthday"].ToString()
                    };

                    listStudent.Add(student);
                }
                sqlConnection.Close();
            }
            return listStudent;
        }



        public void PostStudent(FWStudent student)
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


    }
}
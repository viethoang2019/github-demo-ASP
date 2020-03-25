using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


using System.Data;
using System.Data.SqlClient;

namespace FrameWorkASPwithADO_Photo.Models
{
    public class FWModelDAO
    {
        string strConnect = "server=.;database=VNTeamDB;Trusted_Connection=true";

        public IEnumerable<FWPlayer> GetPlayers()
        {
            List<FWPlayer> listPlayer = new List<FWPlayer>();
            using (SqlConnection sqlConnection = new SqlConnection(strConnect))
            {
                // Dung cau truy van
                string sqlSelect = "Select * From tbPlayer";
                SqlCommand sqlCommand = new SqlCommand(sqlSelect, sqlConnection);

                //Dung Store Procedure
                //SqlCommand sqlCommand = new SqlCommand("ViewStudents", sqlConnection);
                //sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlConnection.Open();
                SqlDataReader rd = sqlCommand.ExecuteReader();

                while (rd.Read())
                {
                    FWPlayer player = new FWPlayer
                    {
                        Id = int.Parse(rd["Id"].ToString()),
                        Name = rd["Name"].ToString(),
                        DateOfBirth = Convert.ToDateTime(rd["DateOfBirth"].ToString()),
                        Age = int.Parse(rd["Age"].ToString()),
                        Position = rd["Position"].ToString(),
                        Photo = rd["Photo"].ToString()
                    };

                    listPlayer.Add(player);
                }
                sqlConnection.Close();
            }
            return listPlayer;
        }

        public void PostPlayer(FWPlayer player)
        {
            using (SqlConnection connection = new SqlConnection(strConnect))
            {
                // Dung cau truy van
                string sqlInsert = "INSERT tbPlayer VALUES(@Name,@DateOfBirth,@Age,@Position,@Photo)";
                SqlCommand sqlCommand = new SqlCommand(sqlInsert, connection);

                //Dung Store Procedure
                //SqlCommand sqlCommand = new SqlCommand("AddStudent", connection);
                //sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@Name", player.Name);
                sqlCommand.Parameters.AddWithValue("@DateOfBirth", player.DateOfBirth);
                sqlCommand.Parameters.AddWithValue("@Age", player.Age);
                sqlCommand.Parameters.AddWithValue("@Position", player.Position);
                sqlCommand.Parameters.AddWithValue("@Photo", player.Photo);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();
            }

        }

    }
}
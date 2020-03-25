using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;


namespace ASPwithADO_Photo.Models
{
    public class ModelDAO
    {

        string strConnect = "server=.;database=VNTeamDB;Trusted_Connection=true";

        public IEnumerable<Player> GetPlayers()
        {
            List<Player> listPlayer = new List<Player>();
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
                    Player player = new Player
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


        public void PostPlayer(Player player)
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


        public void Delete(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(strConnect);

                string sqlDelete = "DELETE from tbPlayer WHERE Id=@playerId";
                SqlCommand sqlCommand = new SqlCommand(sqlDelete, connection);

                //SqlCommand sqlCommand = new SqlCommand("DeleteStudent", connection);
                //sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@playerId", id);

                connection.Open();
                sqlCommand.ExecuteNonQuery();
                connection.Close();



            }
            catch (Exception)
            {

                throw;

            }
        }


        public void EditPlayer(Player player)
        {
            using (SqlConnection connection = new SqlConnection(strConnect))
            {
                // Dung cau truy van
                string sqlInsert = "UPDATE tbPlayer SET Name = @Name,DateOfBirth = @DateOfBirth,Age = @Age,Position = @Position,Photo = @Photo WHERE Id =@Id";
                SqlCommand sqlCommand = new SqlCommand(sqlInsert, connection);

                //Dung Store Procedure
                //SqlCommand sqlCommand = new SqlCommand("AddStudent", connection);
                //sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.AddWithValue("@Id", player.Id);
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



        public Player FindId(int id)
        {
            Player player = new Player();
            using (SqlConnection con = new SqlConnection(strConnect))
            {
                string sqlQuery = "SELECT * FROM tbPlayer WHERE Id=@Id ";
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                cmd.Parameters.AddWithValue("@Id", id);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                    {
                        player.Id = int.Parse(rdr["Id"].ToString());
                        player.Name = rdr["Name"].ToString();
                        player.DateOfBirth = Convert.ToDateTime(rdr["DateOfBirth"].ToString());
                        player.Age = int.Parse(rdr["Age"].ToString());
                        player.Position = rdr["Position"].ToString();
                        player.Photo = rdr["Photo"].ToString();

              
                    }


                }

                con.Close();
            }

            return player;

        }

        public Player SearchByName(string name)
        {
            Player player = new Player();

            using (SqlConnection connection = new SqlConnection(strConnect))
            {
                string sql = "select * from tbPlayer where Name like'%" + name + "%'";
                SqlCommand cmd = new SqlCommand(sql, connection);

                cmd.Parameters.AddWithValue("name", name);
                connection.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {

                        player.Id = int.Parse(rdr["Id"].ToString());
                        player.Name = rdr["Name"].ToString();
                        player.DateOfBirth = Convert.ToDateTime(rdr["DateOfBirth"].ToString());
                        player.Age = int.Parse(rdr["Age"].ToString());
                        player.Position = rdr["Position"].ToString();
                        player.Photo = rdr["Photo"].ToString();


             
                }

                connection.Close();
            }

            return player;
        }



    }
}

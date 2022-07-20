using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transparent_Form
{
    class ScoreClass
    {
        DBconnect connect = new DBconnect();
        //create a function add score
        public bool insertScore(int stdid, string courName, double scor, string desc)
        {
            MySqlCommand command = new MySqlCommand("INSERT INTO `score`(`StudentId`, `CourseName`, `Score`, `Description`) VALUES (@stid,@cn,@sco,@desc)", connect.getconnection);
            //@stid,@cn,@sco,@desc
            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@cn", MySqlDbType.VarChar).Value = courName;
            command.Parameters.Add("@sco", MySqlDbType.Double).Value = scor;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        //create a functon to get list
        public DataTable getList(MySqlCommand command)
        {
            command.Connection = connect.getconnection;
            MySqlDataAdapter adapter = new MySqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        // create a function to check already course score
        public bool checkScore(int stdId, string cName)
        {
            DataTable table = getList(new MySqlCommand("SELECT * FROM `score` WHERE `StudentId`= '" + stdId + "' AND `CourseName`= '" + cName + "'"));
            if (table.Rows.Count > 0)
            { return true; }
            else
            { return false; }
        }
        // Create A function to edit score data
        public bool updateScore(int stdid,string scn, double scor, string desc)
        {
            MySqlCommand command = new MySqlCommand("UPDATE `score` SET `Score`=@sco,`Description`=@desc WHERE `StudentId`=@stid AND `CourseName`=@scn", connect.getconnection);
            //@stid,@sco,@desc
            command.Parameters.Add("@scn", MySqlDbType.VarChar).Value = scn;
            command.Parameters.Add("@stid", MySqlDbType.Int32).Value = stdid;
            command.Parameters.Add("@sco", MySqlDbType.Double).Value = scor;
            command.Parameters.Add("@desc", MySqlDbType.VarChar).Value = desc;
            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
        //Create a function to delete a score data
        public bool deleteScore(int id)
        {
            MySqlCommand command = new MySqlCommand("DELETE FROM `score` WHERE `StudentId`=@id", connect.getconnection);

            //@id
            command.Parameters.Add("@id", MySqlDbType.Int32).Value = id;

            connect.openConnect();
            if (command.ExecuteNonQuery() == 1)
            {
                connect.closeConnect();
                return true;
            }
            else
            {
                connect.closeConnect();
                return false;
            }
        }
    }
}

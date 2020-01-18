using System.Collections;
using System.Data;
using Mono.Data.Sqlite;
using UnityEngine;

public class SQLiteTest : MonoBehaviour {
    private string dbPath;
    

    private void Start()
    {
        dbPath = "URI=file:" + Application.dataPath + "/database.db";
        CreateDatabase();
    }
    /// <summary>
    /// Creates a Database if it doesn't exist. Table has level number, Beer score number and time score number.
    /// </summary>
    private void CreateDatabase()
    {
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("CREATE TABLE IF NOT EXISTS 'topScores' ( `level` INTEGER NOT NULL, `beerScore` INTEGER NOT NULL, `timeScore` INTEGER NOT NULL, `ID` INTEGER NOT NULL, PRIMARY KEY(`ID`))");
                cmd.ExecuteScalar();

            }
            conn.Close();
        }

    }
    /// <summary>
    /// Use this method to insert level scores into the database
    /// </summary>
    /// <param name="level"></param>
    /// <param name="beerScore"></param>
    /// <param name="timeScore"></param>
    public void InsertScore(int level, int beerScore, int timeScore)
    {
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {


                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = ("INSERT INTO 'topScores' (level, beerScore, timeScore) VALUES (" + level + "," + beerScore +","+timeScore+ ");");
                    cmd.ExecuteScalar();
                    conn.Close();
                
            }
        }
    }
    public void EmptyTable ()
    {

        dbPath = "URI=file:" + Application.dataPath + "/database.db";
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {


                cmd.CommandType = CommandType.Text;
                cmd.CommandText = ("DELETE FROM topScores;");
                cmd.ExecuteScalar();
                conn.Close();

            }
        }
    }
    /// <summary>
    /// A internal method that is used to collect beerScore data that is returned in an other method
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private int GetBeerScore(int level)
    {
        dbPath = "URI=file:" + Application.dataPath + "/database.db";

        bool found=false;
        using (var conn = new SqliteConnection(dbPath))
        {
            conn.Open();
            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT beerScore FROM topScores WHERE level = "+level+" ORDER BY beerScore DESC;";
                cmd.ExecuteScalar();

                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if (found == false)
                    {
                        var beerScore = reader.GetInt32(0);
                        found = true;
                        return beerScore;
                    }
     
                    conn.Close();
                    reader.Close();
                    break;   
                }
                return 0;
            }
            
        }
        
    }
    /// <summary>
    /// A internal method that is used to collect timeScore data that is returned in an other method
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private int GetTimeScore(int level)
        {
        dbPath = "URI=file:" + Application.dataPath + "/database.db";
        bool found = false;
        using (var conn = new SqliteConnection(dbPath))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT timeScore FROM topScores WHERE level = "+level+" ORDER BY timeScore ASC;";
                    cmd.ExecuteScalar();
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        if (found == false)
                        {
                            int timeScore = reader.GetInt32(0);
                            found = true;
                            return timeScore;
                        }
                    conn.Close();
                    reader.Close();
                    break;
                    }
                return 0;
                }

            }
        }
    /// <summary>
    /// Method that is used to collect a string that shows time and beer score from the level
    /// </summary>
    /// <param name="searchLevel"></param>
    /// <returns></returns>
    public string GetHighScoreOfLevel(int searchLevel)
    {
        string result;
        var a = GetBeerScore(searchLevel);
        var b = GetTimeScore(searchLevel);
        result = "Time: " + b + ", Beer: " + a;
        return result;
    }
    /// <summary>
    /// Method that will be used to collect all the the beer data into a single return statement. Planned use for the story
    /// </summary>
    /// <returns></returns>
    public int GetTotalBeer()
    {
        int b = GetBeerScore(1) + GetBeerScore(2) + GetBeerScore(3)+GetBeerScore(4);

        return b;
       
    }

}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SouthEastCon2017
{
    public class peopleData
    {
        //.
        /// <summary>
        /// This function retrieves all of the people in the database that have 
        /// favorite things.
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns>a simpledata object containing the peoples' names</returns>
        public simpledata getPeople(string connectionString)
        {
            simpledata peopleInfo = new simpledata();
            int intTotItems;
            int i;
            DataRow dRow;
            SqlConnection con;
            SqlDataAdapter adapter;
            DataSet ds1;
            con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = connectionString;
            SqlCommand command = new SqlCommand("dbo.spGetPeople", con);
            command.CommandType = CommandType.StoredProcedure;
            ds1 = new DataSet();
            con.Open();
            adapter = new System.Data.SqlClient.SqlDataAdapter(command);
            try
            {
                adapter.Fill(ds1, "BLInfo");
            }
            catch (Exception e)
            {
                var msg = e.Message;
            }
            con.Close();
            intTotItems = ds1.Tables["BLInfo"].Rows.Count;
            peopleInfo.intPersonID = new int[intTotItems];
            peopleInfo.strFirstNames = new string[intTotItems];
            peopleInfo.strLastNames = new string[intTotItems];
            for (i = 0; i < intTotItems; i++)
            {
                dRow = ds1.Tables["BLInfo"].Rows[i];
                peopleInfo.intPersonID[i] = int.Parse(dRow.ItemArray.GetValue(0).ToString());
                peopleInfo.strLastNames[i] = dRow.ItemArray.GetValue(1).ToString();
                peopleInfo.strFirstNames[i] = dRow.ItemArray.GetValue(2).ToString();
            }
            return (peopleInfo);
        }

        /// <summary>
        /// This function gets a list of favorites of the person that has been selected.
        /// </summary>
        /// <param name="connectionString">connection string to the database</param>
        /// <param name="userID">User id of the person selected</param>
        /// <returns>favorites object containing the favorite items of the selected person</returns>
        public favorites getFavoritesData(string connectionString, string userID)
        {
            favorites favoriteInfo = new favorites();
            int intTotItems;
            int i;
            DataRow dRow;
            SqlConnection con;
            SqlDataAdapter adapter;
            DataSet ds1;
            con = new System.Data.SqlClient.SqlConnection();
            con.ConnectionString = connectionString;
            SqlCommand command = new SqlCommand("dbo.spGetFavorites", con);
            command.Parameters.AddWithValue("@PersonId", userID);
            command.CommandType = CommandType.StoredProcedure;
            ds1 = new DataSet();
            con.Open();
            adapter = new System.Data.SqlClient.SqlDataAdapter(command);
            try
            {
                adapter.Fill(ds1, "BLInfo");
            }
            catch (Exception e)
            {
                var msg = e.Message;
            }

            con.Close();
            intTotItems = ds1.Tables["BLInfo"].Rows.Count;
            int intCurFavID;
            string strCurFavorite;
            for (i = 0; i < intTotItems; i++)
            {
                dRow = ds1.Tables["BLInfo"].Rows[i];
                favoriteInfo.intPersonID = int.Parse(dRow.ItemArray.GetValue(0).ToString());
                intCurFavID = int.Parse(dRow.ItemArray.GetValue(1).ToString());
                strCurFavorite = dRow.ItemArray.GetValue(2).ToString();
                switch (intCurFavID)
                {
                    case 1: favoriteInfo.strFavMovie = strCurFavorite; break;
                    case 2: favoriteInfo.strFavBand = strCurFavorite; break;
                    case 3: favoriteInfo.strFavFood = strCurFavorite; break;
                    case 4: favoriteInfo.strFavPet = strCurFavorite; break;
                }
            }
            return (favoriteInfo);
            
        }
    }
}
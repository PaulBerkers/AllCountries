using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AllCountries.Classes
{
    public class DbConnection
    {
        SqlConnection conn = new SqlConnection(@"Data Source=(localdb)\mssqllocaldb; 
                                                Initial Catalog=Countries;Integrated Security=True");

        public DataView GetAllCountries()
        {
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select * from tblLanden";

            SqlDataReader reader = command.ExecuteReader();

            DataTable dtData = new DataTable();
            dtData.Load(reader);

            conn.Close();

            return dtData.DefaultView;
        }

        public string GetCountryById(string code)
        {
            string sCountry = "";
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select * from tblLanden where code = @code";
            command.Parameters.AddWithValue("@code", code);
            SqlDataReader reader = command.ExecuteReader();

            DataTable dtData = new DataTable();
            dtData.Load(reader);

            conn.Close();
            try
            {
                DataRow row = dtData.Rows[0];
                sCountry = row["vlag"].ToString();
            }
            catch (Exception)
            {
                Exception e = new Exception();
                return e.ToString();
            }

            return sCountry;
        }

        public List<Country> ListCountries()
        {
            List<Country> retValue = new List<Country>();
            conn.Open();

            SqlCommand command = conn.CreateCommand();
            command.CommandText = "select * from tblLanden";

            SqlDataReader reader = command.ExecuteReader();

            DataTable dtData = new DataTable();
            dtData.Load(reader);

            conn.Close();

            foreach (DataRow row in dtData.Rows)
            {
                Country country = new Country();
                country.Code = row["code"].ToString();
                country.Omschrijving = row["omschrijving"].ToString();
                if (row["vlag"] != DBNull.Value)
                {
                    byte[] imageArr = (byte[])row["vlag"];
                    country.ByteArray = imageArr;
                }

                retValue.Add(country);
            }

            return retValue;
        }

        public byte[] GetImageByteArray(string sCountry) 
        {
            byte[] imageArr = null;
            try
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandText = "select vlag from tblLanden where omschrijving = @country";
                command.Parameters.AddWithValue("@country", sCountry);
                imageArr = (byte[])command.ExecuteScalar();
            }
            catch (Exception)
            {
                //er wordt geen foutmelding afgehandeld
            }

            conn.Close();
            return imageArr;
        }
    }
}


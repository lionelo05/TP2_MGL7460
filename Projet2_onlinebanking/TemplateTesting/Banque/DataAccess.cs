using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace TemplateTesting.Banque
{
    public class DataAccess
    {
        public string ConnectionString { get; set; }
        SqlConnection m_sqlConn = null;


        public DataAccess()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        }

        public bool Connect()
        {
            m_sqlConn = new SqlConnection(ConnectionString);
            m_sqlConn.Open();

            return m_sqlConn.State == System.Data.ConnectionState.Open;
        }

        public bool Disconnect()
        {
            m_sqlConn = new SqlConnection(ConnectionString);
            m_sqlConn.Close();

            return m_sqlConn.State == System.Data.ConnectionState.Closed;
        }

        public SqlCommand GetSqlCommand(string storedProcedure)
        {
            SqlCommand sqlCmd = new SqlCommand(storedProcedure, m_sqlConn);
            sqlCmd.CommandType = System.Data.CommandType.StoredProcedure;
            return sqlCmd;
        }

        public DataTable ExecuteSqlCommand(SqlCommand response)
        {
            SqlDataAdapter sqlDa = new SqlDataAdapter(response);
            DataTable result = new DataTable();
            sqlDa.Fill(result);
            return result;
        }

        public bool InsertCustomer(Customer customer)
        {
            string spoName = "InsertCustomer";
            try
            {
                SqlCommand response = GetSqlCommand(spoName);
                response.Parameters.Add("Name", SqlDbType.NVarChar).Value = customer.Name;
                response.Parameters.Add("Email", SqlDbType.NVarChar).Value = customer.Email;

                int rows = response.ExecuteNonQuery();

                if (rows > 0)
                    return true;
                else
                    return false;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public Customer GetCustomer(string name)
        {
            string spoName = "GetCustomer";

            try
            {
                SqlCommand sqlCmd = GetSqlCommand(spoName);
                sqlCmd.Parameters.Add("Name", SqlDbType.NVarChar).Value = name;
                SqlDataReader sqlData = sqlCmd.ExecuteReader();

                if (!sqlData.HasRows)
                    return null;

                Customer c = new Customer();

                while (sqlData.Read())
                {
                    c.Name = sqlData.GetString(0);
                    c.Email = sqlData.GetString(1);
                }

                sqlData.Close();

                return c;
            }
            catch (SqlException)
            {
                return null;
            }
        }

    }
}

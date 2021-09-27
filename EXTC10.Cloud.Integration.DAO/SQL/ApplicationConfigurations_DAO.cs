using EXTC10.Cloud.Integration.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EXTC10.Cloud.Integration.DAO.SQL
{
    public class ApplicationConfigurations_DAO
    {
        public string DatabaseConnectionString { get; set; }
        public async Task<List<ApplicationConfiguration>> GetAllConfigurationKeysAndValues()
        {
            List<ApplicationConfiguration> applicationConfigurationsList = new List<ApplicationConfiguration>();
            SqlDataReader sqlDataReader=null;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            try
            {

                sqlConnection = new SqlConnection(DatabaseConnectionString);
                sqlCommand = new SqlCommand();
                
                sqlCommand.CommandText = SQLConstant.GETALLCONFIGURATIONKEYSANDVALUES;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;
               
                
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    sqlConnection.Open();

                sqlDataReader = await sqlCommand.ExecuteReaderAsync();


                //Test this file
                while (await sqlDataReader.ReadAsync())
                {
                    ApplicationConfiguration applicationConfiguration = new ApplicationConfiguration();

                    /*if (sqlDataReader["Config_Key"] != DBNull.Value)
                        applicationConfiguration.ConfigKey = Convert.ToString(sqlDataReader["Config_Key"]);
                    else
                        applicationConfiguration.ConfigKey = "";*/


                    applicationConfiguration.ConfigKey = sqlDataReader["Config_Key"] != DBNull.Value ? Convert.ToString(sqlDataReader["Config_Key"]) : "";
                    applicationConfiguration.ConfigValue = sqlDataReader["Config_Value"] != DBNull.Value ? Convert.ToString(sqlDataReader["Config_Value"]) : "";
                    applicationConfiguration.CreatedBy = sqlDataReader["Created_By"] != DBNull.Value ? Convert.ToString(sqlDataReader["Created_By"]) : "";
                    applicationConfiguration.CreatedDate = sqlDataReader["Created_Date"] != DBNull.Value ? Convert.ToDateTime(sqlDataReader["Created_By"]) : DateTime.MinValue;

                    applicationConfiguration.UpdatedBy = sqlDataReader["Updated_By"] != DBNull.Value ? Convert.ToString(sqlDataReader["Updated_By"]) : "";
                    applicationConfiguration.UpdatedDate = sqlDataReader["Updated_Date"] != DBNull.Value ? Convert.ToDateTime(sqlDataReader["Updated_Date"]) : DateTime.MinValue;
                    applicationConfigurationsList.Add(applicationConfiguration);


                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if(sqlDataReader!=null && !sqlDataReader.IsClosed)
                    await sqlDataReader.CloseAsync();

                if (sqlCommand != null)
                   await sqlCommand.DisposeAsync();

                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Open)
                    await sqlConnection.CloseAsync();
            }

            return applicationConfigurationsList;
        }
    }
}

﻿using EXTC10.Cloud.Integration.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace EXTC10.Cloud.Integration.DAO.SQL
{
    /// <summary>
    /// The application configurations data access objects.
    /// </summary>
    public class ApplicationConfigurations_DAO
    {
        /// <summary>
        /// Gets or sets the database connection string.
        /// </summary>
        public string DatabaseConnectionString { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationConfigurations_DAO"/> class.
        /// </summary>
        /// <param name="databaseConnectionString">The database connection string.</param>
        public ApplicationConfigurations_DAO(string databaseConnectionString)
        {
            DatabaseConnectionString = databaseConnectionString;
        }


        /// <summary>
        /// Gets the all configuration keys and values.
        /// </summary>
        /// <returns>A Task<List<ApplicationConfiguration>></returns>
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

        /// <summary>
        /// Gets the configuration value by config key.
        /// </summary>
        /// <param name="configKey">The config key.</param>
        /// <returns>A Task<ApplicationConfiguration>.</returns>
        public async Task<ApplicationConfiguration> GetConfigurationValueByConfigKey(string configKey)
        {
            ApplicationConfiguration applicationConfiguration = new ApplicationConfiguration();
            SqlDataReader sqlDataReader = null;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            try
            {
               // @ConfigKey
                sqlConnection = new SqlConnection(DatabaseConnectionString);
                sqlCommand = new SqlCommand();

                sqlCommand.CommandText = SQLConstant.GETALLCONFIGURATIONKEYSANDVALUES;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;

                sqlCommand.Parameters.AddWithValue("@ConfigKey", configKey);

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    sqlConnection.Open();

                sqlDataReader = await sqlCommand.ExecuteReaderAsync();


                //Test this file
                while (await sqlDataReader.ReadAsync())
                {
                    

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
                    


                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (sqlDataReader != null && !sqlDataReader.IsClosed)
                    await sqlDataReader.CloseAsync();

                if (sqlCommand != null)
                    await sqlCommand.DisposeAsync();

                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Open)
                    await sqlConnection.CloseAsync();
            }

            return applicationConfiguration;
        }
        //INSERT

        /// <summary>
        /// Adds the new key value in configurations async.
        /// </summary>
        /// <param name="applicationConfiguration">The application configuration.</param>
        /// <returns>A Task<bool>.</returns>
        public async Task<bool> AddNewKeyValueInConfigurationsAsync (ApplicationConfiguration applicationConfiguration)
        {
            SqlConnection sqlConnection=null;
            SqlCommand sqlCommand=null;
            bool returnValue;

            try
            {
                sqlConnection = new SqlConnection(DatabaseConnectionString);

                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = SQLConstant.ADDNEWKEYVALUEINCONFIGURATIONS;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;

                sqlCommand.Parameters.AddWithValue("@ConfigKey", applicationConfiguration.ConfigKey);
                sqlCommand.Parameters.AddWithValue("@ConfigValue", applicationConfiguration.ConfigValue);
                sqlCommand.Parameters.AddWithValue("@CreatedBy", applicationConfiguration.CreatedBy);


                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                   await sqlConnection.OpenAsync();

               await sqlCommand.ExecuteNonQueryAsync();

                returnValue = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (sqlCommand != null)
                    await sqlCommand.DisposeAsync();

                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Open)
                    await sqlConnection.CloseAsync();
            }

            return returnValue;
        }


        //UPADATE
        /// <summary>
        /// Updates the value in configuration by config key async.
        /// </summary>
        /// <param name="applicationConfiguration">The application configuration.</param>
        /// <returns>A Task<bool>.</returns>
        public async Task<bool> UpdateValueInConfigurationByConfigKeyAsync(ApplicationConfiguration applicationConfiguration)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            bool returnValue;

            try
            {
                sqlConnection = new SqlConnection(DatabaseConnectionString);

                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = SQLConstant.UPDATEVALUEINCONFIGURATIONBYCONFIGKEY;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;

                sqlCommand.Parameters.AddWithValue("@ConfigKey", applicationConfiguration.ConfigKey);
                sqlCommand.Parameters.AddWithValue("@ConfigValue", applicationConfiguration.ConfigValue);
                sqlCommand.Parameters.AddWithValue("@UpdatedBy", applicationConfiguration.UpdatedBy);

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    await sqlConnection.OpenAsync();

                await sqlCommand.ExecuteNonQueryAsync();

                returnValue = true;
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                if (sqlCommand != null)
                    await sqlCommand.DisposeAsync();

                if (sqlConnection != null && sqlConnection.State == System.Data.ConnectionState.Open)
                    await sqlConnection.CloseAsync();
            }

            return returnValue;
        }
    }
}

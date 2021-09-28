using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EXTC10.Cloud.Integration.Entities;

namespace EXTC10.Cloud.Integration.DAO.SQL
{
    /// <summary>
    /// The request queue data access object.
    /// </summary>
    public class RequestQueue_DAO
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestQueue_DAO"/> class.
        /// </summary>
        /// <param name="databaseConnectionString">The database connection string.</param>
        public RequestQueue_DAO(string databaseConnectionString)
        {
            DatabaseConnectionString = databaseConnectionString;
        }
        /// <summary>
        /// Gets or sets the database connection string.
        /// </summary>
        public string DatabaseConnectionString { get; set; }

        //INSERT

        /// <summary>
        /// Adds the request queue entry async.
        /// </summary>
        /// <param name="RequestQueue">The request queue.</param>
        /// <returns>A Task.</returns>
        public async Task<bool> AddRequestQueueEntryAsync(RequestQueue RequestQueue)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            bool returnValue;

            try
            {
                sqlConnection = new SqlConnection(DatabaseConnectionString);

                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = SQLConstant.ADDREQUESTQUEUEENTRY;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;

                sqlCommand.Parameters.AddWithValue("@RequestId", RequestQueue.RequestId);
                sqlCommand.Parameters.AddWithValue("@SourceSystem", RequestQueue.SourceSystem);
                sqlCommand.Parameters.AddWithValue("@TargetSystem", RequestQueue.TargetSystem);
                sqlCommand.Parameters.AddWithValue("@RequestedAction", RequestQueue.RequestedAction);
                sqlCommand.Parameters.AddWithValue("@RequestStatus", RequestQueue.RequestStatus);


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
        /// Updates the request queue status async.
        /// </summary>
        /// <param name="RequestQueue">The request queue.</param>
        /// <returns>A Task.</returns>
        public async Task<bool> UpdateRequestQueueStatusAsync(RequestQueue RequestQueue)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            bool returnValue;

            try
            {
                sqlConnection = new SqlConnection(DatabaseConnectionString);

                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = SQLConstant.UPDATEREQUESTQUEUESTATUS;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;

                sqlCommand.Parameters.AddWithValue("@RequestId", RequestQueue.RequestId);
                sqlCommand.Parameters.AddWithValue("@RequestStatus", RequestQueue.SourceSystem);


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

        /// <summary>
        /// Gets the request queue by id.
        /// </summary>
        /// <param name="requestId">The request id.</param>
        /// <returns>A Task.</returns>
        public async Task<RequestQueue> GetRequestQueuebyId(string requestId)
        {
            RequestQueue requestQueue = new RequestQueue();
            SqlDataReader sqlDataReader = null;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            try
            {
                // @ConfigKey
                sqlConnection = new SqlConnection(DatabaseConnectionString);
                sqlCommand = new SqlCommand();

                sqlCommand.CommandText = SQLConstant.GETREQUESTQUEUEBYID;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;

                sqlCommand.Parameters.AddWithValue("@Request_Id", requestId);

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    sqlConnection.Open();

                sqlDataReader = await sqlCommand.ExecuteReaderAsync();


                //Test this file
                while (await sqlDataReader.ReadAsync())
                {
                    requestQueue.RequestId = sqlDataReader["Request_Id"] != DBNull.Value ? Convert.ToString(sqlDataReader["Request_Id"]) : "";
                    requestQueue.SourceSystem = sqlDataReader["Source_System"] != DBNull.Value ? Convert.ToString(sqlDataReader["Source_System"]) : "";
                    requestQueue.TargetSystem = sqlDataReader["Target_System"] != DBNull.Value ? Convert.ToString(sqlDataReader["Target_System"]) : "";
                    requestQueue.RequestedAction = sqlDataReader["Requested_Action"] != DBNull.Value ? Convert.ToString(sqlDataReader["Requested_Action"]) : "";
                    requestQueue.Requesteddate = sqlDataReader["Requested_date"] != DBNull.Value ? Convert.ToDateTime(sqlDataReader["Requested_date"]) : DateTime.MinValue;
                    requestQueue.RequestStatus = sqlDataReader["RequestStatus"] != DBNull.Value ? Convert.ToInt32(sqlDataReader["RequestStatus"]) : 0;
                    requestQueue.CreatedDate = sqlDataReader["Created_Date"] != DBNull.Value ? Convert.ToDateTime(sqlDataReader["Created_By"]) : DateTime.MinValue;
                    requestQueue.UpdatedDate = sqlDataReader["Updated_Date"] != DBNull.Value ? Convert.ToDateTime(sqlDataReader["Updated_Date"]) : DateTime.MinValue;
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

            return requestQueue;
        }
    }
}

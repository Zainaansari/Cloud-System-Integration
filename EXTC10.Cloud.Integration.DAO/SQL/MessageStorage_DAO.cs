using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Threading.Tasks;
using EXTC10.Cloud.Integration.Entities;

namespace EXTC10.Cloud.Integration.DAO.SQL
{
    public class MessageStorage_DAO
    {
        public string DatabaseConnectionString { get; set; }

        public async Task<bool> AddRequestMessageInStoreAsync(MessageStorage MessageStorage)
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            bool returnValue;

            try
            {
                sqlConnection = new SqlConnection(DatabaseConnectionString);

                sqlCommand = new SqlCommand();
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandText = SQLConstant.ADDREQUESTMESSAGEINSTORE;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;

                sqlCommand.Parameters.AddWithValue("@RequestId", MessageStorage.RequestId);
                sqlCommand.Parameters.AddWithValue("@Messagecontent", MessageStorage.MessageContent);
                


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

        public async Task<MessageStorage> GetRequestMessageById(string requestId)
        {
            MessageStorage messageStorage = new MessageStorage();
            SqlDataReader sqlDataReader = null;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            try
            {
                // @ConfigKey
                sqlConnection = new SqlConnection(DatabaseConnectionString);
                sqlCommand = new SqlCommand();

                sqlCommand.CommandText = SQLConstant.GETREQUESTMESSAGEBYID;
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.CommandTimeout = 0;

                sqlCommand.Parameters.AddWithValue("@Request_Id", requestId);

                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                    sqlConnection.Open();

                sqlDataReader = await sqlCommand.ExecuteReaderAsync();


                //Test this file
                while (await sqlDataReader.ReadAsync())
                {
                    messageStorage.MessageContent = sqlDataReader["Message_content"] != DBNull.Value ? Convert.ToString(sqlDataReader["Message_content"]) : "";                    
                   
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

            return messageStorage;
        }

    }
}

using EXTC10.Cloud.Integration.DAO.SQL;
using EXTC10.Cloud.Integration.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EXTC10.Cloud.Integration.Manager
{
    /// <summary>
    /// The configuration.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        /// <param name="databaseConnectionString">The database connection string.</param>
        public Configuration(string databaseConnectionString)
        {
            DatabaseConnectionString = databaseConnectionString;
            applicationConfigurations_DAO = new ApplicationConfigurations_DAO(DatabaseConnectionString);
        }

        /// <summary>
        /// Gets or sets the database connection string.
        /// </summary>
        public string DatabaseConnectionString { get; set; }

        private readonly ApplicationConfigurations_DAO applicationConfigurations_DAO;


        /// <summary>
        /// Gets the all configuration keys and values.
        /// </summary>
        /// <returns>A Task<List<ApplicationConfiguration>>.</returns>
        public async Task<List<ApplicationConfiguration>> GetAllConfigurationKeysAndValues()
        {
            return await applicationConfigurations_DAO.GetAllConfigurationKeysAndValues();
        }

        /// <summary>
        /// Gets the configuration value by config key.
        /// </summary>
        /// <param name="configKey">The config key.</param>
        /// <returns>A Task<ApplicationConfiguration>.</returns>
        public async Task<ApplicationConfiguration> GetConfigurationValueByConfigKey(string configKey)
        {
            return await applicationConfigurations_DAO.GetConfigurationValueByConfigKey(configKey);
        }

        /// <summary>
        /// Adds the new key value in configurations async.
        /// </summary>
        /// <param name="applicationConfiguration">The application configuration.</param>
        /// <returns>A Task<bool>.</returns>
        public async Task<bool> AddNewKeyValueInConfigurationsAsync(ApplicationConfiguration applicationConfiguration)
        {
            return await applicationConfigurations_DAO.AddNewKeyValueInConfigurationsAsync(applicationConfiguration);
        }

        /// <summary>
        /// Updates the value in configuration by config key async.
        /// </summary>
        /// <param name="applicationConfiguration">The application configuration.</param>
        /// <returns>A Task<bool>.</returns>
        public async Task<bool> UpdateValueInConfigurationByConfigKeyAsync(ApplicationConfiguration applicationConfiguration)
        {
            return await applicationConfigurations_DAO.UpdateValueInConfigurationByConfigKeyAsync(applicationConfiguration);
        }

    }
}

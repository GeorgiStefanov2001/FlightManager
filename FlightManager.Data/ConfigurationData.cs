using System;
using System.Collections.Generic;
using System.Text;

namespace FlightManager.Data 
{ 
    /// <summary>
    /// This class holds the connection string for connecting to the data base
    /// </summary>
    public static class ConfigurationData
    {
        /// <summary>
        /// The connection string, used for connecting to the database
        /// </summary>
        public const string ConnectionString = "Server=GAS-LAPTOP; Database=FlightManager; Integrated Security=true";
    }
}

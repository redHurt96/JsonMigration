using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GamedevForge.DataMigration.Core
{
    public class Migrator
    {
        private readonly IOrderedEnumerable<IMigration> _migrations;

        public Migrator(params IMigration[] migrations) => 
            _migrations = migrations.OrderBy(x => x.ToVersion);

        public T Execute<T>(string rawData)
        {
            if (string.IsNullOrEmpty(rawData))
                throw new Exception($"There is no data to migrate.");
            
            JObject data = JObject.Parse(rawData);
            
            string rawVersion = data["Version"]?.ToString();
            
            if (string.IsNullOrEmpty(rawVersion))
                throw new Exception($"The data to migrate does not contain a version.");

            return Execute<T>(rawData, rawVersion);
        }

        public T Execute<T>(string rawData, string rawVersion)
        {
            Version version;

            try
            {
                version = new Version(rawVersion);
            }
            catch (Exception e)
            {
                throw new Exception($"The version '{rawVersion}' is not a valid version.", e);
            }
            
            return Execute<T>(rawData, version);
        }

        public T Execute<T>(string rawData, Version version)
        {
            JObject data = JObject.Parse(rawData);

            if (version.CompareTo(_migrations.Last().ToVersion) >= 0)
                return Parse<T>(data);
            
            foreach (IMigration migration in _migrations)
            {
                if (version.CompareTo(migration.ToVersion) < 0)
                {
                    data = migration.Migrate(data);
                    data["Version"] = migration.ToVersion.ToString();
                    version = migration.ToVersion;
                }
            }
            
            return Parse<T>(data);
        }
        
        private T Parse<T>(JObject data) => 
            JsonConvert.DeserializeObject<T>(data.ToString());
    }
}
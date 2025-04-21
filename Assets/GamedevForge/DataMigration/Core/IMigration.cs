using System;
using Newtonsoft.Json.Linq;

namespace GamedevForge.DataMigration.Core
{
    public interface IMigration
    {
        Version ToVersion { get; }
        JObject Migrate(JObject data);
    }
}
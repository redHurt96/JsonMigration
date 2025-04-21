using System;
using GamedevForge.DataMigration.Core;
using Newtonsoft.Json.Linq;

namespace GamedevForge.DataMigration.Sample.Logic
{
    public class Migration_2_3 : IMigration
    {
        public Version ToVersion => new Version(0, 3, 0);

        public JObject Migrate(JObject data)
        {
            int woodAmount = data["Wood"]?.Value<int>() ?? 0;
            int stoneAmount = data["Stone"]?.Value<int>() ?? 0;
            JArray resources = data["Resources"] as JArray ?? new JArray();

            resources.Add(JObject.FromObject(new { Name = "Wood", Amount = woodAmount }));
            resources.Add(JObject.FromObject(new { Name = "Stone", Amount = stoneAmount }));
            
            data["Resources"] = resources;
            
            data.Remove("Wood");
            data.Remove("Stone");

            return data;
        }
    }
}
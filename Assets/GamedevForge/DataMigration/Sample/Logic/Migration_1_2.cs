using System;
using GamedevForge.DataMigration.Core;
using Newtonsoft.Json.Linq;

namespace GamedevForge.DataMigration.Sample.Logic
{
    public class Migration_1_2 : IMigration
    {
        public Version ToVersion { get; } = new Version(0, 2, 0);

        public JObject Migrate(JObject data)
        {
            string fullName = data["PlayerName"].ToString();
            string[] names = fullName.Split(' ');

            data.Remove("PlayerName");
            
            data["FirstName"] = names[0];
            data["LastName"] = names[1];

            return data;
        }
    }
}
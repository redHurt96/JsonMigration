using System;
using System.Collections.Generic;

namespace GamedevForge.DataMigration.Sample.Logic
{
    [Serializable]
    public class GameData
    {
        public string Version = "0.1.0";
        public string FirstName;
        public string LastName;
        public List<Resource> Resources = new List<Resource>();
    }

    [Serializable]
    public class Resource
    {
        public string Name;
        public int Amount;
    }
}

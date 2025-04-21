using System.IO;
using GamedevForge.DataMigration.Core;
using Newtonsoft.Json;
using UnityEngine;

namespace GamedevForge.DataMigration.Sample.Logic
{
    public class GameDataEditor : MonoBehaviour
    {
        [SerializeField, TextArea] private string _savedData;
        [SerializeField] private GameData _gameData;
        
        private string Path => System.IO.Path.Combine(Application.streamingAssetsPath, "GameData.json");
        
        [ContextMenu(nameof(Save))]
        private void Save() =>
            File.WriteAllText(Path, JsonConvert.SerializeObject(_gameData));

        [ContextMenu(nameof(Save))]
        private void Load()
        {
            string json = File.ReadAllText(Path);
            _gameData = new Migrator()
                .Execute<GameData>(json);
        }

        [ContextMenu(nameof(Save))]
        private void Show()
        {
            if (File.Exists(Path))
                _savedData = File.ReadAllText(Path);
        }

        [ContextMenu(nameof(Save))]
        private void Clean()
        {
            _savedData = null;
            _gameData = null;
        }
    }
}
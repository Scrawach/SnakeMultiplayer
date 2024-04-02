using System;
using System.Collections.Generic;
using System.IO;
using StaticData;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class SnakeStaticDataModification : AssetModificationProcessor
    {
        private static readonly Type SnakeStaticDataType = typeof(SnakeStaticData);
        private const string PathToServerStaticData = "../../server/SnakeMultiplayerServer/src/staticData";
        
        private static string[] OnWillSaveAssets(string[] paths)
        {
            foreach (var path in paths)
            {
                var assetType = AssetDatabase.GetMainAssetTypeAtPath(path);

                if (assetType != SnakeStaticDataType) 
                    continue;
                
                var asset = AssetDatabase.LoadAssetAtPath<SnakeStaticData>(path);
                UpdateSkinDataOnServer(asset.Skins);
            }
            
            return paths;
        }

        private static void UpdateSkinDataOnServer(SnakeSkins skins)
        {
            var jsonData = GetAvailableSkinsCountAsJson(skins);
            ExportToServerStaticData(nameof(SnakeSkins), jsonData);
            Debug.Log($"[Snake skins saves] Update available skin count on server side! Path: {PathToServerStaticData}; Data: {jsonData}");
        }
        
        private static void ExportToServerStaticData(string filename, string content)
        {
            var correctPathToServer = PathToServerStaticData.Replace('/', Path.DirectorySeparatorChar);
            using var writer = new StreamWriter(Path.Combine(correctPathToServer, $"{filename}.json"), false);
            writer.WriteLine(content);
        }

        private static string GetAvailableSkinsCountAsJson(SnakeSkins skins) =>
            JsonUtility.ToJson(new SnakeSkinsServer(skins.Materials?.Length ?? 0));

        private class SnakeSkinsServer
        {
            public int AvailableSkins;

            public SnakeSkinsServer(int availableSkins) => 
                AvailableSkins = availableSkins;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ET
{
    public class ToolConfig
    {
        [JsonPropertyName("UnityAssetPath")]
        public string UnityAssetPath { get; set; }

        [JsonPropertyName("ExcelPath")]
        public string ExcelPath { get; set; }

        [JsonPropertyName("LubanPath")]
        public string LubanPath { get; set; }

        public override string ToString()
        {
            return $"UnityAssetPath = {UnityAssetPath}  ExcelPath = {ExcelPath}   LubanPath = {LubanPath}";
        }
    }

    
}

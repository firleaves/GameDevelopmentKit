﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Game.Editor;
using SimpleJSON;

namespace ET
{
    public static class GenerateUGFEntityId
    {
        public static string UnityPath = "../Unity/Assets/";
        public static  string s_LubanEntityAsset = Path.GetFullPath($"{UnityPath}/Res/Editor/Hot/Luban/dtentity.json");
        public static void Reload()
        {
            s_LubanEntityAsset = Path.GetFullPath($"{UnityPath}/Res/Editor/Hot/Luban/dtentity.json");
        }
        public static void GenerateCode()
        {
            if (ExcelExporter.ExcelExporter_Luban.IsEnableET)
            {
                GenerateCS("ET.Client", "UGFEntityId",
                    Path.GetFullPath($"{UnityPath}/Scripts/Game/ET/Code/ModelView/Client/Generate/UGF/UGFEntityId.cs"));
            }

            if (ExcelExporter.ExcelExporter_Luban.IsEnableGameHot)
            {
                GenerateCS("Game.Hot", "EntityId",
                    Path.GetFullPath($"{UnityPath}/Scripts/Game/Hot/Code/Runtime/Generate/UGF/EntityId.cs"));
            }
            else
            {
                s_LubanEntityAsset = Path.GetFullPath($"{UnityPath}/Res/Editor/Luban/dtentity.json");
                GenerateCS("Game", "EntityId",
                    Path.GetFullPath($"{UnityPath}/Scripts/Game/Generate/UGF/EntityId.cs"));
            }
        }
        
        private static void GenerateCS(string nameSpaceName, string className, string codeFile)
        {
            if (string.IsNullOrEmpty(nameSpaceName))
            {
                throw new Exception($"Generate UGFEntityId code fail, namespace is empty.");
            }
            if (string.IsNullOrEmpty(className))
            {
                throw new Exception($"Generate UGFEntityId code fail, class name is empty.");
            }
            if (string.IsNullOrEmpty(codeFile))
            {
                throw new Exception($"Generate UGFEntityId code fail, code file is empty.");
            }
            
            JSONNode jsonNode = JSONNode.Parse(File.ReadAllText(s_LubanEntityAsset));
            List<DREntity> drEntities = new List<DREntity>();
            foreach (var childNode in jsonNode.Children)
            {
                DREntity drEntity = DREntity.LoadJsonDREntity(childNode);
                drEntities.Add(drEntity);
            }

            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("// This is an automatically generated class by Share.Tool. Please do not modify it.");
            stringBuilder.AppendLine("");
            stringBuilder.AppendLine($"namespace {nameSpaceName}");
            stringBuilder.AppendLine("{");
            stringBuilder.AppendLine("    /// <summary>");
            stringBuilder.AppendLine("    /// 实体编号。");
            stringBuilder.AppendLine("    /// </summary>");
            stringBuilder.AppendLine($"    public static class {className}");
            stringBuilder.AppendLine("    {");
            stringBuilder.AppendLine("        public const int Undefined = 0;");
            foreach (DREntity drEntity in drEntities)
            {
                if (string.IsNullOrEmpty(drEntity.CSName))
                {
                    throw new Exception($"UGFEntityId {drEntity.Id} CSName is empty!");
                }
                stringBuilder.AppendLine("");
                stringBuilder.AppendLine("        /// <summary>");
                stringBuilder.AppendLine($"        /// {drEntity.Desc}。");
                stringBuilder.AppendLine("        /// </summary>");
                stringBuilder.AppendLine($"        public const int {drEntity.CSName} = {drEntity.Id};");
            }

            stringBuilder.AppendLine("    }");
            stringBuilder.AppendLine("}");
            string codeContent = stringBuilder.ToString();
            string dir = Path.GetDirectoryName(codeFile);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists(codeFile) || !string.Equals(codeContent, File.ReadAllText(codeFile)))
            {
                File.WriteAllText(codeFile, codeContent);
                Log.Info($"Generate code : {codeFile}!");
            }
        }

        private static string GetEntityName(DREntity drEntity)
        {
            return drEntity.AssetName.EndsWith("Entity", StringComparison.OrdinalIgnoreCase)
                    ? drEntity.AssetName.Substring(0, drEntity.AssetName.Length - 6) : drEntity.AssetName;
        }
    }
}
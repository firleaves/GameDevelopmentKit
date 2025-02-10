using ET.Server;
using System;
using System.IO;

namespace ET
{
    public static partial class ExcelExporter
    {
        private static string s_LocalizationExcelFile = Path.GetFullPath($"{Define.WorkDir}/../Design/Excel/Localization.xlsx");

     

        public static void ExportAll()
        {
            //根据导入的config文件修改
            if(Init.GToolConfig!=null)
            {


                Define.UNITY_ASSETS_PATH = Init.GToolConfig.UnityAssetPath;

                s_LocalizationExcelFile = Path.GetFullPath($"{Define.WorkDir}/{Init.GToolConfig.ExcelPath}/Localization.xlsx");

                ExcelExporter_Luban.GEN_CLIENT = $"{Init.GToolConfig.LubanPath}/Tools/Luban/Luban.dll";
                ExcelExporter_Luban.CUSTOM_TEMPLATE_DIR = $"{Init.GToolConfig.LubanPath}/CustomTemplates";
                ExcelExporter_Luban.EXCEL_DIR = $"{Init.GToolConfig.ExcelPath}";




                ExcelExporter_Localization.s_LocalizationOutPutDir = Path.GetFullPath($"{Init.GToolConfig.UnityAssetPath}/Res/Localization");
                ExcelExporter_Localization.s_AssetUtilityCodeFile = Path.GetFullPath($"{Init.GToolConfig.UnityAssetPath}/Scripts/Game/Generate/Localization/AssetUtility.Localization.cs");
                ExcelExporter_Localization.s_LocalizationReadyLanguageCodeFile = Path.GetFullPath($"{Init.GToolConfig.UnityAssetPath}/Scripts/Game/Editor/Generate/Localization/LocalizationReadyLanguage.cs");
                ExcelExporter_Localization.s_UXToolEditorLocalizationToolCodeFile = Path.GetFullPath($"{Init.GToolConfig.UnityAssetPath}/Scripts/Library/UXTool/Editor/Common/EditorLocalization/EditorLocalizationTool.Generate.cs");
           
                GenerateUGFAllSoundId.UnityPath = Init.GToolConfig.UnityAssetPath;
                GenerateUGFEntityId.UnityPath = Init.GToolConfig.UnityAssetPath ;
                GenerateUGFUIFormId.UnityPath = Init.GToolConfig.UnityAssetPath;
                GenerateUGFAllSoundId.Reload();
                GenerateUGFEntityId.Reload();
                GenerateUGFUIFormId.Reload();


                Console.WriteLine(s_LocalizationExcelFile);
                Console.WriteLine(ExcelExporter_Luban.GEN_CLIENT);
                Console.WriteLine(ExcelExporter_Luban.CUSTOM_TEMPLATE_DIR);
                Console.WriteLine(ExcelExporter_Luban.EXCEL_DIR);
                Console.WriteLine(ExcelExporter_Localization.s_LocalizationOutPutDir);
                Console.WriteLine(ExcelExporter_Localization.s_AssetUtilityCodeFile);
                Console.WriteLine(ExcelExporter_Localization.s_LocalizationReadyLanguageCodeFile);
                Console.WriteLine(ExcelExporter_Localization.s_UXToolEditorLocalizationToolCodeFile);
                Console.WriteLine(GenerateUGFAllSoundId.s_LubanMusicAsset);
                Console.WriteLine(GenerateUGFAllSoundId.s_LubanUISoundAsset);
                Console.WriteLine(GenerateUGFAllSoundId.s_LubanSoundAsset);
                Console.WriteLine(GenerateUGFEntityId.s_LubanEntityAsset);
                Console.WriteLine(GenerateUGFUIFormId.s_LubanUIFormAsset);

            }
                

            ExcelExporter_Luban.DoExport();
            ExcelExporter_Localization.DoExport();
        }

        public static void ExportLocalization()
        {
            ExcelExporter_Localization.DoExport();
        }
    }
}
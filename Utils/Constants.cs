namespace ENSEKTestAPIApplication.Utils
{
    class Constants
    {
        public static string RootPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        public const string EndPointFileName = "ApiConfig.json";
        public const string AppConfigFileName = "AppConfig.json";
        public const string DataConfigFolderName = "Data";
        public const string CommonDataFileName = "CommonData.json";
        public static string JsonSchemaFolder = "Models" + Path.DirectorySeparatorChar.ToString() + "JsonSchema";
    }
}

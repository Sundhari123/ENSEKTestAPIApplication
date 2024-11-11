using ENSEKTestAPIApplication.CommonCalls;
using ENSEKTestAPIApplication.Models.RequestModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace ENSEKTestAPIApplication.Utils
{
    public class APIWrapper
    {
        public static JObject apiEndPoints;
        public static JObject apiBaseURLS;
        private static JObject scenariosData;
        public static JObject commonFileData;
        public static RestResponse Response;
        public static string accessToken;
        public static string environment;

        public static JsonTextReader ReadJsonFile(string path)
        {
            JsonTextReader reader;

            StreamReader r = File.OpenText(path);
            reader = new JsonTextReader(r);

            return reader;
        }
        public static void retrieveAPIEndpoints()
        {
            string apiEndPointsJson;

            using (StreamReader r = new StreamReader(Constants.RootPath + Path.DirectorySeparatorChar + Constants.EndPointFileName))
            {
                apiEndPointsJson = r.ReadToEnd();
            }

            apiEndPoints = (JObject)JsonConvert.DeserializeObject(apiEndPointsJson);
        }


        public static void retrieveBaseURLs()
        {
            string apibaseURLSJson;

            using (StreamReader r = new StreamReader(Constants.RootPath + Path.DirectorySeparatorChar + Constants.AppConfigFileName))
            {
                apibaseURLSJson = r.ReadToEnd();
            }

            apiBaseURLS = (JObject)JsonConvert.DeserializeObject(apibaseURLSJson);
        }

        public static void retrieveCommonDataFile()
        {
            string commonData;
            using (StreamReader r = new StreamReader(Constants.RootPath + Path.DirectorySeparatorChar + Constants.DataConfigFolderName + Path.DirectorySeparatorChar + Constants.CommonDataFileName))
            {
                commonData = r.ReadToEnd();
            }
            commonFileData = (JObject)JsonConvert.DeserializeObject(commonData);
        }

        public static async Task getAccessToken()
        {
            string baseURL = (string)apiBaseURLS["baseURL"+environment];
            baseURL = baseURL + apiEndPoints["loginAPI"].ToString();
            LoginRequest LoginRequest = new LoginRequest()
            {
                username = (string)commonFileData["username"],
                password = (string)commonFileData["password"]
            };
            Response = await PostCalls.postAPICall(baseURL, LoginRequest);
            dynamic loginAccessTokenData = JsonConvert.DeserializeObject(Response.Content);
            accessToken = (string)loginAccessTokenData["access_token"];
        }
    }
}

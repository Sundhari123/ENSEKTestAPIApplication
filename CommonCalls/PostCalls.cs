using Newtonsoft.Json;
using RestSharp;

namespace ENSEKTestAPIApplication.CommonCalls
{
    public class PostCalls
    {
        private static RestClient client = new RestClient();

        public static async Task<RestResponse> postAPICall(string endPoint, string accessToken)
        {
            return await CommonCall.commonAPICall(endPoint, accessToken, Method.Post);
        }

        public static async Task<RestResponse> postAPICall(string endPoint, object classType)
        {
            RestRequest request = new RestRequest(endPoint, Method.Post);
            request.AddHeader("Content-Type", "application/json; charset=utf-8");
            request.AddHeader("Accept", "*/*");
            request.AddBody(JsonConvert.SerializeObject(classType));
            RestResponse response = await client.ExecutePostAsync(request);
            return response;
        }
    }
}

using RestSharp;

namespace ENSEKTestAPIApplication.CommonCalls
{
    public class CommonCall
    {
        private static RestClient client = new RestClient();

        public static async Task<RestResponse> commonAPICall(string endPoint, string accessToken, Method method, string requestJson = null)
        {
            RestRequest request = new RestRequest(endPoint, method);
            request.AddHeader("Authorization", "Bearer " + accessToken);
            if (requestJson != null)
                request.AddBody(requestJson);
            RestResponse response = await client.ExecuteAsync(request);
            return response;
        }
    }
}

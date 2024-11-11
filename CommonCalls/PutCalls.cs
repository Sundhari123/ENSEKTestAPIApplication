using RestSharp;

namespace ENSEKTestAPIApplication.CommonCalls
{
    internal class PutCalls
    {
        public static async Task<RestResponse> putAPICall(string endPoint, string accessToken, string requestJson = null)
        {
            return await CommonCall.commonAPICall(endPoint, accessToken, Method.Put);
        }
    }
}

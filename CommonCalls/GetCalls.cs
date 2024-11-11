using RestSharp;

namespace ENSEKTestAPIApplication.CommonCalls
{
    public class GetCalls
    {
        public static async Task<RestResponse> getAPICall(string endPoint, string accessToken)
        {
            return await CommonCall.commonAPICall(endPoint, accessToken, Method.Get);
        }
    }
}

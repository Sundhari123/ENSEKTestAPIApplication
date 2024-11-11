using RestSharp;

namespace ENSEKTestAPIApplication.CommonCalls
{
    public class DeleteCalls
    {
        public static async Task<RestResponse> deleteAPICall(string endPoint, string accessToken)
        {
            return await CommonCall.commonAPICall(endPoint, accessToken, Method.Delete);
        }
    }
}

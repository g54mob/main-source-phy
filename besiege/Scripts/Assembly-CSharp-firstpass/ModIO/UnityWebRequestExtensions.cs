using UnityEngine.Networking;

namespace ModIO
{
	public static class UnityWebRequestExtensions
	{
		public static UnityWebRequestAsyncOperation SendWebRequest(this UnityWebRequest request)
		{
			return WebRequestDispatcher.Dispatch(request);
		}

		public static bool isNetworkError(this UnityWebRequest request)
		{
			return request.isError || request.responseCode <= 0;
		}

		public static bool IsErrorResponseCode(long responseCode)
		{
			return responseCode <= 0 || responseCode >= 400;
		}

		public static bool isHttpError(this UnityWebRequest request)
		{
			return request.isError || IsErrorResponseCode(request.responseCode);
		}
	}
}

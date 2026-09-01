namespace HTraceAO.Scripts.Extensions.CameraHistorySystem
{
	public class CameraHistorySystem<T> where T : struct
	{
		private const int MaxCameraCount = 4;

		private int _cameraHistoryIndex;

		private readonly T[] _cameraHistoryData;

		public int UpdateCameraHistoryIndex(int currentCameraHash)
		{
			return 0;
		}

		private int GetCameraHistoryDataIndex(int cameraHash)
		{
			return 0;
		}

		public void UpdateCameraHistoryData()
		{
		}

		public ref T GetCameraData()
		{
			throw null;
		}

		public T[] GetCameraDatas()
		{
			return null;
		}

		public void SetCameraData(T data)
		{
		}

		public void Cleanup()
		{
		}
	}
}

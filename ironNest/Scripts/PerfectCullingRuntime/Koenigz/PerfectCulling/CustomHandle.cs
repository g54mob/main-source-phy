using UnityEngine;

namespace Koenigz.PerfectCulling
{
	public static class CustomHandle
	{
		public interface IResizableByHandle
		{
			Vector3 HandleSized { get; set; }
		}

		public class ActualHandle<T, U> where U : struct
		{
		}
	}
}

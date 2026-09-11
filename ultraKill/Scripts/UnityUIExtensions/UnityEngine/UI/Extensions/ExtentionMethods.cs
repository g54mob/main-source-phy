namespace UnityEngine.UI.Extensions
{
	public static class ExtentionMethods
	{
		public static T GetOrAddComponent<T>(this GameObject child) where T : Component
		{
			T val = child.GetComponent<T>();
			if (val == null)
			{
				val = child.AddComponent<T>();
			}
			return val;
		}
	}
}

namespace Kamgam.SettingsGenerator
{
	public static class CompatibilityUtils
	{
		public static T FindObjectOfType<T>(bool includeInactive = false)
		{
			return default(T);
		}

		public static T[] FindObjectsOfType<T>(bool includeInactive = false)
		{
			return null;
		}
	}
}

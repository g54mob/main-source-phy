namespace Battlehub.RTCommon
{
	public static class IRuntimeSelectionExt
	{
		public static bool IsNullOrEmpty(this IRuntimeSelection selection)
		{
			if (selection.objects != null)
			{
				return selection.objects.Length == 0;
			}
			return true;
		}
	}
}

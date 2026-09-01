namespace TFBGames
{
	public class AsyncCounter
	{
		private readonly int maxCount;

		private int count;

		public AsyncCounter(int asyncItemsCount)
		{
			maxCount = asyncItemsCount;
		}

		public bool OnAsyncDone()
		{
			count++;
			return count >= maxCount;
		}
	}
}

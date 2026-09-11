namespace plog.unity.Models
{
	public record UnityConfiguration
	{
		public static UnityConfiguration Default => new UnityConfiguration
		{
			ColorizeMessages = true,
			ColorizeLoggerTags = true,
			ShowLoggerTags = true
		};

		public static UnityConfiguration RuntimeDefault => new UnityConfiguration
		{
			ColorizeMessages = false,
			ColorizeLoggerTags = false,
			ShowLoggerTags = true
		};

		public bool ColorizeMessages;

		public bool ColorizeLoggerTags;

		public bool ShowLoggerTags;
	}
}

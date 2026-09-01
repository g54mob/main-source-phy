using System;

namespace MoreMountains.Feedbacks
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class FeedbackPathAttribute : Attribute
	{
		public string Path;

		public string Name;

		public FeedbackPathAttribute(string path)
		{
		}

		public static string GetFeedbackDefaultName(Type type)
		{
			return null;
		}

		public static string GetFeedbackDefaultPath(Type type)
		{
			return null;
		}
	}
}

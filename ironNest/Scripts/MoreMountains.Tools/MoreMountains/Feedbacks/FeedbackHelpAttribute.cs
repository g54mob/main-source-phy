using System;

namespace MoreMountains.Feedbacks
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class FeedbackHelpAttribute : Attribute
	{
		public string HelpText;

		public FeedbackHelpAttribute(string helpText)
		{
		}

		public static string GetFeedbackHelpText(Type type)
		{
			return null;
		}
	}
}

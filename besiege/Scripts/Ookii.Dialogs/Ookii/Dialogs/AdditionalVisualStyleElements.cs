using System.Windows.Forms.VisualStyles;

namespace Ookii.Dialogs
{
	public static class AdditionalVisualStyleElements
	{
		public static class TextStyle
		{
			private const string _className = "TEXTSTYLE";

			private static VisualStyleElement _mainInstruction;

			private static VisualStyleElement _bodyText;

			public static VisualStyleElement MainInstruction
			{
				get
				{
					return _mainInstruction ?? (_mainInstruction = VisualStyleElement.CreateElement("TEXTSTYLE", 1, 0));
				}
			}

			public static VisualStyleElement BodyText
			{
				get
				{
					return _bodyText ?? (_bodyText = VisualStyleElement.CreateElement("TEXTSTYLE", 4, 0));
				}
			}
		}

		public static class TaskDialog
		{
			private const string _className = "TASKDIALOG";

			private static VisualStyleElement _primaryPanel;

			private static VisualStyleElement _secondaryPanel;

			public static VisualStyleElement PrimaryPanel
			{
				get
				{
					return _primaryPanel ?? (_primaryPanel = VisualStyleElement.CreateElement("TASKDIALOG", 1, 0));
				}
			}

			public static VisualStyleElement SecondaryPanel
			{
				get
				{
					return _secondaryPanel ?? (_secondaryPanel = VisualStyleElement.CreateElement("TASKDIALOG", 8, 0));
				}
			}
		}
	}
}

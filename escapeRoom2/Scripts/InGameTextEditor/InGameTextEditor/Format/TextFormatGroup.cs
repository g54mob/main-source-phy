using UnityEngine;

namespace InGameTextEditor.Format
{
	public class TextFormatGroup
	{
		public int startIndex;

		public int endIndex;

		public TextStyle textStyle;

		public TextFormatGroup(int startIndex, int endIndex, TextStyle textStyle)
		{
			this.startIndex = startIndex;
			this.endIndex = endIndex;
			this.textStyle = textStyle;
			if (startIndex > endIndex)
			{
				throw new UnityException("startIndex must not be greater than endIndex");
			}
		}

		public static int Sort(TextFormatGroup textFormatGroup1, TextFormatGroup textFormatGroup2)
		{
			if (textFormatGroup1 == null)
			{
				return 1;
			}
			if (textFormatGroup2 == null)
			{
				return -1;
			}
			if (textFormatGroup1.startIndex < textFormatGroup2.startIndex)
			{
				return -1;
			}
			if (textFormatGroup1.startIndex > textFormatGroup2.startIndex)
			{
				return 1;
			}
			return 0;
		}
	}
}

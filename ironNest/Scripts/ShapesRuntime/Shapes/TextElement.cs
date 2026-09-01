using System;
using System.Text;

namespace Shapes
{
	public class TextElement : IDisposable
	{
		private static int idCounter;

		public readonly int id;

		private StringBuilder sb;

		public TextMeshProShapes Tmp => null;

		public static int GetNextId()
		{
			return 0;
		}

		public void Dispose()
		{
		}

		public void ClearText()
		{
		}

		public void AppendInt(int value, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), int maxCharCount = 12)
		{
		}

		public void AppendFloat(float value, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), int maxCharCount = 32)
		{
		}

		public void AppendDouble(double value, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), int maxCharCount = 32)
		{
		}

		public void AppendString(ReadOnlySpan<char> stringValue)
		{
		}
	}
}

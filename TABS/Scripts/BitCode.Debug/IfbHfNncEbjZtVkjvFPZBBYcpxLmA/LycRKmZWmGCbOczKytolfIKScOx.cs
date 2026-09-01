using System;
using System.Text;
using BitCode.Debug;
using JetBrains.Annotations;

namespace IfbHfNncEbjZtVkjvFPZBBYcpxLmA
{
	internal class LycRKmZWmGCbOczKytolfIKScOx : IDebugConsoleWriter
	{
		private readonly StringBuilder eRqPlYRcAYAqwDlfrCoIGDoiSkTIB;

		public LycRKmZWmGCbOczKytolfIKScOx([NotNull] StringBuilder P_0)
		{
			eRqPlYRcAYAqwDlfrCoIGDoiSkTIB = P_0 ?? throw new ArgumentNullException("builder");
		}

		public void Append(string text)
		{
			eRqPlYRcAYAqwDlfrCoIGDoiSkTIB.Append(text);
		}

		public void AppendLine()
		{
			eRqPlYRcAYAqwDlfrCoIGDoiSkTIB.AppendLine();
		}

		public void AppendLine(string text)
		{
			eRqPlYRcAYAqwDlfrCoIGDoiSkTIB.AppendLine(text);
		}

		[StringFormatMethod("args")]
		public void AppendFormat(string text, params object[] args)
		{
			eRqPlYRcAYAqwDlfrCoIGDoiSkTIB.AppendFormat(text, args);
		}
	}
}

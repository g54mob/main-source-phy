using System;
using JetBrains.Annotations;

namespace BitCode.Logging
{
	public class LogWriterDecorator : ILogWriter
	{
		private sealed class ZXjJJXdfjUCYdxswWWjOSJImoRgO
		{
			public string OXmNQeRrolXidCqrdyLJAcNAoyDb;

			internal string mxrfJMrRGnhBwaPeWjTHjZxjIBpDA(LogSeverity P_0, string P_1)
			{
				return string.Format(OXmNQeRrolXidCqrdyLJAcNAoyDb, P_1);
			}
		}

		private readonly ILogWriter KLowHZDATMOvVceqBLhtEjGVHyfJ;

		private readonly Func<LogSeverity, string, string> yiodBRSRTcygjNccmGSdlGtbSxOu;

		public LogWriterDecorator([NotNull] ILogWriter writer, [NotNull] string decorationFormatString)
		{
			ZXjJJXdfjUCYdxswWWjOSJImoRgO zXjJJXdfjUCYdxswWWjOSJImoRgO = new ZXjJJXdfjUCYdxswWWjOSJImoRgO
			{
				OXmNQeRrolXidCqrdyLJAcNAoyDb = decorationFormatString
			};
			base._002Ector();
			KLowHZDATMOvVceqBLhtEjGVHyfJ = writer ?? throw new ArgumentNullException("writer");
			rWsqNfHiNqlfUXOnwMHGlrJvjaSw(zXjJJXdfjUCYdxswWWjOSJImoRgO.OXmNQeRrolXidCqrdyLJAcNAoyDb);
			yiodBRSRTcygjNccmGSdlGtbSxOu = zXjJJXdfjUCYdxswWWjOSJImoRgO.mxrfJMrRGnhBwaPeWjTHjZxjIBpDA;
		}

		public LogWriterDecorator([NotNull] ILogWriter writer, [NotNull] Func<LogSeverity, string, string> decorator)
		{
			KLowHZDATMOvVceqBLhtEjGVHyfJ = writer ?? throw new ArgumentNullException("writer");
			yiodBRSRTcygjNccmGSdlGtbSxOu = decorator ?? throw new ArgumentNullException("decorator");
		}

		public void Write(LogSeverity severity, string message)
		{
			KLowHZDATMOvVceqBLhtEjGVHyfJ.Write(severity, yiodBRSRTcygjNccmGSdlGtbSxOu(severity, message));
		}

		public void WriteException(Exception ex)
		{
			KLowHZDATMOvVceqBLhtEjGVHyfJ.WriteException(ex);
		}

		private static void rWsqNfHiNqlfUXOnwMHGlrJvjaSw(string P_0)
		{
			if (P_0 == null)
			{
				goto IL_0003;
			}
			goto IL_0047;
			IL_0003:
			int num = 1935596024;
			goto IL_0008;
			IL_0008:
			uint num2;
			switch ((num2 = (uint)(num ^ 0x4FEE9C8D)) % 5)
			{
			case 3u:
				break;
			default:
				return;
			case 2u:
				throw new ArgumentNullException("decorationFormatString");
			case 1u:
				goto IL_0047;
			case 0u:
				throw new ArgumentException("The format string must contain the substring \"{0}\".", "decorationFormatString");
			case 4u:
				return;
			}
			goto IL_0003;
			IL_0047:
			int num3;
			if (!P_0.Contains("{0}"))
			{
				num = 1306315437;
				num3 = num;
			}
			else
			{
				num = 1087619910;
				num3 = num;
			}
			goto IL_0008;
		}
	}
}

using JetBrains.Annotations;

namespace BitCode.Debug
{
	public interface IDebugConsoleWriter
	{
		void Append(string text);

		void AppendLine(string text);

		void AppendFormat(string text, [NotNull] params object[] args);

		void AppendLine();
	}
}

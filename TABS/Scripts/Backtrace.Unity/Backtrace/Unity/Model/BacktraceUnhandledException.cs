using System;
using System.Collections.Generic;
using System.Linq;
using Backtrace.Unity.Types;

namespace Backtrace.Unity.Model
{
	public class BacktraceUnhandledException : Exception
	{
		private bool _header;

		private string _message;

		private readonly string _stacktrace;

		public readonly List<BacktraceStackFrame> StackFrames = new List<BacktraceStackFrame>();

		public bool Header => _header;

		public override string Message => _message;

		public string Classifier { get; set; }

		public override string StackTrace => _stacktrace;

		public BacktraceUnhandledException(string message, string stacktrace)
			: base(message)
		{
			_message = message;
			_stacktrace = stacktrace;
			if (!string.IsNullOrEmpty(stacktrace))
			{
				ConvertStackFrames();
			}
			if (string.IsNullOrEmpty(stacktrace) || StackFrames.Count == 0)
			{
				_message = message;
				BacktraceStackTrace backtraceStackTrace = new BacktraceStackTrace(null);
				StackFrames = backtraceStackTrace.StackFrames;
			}
			TrySetClassifier();
		}

		private void ConvertStackFrames()
		{
			string[] array = _stacktrace.Split('\n');
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (string.IsNullOrEmpty(text))
				{
					continue;
				}
				string text2 = text.Trim();
				int num = text2.IndexOf(')');
				if (num == -1)
				{
					if (i == 0)
					{
						if (string.IsNullOrEmpty(_message))
						{
							_message = text2;
						}
						_header = true;
					}
					continue;
				}
				if (num < 1 && text2[num - 1] != '(')
				{
					break;
				}
				BacktraceStackFrame backtraceStackFrame = null;
				backtraceStackFrame = (text2.StartsWith("0x") ? SetNativeStackTraceInformation(text2) : (text2.StartsWith("#") ? SetJITStackTraceInformation(text2) : ((text2.IndexOf('(', num + 1) <= -1) ? SetAndroidStackTraceInformation(text2) : SetDefaultStackTraceInformation(text2))));
				if (backtraceStackFrame == null || string.IsNullOrEmpty(backtraceStackFrame.FunctionName))
				{
					backtraceStackFrame = new BacktraceStackFrame
					{
						FunctionName = text2
					};
				}
				StackFrames.Add(backtraceStackFrame);
			}
		}

		private BacktraceStackFrame SetJITStackTraceInformation(string frameString)
		{
			BacktraceStackFrame backtraceStackFrame = new BacktraceStackFrame();
			backtraceStackFrame.StackFrameType = BacktraceStackFrameType.Native;
			if (!frameString.StartsWith("#"))
			{
				backtraceStackFrame.FunctionName = frameString;
				return backtraceStackFrame;
			}
			frameString = frameString.Substring(frameString.IndexOf(' ')).Trim();
			int num = frameString.IndexOf("(Mono JIT Code)");
			if (num != -1)
			{
				frameString = frameString.Substring(num + "(Mono JIT Code)".Length).Trim();
			}
			int num2 = frameString.IndexOf("(wrapper managed-to-native)");
			if (num2 != -1)
			{
				frameString = frameString.Substring(num2 + "(wrapper managed-to-native)".Length).Trim();
			}
			int num3 = frameString.IndexOf('(');
			int num4 = frameString.IndexOf(')');
			if (num3 != -1 && num4 != -1 && num4 > num3)
			{
				backtraceStackFrame.FunctionName = frameString.Substring(0, num3).Trim();
			}
			else
			{
				backtraceStackFrame.FunctionName = frameString;
			}
			return backtraceStackFrame;
		}

		private BacktraceStackFrame SetNativeStackTraceInformation(string frameString)
		{
			BacktraceStackFrame backtraceStackFrame = new BacktraceStackFrame();
			backtraceStackFrame.StackFrameType = BacktraceStackFrameType.Native;
			int num = frameString.IndexOf(' ');
			backtraceStackFrame.Address = frameString.Substring(0, num);
			int num2 = num + 1;
			if (frameString[num2] == '(')
			{
				num2++;
				int num3 = frameString.IndexOf(')', num2);
				backtraceStackFrame.Library = frameString.Substring(num2, num3 - num2);
				num2 = num3 + 2;
			}
			backtraceStackFrame.FunctionName = frameString.Substring(num2);
			if (backtraceStackFrame.FunctionName.StartsWith("(wrapper managed-to-native)"))
			{
				backtraceStackFrame.FunctionName = backtraceStackFrame.FunctionName.Replace("(wrapper managed-to-native)", string.Empty).Trim();
			}
			if (backtraceStackFrame.FunctionName.StartsWith("(wrapper runtime-invoke)"))
			{
				backtraceStackFrame.FunctionName = backtraceStackFrame.FunctionName.Replace("(wrapper runtime-invoke)", string.Empty).Trim();
			}
			int num4 = backtraceStackFrame.FunctionName.IndexOf('[');
			int num5 = backtraceStackFrame.FunctionName.IndexOf(']');
			if (num4 != -1 && num5 != -1)
			{
				num4++;
				string[] array = backtraceStackFrame.FunctionName.Substring(num4, num5 - num4).Split(new char[1] { ':' }, 2);
				if (array.Length == 2)
				{
					int.TryParse(array[1], out backtraceStackFrame.Line);
					backtraceStackFrame.Library = array[0];
					backtraceStackFrame.FunctionName = backtraceStackFrame.FunctionName.Substring(num5 + 2);
				}
			}
			return backtraceStackFrame;
		}

		private BacktraceStackFrame SetAndroidStackTraceInformation(string frameString)
		{
			int num = frameString.LastIndexOf('(') + 1;
			int num2 = frameString.LastIndexOf(')');
			BacktraceStackFrame backtraceStackFrame = new BacktraceStackFrame();
			backtraceStackFrame.StackFrameType = BacktraceStackFrameType.Android;
			if (num != -1 && num2 != -1 && num2 - num > 1)
			{
				backtraceStackFrame.FunctionName = frameString.Substring(0, num - 1);
				string text = frameString.Substring(num, num2 - num);
				string[] array = text.Split(':');
				if (array.Length == 2)
				{
					backtraceStackFrame.Library = array[0];
					int.TryParse(array[1], out backtraceStackFrame.Line);
				}
				else if (frameString.StartsWith("java.lang") || text == "Unknown Source")
				{
					backtraceStackFrame.Library = text;
				}
			}
			return backtraceStackFrame;
		}

		private BacktraceStackFrame SetDefaultStackTraceInformation(string frameString)
		{
			if (frameString.StartsWith("(wrapper remoting-invoke-with-check)"))
			{
				frameString = frameString.Replace("(wrapper remoting-invoke-with-check)", string.Empty);
			}
			int num = frameString.IndexOf(')');
			int num2 = frameString.IndexOf('(', num + 1);
			if (num2 == -1)
			{
				return new BacktraceStackFrame
				{
					FunctionName = frameString,
					StackFrameType = BacktraceStackFrameType.Dotnet
				};
			}
			int length = frameString.Length - num2;
			string text = frameString.Trim().Substring(num2, length);
			int num3 = text.LastIndexOf(':') + 1;
			int num4 = text.LastIndexOf(')') - num3;
			BacktraceStackFrame backtraceStackFrame = new BacktraceStackFrame
			{
				FunctionName = frameString.Substring(0, num + 1).Trim(),
				StackFrameType = BacktraceStackFrameType.Dotnet
			};
			if (num4 > 0 && num3 > 0)
			{
				int.TryParse(text.Substring(num3, num4), out backtraceStackFrame.Line);
			}
			if (text[0] == '(' && num3 != -1)
			{
				int num5 = ((!text.StartsWith("(at")) ? 1 : 3);
				int length2 = ((num3 == 0) ? (text.LastIndexOf(')') - num5) : (num3 - 1 - num5));
				string text2 = text.Substring(num5, length2);
				backtraceStackFrame.Library = ((text2 == null) ? string.Empty : text2.Trim());
				if (!string.IsNullOrEmpty(backtraceStackFrame.Library) && string.Copy(backtraceStackFrame.Library).Replace("0", string.Empty).Length <= 2)
				{
					backtraceStackFrame.Library = null;
				}
			}
			return backtraceStackFrame;
		}

		private void TrySetClassifier()
		{
			Classifier = "error";
			if (string.IsNullOrEmpty(_message))
			{
				return;
			}
			if (_message.EndsWith("Exception"))
			{
				Classifier = _message.Split(' ').Last();
				return;
			}
			string[] array = _message.Split(':');
			string text = array[0].Trim();
			if (!string.IsNullOrEmpty(text) && text.EndsWith("Exception"))
			{
				if (text == "AndroidJavaException" && text.Length > 1 && array[1].EndsWith("Exception"))
				{
					Classifier = array[1].Trim();
				}
				else
				{
					Classifier = text;
				}
			}
		}
	}
}

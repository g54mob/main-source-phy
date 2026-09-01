using System;
using System.Reflection;

namespace BitCode.Debug
{
	public class ParameterResolverException : Exception
	{
		public readonly ParameterInfo ParameterInfo;

		public ParameterResolverException(ParameterInfo parameterInfo)
		{
			ParameterInfo = parameterInfo;
		}

		public ParameterResolverException(ParameterInfo parameterInfo, string message)
			: base(message)
		{
			while (true)
			{
				int num = 1945752782;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ 0x74F66192)) % 3)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						goto IL_0029;
					case 0u:
						return;
					}
					break;
					IL_0029:
					ParameterInfo = parameterInfo;
					num = (int)((num2 * 472301591) ^ 0x2F3FCC4D);
				}
			}
		}

		public ParameterResolverException(ParameterInfo parameterInfo, string message, Exception innerException)
			: base(message, innerException)
		{
			ParameterInfo = parameterInfo;
		}
	}
}

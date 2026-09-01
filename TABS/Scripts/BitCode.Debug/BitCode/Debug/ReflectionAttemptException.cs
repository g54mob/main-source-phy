using System;

namespace BitCode.Debug
{
	public class ReflectionAttemptException : Exception
	{
		public readonly Type ReflectingType;

		public ReflectionAttemptException(Type reflectingType)
		{
			while (true)
			{
				int num = -1065233280;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -828754838)) % 3)
					{
					case 2u:
						break;
					default:
						return;
					case 1u:
						goto IL_0028;
					case 0u:
						return;
					}
					break;
					IL_0028:
					ReflectingType = reflectingType;
					num = (int)(num2 * 1402584508) ^ -2021217080;
				}
			}
		}

		public ReflectionAttemptException(Type reflectingType, string message)
			: base(message)
		{
			ReflectingType = reflectingType;
		}

		public ReflectionAttemptException(Type reflectingType, string message, Exception innerException)
			: base(message, innerException)
		{
			ReflectingType = reflectingType;
		}
	}
}

using System;
using System.Reflection;
using DdQbeCzwvEdCSCHcDJqhScymDgUBA;
using MFpUPImZGydFZMatqxYnCurkNhQN;

namespace BitCode.Debug.MemberWrappers
{
	public static class MemberWrapper
	{
		public static IFieldWrapper WrapField(FieldInfo fieldInfo, object context)
		{
			if (context == null)
			{
				goto IL_0003;
			}
			goto IL_0079;
			IL_0003:
			int num = -1861733099;
			goto IL_0008;
			IL_0008:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ -2089609102)) % 7)
				{
				case 3u:
					break;
				case 4u:
				{
					int num5;
					int num6;
					if (fieldInfo.IsStatic)
					{
						num5 = 441536649;
						num6 = num5;
					}
					else
					{
						num5 = 1339717955;
						num6 = num5;
					}
					num = num5 ^ (int)(num2 * 563488846);
					continue;
				}
				case 0u:
					throw new InvalidOperationException($"{fieldInfo} is not static, but no context was provided.");
				case 5u:
					goto IL_0079;
				case 1u:
				{
					int num3;
					int num4;
					if (!fieldInfo.IsStatic)
					{
						num3 = -1609506920;
						num4 = num3;
					}
					else
					{
						num3 = -634569923;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 465960490);
					continue;
				}
				case 2u:
					throw new InvalidOperationException($"{fieldInfo} is static, but a context object was provided.");
				default:
					return new jmObiOtYsUXsXLwFUUlRGnvuiMQk(fieldInfo, context);
				}
				break;
			}
			goto IL_0003;
			IL_0079:
			int num7;
			if (context == null)
			{
				num = -1130947151;
				num7 = num;
			}
			else
			{
				num = -891547603;
				num7 = num;
			}
			goto IL_0008;
		}

		public static IFieldWrapper WrapStaticField(FieldInfo fieldInfo)
		{
			return WrapField(null, fieldInfo);
		}

		public static IPropertyWrapper WrapProperty(PropertyInfo propertyInfo, object context)
		{
			if (context == null)
			{
				goto IL_0003;
			}
			goto IL_0038;
			IL_0003:
			int num = 279949954;
			goto IL_0008;
			IL_0008:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x6CCA45D2)) % 7)
				{
				case 4u:
					break;
				case 3u:
					goto IL_0038;
				case 2u:
					throw new InvalidOperationException($"{propertyInfo} is not static, but no context was provided.");
				case 1u:
				{
					int num5;
					int num6;
					if (propertyInfo.JamBOueAKkqFJZsfzogvNpBolVeCA())
					{
						num5 = -260360591;
						num6 = num5;
					}
					else
					{
						num5 = -1691256050;
						num6 = num5;
					}
					num = num5 ^ (int)(num2 * 1148596867);
					continue;
				}
				case 0u:
				{
					int num3;
					int num4;
					if (propertyInfo.JamBOueAKkqFJZsfzogvNpBolVeCA())
					{
						num3 = 116544194;
						num4 = num3;
					}
					else
					{
						num3 = 319032210;
						num4 = num3;
					}
					num = num3 ^ ((int)num2 * -1091597209);
					continue;
				}
				case 5u:
					throw new InvalidOperationException($"{propertyInfo} is static, but a context object was provided.");
				default:
					return new kNoyShfviXrASqjbBJKbwjaYWtmX(propertyInfo, context);
				}
				break;
			}
			goto IL_0003;
			IL_0038:
			int num7;
			if (context != null)
			{
				num = 1854487942;
				num7 = num;
			}
			else
			{
				num = 1851478622;
				num7 = num;
			}
			goto IL_0008;
		}

		public static IPropertyWrapper WrapStaticProperty(PropertyInfo propertyInfo)
		{
			return WrapProperty(null, propertyInfo);
		}

		public static IMethodWrapper WrapMethod(MethodInfo methodInfo, object context)
		{
			if (context == null)
			{
				goto IL_0006;
			}
			goto IL_00c3;
			IL_0006:
			int num = 1652311365;
			goto IL_000b;
			IL_000b:
			while (true)
			{
				uint num2;
				switch ((num2 = (uint)(num ^ 0x48037F63)) % 7)
				{
				case 2u:
					break;
				case 6u:
				{
					int num5;
					int num6;
					if (!methodInfo.IsStatic)
					{
						num5 = -840322524;
						num6 = num5;
					}
					else
					{
						num5 = -1126396578;
						num6 = num5;
					}
					num = num5 ^ ((int)num2 * -546679514);
					continue;
				}
				case 4u:
				{
					int num3;
					int num4;
					if (!methodInfo.IsStatic)
					{
						num3 = 1145009044;
						num4 = num3;
					}
					else
					{
						num3 = 1286920665;
						num4 = num3;
					}
					num = num3 ^ (int)(num2 * 2119687944);
					continue;
				}
				case 0u:
					throw new InvalidOperationException($"{methodInfo} is static, but a context object was provided.");
				case 5u:
					throw new InvalidOperationException($"{methodInfo} is not static, but no context was provided.");
				case 3u:
					goto IL_00c3;
				default:
					return new MethodWrapper(methodInfo, context);
				}
				break;
			}
			goto IL_0006;
			IL_00c3:
			int num7;
			if (context == null)
			{
				num = 1236521036;
				num7 = num;
			}
			else
			{
				num = 954467512;
				num7 = num;
			}
			goto IL_000b;
		}

		public static IMethodWrapper WrapStaticMethod(MethodInfo propertyInfo)
		{
			return WrapMethod(null, propertyInfo);
		}
	}
}

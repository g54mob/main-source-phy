using System;
using System.Reflection;
using BitCode.Attributes;
using BitCode.Debug.MemberWrappers;
using DdQbeCzwvEdCSCHcDJqhScymDgUBA;

namespace BitCode.Debug.Commands
{
	public static class TypeCommands
	{
		[DebugCommand(Description = "Prints a list of all static fields and their values on the context type.")]
		public static void PrintFields(this Type type, IDebugConsoleWriter writer, bool publicOnly = true)
		{
			if (publicOnly)
			{
				goto IL_0003;
			}
			int num = 56;
			goto IL_002b;
			IL_002b:
			BindingFlags bindingAttr = (BindingFlags)num;
			mpNTiEnCAkYpVUOkiYKiEmrcsGkp.qsmadcCvBDwckCLfyEFTPxKcJCrH(type.GetFields(bindingAttr), writer);
			int num2 = -1071845238;
			goto IL_0008;
			IL_0003:
			num2 = -494462167;
			goto IL_0008;
			IL_0008:
			uint num3;
			switch ((num3 = (uint)(num2 ^ -239092566)) % 3)
			{
			case 0u:
				break;
			default:
				return;
			case 1u:
				goto IL_0025;
			case 2u:
				return;
			}
			goto IL_0003;
			IL_0025:
			num = 24;
			goto IL_002b;
		}

		[DebugCommand(Description = "Prints a list of all static properties and their values on the context type.")]
		public static void PrintProperties(this Type type, IDebugConsoleWriter writer, bool publicOnly = true)
		{
			if (publicOnly)
			{
				goto IL_0003;
			}
			int num = 56;
			goto IL_002b;
			IL_002b:
			BindingFlags bindingAttr = (BindingFlags)num;
			mpNTiEnCAkYpVUOkiYKiEmrcsGkp.qsmadcCvBDwckCLfyEFTPxKcJCrH(type.GetProperties(bindingAttr), writer);
			int num2 = -5333306;
			goto IL_0008;
			IL_0003:
			num2 = -1207982056;
			goto IL_0008;
			IL_0008:
			uint num3;
			switch ((num3 = (uint)(num2 ^ -1834382947)) % 3)
			{
			case 0u:
				break;
			default:
				return;
			case 1u:
				goto IL_0025;
			case 2u:
				return;
			}
			goto IL_0003;
			IL_0025:
			num = 24;
			goto IL_002b;
		}

		[DebugCommand(Description = "Prints a list of all static methods and their signatures on the context type.")]
		public static void PrintMethods(this Type type, IDebugConsoleWriter writer, bool publicOnly = true)
		{
			if (publicOnly)
			{
				goto IL_0003;
			}
			int num = 56;
			goto IL_002f;
			IL_002f:
			BindingFlags bindingAttr = (BindingFlags)num;
			int num2 = 687184011;
			goto IL_0008;
			IL_0003:
			num2 = 1000349141;
			goto IL_0008;
			IL_0008:
			while (true)
			{
				uint num3;
				switch ((num3 = (uint)(num2 ^ 0x163B0488)) % 4)
				{
				case 0u:
					break;
				default:
					return;
				case 1u:
					goto IL_0029;
				case 3u:
					mpNTiEnCAkYpVUOkiYKiEmrcsGkp.qsmadcCvBDwckCLfyEFTPxKcJCrH(type.GetMethods(bindingAttr), writer);
					num2 = ((int)num3 * -1001235388) ^ 0x191D907E;
					continue;
				case 2u:
					return;
				}
				break;
			}
			goto IL_0003;
			IL_0029:
			num = 24;
			goto IL_002f;
		}

		[DebugCommand(Description = "Gets or sets a static field with the given name on the context object.")]
		public static IFieldWrapper Field(this Type type, string fieldName)
		{
			try
			{
				return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.ONkRbvQUZruPdujPEIyNjyYjoAHF(null, type, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, fieldName);
			}
			catch (ReflectionAttemptException innerException)
			{
				throw new CommandExecutionException("Field", $"Error occurred while attempting to reflect into type {type}.\nSee inner exception for more details.", innerException);
			}
		}

		[DebugCommand(Description = "Gets or sets a static property with the given name on the context object.")]
		public static IPropertyWrapper Property(this Type type, string propertyName)
		{
			try
			{
				return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(null, type, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, propertyName);
			}
			catch (ReflectionAttemptException innerException)
			{
				throw new CommandExecutionException("Property", $"Error occurred while attempting to reflect into type {type}.\nSee inner exception for more details.", innerException);
			}
		}

		[DebugCommand(Description = "Finds a static method with the given name on the context object.")]
		public static IMethodWrapper Method(this Type type, string methodName)
		{
			try
			{
				return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.YOszgTUGxobOSChgEqYAtqFrQhMHA(null, type, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, methodName);
			}
			catch (AmbiguousMatchException innerException)
			{
				throw new CommandExecutionException("Method", $"Method {methodName} has multiple overloads on type {type}.", innerException);
			}
			catch (ReflectionAttemptException innerException2)
			{
				throw new CommandExecutionException("Method", $"Error occurred while attempting to reflect into type {type}.\nSee inner exception for more details.", innerException2);
			}
		}
	}
}

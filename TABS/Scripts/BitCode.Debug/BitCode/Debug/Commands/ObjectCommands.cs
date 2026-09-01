using System;
using System.Reflection;
using BitCode.Attributes;
using BitCode.Debug.MemberWrappers;
using DdQbeCzwvEdCSCHcDJqhScymDgUBA;

namespace BitCode.Debug.Commands
{
	public static class ObjectCommands
	{
		[DebugCommand(Description = "Prints a list of all fields and their values on the context object.")]
		public static void PrintFields(this object instance, IDebugConsoleWriter writer, bool publicOnly = true)
		{
			if (publicOnly)
			{
				goto IL_0003;
			}
			int num = 52;
			goto IL_002f;
			IL_0029:
			num = 20;
			goto IL_002f;
			IL_0003:
			int num2 = 987253200;
			goto IL_0008;
			IL_0008:
			BindingFlags bindingAttr = default(BindingFlags);
			while (true)
			{
				uint num3;
				switch ((num3 = (uint)(num2 ^ 0x3FA684DA)) % 4)
				{
				case 0u:
					break;
				default:
					return;
				case 2u:
					goto IL_0029;
				case 1u:
					mpNTiEnCAkYpVUOkiYKiEmrcsGkp.qsmadcCvBDwckCLfyEFTPxKcJCrH(instance.GetType().GetFields(bindingAttr), writer, true, instance);
					num2 = ((int)num3 * -1850548925) ^ 0x174F07EE;
					continue;
				case 3u:
					return;
				}
				break;
			}
			goto IL_0003;
			IL_002f:
			bindingAttr = (BindingFlags)num;
			num2 = 498925103;
			goto IL_0008;
		}

		[DebugCommand(Description = "Prints a list of all properties and their values on the context object.")]
		public static void PrintProperties(this object instance, IDebugConsoleWriter writer, bool publicOnly = true)
		{
			if (publicOnly)
			{
				goto IL_0003;
			}
			int num = 52;
			goto IL_002b;
			IL_002b:
			BindingFlags bindingAttr = (BindingFlags)num;
			mpNTiEnCAkYpVUOkiYKiEmrcsGkp.qsmadcCvBDwckCLfyEFTPxKcJCrH(instance.GetType().GetProperties(bindingAttr), writer, true, instance);
			int num2 = -919337444;
			goto IL_0008;
			IL_0003:
			num2 = -438281852;
			goto IL_0008;
			IL_0008:
			uint num3;
			switch ((num3 = (uint)(num2 ^ -1701455924)) % 3)
			{
			case 0u:
				break;
			default:
				return;
			case 2u:
				goto IL_0025;
			case 1u:
				return;
			}
			goto IL_0003;
			IL_0025:
			num = 20;
			goto IL_002b;
		}

		[DebugCommand(Description = "Prints a list of all methods and their signatures on the context object.")]
		public static void PrintMethods(this object instance, IDebugConsoleWriter writer, bool publicOnly = true)
		{
			if (publicOnly)
			{
				goto IL_0003;
			}
			int num = 52;
			goto IL_002f;
			IL_002f:
			BindingFlags bindingAttr = (BindingFlags)num;
			int num2 = 1543706309;
			goto IL_0008;
			IL_0003:
			num2 = 1123284227;
			goto IL_0008;
			IL_0008:
			while (true)
			{
				uint num3;
				switch ((num3 = (uint)(num2 ^ 0x5A0D3B12)) % 4)
				{
				case 0u:
					break;
				default:
					return;
				case 1u:
					goto IL_0029;
				case 3u:
					mpNTiEnCAkYpVUOkiYKiEmrcsGkp.qsmadcCvBDwckCLfyEFTPxKcJCrH(instance.GetType().GetMethods(bindingAttr), writer);
					num2 = (int)((num3 * 1076077661) ^ 0x3A0825B3);
					continue;
				case 2u:
					return;
				}
				break;
			}
			goto IL_0003;
			IL_0029:
			num = 20;
			goto IL_002f;
		}

		[DebugCommand(Description = "Gets or sets a field with the given name on the context object.")]
		public static IFieldWrapper Field(this object instance, string fieldName)
		{
			Type type = instance.GetType();
			try
			{
				return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.ONkRbvQUZruPdujPEIyNjyYjoAHF(instance, type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, fieldName);
			}
			catch (ReflectionAttemptException innerException)
			{
				throw new CommandExecutionException("Field", $"Error occurred while attempting to reflect into {instance}.\nSee inner exception for more details.", innerException);
			}
		}

		[DebugCommand(Description = "Gets or sets a property with the given name on the context object.")]
		public static IPropertyWrapper Property(this object instance, string propertyName)
		{
			Type type = instance.GetType();
			try
			{
				return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.iwZhMdtbbRbxbjdpskMyLNSWJwIO(instance, type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, propertyName);
			}
			catch (ReflectionAttemptException innerException)
			{
				throw new CommandExecutionException("Property", $"Error occurred while attempting to reflect into {instance}.\nSee inner exception for more details.", innerException);
			}
		}

		[DebugCommand(Description = "Finds a method with the given name on the context object.")]
		public static IMethodWrapper Method(this object instance, string methodName)
		{
			Type type = instance.GetType();
			try
			{
				return mpNTiEnCAkYpVUOkiYKiEmrcsGkp.YOszgTUGxobOSChgEqYAtqFrQhMHA(instance, type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, methodName);
			}
			catch (AmbiguousMatchException innerException)
			{
				throw new CommandExecutionException("Method", $"Method {methodName} has multiple overloads on type {type}.", innerException);
			}
			catch (ReflectionAttemptException innerException2)
			{
				throw new CommandExecutionException("Method", $"Error occurred while attempting to reflect into {instance}.\nSee inner exception for more details.", innerException2);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace BitCode.Debug.MemberWrappers
{
	public class MethodWrapper : IMemberWrapper<MethodInfo>, IInvokableMember, IMemberWrapper, IMethodWrapper
	{
		[CompilerGenerated]
		private readonly MethodInfo DqAdAiGNfZrVcwREwKRmCfInDbts;

		[CompilerGenerated]
		private readonly object piahqRPDQjFphruNZUQLOWSMrBTo;

		public MethodInfo Member
		{
			[CompilerGenerated]
			get
			{
				return DqAdAiGNfZrVcwREwKRmCfInDbts;
			}
		}

		MemberInfo IMemberWrapper.Member => Member;

		public object Context
		{
			[CompilerGenerated]
			get
			{
				return piahqRPDQjFphruNZUQLOWSMrBTo;
			}
		}

		public Type MemberType => Member.ReturnType;

		public MethodWrapper(MethodInfo methodInfo, object context)
		{
			DqAdAiGNfZrVcwREwKRmCfInDbts = methodInfo;
			piahqRPDQjFphruNZUQLOWSMrBTo = context;
		}

		public object Invoke(IParameterResolver resolver, IReadOnlyList<string> tokens, ref int lastResolvedToken)
		{
			ParameterInfo[] parameters = Member.GetParameters();
			object[] array = new object[parameters.Length];
			int num = 0;
			while (true)
			{
				int num2 = 395976155;
				while (true)
				{
					uint num3;
					switch ((num3 = (uint)(num2 ^ 0x52722483)) % 5)
					{
					case 2u:
						break;
					case 1u:
						num2 = (int)(num3 * 1600681479) ^ -1983987912;
						continue;
					case 4u:
						array[num] = resolver.ResolveParameter(parameters[num], tokens, ref lastResolvedToken);
						num++;
						num2 = 1849073232;
						continue;
					case 0u:
					{
						int num4;
						if (num < parameters.Length)
						{
							num2 = 478784106;
							num4 = num2;
						}
						else
						{
							num2 = 1118440915;
							num4 = num2;
						}
						continue;
					}
					default:
						return Member.Invoke(Context, array);
					}
					break;
				}
			}
		}
	}
}

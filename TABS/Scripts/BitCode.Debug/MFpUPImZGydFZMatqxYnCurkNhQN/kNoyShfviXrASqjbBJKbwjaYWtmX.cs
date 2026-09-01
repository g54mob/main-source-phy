using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using BitCode.Debug;
using BitCode.Debug.MemberWrappers;
using BitCode.Debug.TokenResolvers;

namespace MFpUPImZGydFZMatqxYnCurkNhQN
{
	internal class kNoyShfviXrASqjbBJKbwjaYWtmX : IMemberWrapper<PropertyInfo>, IMemberWrapper, IPropertyWrapper, IReadableMember, IWriteableMember
	{
		[CompilerGenerated]
		private readonly PropertyInfo DqAdAiGNfZrVcwREwKRmCfInDbts;

		[CompilerGenerated]
		private readonly object piahqRPDQjFphruNZUQLOWSMrBTo;

		[CompilerGenerated]
		private readonly bool FBFKYhPugIBmSZSPuotFGbYWHFpW;

		[CompilerGenerated]
		private readonly bool HqaBOyTgUGeGgchzMrnrjPQrtxLs;

		public PropertyInfo Member
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

		public Type MemberType => Member.PropertyType;

		public bool CanWrite
		{
			[CompilerGenerated]
			get
			{
				return FBFKYhPugIBmSZSPuotFGbYWHFpW;
			}
		}

		public bool CanRead
		{
			[CompilerGenerated]
			get
			{
				return HqaBOyTgUGeGgchzMrnrjPQrtxLs;
			}
		}

		public kNoyShfviXrASqjbBJKbwjaYWtmX(PropertyInfo P_0, object P_1)
		{
			while (true)
			{
				int num = -1422316876;
				while (true)
				{
					uint num2;
					switch ((num2 = (uint)(num ^ -1464388970)) % 3)
					{
					case 0u:
						break;
					case 2u:
						goto IL_0028;
					default:
						HqaBOyTgUGeGgchzMrnrjPQrtxLs = P_0.CanRead;
						return;
					}
					break;
					IL_0028:
					DqAdAiGNfZrVcwREwKRmCfInDbts = P_0;
					piahqRPDQjFphruNZUQLOWSMrBTo = P_1;
					FBFKYhPugIBmSZSPuotFGbYWHFpW = P_0.CanWrite;
					num = (int)(num2 * 1802889459) ^ -357954810;
				}
			}
		}

		public object GetValue()
		{
			return Member.GetValue(Context);
		}

		public void SetValue(IParameterResolver resolver, string token)
		{
			object value = (resolver.GetResolverForType(MemberType) ?? throw new ReflectionAttemptException(Member.DeclaringType, $"Couldn't resolve value for property {Member}. No resolver registered for type {MemberType}.")).ResolveSingleToken(MemberType, token);
			Member.SetValue(Context, value);
		}
	}
}

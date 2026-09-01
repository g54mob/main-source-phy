using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using BitCode.Debug;
using BitCode.Debug.MemberWrappers;
using BitCode.Debug.TokenResolvers;

namespace MFpUPImZGydFZMatqxYnCurkNhQN
{
	internal class jmObiOtYsUXsXLwFUUlRGnvuiMQk : IMemberWrapper<FieldInfo>, IFieldWrapper, IMemberWrapper, IReadableMember, IWriteableMember
	{
		[CompilerGenerated]
		private readonly FieldInfo DqAdAiGNfZrVcwREwKRmCfInDbts;

		[CompilerGenerated]
		private readonly object piahqRPDQjFphruNZUQLOWSMrBTo;

		[CompilerGenerated]
		private readonly bool FBFKYhPugIBmSZSPuotFGbYWHFpW;

		public FieldInfo Member
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

		public Type MemberType => Member.FieldType;

		public bool CanWrite
		{
			[CompilerGenerated]
			get
			{
				return FBFKYhPugIBmSZSPuotFGbYWHFpW;
			}
		}

		public bool CanRead => true;

		public jmObiOtYsUXsXLwFUUlRGnvuiMQk(FieldInfo P_0, object P_1)
		{
			DqAdAiGNfZrVcwREwKRmCfInDbts = P_0;
			piahqRPDQjFphruNZUQLOWSMrBTo = P_1;
			FBFKYhPugIBmSZSPuotFGbYWHFpW = !P_0.IsInitOnly && !P_0.IsLiteral;
		}

		public object GetValue()
		{
			return Member.GetValue(Context);
		}

		public void SetValue(IParameterResolver resolver, string token)
		{
			object value = (resolver.GetResolverForType(MemberType) ?? throw new ReflectionAttemptException(Member.DeclaringType, $"Couldn't resolve value for field {Member}. No resolver registered for type {MemberType}.")).ResolveSingleToken(MemberType, token);
			Member.SetValue(Context, value);
		}
	}
}

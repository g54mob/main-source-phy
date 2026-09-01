namespace IKVM.Reflection
{
	internal sealed class ParameterInfoWrapper : ParameterInfo
	{
		private readonly MemberInfo member;

		private readonly ParameterInfo forward;

		public override string Name
		{
			get
			{
				return forward.Name;
			}
		}

		public override Type ParameterType
		{
			get
			{
				return forward.ParameterType;
			}
		}

		public override ParameterAttributes Attributes
		{
			get
			{
				return forward.Attributes;
			}
		}

		public override int Position
		{
			get
			{
				return forward.Position;
			}
		}

		public override object RawDefaultValue
		{
			get
			{
				return forward.RawDefaultValue;
			}
		}

		public override MemberInfo Member
		{
			get
			{
				return member;
			}
		}

		public override int MetadataToken
		{
			get
			{
				return forward.MetadataToken;
			}
		}

		internal override Module Module
		{
			get
			{
				return member.Module;
			}
		}

		internal ParameterInfoWrapper(MemberInfo member, ParameterInfo forward)
		{
			this.member = member;
			this.forward = forward;
		}

		public override CustomModifiers __GetCustomModifiers()
		{
			return forward.__GetCustomModifiers();
		}

		public override bool __TryGetFieldMarshal(out FieldMarshal fieldMarshal)
		{
			return forward.__TryGetFieldMarshal(out fieldMarshal);
		}
	}
}

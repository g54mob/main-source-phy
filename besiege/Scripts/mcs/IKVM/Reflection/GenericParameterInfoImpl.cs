namespace IKVM.Reflection
{
	internal sealed class GenericParameterInfoImpl : ParameterInfo
	{
		private readonly GenericMethodInstance method;

		private readonly ParameterInfo parameterInfo;

		public override string Name
		{
			get
			{
				return parameterInfo.Name;
			}
		}

		public override Type ParameterType
		{
			get
			{
				return parameterInfo.ParameterType.BindTypeParameters(method);
			}
		}

		public override ParameterAttributes Attributes
		{
			get
			{
				return parameterInfo.Attributes;
			}
		}

		public override int Position
		{
			get
			{
				return parameterInfo.Position;
			}
		}

		public override object RawDefaultValue
		{
			get
			{
				return parameterInfo.RawDefaultValue;
			}
		}

		public override MemberInfo Member
		{
			get
			{
				return method;
			}
		}

		public override int MetadataToken
		{
			get
			{
				return parameterInfo.MetadataToken;
			}
		}

		internal override Module Module
		{
			get
			{
				return method.Module;
			}
		}

		internal GenericParameterInfoImpl(GenericMethodInstance method, ParameterInfo parameterInfo)
		{
			this.method = method;
			this.parameterInfo = parameterInfo;
		}

		public override CustomModifiers __GetCustomModifiers()
		{
			return parameterInfo.__GetCustomModifiers().Bind(method);
		}

		public override bool __TryGetFieldMarshal(out FieldMarshal fieldMarshal)
		{
			return parameterInfo.__TryGetFieldMarshal(out fieldMarshal);
		}
	}
}

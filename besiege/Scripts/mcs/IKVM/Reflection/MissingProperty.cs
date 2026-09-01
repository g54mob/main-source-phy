namespace IKVM.Reflection
{
	internal sealed class MissingProperty : PropertyInfo
	{
		private readonly Type declaringType;

		private readonly string name;

		private readonly PropertySignature signature;

		public override PropertyAttributes Attributes
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		public override bool CanRead
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		public override bool CanWrite
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		internal override bool IsPublic
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		internal override bool IsNonPrivate
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		internal override bool IsStatic
		{
			get
			{
				throw new MissingMemberException(this);
			}
		}

		internal override PropertySignature PropertySignature
		{
			get
			{
				return signature;
			}
		}

		public override string Name
		{
			get
			{
				return name;
			}
		}

		public override Type DeclaringType
		{
			get
			{
				return declaringType;
			}
		}

		public override Module Module
		{
			get
			{
				return declaringType.Module;
			}
		}

		internal override bool IsBaked
		{
			get
			{
				return declaringType.IsBaked;
			}
		}

		internal MissingProperty(Type declaringType, string name, PropertySignature signature)
		{
			this.declaringType = declaringType;
			this.name = name;
			this.signature = signature;
		}

		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			throw new MissingMemberException(this);
		}

		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			throw new MissingMemberException(this);
		}

		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			throw new MissingMemberException(this);
		}

		public override object GetRawConstantValue()
		{
			throw new MissingMemberException(this);
		}

		internal override int GetCurrentToken()
		{
			throw new MissingMemberException(this);
		}
	}
}

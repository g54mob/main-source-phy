using IKVM.Reflection.Reader;

namespace IKVM.Reflection
{
	internal sealed class MissingTypeParameter : TypeParameterType
	{
		private readonly MemberInfo owner;

		private readonly int index;

		public override Module Module
		{
			get
			{
				return owner.Module;
			}
		}

		public override string Name
		{
			get
			{
				return null;
			}
		}

		public override int GenericParameterPosition
		{
			get
			{
				return index;
			}
		}

		public override MethodBase DeclaringMethod
		{
			get
			{
				return owner as MethodBase;
			}
		}

		public override Type DeclaringType
		{
			get
			{
				return owner as Type;
			}
		}

		internal override bool IsBaked
		{
			get
			{
				return owner.IsBaked;
			}
		}

		internal MissingTypeParameter(Type owner, int index)
			: this(owner, index, 19)
		{
		}

		internal MissingTypeParameter(MethodInfo owner, int index)
			: this(owner, index, 30)
		{
		}

		private MissingTypeParameter(MemberInfo owner, int index, byte sigElementType)
			: base(sigElementType)
		{
			this.owner = owner;
			this.index = index;
		}

		internal override Type BindTypeParameters(IGenericBinder binder)
		{
			if (owner is MethodBase)
			{
				return binder.BindMethodParameter(this);
			}
			return binder.BindTypeParameter(this);
		}
	}
}

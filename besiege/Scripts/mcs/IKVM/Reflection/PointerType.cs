namespace IKVM.Reflection
{
	internal sealed class PointerType : ElementHolderType
	{
		public override Type BaseType
		{
			get
			{
				return null;
			}
		}

		public override TypeAttributes Attributes
		{
			get
			{
				return TypeAttributes.AnsiClass;
			}
		}

		internal static Type Make(Type type, CustomModifiers mods)
		{
			return type.Universe.CanonicalizeType(new PointerType(type, mods));
		}

		private PointerType(Type type, CustomModifiers mods)
			: base(type, mods, 15)
		{
		}

		public override bool Equals(object o)
		{
			return EqualsHelper(o as PointerType);
		}

		public override int GetHashCode()
		{
			return elementType.GetHashCode() * 7;
		}

		internal override string GetSuffix()
		{
			return "*";
		}

		protected override Type Wrap(Type type, CustomModifiers mods)
		{
			return Make(type, mods);
		}
	}
}

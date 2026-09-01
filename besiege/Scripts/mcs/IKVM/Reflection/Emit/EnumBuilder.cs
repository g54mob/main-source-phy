namespace IKVM.Reflection.Emit
{
	public sealed class EnumBuilder : TypeInfo
	{
		private readonly TypeBuilder typeBuilder;

		private readonly FieldBuilder fieldBuilder;

		internal override TypeName TypeName
		{
			get
			{
				return typeBuilder.TypeName;
			}
		}

		public override string Name
		{
			get
			{
				return typeBuilder.Name;
			}
		}

		public override string FullName
		{
			get
			{
				return typeBuilder.FullName;
			}
		}

		public override Type BaseType
		{
			get
			{
				return typeBuilder.BaseType;
			}
		}

		public override TypeAttributes Attributes
		{
			get
			{
				return typeBuilder.Attributes;
			}
		}

		public override Module Module
		{
			get
			{
				return typeBuilder.Module;
			}
		}

		public TypeToken TypeToken
		{
			get
			{
				return typeBuilder.TypeToken;
			}
		}

		public FieldBuilder UnderlyingField
		{
			get
			{
				return fieldBuilder;
			}
		}

		internal override bool IsBaked
		{
			get
			{
				return typeBuilder.IsBaked;
			}
		}

		internal EnumBuilder(TypeBuilder typeBuilder, FieldBuilder fieldBuilder)
			: base(typeBuilder)
		{
			this.typeBuilder = typeBuilder;
			this.fieldBuilder = fieldBuilder;
		}

		public FieldBuilder DefineLiteral(string literalName, object literalValue)
		{
			FieldBuilder obj = typeBuilder.DefineField(literalName, typeBuilder, FieldAttributes.Public | FieldAttributes.Static | FieldAttributes.Literal);
			obj.SetConstant(literalValue);
			return obj;
		}

		public Type CreateType()
		{
			return typeBuilder.CreateType();
		}

		public TypeInfo CreateTypeInfo()
		{
			return typeBuilder.CreateTypeInfo();
		}

		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			typeBuilder.SetCustomAttribute(con, binaryAttribute);
		}

		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			typeBuilder.SetCustomAttribute(customBuilder);
		}

		public override Type GetEnumUnderlyingType()
		{
			return fieldBuilder.FieldType;
		}
	}
}

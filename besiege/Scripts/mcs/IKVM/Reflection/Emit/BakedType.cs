namespace IKVM.Reflection.Emit
{
	internal sealed class BakedType : TypeInfo
	{
		public override string AssemblyQualifiedName
		{
			get
			{
				return underlyingType.AssemblyQualifiedName;
			}
		}

		public override Type BaseType
		{
			get
			{
				return underlyingType.BaseType;
			}
		}

		internal override TypeName TypeName
		{
			get
			{
				return underlyingType.TypeName;
			}
		}

		public override string Name
		{
			get
			{
				return TypeNameParser.Escape(underlyingType.__Name);
			}
		}

		public override string FullName
		{
			get
			{
				return GetFullName();
			}
		}

		public override TypeAttributes Attributes
		{
			get
			{
				return underlyingType.Attributes;
			}
		}

		public override Type DeclaringType
		{
			get
			{
				return underlyingType.DeclaringType;
			}
		}

		public override bool IsGenericType
		{
			get
			{
				return underlyingType.IsGenericType;
			}
		}

		public override bool IsGenericTypeDefinition
		{
			get
			{
				return underlyingType.IsGenericTypeDefinition;
			}
		}

		public override bool ContainsGenericParameters
		{
			get
			{
				return underlyingType.ContainsGenericParameters;
			}
		}

		public override int MetadataToken
		{
			get
			{
				return underlyingType.MetadataToken;
			}
		}

		public override Module Module
		{
			get
			{
				return underlyingType.Module;
			}
		}

		internal override bool IsBaked
		{
			get
			{
				return true;
			}
		}

		internal BakedType(TypeBuilder typeBuilder)
			: base(typeBuilder)
		{
		}

		public override Type[] __GetDeclaredInterfaces()
		{
			return underlyingType.__GetDeclaredInterfaces();
		}

		public override MethodBase[] __GetDeclaredMethods()
		{
			return underlyingType.__GetDeclaredMethods();
		}

		public override __MethodImplMap __GetMethodImplMap()
		{
			return underlyingType.__GetMethodImplMap();
		}

		public override FieldInfo[] __GetDeclaredFields()
		{
			return underlyingType.__GetDeclaredFields();
		}

		public override EventInfo[] __GetDeclaredEvents()
		{
			return underlyingType.__GetDeclaredEvents();
		}

		public override PropertyInfo[] __GetDeclaredProperties()
		{
			return underlyingType.__GetDeclaredProperties();
		}

		public override Type[] __GetDeclaredTypes()
		{
			return underlyingType.__GetDeclaredTypes();
		}

		public override bool __GetLayout(out int packingSize, out int typeSize)
		{
			return underlyingType.__GetLayout(out packingSize, out typeSize);
		}

		public override Type[] GetGenericArguments()
		{
			return underlyingType.GetGenericArguments();
		}

		internal override Type GetGenericTypeArgument(int index)
		{
			return underlyingType.GetGenericTypeArgument(index);
		}

		public override CustomModifiers[] __GetGenericArgumentsCustomModifiers()
		{
			return underlyingType.__GetGenericArgumentsCustomModifiers();
		}

		internal override int GetModuleBuilderToken()
		{
			return underlyingType.GetModuleBuilderToken();
		}
	}
}

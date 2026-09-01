using System.Collections.Generic;

namespace IKVM.Reflection.Reader
{
	internal abstract class TypeParameterType : TypeInfo
	{
		public sealed override string AssemblyQualifiedName
		{
			get
			{
				return null;
			}
		}

		public sealed override bool IsValueType
		{
			get
			{
				return (GenericParameterAttributes & GenericParameterAttributes.NotNullableValueTypeConstraint) != 0;
			}
		}

		public sealed override Type BaseType
		{
			get
			{
				Type[] genericParameterConstraints = GetGenericParameterConstraints();
				foreach (Type type in genericParameterConstraints)
				{
					if (!type.IsInterface && !type.IsGenericParameter)
					{
						return type;
					}
				}
				if (!IsValueType)
				{
					return Module.universe.System_Object;
				}
				return Module.universe.System_ValueType;
			}
		}

		public sealed override TypeAttributes Attributes
		{
			get
			{
				return TypeAttributes.Public;
			}
		}

		public sealed override string FullName
		{
			get
			{
				return null;
			}
		}

		protected sealed override bool ContainsMissingTypeImpl
		{
			get
			{
				return Type.ContainsMissingType(GetGenericParameterConstraints());
			}
		}

		protected TypeParameterType(byte sigElementType)
			: base(sigElementType)
		{
		}

		public override Type[] __GetDeclaredInterfaces()
		{
			List<Type> list = new List<Type>();
			Type[] genericParameterConstraints = GetGenericParameterConstraints();
			foreach (Type type in genericParameterConstraints)
			{
				if (type.IsInterface)
				{
					list.Add(type);
				}
			}
			return list.ToArray();
		}

		public sealed override string ToString()
		{
			return Name;
		}
	}
}

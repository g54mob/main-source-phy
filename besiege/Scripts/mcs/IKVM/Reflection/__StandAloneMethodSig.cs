using System.Runtime.InteropServices;

namespace IKVM.Reflection
{
	public sealed class __StandAloneMethodSig
	{
		private readonly bool unmanaged;

		private readonly CallingConvention unmanagedCallingConvention;

		private readonly CallingConventions callingConvention;

		private readonly Type returnType;

		private readonly Type[] parameterTypes;

		private readonly Type[] optionalParameterTypes;

		private readonly PackedCustomModifiers customModifiers;

		public bool IsUnmanaged
		{
			get
			{
				return unmanaged;
			}
		}

		public CallingConventions CallingConvention
		{
			get
			{
				return callingConvention;
			}
		}

		public CallingConvention UnmanagedCallingConvention
		{
			get
			{
				return unmanagedCallingConvention;
			}
		}

		public Type ReturnType
		{
			get
			{
				return returnType;
			}
		}

		public Type[] ParameterTypes
		{
			get
			{
				return Util.Copy(parameterTypes);
			}
		}

		public Type[] OptionalParameterTypes
		{
			get
			{
				return Util.Copy(optionalParameterTypes);
			}
		}

		public bool ContainsMissingType
		{
			get
			{
				if (!returnType.__ContainsMissingType && !Type.ContainsMissingType(parameterTypes) && !Type.ContainsMissingType(optionalParameterTypes))
				{
					return customModifiers.ContainsMissingType;
				}
				return true;
			}
		}

		internal int ParameterCount
		{
			get
			{
				return parameterTypes.Length + optionalParameterTypes.Length;
			}
		}

		internal __StandAloneMethodSig(bool unmanaged, CallingConvention unmanagedCallingConvention, CallingConventions callingConvention, Type returnType, Type[] parameterTypes, Type[] optionalParameterTypes, PackedCustomModifiers customModifiers)
		{
			this.unmanaged = unmanaged;
			this.unmanagedCallingConvention = unmanagedCallingConvention;
			this.callingConvention = callingConvention;
			this.returnType = returnType;
			this.parameterTypes = parameterTypes;
			this.optionalParameterTypes = optionalParameterTypes;
			this.customModifiers = customModifiers;
		}

		public bool Equals(__StandAloneMethodSig other)
		{
			if (other != null && other.unmanaged == unmanaged && other.unmanagedCallingConvention == unmanagedCallingConvention && other.callingConvention == callingConvention && other.returnType == returnType && Util.ArrayEquals(other.parameterTypes, parameterTypes) && Util.ArrayEquals(other.optionalParameterTypes, optionalParameterTypes))
			{
				return other.customModifiers.Equals(customModifiers);
			}
			return false;
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as __StandAloneMethodSig);
		}

		public override int GetHashCode()
		{
			return returnType.GetHashCode() ^ Util.GetHashCode(parameterTypes);
		}

		public CustomModifiers GetReturnTypeCustomModifiers()
		{
			return customModifiers.GetReturnTypeCustomModifiers();
		}

		public CustomModifiers GetParameterCustomModifiers(int index)
		{
			return customModifiers.GetParameterCustomModifiers(index);
		}
	}
}

namespace IKVM.Reflection
{
	internal sealed class PropertyInfoWithReflectedType : PropertyInfo
	{
		private readonly Type reflectedType;

		private readonly PropertyInfo property;

		public override PropertyAttributes Attributes
		{
			get
			{
				return property.Attributes;
			}
		}

		public override bool CanRead
		{
			get
			{
				return property.CanRead;
			}
		}

		public override bool CanWrite
		{
			get
			{
				return property.CanWrite;
			}
		}

		internal override bool IsPublic
		{
			get
			{
				return property.IsPublic;
			}
		}

		internal override bool IsNonPrivate
		{
			get
			{
				return property.IsNonPrivate;
			}
		}

		internal override bool IsStatic
		{
			get
			{
				return property.IsStatic;
			}
		}

		internal override PropertySignature PropertySignature
		{
			get
			{
				return property.PropertySignature;
			}
		}

		public override bool __IsMissing
		{
			get
			{
				return property.__IsMissing;
			}
		}

		public override Type DeclaringType
		{
			get
			{
				return property.DeclaringType;
			}
		}

		public override Type ReflectedType
		{
			get
			{
				return reflectedType;
			}
		}

		public override int MetadataToken
		{
			get
			{
				return property.MetadataToken;
			}
		}

		public override Module Module
		{
			get
			{
				return property.Module;
			}
		}

		public override string Name
		{
			get
			{
				return property.Name;
			}
		}

		internal override bool IsBaked
		{
			get
			{
				return property.IsBaked;
			}
		}

		internal PropertyInfoWithReflectedType(Type reflectedType, PropertyInfo property)
		{
			this.reflectedType = reflectedType;
			this.property = property;
		}

		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			return MemberInfo.SetReflectedType(property.GetGetMethod(nonPublic), reflectedType);
		}

		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			return MemberInfo.SetReflectedType(property.GetSetMethod(nonPublic), reflectedType);
		}

		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			return MemberInfo.SetReflectedType(property.GetAccessors(nonPublic), reflectedType);
		}

		public override object GetRawConstantValue()
		{
			return property.GetRawConstantValue();
		}

		public override ParameterInfo[] GetIndexParameters()
		{
			ParameterInfo[] indexParameters = property.GetIndexParameters();
			for (int i = 0; i < indexParameters.Length; i++)
			{
				indexParameters[i] = new ParameterInfoWrapper(this, indexParameters[i]);
			}
			return indexParameters;
		}

		internal override PropertyInfo BindTypeParameters(Type type)
		{
			return property.BindTypeParameters(type);
		}

		public override string ToString()
		{
			return property.ToString();
		}

		public override bool Equals(object obj)
		{
			PropertyInfoWithReflectedType propertyInfoWithReflectedType = obj as PropertyInfoWithReflectedType;
			if (propertyInfoWithReflectedType != null && propertyInfoWithReflectedType.reflectedType == reflectedType)
			{
				return propertyInfoWithReflectedType.property == property;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return reflectedType.GetHashCode() ^ property.GetHashCode();
		}

		internal override int GetCurrentToken()
		{
			return property.GetCurrentToken();
		}
	}
}

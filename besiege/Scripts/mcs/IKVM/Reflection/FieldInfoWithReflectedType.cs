using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	internal sealed class FieldInfoWithReflectedType : FieldInfo
	{
		private readonly Type reflectedType;

		private readonly FieldInfo field;

		public override FieldAttributes Attributes
		{
			get
			{
				return field.Attributes;
			}
		}

		public override int __FieldRVA
		{
			get
			{
				return field.__FieldRVA;
			}
		}

		internal override FieldSignature FieldSignature
		{
			get
			{
				return field.FieldSignature;
			}
		}

		public override bool __IsMissing
		{
			get
			{
				return field.__IsMissing;
			}
		}

		public override Type DeclaringType
		{
			get
			{
				return field.DeclaringType;
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
				return field.MetadataToken;
			}
		}

		public override Module Module
		{
			get
			{
				return field.Module;
			}
		}

		public override string Name
		{
			get
			{
				return field.Name;
			}
		}

		internal override bool IsBaked
		{
			get
			{
				return field.IsBaked;
			}
		}

		internal FieldInfoWithReflectedType(Type reflectedType, FieldInfo field)
		{
			this.reflectedType = reflectedType;
			this.field = field;
		}

		public override void __GetDataFromRVA(byte[] data, int offset, int length)
		{
			field.__GetDataFromRVA(data, offset, length);
		}

		public override bool __TryGetFieldOffset(out int offset)
		{
			return field.__TryGetFieldOffset(out offset);
		}

		public override object GetRawConstantValue()
		{
			return field.GetRawConstantValue();
		}

		public override FieldInfo __GetFieldOnTypeDefinition()
		{
			return field.__GetFieldOnTypeDefinition();
		}

		internal override int ImportTo(ModuleBuilder module)
		{
			return field.ImportTo(module);
		}

		internal override FieldInfo BindTypeParameters(Type type)
		{
			return field.BindTypeParameters(type);
		}

		public override bool Equals(object obj)
		{
			FieldInfoWithReflectedType fieldInfoWithReflectedType = obj as FieldInfoWithReflectedType;
			if (fieldInfoWithReflectedType != null && fieldInfoWithReflectedType.reflectedType == reflectedType)
			{
				return fieldInfoWithReflectedType.field == field;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return reflectedType.GetHashCode() ^ field.GetHashCode();
		}

		public override string ToString()
		{
			return field.ToString();
		}

		internal override int GetCurrentToken()
		{
			return field.GetCurrentToken();
		}
	}
}

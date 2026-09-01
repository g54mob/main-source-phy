using IKVM.Reflection.Emit;

namespace IKVM.Reflection
{
	internal sealed class GenericFieldInstance : FieldInfo
	{
		private readonly Type declaringType;

		private readonly FieldInfo field;

		public override FieldAttributes Attributes
		{
			get
			{
				return field.Attributes;
			}
		}

		public override string Name
		{
			get
			{
				return field.Name;
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

		public override int MetadataToken
		{
			get
			{
				return field.MetadataToken;
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
				return field.FieldSignature.ExpandTypeParameters(declaringType);
			}
		}

		internal override bool IsBaked
		{
			get
			{
				return field.IsBaked;
			}
		}

		internal GenericFieldInstance(Type declaringType, FieldInfo field)
		{
			this.declaringType = declaringType;
			this.field = field;
		}

		public override bool Equals(object obj)
		{
			GenericFieldInstance genericFieldInstance = obj as GenericFieldInstance;
			if (genericFieldInstance != null && genericFieldInstance.declaringType.Equals(declaringType))
			{
				return genericFieldInstance.field.Equals(field);
			}
			return false;
		}

		public override int GetHashCode()
		{
			return (declaringType.GetHashCode() * 3) ^ field.GetHashCode();
		}

		public override object GetRawConstantValue()
		{
			return field.GetRawConstantValue();
		}

		public override void __GetDataFromRVA(byte[] data, int offset, int length)
		{
			field.__GetDataFromRVA(data, offset, length);
		}

		public override bool __TryGetFieldOffset(out int offset)
		{
			return field.__TryGetFieldOffset(out offset);
		}

		public override FieldInfo __GetFieldOnTypeDefinition()
		{
			return field;
		}

		internal override int ImportTo(ModuleBuilder module)
		{
			return module.ImportMethodOrField(declaringType, field.Name, field.FieldSignature);
		}

		internal override FieldInfo BindTypeParameters(Type type)
		{
			return new GenericFieldInstance(declaringType.BindTypeParameters(type), field);
		}

		internal override int GetCurrentToken()
		{
			return field.GetCurrentToken();
		}
	}
}

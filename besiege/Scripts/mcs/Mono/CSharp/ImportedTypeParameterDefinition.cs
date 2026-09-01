using System;

namespace Mono.CSharp
{
	internal class ImportedTypeParameterDefinition : ImportedDefinition, ITypeDefinition, IMemberDefinition
	{
		public IAssemblyDefinition DeclaringAssembly
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		bool ITypeDefinition.IsComImport
		{
			get
			{
				return false;
			}
		}

		bool ITypeDefinition.IsPartial
		{
			get
			{
				return false;
			}
		}

		bool ITypeDefinition.IsTypeForwarder
		{
			get
			{
				return false;
			}
		}

		bool ITypeDefinition.IsCyclicTypeForwarder
		{
			get
			{
				return false;
			}
		}

		public string Namespace
		{
			get
			{
				return null;
			}
		}

		public int TypeParametersCount
		{
			get
			{
				return 0;
			}
		}

		public TypeParameterSpec[] TypeParameters
		{
			get
			{
				return null;
			}
		}

		public ImportedTypeParameterDefinition(Type type, MetadataImporter importer)
			: base(type, importer)
		{
		}

		public TypeSpec GetAttributeCoClass()
		{
			return null;
		}

		public string GetAttributeDefaultMember()
		{
			throw new NotSupportedException();
		}

		public AttributeUsageAttribute GetAttributeUsage(PredefinedAttribute pa)
		{
			throw new NotSupportedException();
		}

		bool ITypeDefinition.IsInternalAsPublic(IAssemblyDefinition assembly)
		{
			throw new NotImplementedException();
		}

		public void LoadMembers(TypeSpec declaringType, bool onlyTypes, ref MemberCache cache)
		{
			throw new NotImplementedException();
		}
	}
}

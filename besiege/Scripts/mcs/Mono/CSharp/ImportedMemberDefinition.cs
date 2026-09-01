using System.Reflection;

namespace Mono.CSharp
{
	internal class ImportedMemberDefinition : ImportedDefinition
	{
		private readonly TypeSpec type;

		public TypeSpec MemberType
		{
			get
			{
				return type;
			}
		}

		public ImportedMemberDefinition(MemberInfo member, TypeSpec type, MetadataImporter importer)
			: base(member, importer)
		{
			this.type = type;
		}
	}
}

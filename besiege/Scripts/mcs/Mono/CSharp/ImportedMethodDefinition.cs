using System.Reflection;

namespace Mono.CSharp
{
	internal class ImportedMethodDefinition : ImportedParameterMemberDefinition, IMethodDefinition, IMemberDefinition
	{
		MethodBase IMethodDefinition.Metadata
		{
			get
			{
				return (MethodBase)provider;
			}
		}

		public ImportedMethodDefinition(MethodBase provider, TypeSpec type, AParametersCollection parameters, MetadataImporter importer)
			: base(provider, type, parameters, importer)
		{
		}
	}
}

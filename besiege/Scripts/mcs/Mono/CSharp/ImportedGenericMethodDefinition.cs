using System.Reflection;

namespace Mono.CSharp
{
	internal class ImportedGenericMethodDefinition : ImportedMethodDefinition, IGenericMethodDefinition, IMethodDefinition, IMemberDefinition
	{
		private readonly TypeParameterSpec[] tparams;

		public TypeParameterSpec[] TypeParameters
		{
			get
			{
				return tparams;
			}
		}

		public int TypeParametersCount
		{
			get
			{
				return tparams.Length;
			}
		}

		public ImportedGenericMethodDefinition(MethodInfo provider, TypeSpec type, AParametersCollection parameters, TypeParameterSpec[] tparams, MetadataImporter importer)
			: base(provider, type, parameters, importer)
		{
			this.tparams = tparams;
		}
	}
}

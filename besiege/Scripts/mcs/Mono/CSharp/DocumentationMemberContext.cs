using System;

namespace Mono.CSharp
{
	internal sealed class DocumentationMemberContext : IMemberContext, IModuleContext
	{
		private readonly MemberCore host;

		private MemberName contextName;

		public TypeSpec CurrentType
		{
			get
			{
				return host.CurrentType;
			}
		}

		public TypeParameters CurrentTypeParameters
		{
			get
			{
				return contextName.TypeParameters;
			}
		}

		public MemberCore CurrentMemberDefinition
		{
			get
			{
				return host.CurrentMemberDefinition;
			}
		}

		public bool IsObsolete
		{
			get
			{
				return false;
			}
		}

		public bool IsUnsafe
		{
			get
			{
				return host.IsStatic;
			}
		}

		public bool IsStatic
		{
			get
			{
				return host.IsStatic;
			}
		}

		public ModuleContainer Module
		{
			get
			{
				return host.Module;
			}
		}

		public DocumentationMemberContext(MemberCore host, MemberName contextName)
		{
			this.host = host;
			this.contextName = contextName;
		}

		public string GetSignatureForError()
		{
			return host.GetSignatureForError();
		}

		public ExtensionMethodCandidates LookupExtensionMethod(string name, int arity)
		{
			return null;
		}

		public FullNamedExpression LookupNamespaceOrType(string name, int arity, LookupMode mode, Location loc)
		{
			if (arity == 0)
			{
				TypeParameters currentTypeParameters = CurrentTypeParameters;
				if (currentTypeParameters != null)
				{
					for (int i = 0; i < currentTypeParameters.Count; i++)
					{
						TypeParameter typeParameter = currentTypeParameters[i];
						if (typeParameter.Name == name)
						{
							typeParameter.Type.DeclaredPosition = i;
							return new TypeParameterExpr(typeParameter, loc);
						}
					}
				}
			}
			return host.Parent.LookupNamespaceOrType(name, arity, mode, loc);
		}

		public FullNamedExpression LookupNamespaceAlias(string name)
		{
			throw new NotImplementedException();
		}
	}
}

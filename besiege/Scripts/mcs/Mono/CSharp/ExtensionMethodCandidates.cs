using System.Collections.Generic;

namespace Mono.CSharp
{
	public class ExtensionMethodCandidates
	{
		private readonly NamespaceContainer container;

		private readonly IList<MethodSpec> methods;

		private readonly int index;

		private readonly IMemberContext context;

		public NamespaceContainer Container
		{
			get
			{
				return container;
			}
		}

		public IMemberContext Context
		{
			get
			{
				return context;
			}
		}

		public int LookupIndex
		{
			get
			{
				return index;
			}
		}

		public IList<MethodSpec> Methods
		{
			get
			{
				return methods;
			}
		}

		public ExtensionMethodCandidates(IMemberContext context, IList<MethodSpec> methods, NamespaceContainer nsContainer, int lookupIndex)
		{
			this.context = context;
			this.methods = methods;
			container = nsContainer;
			index = lookupIndex;
		}
	}
}

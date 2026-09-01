using System;
using System.Collections.Generic;
using System.IO;

namespace IKVM.Reflection.Emit
{
	internal sealed class ManifestModule : NonPEModule
	{
		private readonly AssemblyBuilder assembly;

		private readonly Guid guid = Guid.NewGuid();

		public override int MDStreamVersion
		{
			get
			{
				return assembly.mdStreamVersion;
			}
		}

		public override Assembly Assembly
		{
			get
			{
				return assembly;
			}
		}

		public override string FullyQualifiedName
		{
			get
			{
				return Path.Combine(assembly.dir, "RefEmit_InMemoryManifestModule");
			}
		}

		public override string Name
		{
			get
			{
				return "<In Memory Module>";
			}
		}

		public override Guid ModuleVersionId
		{
			get
			{
				return guid;
			}
		}

		public override string ScopeName
		{
			get
			{
				return "RefEmit_InMemoryManifestModule";
			}
		}

		internal ManifestModule(AssemblyBuilder assembly)
			: base(assembly.universe)
		{
			this.assembly = assembly;
		}

		internal override Type FindType(TypeName typeName)
		{
			return null;
		}

		internal override Type FindTypeIgnoreCase(TypeName lowerCaseName)
		{
			return null;
		}

		internal override void GetTypesImpl(List<Type> list)
		{
		}

		protected override Exception NotSupportedException()
		{
			return new InvalidOperationException();
		}
	}
}

using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using Mono.CSharp;

namespace Microsoft.CSharp
{
	/// <summary>Provides access to instances of the C# code generator and code compiler.</summary>
	public class CSharpCodeProvider : CodeDomProvider
	{
		private IDictionary<string, string> providerOptions;

		/// <summary>Gets the file name extension to use when creating source code files.</summary>
		/// <returns>The file name extension to use for generated source code files.</returns>
		public override string FileExtension
		{
			get
			{
				return "cs";
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.CSharp.CSharpCodeProvider" /> class. </summary>
		public CSharpCodeProvider()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.CSharp.CSharpCodeProvider" /> class by using the specified provider options. </summary>
		/// <param name="providerOptions">A <see cref="T:System.Collections.Generic.IDictionary`2" /> object that contains the provider options from the configuration file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerOptions" /> is null.</exception>
		public CSharpCodeProvider(IDictionary<string, string> providerOptions)
		{
			this.providerOptions = providerOptions;
		}

		/// <summary>Gets an instance of the C# code compiler.</summary>
		/// <returns>An instance of the C# <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> implementation.</returns>
		[Obsolete("Use CodeDomProvider class")]
		public override ICodeCompiler CreateCompiler()
		{
			if (providerOptions != null && providerOptions.Count > 0)
			{
				return new Mono.CSharp.CSharpCodeCompiler(providerOptions);
			}
			return new Mono.CSharp.CSharpCodeCompiler();
		}

		/// <summary>Gets an instance of the C# code generator.</summary>
		/// <returns>An instance of the C# <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> implementation.</returns>
		[Obsolete("Use CodeDomProvider class")]
		public override ICodeGenerator CreateGenerator()
		{
			if (providerOptions != null && providerOptions.Count > 0)
			{
				return new Mono.CSharp.CSharpCodeGenerator(providerOptions);
			}
			return new Mono.CSharp.CSharpCodeGenerator();
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type of object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type.</returns>
		/// <param name="type">The type of object to retrieve a type converter for. </param>
		[System.MonoTODO]
		public override TypeConverter GetConverter(Type Type)
		{
			throw new NotImplementedException();
		}

		/// <summary>Generates code for the specified class member using the specified text writer and code generator options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> to generate code for.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		/// <param name="options">The <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> to use when generating the code.</param>
		[System.MonoTODO]
		public override void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			throw new NotImplementedException();
		}
	}
}

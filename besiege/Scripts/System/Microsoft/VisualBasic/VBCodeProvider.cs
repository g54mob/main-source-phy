using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;

namespace Microsoft.VisualBasic
{
	/// <summary>Provides access to instances of the Visual Basic code generator and code compiler.</summary>
	/// <filterpriority>2</filterpriority>
	public class VBCodeProvider : CodeDomProvider
	{
		/// <summary>Gets the file name extension to use when creating source code files.</summary>
		/// <returns>The file name extension to use for generated source code files.</returns>
		/// <filterpriority>2</filterpriority>
		public override string FileExtension
		{
			get
			{
				return "vb";
			}
		}

		/// <summary>Gets a language features identifier.</summary>
		/// <returns>A <see cref="T:System.CodeDom.Compiler.LanguageOptions" /> that indicates special features of the language.</returns>
		/// <filterpriority>2</filterpriority>
		public override LanguageOptions LanguageOptions
		{
			get
			{
				return LanguageOptions.CaseInsensitive;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.VBCodeProvider" /> class. </summary>
		public VBCodeProvider()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:Microsoft.VisualBasic.VBCodeProvider" /> class by using the specified provider options. </summary>
		/// <param name="providerOptions">A <see cref="T:System.Collections.Generic.IDictionary`2" /> object that contains the provider options from the configuration file.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="providerOptions" /> is null.</exception>
		public VBCodeProvider(IDictionary<string, string> providerOptions)
		{
		}

		/// <summary>Gets an instance of the Visual Basic code compiler.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeCompiler" /> implementation.</returns>
		/// <filterpriority>2</filterpriority>
		[Obsolete("Use CodeDomProvider class")]
		public override ICodeCompiler CreateCompiler()
		{
			return new Microsoft.VisualBasic.VBCodeCompiler();
		}

		/// <summary>Gets an instance of the Visual Basic code generator.</summary>
		/// <returns>An instance of the Visual Basic <see cref="T:System.CodeDom.Compiler.ICodeGenerator" /> implementation.</returns>
		/// <filterpriority>2</filterpriority>
		[Obsolete("Use CodeDomProvider class")]
		public override ICodeGenerator CreateGenerator()
		{
			return new Microsoft.VisualBasic.VBCodeGenerator();
		}

		/// <summary>Gets a <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type of object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the specified type.</returns>
		/// <param name="type">The type of object to retrieve a type converter for. </param>
		/// <filterpriority>2</filterpriority>
		public override TypeConverter GetConverter(Type type)
		{
			return TypeDescriptor.GetConverter(type);
		}

		/// <summary>Generates code for the specified class member using the specified text writer and code generator options.</summary>
		/// <param name="member">A <see cref="T:System.CodeDom.CodeTypeMember" /> to generate code for.</param>
		/// <param name="writer">The <see cref="T:System.IO.TextWriter" /> to write to.</param>
		/// <param name="options">The <see cref="T:System.CodeDom.Compiler.CodeGeneratorOptions" /> to use when generating the code.</param>
		/// <filterpriority>1</filterpriority>
		[System.MonoTODO]
		public override void GenerateCodeFromMember(CodeTypeMember member, TextWriter writer, CodeGeneratorOptions options)
		{
			throw new NotImplementedException();
		}
	}
}

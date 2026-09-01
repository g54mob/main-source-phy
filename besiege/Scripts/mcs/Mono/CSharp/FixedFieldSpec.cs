using System.Reflection;

namespace Mono.CSharp
{
	internal class FixedFieldSpec : FieldSpec
	{
		private readonly FieldSpec element;

		public FieldSpec Element
		{
			get
			{
				return element;
			}
		}

		public TypeSpec ElementType
		{
			get
			{
				return element.MemberType;
			}
		}

		public FixedFieldSpec(ModuleContainer module, TypeSpec declaringType, IMemberDefinition definition, FieldInfo info, FieldSpec element, Modifiers modifiers)
			: base(declaringType, definition, PointerContainer.MakeType(module, element.MemberType), info, modifiers)
		{
			this.element = element;
			state &= ~StateFlags.CLSCompliant_Undetected;
		}
	}
}

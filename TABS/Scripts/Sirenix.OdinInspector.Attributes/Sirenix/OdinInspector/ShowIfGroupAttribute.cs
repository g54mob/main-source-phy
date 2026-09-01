using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[Conditional("UNITY_EDITOR")]
	public class ShowIfGroupAttribute : PropertyGroupAttribute
	{
		private string memberName;

		public bool Animate;

		public object Value;

		public string MemberName
		{
			get
			{
				if (!string.IsNullOrEmpty(memberName))
				{
					return memberName;
				}
				return GroupName;
			}
			set
			{
				memberName = value;
			}
		}

		public ShowIfGroupAttribute(string path, bool animate = true)
			: base(path)
		{
			Animate = animate;
		}

		public ShowIfGroupAttribute(string path, object value, bool animate = true)
			: base(path)
		{
			Value = value;
			Animate = animate;
		}

		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			ShowIfGroupAttribute showIfGroupAttribute = other as ShowIfGroupAttribute;
			if (!string.IsNullOrEmpty(memberName))
			{
				showIfGroupAttribute.memberName = memberName;
			}
			if (!Animate)
			{
				showIfGroupAttribute.Animate = Animate;
			}
			if (Value != null)
			{
				showIfGroupAttribute.Value = Value;
			}
		}
	}
}

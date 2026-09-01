using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	[Conditional("UNITY_EDITOR")]
	public class HideIfGroupAttribute : PropertyGroupAttribute
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

		public HideIfGroupAttribute(string path, bool animate = true)
			: base(path)
		{
			Animate = animate;
		}

		public HideIfGroupAttribute(string path, object value, bool animate = true)
			: base(path)
		{
			Value = value;
			Animate = animate;
		}

		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			HideIfGroupAttribute hideIfGroupAttribute = other as HideIfGroupAttribute;
			if (!string.IsNullOrEmpty(memberName))
			{
				hideIfGroupAttribute.memberName = memberName;
			}
			if (!Animate)
			{
				hideIfGroupAttribute.Animate = Animate;
			}
			if (Value != null)
			{
				hideIfGroupAttribute.Value = Value;
			}
		}
	}
}

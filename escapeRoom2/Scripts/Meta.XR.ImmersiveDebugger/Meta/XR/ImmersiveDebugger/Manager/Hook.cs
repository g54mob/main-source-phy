using System.Reflection;

namespace Meta.XR.ImmersiveDebugger.Manager
{
	internal abstract class Hook
	{
		private readonly DebugMember _attribute;

		protected readonly MemberInfo _memberInfo;

		protected readonly object _instance;

		public DebugMember Attribute => _attribute;

		protected Hook(MemberInfo memberInfo, object instance, DebugMember attribute)
		{
			_memberInfo = memberInfo;
			_instance = instance;
			_attribute = attribute;
		}

		public bool Matches(MemberInfo memberInfo, object instance)
		{
			if (_memberInfo == memberInfo)
			{
				return _instance == instance;
			}
			return false;
		}
	}
}

using System;
using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblSocialManagerEvent
	{
		public XUserHandle User { get; private set; }

		public XblSocialManagerEventType EventType { get; private set; }

		public int Hr { get; private set; }

		public XblSocialManagerUserGroupHandle LoadedGroup { get; private set; }

		public XblSocialManagerUser[] UsersAffected { get; private set; }

		internal XblSocialManagerEvent(XGamingRuntime.Interop.XblSocialManagerEvent interopEvent)
		{
			User = new XUserHandle(interopEvent.user);
			EventType = interopEvent.eventType;
			Hr = interopEvent.hr;
			LoadedGroup = new XblSocialManagerUserGroupHandle(interopEvent.loadedGroup);
			UsersAffected = Array.ConvertAll(interopEvent.GetUserArray(), (XGamingRuntime.Interop.XblSocialManagerUser u) => new XblSocialManagerUser(u));
		}
	}
}

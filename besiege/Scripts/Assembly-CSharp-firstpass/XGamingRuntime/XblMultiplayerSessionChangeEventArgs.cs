using XGamingRuntime.Interop;

namespace XGamingRuntime
{
	public class XblMultiplayerSessionChangeEventArgs
	{
		public XblMultiplayerSessionReference SessionReference { get; private set; }

		public string Branch { get; private set; }

		public ulong ChangeNumber { get; private set; }

		internal XblMultiplayerSessionChangeEventArgs(XGamingRuntime.Interop.XblMultiplayerSessionChangeEventArgs interopStruct)
		{
			SessionReference = new XblMultiplayerSessionReference(interopStruct.SessionReference);
			Branch = interopStruct.GetBranch();
			ChangeNumber = interopStruct.ChangeNumber;
		}
	}
}

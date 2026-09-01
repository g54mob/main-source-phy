namespace Photon.Bolt
{
	[Documentation]
	public class NetworkArray_ProtocolToken : NetworkArray_Values<IProtocolToken>
	{
		internal NetworkArray_ProtocolToken(int length, int stride)
			: base(length, stride)
		{
		}

		protected override IProtocolToken GetValue(int index)
		{
			return Storage.Values[index].ProtocolToken;
		}

		protected override bool SetValue(int index, IProtocolToken value)
		{
			if (Storage.Values[index].ProtocolToken != value)
			{
				Storage.Values[index].ProtocolToken.Release();
				Storage.Values[index].ProtocolToken = value;
				return true;
			}
			return false;
		}
	}
}

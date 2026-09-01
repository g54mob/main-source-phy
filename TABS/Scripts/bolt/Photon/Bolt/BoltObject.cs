namespace Photon.Bolt
{
	public class BoltObject
	{
		[Documentation(Ignore = true)]
		public static implicit operator bool(BoltObject obj)
		{
			return obj != null;
		}
	}
}

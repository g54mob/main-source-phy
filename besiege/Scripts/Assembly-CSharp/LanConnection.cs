public class LanConnection : UnetConnection
{
	protected override bool IsLAN
	{
		get
		{
			return true;
		}
	}
}

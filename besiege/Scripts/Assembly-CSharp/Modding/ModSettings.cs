namespace Modding
{
	public class ModSettings : SingleInstanceFindOnly<ModSettings>
	{
		public override string Name
		{
			get
			{
				return "ModSettings";
			}
		}
	}
}

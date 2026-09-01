using UnityEngine.UI;

public class RegionOptionData : Dropdown.OptionData
{
	public Region Region { get; private set; }

	public RegionOptionData(Region region, string text)
		: base(text)
	{
		Region = region;
	}
}

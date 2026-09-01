using UnityEngine;

namespace Kamgam.SettingsGenerator;

public abstract class ColorOptionConnectionSO : ConnectionSO, IConnectionSO<IConnectionWithOptions<Color>>
{
	public abstract IConnectionWithOptions<Color> GetConnection();

	public override SettingData.DataType GetDataType()
	{
		return SettingData.DataType.ColorOption;
	}
}

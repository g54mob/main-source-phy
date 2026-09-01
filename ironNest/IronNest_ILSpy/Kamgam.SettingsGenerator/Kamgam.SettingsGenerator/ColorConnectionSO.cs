using UnityEngine;

namespace Kamgam.SettingsGenerator;

public abstract class ColorConnectionSO : ConnectionSO, IConnectionSO<IConnection<Color>>
{
	public abstract IConnection<Color> GetConnection();

	public override SettingData.DataType GetDataType()
	{
		return SettingData.DataType.Color;
	}
}

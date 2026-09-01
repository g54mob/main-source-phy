using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public abstract class ConnectionSO : ScriptableObject
	{
		public abstract void DestroyConnection();

		public abstract SettingData.DataType GetDataType();
	}
}

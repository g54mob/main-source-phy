using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public interface ISettingWithConnection<TValue> : ISettingWithValue<TValue>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
	{
		void SetConnection(IConnection<TValue> connection);

		IConnection<TValue> GetConnection();
	}
}

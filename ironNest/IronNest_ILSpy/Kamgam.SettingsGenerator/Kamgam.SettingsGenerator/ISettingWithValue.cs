using System;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public interface ISettingWithValue<TValue> : ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
{
	TValue GetValue();

	void SetValue(TValue value, bool propagateChange = true);

	void SetDefaultFromConnection(IConnection<TValue> connection);

	void AddChangeListener(Action<TValue> onChanged);

	void RemoveChangeListener(Action<TValue> onChanged);

	void AddPulledFromConnectionListener(Action<TValue> onPulled);

	void RemovePulledFromConnectionListener(Action<TValue> onPulled);
}

using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator;

public interface ISettingWithOptions<TOption> : ISettingWithConnection<int>, ISettingWithValue<int>, ISetting, ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
{
	bool HasOptions();

	List<TOption> GetOptionLabels();

	void SetOptionLabels(List<TOption> options);

	bool GetOverrideConnectionLabels();

	void SetOverrideConnectionLabels(bool overrideLabels);
}

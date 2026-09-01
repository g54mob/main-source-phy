using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kamgam.SettingsGenerator
{
	public interface ISetting : ISerializationCallbackReceiver, IQualityChangeReceiver, ISettingWithConnectionSO
	{
		bool IsActive { get; set; }

		event Action<ISetting> OnSettingChanged;

		bool HasUserData();

		void SetHasUserData(bool hasUserData);

		string GetID();

		bool MatchesID(string path);

		SettingData.DataType GetDataType();

		bool MatchesAnyDataType(IList<SettingData.DataType> dataTypes);

		List<string> GetGroups();

		void SetGroups(List<string> groups);

		bool MatchesAnyGroup(string[] groups);

		object GetValueAsObject();

		void SetValueFromObject(object value, bool propagateChange = true);

		void ResetToDefault();

		void ResetToUnappliedValue(bool propagateChange = true);

		SettingData SerializeValueToData();

		void DeserializeValueFromData(SettingData data);

		void OnChanged();

		void AddPulledFromConnectionListener(Action onPulled);

		void RemovePulledFromConnectionListener(Action onPulled);

		void Apply();

		bool HasUnappliedChanges();

		void MarkAsChanged();

		void MarkAsUnchanged();

		void InitializeConnection();

		bool HasConnection();

		bool HasConnectionObject();

		int GetConnectionOrder();

		void PullFromConnection();

		void PullFromConnection(bool propagateChange);

		void PushToConnection();

		IConnection GetConnectionInterface();
	}
}

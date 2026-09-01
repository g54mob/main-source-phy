using System;

namespace PlayFab
{
	internal class PlayFabSettingsRedirect : PlayFabApiSettings
	{
		private readonly Func<PlayFabSharedSettings> GetSO;

		public override string ProductionEnvironmentUrl
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				return (!(playFabSharedSettings == null)) ? playFabSharedSettings.ProductionEnvironmentUrl : base.ProductionEnvironmentUrl;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.ProductionEnvironmentUrl = value;
				}
				base.ProductionEnvironmentUrl = value;
			}
		}

		internal override string VerticalName
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				return (!(playFabSharedSettings == null)) ? playFabSharedSettings.VerticalName : base.VerticalName;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.VerticalName = value;
				}
				base.VerticalName = value;
			}
		}

		public override string TitleId
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				return (!(playFabSharedSettings == null)) ? playFabSharedSettings.TitleId : base.TitleId;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.TitleId = value;
				}
				base.TitleId = value;
			}
		}

		public override string AdvertisingIdType
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				return (!(playFabSharedSettings == null)) ? playFabSharedSettings.AdvertisingIdType : base.AdvertisingIdType;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.AdvertisingIdType = value;
				}
				base.AdvertisingIdType = value;
			}
		}

		public override string AdvertisingIdValue
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				return (!(playFabSharedSettings == null)) ? playFabSharedSettings.AdvertisingIdValue : base.AdvertisingIdValue;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.AdvertisingIdValue = value;
				}
				base.AdvertisingIdValue = value;
			}
		}

		public override bool DisableAdvertising
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				return (!(playFabSharedSettings == null)) ? playFabSharedSettings.DisableAdvertising : base.DisableAdvertising;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.DisableAdvertising = value;
				}
				base.DisableAdvertising = value;
			}
		}

		public override bool DisableDeviceInfo
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				return (!(playFabSharedSettings == null)) ? playFabSharedSettings.DisableDeviceInfo : base.DisableDeviceInfo;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.DisableDeviceInfo = value;
				}
				base.DisableDeviceInfo = value;
			}
		}

		public override bool DisableFocusTimeCollection
		{
			get
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				return (!(playFabSharedSettings == null)) ? playFabSharedSettings.DisableFocusTimeCollection : base.DisableFocusTimeCollection;
			}
			set
			{
				PlayFabSharedSettings playFabSharedSettings = GetSO();
				if (playFabSharedSettings != null)
				{
					playFabSharedSettings.DisableFocusTimeCollection = value;
				}
				base.DisableFocusTimeCollection = value;
			}
		}

		public PlayFabSettingsRedirect(Func<PlayFabSharedSettings> getSO)
		{
			GetSO = getSO;
		}
	}
}

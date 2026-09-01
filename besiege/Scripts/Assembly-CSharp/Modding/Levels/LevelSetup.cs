namespace Modding.Levels
{
	public class LevelSetup
	{
		public string Name
		{
			get
			{
				return InternalObject.Name;
			}
		}

		public int MusicID
		{
			get
			{
				return InternalObject.MusicID;
			}
			set
			{
				InternalObject.MusicID = value;
				OnUpdateSettings();
			}
		}

		public LevelSettings.LevelEnvironment Environment
		{
			get
			{
				return InternalObject.Environment;
			}
			set
			{
				InternalObject.Environment = value;
				OnUpdateSettings();
			}
		}

		public bool VoteMode
		{
			get
			{
				return InternalObject.UseVoting;
			}
			set
			{
				InternalObject.UseVoting = value;
				OnUpdateSettings();
			}
		}

		public bool CurtainMode
		{
			get
			{
				return InternalObject.CurtainMode;
			}
			set
			{
				InternalObject.CurtainMode = value;
				OnUpdateSettings();
			}
		}

		public bool HidePlayerLabels
		{
			get
			{
				return InternalObject.HidePlayerLabels;
			}
			set
			{
				InternalObject.HidePlayerLabels = value;
				OnUpdateSettings();
			}
		}

		public bool AllowCopyMachines
		{
			get
			{
				return InternalObject.AllowCopyMachine;
			}
			set
			{
				InternalObject.AllowCopyMachine = value;
				OnUpdateSettings();
			}
		}

		public bool AllowExcessPlayers
		{
			get
			{
				return InternalObject.AllowExcessPlayers;
			}
			set
			{
				InternalObject.AllowExcessPlayers = value;
				OnUpdateSettings();
			}
		}

		public int WaterHeight
		{
			get
			{
				return InternalObject.WaterHeight;
			}
			set
			{
				InternalObject.WaterHeight = value;
				OnUpdateSettings();
			}
		}

		public int EnvType
		{
			get
			{
				return InternalObject.EnvType;
			}
			set
			{
				InternalObject.EnvType = value;
				OnUpdateSettings();
			}
		}

		public int MinPlayers
		{
			get
			{
				return InternalObject.MinPlayers;
			}
			set
			{
				InternalObject.MinPlayers = value;
				OnUpdateSettings();
			}
		}

		public int MaxPlayers
		{
			get
			{
				return InternalObject.MaxPlayers;
			}
			set
			{
				InternalObject.MaxPlayers = value;
				OnUpdateSettings();
			}
		}

		public int BlockCountLimit
		{
			get
			{
				return InternalObject.BlockCountLimiter;
			}
			set
			{
				InternalObject.BlockCountLimiter = value;
				OnUpdateSettings();
			}
		}

		public LevelSettings InternalObject { get; private set; }

		private LevelSetup(LevelSettings settings)
		{
			InternalObject = settings;
		}

		private void OnUpdateSettings()
		{
			byte[] settingsBytes = LevelEditor.Instance.EncodeSettings(InternalObject);
			NetworkAuxAddPiece.Instance.SendLevelSettings(settingsBytes);
		}

		public override string ToString()
		{
			return "LevelSetup (" + Name + ")";
		}

		public static LevelSetup GetCurrent()
		{
			return From(LevelEditor.Instance.Settings);
		}

		public static LevelSetup From(LevelSettings settings)
		{
			if (settings == null)
			{
				return null;
			}
			return new LevelSetup(settings);
		}
	}
}

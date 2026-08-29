namespace Photon.Voice.Unity.Demos.DemoVoiceUI
{
	public struct MicRef
	{
		public Recorder.MicType MicType;

		public string Name;

		public int PhotonId;

		public string PhotonIdString;

		public MicRef(string name, int id)
		{
			MicType = Recorder.MicType.Photon;
			Name = name;
			PhotonId = id;
			PhotonIdString = string.Empty;
		}

		public MicRef(string name, string id)
		{
			MicType = Recorder.MicType.Photon;
			Name = name;
			PhotonId = -1;
			PhotonIdString = id;
		}

		public MicRef(string name)
		{
			MicType = Recorder.MicType.Unity;
			Name = name;
			PhotonId = -1;
			PhotonIdString = string.Empty;
		}

		public override string ToString()
		{
			return $"Mic reference: {Name}";
		}
	}
}

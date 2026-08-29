using Photon.Voice.Unity;
using UnityEngine;

public class NetPlayerData
{
	public NetPlayerId id;

	public Texture2D avatar;

	public string username;

	public bool isVR;

	public bool isOculus;

	public bool isSwitch;

	public bool isSteam;

	public bool isDemo;

	public PineFmodAudioOut<byte> steamSpeaker;

	public Speaker photonSpeaker;

	public bool isSynced;
}

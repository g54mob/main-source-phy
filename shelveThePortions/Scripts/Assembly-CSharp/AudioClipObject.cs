using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New AudioObject", menuName = "Scriptable Objects/Audio Clip Object")]
public class AudioClipObject : ScriptableObject
{
	public AudioType audioType;

	[Space]
	public AudioClipTypes audioClipType;

	public List<AudioClip> audioClipList;

	[Range(0f, 2f)]
	public float volume = 1f;

	[Space]
	public bool isPlayGlobaly;

	public int copiesPlayedAtOnce = 5;

	public float audioFullVolumeDistance = 3f;

	public AudioClip GetAudioClip()
	{
		return audioClipList[Random.Range(0, audioClipList.Count)];
	}
}

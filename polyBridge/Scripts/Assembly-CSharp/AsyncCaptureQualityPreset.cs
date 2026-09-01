using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "AsyncCaptureQualityPreset", menuName = "Game/AsyncCaptureQualityPreset", order = 2)]
public class AsyncCaptureQualityPreset : ScriptableObject
{
	public int m_Width;

	public int m_Height;

	public int m_Framerate;
}

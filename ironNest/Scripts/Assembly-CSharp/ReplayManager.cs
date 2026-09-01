using System.Collections.Generic;
using UnityEngine;

public class ReplayManager : MonoBehaviour
{
	public static ReplayManager Instance;

	public Camera RenderCam;

	public int RenderCamWidth;

	public int RenderCamHeight;

	private readonly List<byte[]> frames;

	private Texture2D destinationTexture;

	private RenderTexture cameraOutput;

	private int frameSession;

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void OnDestroy()
	{
	}

	public void ClearFrames()
	{
	}

	public IReadOnlyList<byte[]> GetCurrentFrames()
	{
		return null;
	}

	public void RecordFrameImmediate()
	{
	}

	public byte[] CaptureFrameBytes()
	{
		return null;
	}

	public void RecordFrameDelayed(float delaySeconds = 3f)
	{
	}

	public byte[] CreateFrameZip(string extension = "jpg")
	{
		return null;
	}

	private bool CanRecordFrame()
	{
		return false;
	}

	public static byte[] CaptureToBytes(Camera cam, RenderTexture target, Texture2D targetTexture)
	{
		return null;
	}

	public static byte[] CreateFrameZip(IReadOnlyList<byte[]> replayFrames, string extension = "jpg")
	{
		return null;
	}
}

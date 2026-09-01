using System;
using System.Collections;
using System.IO;
using UnityEngine;

public class RecordingKeyInputController : NetKeyInputController
{
	public enum RecordingMode
	{
		Playback = 0,
		Recording = 1
	}

	private const int MAX_RECORDING = 1048576;

	private float recordingStartTime;

	private bool isRecording;

	private bool isPlaying;

	private byte[] recordingBytes;

	private int recordingSize;

	private int playOffset;

	private IEnumerator playRecordingEnumerator;

	private RecordingMode recordingMode;

	public bool IsPlaying
	{
		get
		{
			return isPlaying;
		}
	}

	public bool IsRecording
	{
		get
		{
			return IsRecording;
		}
	}

	public RecordingMode CurrentMode
	{
		get
		{
			return recordingMode;
		}
	}

	public override void Awake()
	{
		base.Awake();
		LoadRecording();
	}

	public override void Toggle(bool toggle)
	{
		base.Toggle(toggle);
		if (toggle)
		{
			if (recordingMode == RecordingMode.Recording)
			{
				StartRecording();
			}
			else
			{
				Play();
			}
		}
		else if (recordingMode == RecordingMode.Recording)
		{
			StopRecording();
		}
		else
		{
			Stop();
		}
	}

	public void SetRecording(byte[] bytes, int length)
	{
		recordingBytes = bytes;
		recordingSize = length;
	}

	public void ToggleRecordingMode()
	{
		if (recordingMode == RecordingMode.Recording)
		{
			SetRecordingMode(RecordingMode.Playback);
		}
		else
		{
			SetRecordingMode(RecordingMode.Recording);
		}
	}

	public void SetRecordingMode(RecordingMode mode)
	{
		if (isPlaying || isRecording)
		{
			Debug.LogWarning("Currently playing or recording, can't change mode now");
			return;
		}
		Debug.Log("RecordingMode is now " + mode);
		recordingMode = mode;
	}

	public void Play()
	{
		if (recordingSize == 0)
		{
			Debug.LogWarning("No recording to play back");
			return;
		}
		Debug.Log("Playing recording back");
		playOffset = 0;
		recordingStartTime = Time.realtimeSinceStartup;
		isPlaying = true;
	}

	public void Stop()
	{
		isPlaying = false;
	}

	public void LoadRecording()
	{
		string fullPath = Path.GetFullPath(Machine.Active().LoadedMachinePath);
		string path = Path.GetFileNameWithoutExtension(fullPath) + ".recording";
		string path2 = Path.Combine(Path.GetDirectoryName(fullPath), path);
		if (!File.Exists(path2))
		{
			return;
		}
		using (FileStream fileStream = new FileStream(path2, FileMode.Open))
		{
			recordingBytes = new byte[fileStream.Length];
			fileStream.Read(recordingBytes, 0, (int)fileStream.Length);
			recordingSize = (int)fileStream.Length;
			Debug.Log("Reading input recording of " + recordingSize + " bytes");
		}
	}

	private void StopRecording()
	{
		Debug.Log("Stopping recording input...");
		isRecording = false;
		if (recordingSize == 0)
		{
			return;
		}
		string fullPath = Path.GetFullPath(Machine.Active().LoadedMachinePath);
		string path = Path.GetFileNameWithoutExtension(fullPath) + ".recording";
		string path2 = Path.Combine(Path.GetDirectoryName(fullPath), path);
		using (FileStream fileStream = new FileStream(path2, FileMode.OpenOrCreate))
		{
			fileStream.Write(recordingBytes, 0, recordingSize);
			fileStream.SetLength(recordingSize);
			Debug.Log("Writing recording of " + recordingSize + " bytes.");
		}
	}

	private void StartRecording()
	{
		if (!isPlaying)
		{
			Debug.Log("Started recording input...");
			recordingBytes = new byte[1048576];
			recordingSize = 0;
			isRecording = true;
			recordingStartTime = Time.realtimeSinceStartup;
		}
	}

	private void Update()
	{
		if (!isPlaying && isRecording && base.isDirty)
		{
			WriteToFileBuffer();
		}
	}

	public override void UpdateKeys()
	{
		if (isPlaying)
		{
			ReadRecording();
		}
		else
		{
			base.UpdateKeys();
		}
	}

	private void ReadRecording()
	{
		if (playOffset >= recordingSize)
		{
			Debug.Log("Recording is done playing...");
			isPlaying = false;
			return;
		}
		float num = BitConverter.ToSingle(recordingBytes, playOffset);
		if (!(Time.realtimeSinceStartup - recordingStartTime < num))
		{
			int offset = playOffset + 4;
			int num2 = NetKeyInputController.SkipInput(recordingBytes, offset);
			byte[] array = new byte[num2 + 4];
			Buffer.BlockCopy(recordingBytes, playOffset, array, 0, array.Length);
			ReadInput(array, 4);
			playOffset += num2 + 4;
		}
	}

	private void WriteToFileBuffer()
	{
		if (recordingSize > recordingBytes.Length || recordingSize + base.InputSize + 4 > recordingBytes.Length)
		{
			Debug.Log("Recording buffer overload, stopping recording...");
			isRecording = false;
			return;
		}
		float value = Time.realtimeSinceStartup - recordingStartTime;
		byte[] array = new byte[4 + base.InputSize];
		Buffer.BlockCopy(BitConverter.GetBytes(value), 0, array, 0, 4);
		WriteInput(array, 4);
		Buffer.BlockCopy(array, 0, recordingBytes, recordingSize, array.Length);
		recordingSize += array.Length;
	}
}

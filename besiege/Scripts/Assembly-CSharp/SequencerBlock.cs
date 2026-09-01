using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Logic/SequencerBlock")]
public class SequencerBlock : BlockBehaviour
{
	public Color ledColor;

	protected MToggle nonAuto;

	protected MToggle holdToDetect;

	protected MToggle loopReplay;

	protected MSlider replaySpeed;

	protected MKey recordKey;

	protected MKey replayKey;

	private bool detectedOnceForThisFrame;

	private float startTime;

	private HashSet<KeyCode> emulated = new HashSet<KeyCode>();

	private static HashSet<KeyCode> keysHeld = new HashSet<KeyCode>();

	private static Dictionary<KeyCode, bool> keys = new Dictionary<KeyCode, bool>();

	[HideInInspector]
	public Dictionary<float, Dictionary<KeyCode, bool>> sequence = new Dictionary<float, Dictionary<KeyCode, bool>>();

	[HideInInspector]
	public List<float> indexToTime = new List<float>();

	private int currentStep;

	private bool recording;

	private bool replaying;

	private bool changed;

	public static int recCount = 0;

	protected MKey[] activationKeys;

	private bool keysLastFrame;

	private static KeyCode[] keyCodes = null;

	public MToggle NonAuto
	{
		get
		{
			return nonAuto;
		}
	}

	public MToggle HoldToDetect
	{
		get
		{
			return holdToDetect;
		}
	}

	public MKey ReplayKey
	{
		get
		{
			return replayKey;
		}
	}

	public MKey RecordKey
	{
		get
		{
			return recordKey;
		}
	}

	public float Height
	{
		get
		{
			return base.transform.TransformPoint(Vector3.forward * 0.25f).y - SingleInstanceFindOnly<AddPiece>.Instance.floorHeight;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		if (keyCodes == null)
		{
			keyCodes = Enum.GetValues(typeof(KeyCode)) as KeyCode[];
		}
		replayKey = AddKey(3768, "replay", KeyCode.Return);
		recordKey = AddKey(3769, "record", KeyCode.R);
		holdToDetect = AddToggle(3771, "hold-to-activate", false);
		loopReplay = AddToggle(3780, "loop-replay", false);
		replaySpeed = AddSlider(2428, "replay-speed", 1f, 0.1f, 2f, string.Empty);
		activationKeys = new MKey[2] { replayKey, recordKey };
		if (isSimulating)
		{
			if (replaySpeed.Value <= 0f)
			{
				replaySpeed.SetValue(0.001f);
			}
			sequence = (BuildingBlock as SequencerBlock).sequence;
			indexToTime = (BuildingBlock as SequencerBlock).indexToTime;
		}
	}

	protected void ReplayKeys()
	{
		if (sequence.Count == 0)
		{
			StopReplay();
			return;
		}
		if (currentStep >= sequence.Count)
		{
			if (!loopReplay.IsActive)
			{
				StopReplay();
				return;
			}
			currentStep = 0;
			startTime = Time.fixedTime - Time.fixedDeltaTime * 0.1f;
		}
		float num = (Time.fixedTime - startTime) * replaySpeed.Value;
		float num2 = indexToTime[currentStep];
		if (!(num > num2))
		{
			return;
		}
		Dictionary<KeyCode, bool> dictionary = sequence[num2];
		foreach (KeyValuePair<KeyCode, bool> item in dictionary)
		{
			if (inputController.KeyUsed(item.Key))
			{
				Emulate(item.Key, item.Value);
			}
		}
		currentStep++;
	}

	private void Emulate(KeyCode key, bool down)
	{
		if (down)
		{
			if (!emulated.Contains(key))
			{
				EmulateKeys(activationKeys, new MKey(0, "sequecer", key), true);
				emulated.Add(key);
			}
		}
		else if (emulated.Contains(key))
		{
			EmulateKeys(activationKeys, new MKey(0, "sequecer", key), false);
			emulated.Remove(key);
		}
	}

	private void StartReplay()
	{
		if (!recording)
		{
			replaying = true;
			currentStep = 0;
			startTime = Time.fixedTime - Time.fixedDeltaTime * 0.1f;
			emulated.Clear();
			ToggleLED(Color.cyan);
		}
	}

	protected void StopReplay()
	{
		if (!replaying)
		{
			return;
		}
		replaying = false;
		currentStep = 0;
		ToggleLED();
		foreach (KeyCode item in emulated)
		{
			if (inputController.KeyUsed(item))
			{
				EmulateKeys(activationKeys, new MKey(0, "sequecer", item), false);
			}
		}
	}

	private void StartRecording()
	{
		if (!recording)
		{
			recCount++;
			recording = true;
			changed = true;
			startTime = Time.fixedTime;
			sequence.Clear();
			indexToTime.Clear();
			StopReplay();
			ToggleLED(Color.red);
		}
	}

	private void StopRecording()
	{
		if (!recording)
		{
			return;
		}
		recCount--;
		recording = false;
		ToggleLED();
		foreach (KeyCode item in keysHeld)
		{
			keys.Add(item, false);
		}
		if (recCount == 0)
		{
			keysHeld.Clear();
		}
		if (keys.Count > 0)
		{
			float num = Time.fixedTime - startTime;
			sequence.Add(num, keys);
			indexToTime.Add(num);
		}
		StoreSequence(sequence, indexToTime);
	}

	private void StoreSequence(Dictionary<float, Dictionary<KeyCode, bool>> seq, List<float> index)
	{
		sequence = seq;
		indexToTime = index;
	}

	private void RecordKeys()
	{
		if (Input.anyKey || keysLastFrame)
		{
			float num = Time.fixedTime - startTime;
			if (keys.Count == 0)
			{
				KeyCode[] array = keyCodes;
				foreach (KeyCode key in array)
				{
					AddKeyCode(key);
				}
			}
			if (keys.Count > 0)
			{
				sequence.Add(num, keys);
				indexToTime.Add(num);
			}
		}
		keysLastFrame = Input.anyKey;
	}

	private void AddKeyCode(KeyCode key)
	{
		switch (key)
		{
		case KeyCode.None:
		case KeyCode.Tab:
		case KeyCode.Escape:
		case KeyCode.Space:
		case KeyCode.DoubleQuote:
		case KeyCode.Caret:
		case KeyCode.F1:
		case KeyCode.F2:
		case KeyCode.F3:
		case KeyCode.F11:
		case KeyCode.F12:
		case KeyCode.LeftWindows:
		case KeyCode.RightWindows:
		case KeyCode.Print:
		case KeyCode.Mouse0:
		case KeyCode.Mouse1:
		case KeyCode.Mouse2:
		case KeyCode.Mouse3:
		case KeyCode.Mouse4:
		case KeyCode.Mouse5:
		case KeyCode.Mouse6:
			return;
		}
		if (!recordKey.HasKey(key) && !replayKey.HasKey(key))
		{
			bool pressed;
			bool released;
			KeyPressed(key, out pressed, out released);
			if (released)
			{
				keys.Add(key, false);
			}
			else if (pressed)
			{
				keys.Add(key, true);
			}
		}
	}

	private void KeyPressed(KeyCode key, out bool pressed, out bool released)
	{
		bool key2 = Input.GetKey(key);
		pressed = false;
		released = false;
		if (key2)
		{
			if (!keysHeld.Contains(key))
			{
				keysHeld.Add(key);
				pressed = true;
			}
		}
		else if (keysHeld.Contains(key))
		{
			keysHeld.Remove(key);
			released = true;
		}
	}

	protected void AutoToggle(bool isactive)
	{
		recordKey.DisplayInMapper = isactive;
		replayKey.DisplayInMapper = isactive;
	}

	public override void RegisterSimUpdates()
	{
		RegisterSimUpdates(Prefab.RegisterSimUpdate, (!isSimulating) ? Prefab.RegisterSimFixedUpdate : SimPhysics, Prefab.RegisterSimLateUpdate, SimPhysics && Prefab.RegisterEmulationUpdate);
	}

	public override void EmulationUpdateBlock()
	{
		CheckReplayKey(replayKey.EmulationPressed(), replayKey.EmulationReleased());
		CheckRecordKey(recordKey.EmulationPressed(), recordKey.EmulationReleased());
		if (keys.Count > 0)
		{
			keys = new Dictionary<KeyCode, bool>();
		}
		detectedOnceForThisFrame = false;
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (isSimulating && Time.timeScale != 0f)
		{
			CheckReplayKey(replayKey.IsPressed, replayKey.IsReleased);
			CheckRecordKey(recordKey.IsPressed, recordKey.IsReleased);
		}
	}

	private void CheckRecordKey(bool pressed, bool released)
	{
		if (holdToDetect.IsActive)
		{
			if (released)
			{
				if (recording)
				{
					StopRecording();
				}
			}
			else if (pressed && !recording)
			{
				StartRecording();
			}
		}
		else if (pressed)
		{
			if (recording)
			{
				StopRecording();
			}
			else
			{
				StartRecording();
			}
		}
	}

	private void CheckReplayKey(bool pressed, bool released)
	{
		if (holdToDetect.IsActive)
		{
			if (released)
			{
				if (replaying)
				{
					StopReplay();
				}
			}
			else if (pressed && !replaying)
			{
				StartReplay();
			}
		}
		else if (pressed)
		{
			if (replaying)
			{
				StopReplay();
			}
			else
			{
				StartReplay();
			}
		}
	}

	public override void SendEmulationUpdateBlock()
	{
		if (SimPhysics && _parentMachine.isReady && !detectedOnceForThisFrame)
		{
			if (recording)
			{
				RecordKeys();
			}
			else if (replaying)
			{
				ReplayKeys();
			}
			detectedOnceForThisFrame = true;
		}
	}

	public override void OnRemoteEmulate(MKey key, bool emulate)
	{
	}

	private void ToggleLED(Color? c = null)
	{
		if (!c.HasValue)
		{
			c = Color.black;
		}
		MeshRenderer.material.SetColor("_EmissCol", c.Value);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (isSimulating)
		{
			StopRecording();
			if (changed)
			{
				XDataHolder data = new XDataHolder();
				BuildingBlock.OnSave(data, CopyMode.All);
			}
		}
	}

	public override void OnSave(XDataHolder data)
	{
		base.OnSave(data);
		if (isSimulating || sequence.Count <= 0)
		{
			return;
		}
		string[] array = new string[sequence.Count];
		int num = 0;
		foreach (KeyValuePair<float, Dictionary<KeyCode, bool>> item in sequence)
		{
			string text = "t:" + item.Key + ";k:";
			foreach (KeyValuePair<KeyCode, bool> item2 in item.Value)
			{
				string text2 = text;
				text = text2 + string.Format(CultureInfo.InvariantCulture.NumberFormat, "{0}", item2.Key) + "=" + item2.Value + ",";
			}
			array[num] = text;
			num++;
		}
		data.Write(new XStringArray("sequence", array));
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (isSimulating || !data.HasKey("sequence"))
		{
			return;
		}
		sequence.Clear();
		string[] array = data.ReadStringArray("sequence");
		for (int i = 0; i < array.Length; i++)
		{
			string[] array2 = array[i].Split(';');
			float key = float.Parse(array2[0].Trim('t', ':'), CultureInfo.InvariantCulture.NumberFormat);
			string[] array3 = array2[1].Split(',');
			keys = new Dictionary<KeyCode, bool>();
			for (int j = 0; j < array3.Length; j++)
			{
				string[] array4 = array3[j].Split('=');
				KeyCode keyCode;
				if (KeyCodeConverter.GetKey(array4[0], out keyCode))
				{
					keys.Add(keyCode, bool.Parse(array4[1]));
				}
			}
			sequence.Add(key, keys);
		}
		keys = new Dictionary<KeyCode, bool>();
	}
}

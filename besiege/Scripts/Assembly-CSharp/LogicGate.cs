using System;
using System.Collections.Generic;
using Localisation;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Blocks/Block Behaviours/Logic/LogicGate")]
public class LogicGate : BlockBehaviour
{
	public enum GateType
	{
		NOT = 0,
		AND = 1,
		OR = 2,
		NOR = 3,
		NAND = 4,
		XOR = 5,
		XNOR = 6,
		Random = 7,
		SRLatch = 8,
		DLatch = 9,
		Counter = 10,
		EdgeDetect = 11
	}

	protected const int MAX_SFX = 32;

	public const short MAX_PULSES = 5;

	protected GateType gateType;

	public Transform leaverA;

	public Transform leaverB;

	public Color ledColor;

	public ParticleSystem sparks;

	public AudioSource shock;

	public AudioSource sfx;

	private bool hasSfx;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	protected static int sfxPlaying;

	protected static float sfxFrame;

	protected MKey aKey;

	protected MKey bKey;

	protected MMenu modeMenu;

	protected MKey emulateKey;

	private MToggle toggledInput;

	private MToggle inverted;

	private bool emulating;

	private bool aToggled;

	private bool bToggled;

	protected MKey[] activationKeys;

	private int counter;

	private int lastCount;

	private int framesPulsed;

	public static bool UseBurnout;

	private bool burnoutProne;

	private bool lastEmulate;

	private bool burnedOut;

	private bool sparked;

	private bool A;

	private bool B;

	private bool aPressed;

	private bool bPressed;

	private bool emuAPressed;

	private bool emuBPressed;

	private bool aHeld;

	private bool bHeld;

	private bool emuAHeld;

	private bool emuBHeld;

	private bool aReleased;

	private bool emuAReleased;

	private bool lastSubmerged;

	private bool lastA;

	private bool lastB;

	private float lastAangle;

	private float lastBangle;

	private float angle;

	public GateType Type
	{
		get
		{
			return gateType;
		}
	}

	public MKey AKey
	{
		get
		{
			return aKey;
		}
	}

	public MKey BKey
	{
		get
		{
			return bKey;
		}
	}

	public MMenu ModeMenu
	{
		get
		{
			return modeMenu;
		}
	}

	public MToggle ToggledInput
	{
		get
		{
			return toggledInput;
		}
	}

	public MKey EmulateKey
	{
		get
		{
			return emulateKey;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		hasSfx = sfx != null;
		if (hasSfx)
		{
			mixer = sfx.outputAudioMixerGroup;
			underwaterMixer = ReferenceMaster.GetWaterMixerFrom(mixer);
		}
		aKey = AddKey(3808, "activate-A", ControlScheme.BlockControls.Activate2, 0, KeyCode.U);
		bKey = AddKey(3809, "activate-B", ControlScheme.BlockControls.Activate2, 1, KeyCode.I);
		modeMenu = AddMenu("Gate", 1, new List<string>
		{
			LocalisationManager.GetTranslation(3811),
			LocalisationManager.GetTranslation(3810),
			LocalisationManager.GetTranslation(3812),
			LocalisationManager.GetTranslation(3813),
			LocalisationManager.GetTranslation(3816),
			LocalisationManager.GetTranslation(3814),
			LocalisationManager.GetTranslation(3815),
			LocalisationManager.GetTranslation(4253),
			LocalisationManager.GetTranslation(4246),
			LocalisationManager.GetTranslation(4247),
			LocalisationManager.GetTranslation(4248),
			LocalisationManager.GetTranslation(4595)
		});
		modeMenu.ValueChanged += UpdateHidden;
		toggledInput = AddToggle(2431, "toggle-mode", false);
		inverted = AddToggle(3898, "inverted", true);
		inverted.DisplayInMapper = false;
		emulateKey = AddEmulatorKey(3769, "emulate", ControlScheme.BlockControls.Automate, 0, KeyCode.C);
		activationKeys = new MKey[2] { aKey, bKey };
		if (UseBurnout && isSimulating)
		{
			ReferenceMaster.onMachinePostSim = (Action<Machine>)Delegate.Combine(ReferenceMaster.onMachinePostSim, new Action<Machine>(CheckLoop));
		}
	}

	private void CheckLoop(Machine m)
	{
		if (m == base.ParentMachine)
		{
			MKey[] inputs = ((gateType == GateType.NOT || gateType == GateType.Random) ? new MKey[1] { aKey } : activationKeys);
			burnoutProne = CheckLoop(inputs, emulateKey);
		}
	}

	protected void UpdateHidden(int gType)
	{
		gateType = (GateType)gType;
		bKey.DisplayInMapper = gateType != GateType.NOT && gateType != GateType.Random && gateType != GateType.EdgeDetect;
		inverted.DisplayInMapper = false;
		switch (gateType)
		{
		case GateType.Counter:
			aKey.DisplayName = LocalisationManager.GetTranslation(4249);
			bKey.DisplayName = LocalisationManager.GetTranslation(2366);
			toggledInput.DisplayInMapper = false;
			break;
		case GateType.DLatch:
			aKey.DisplayName = LocalisationManager.GetTranslation(4250);
			bKey.DisplayName = LocalisationManager.GetTranslation(3565);
			toggledInput.DisplayInMapper = false;
			break;
		case GateType.SRLatch:
			aKey.DisplayName = LocalisationManager.GetTranslation(4251);
			bKey.DisplayName = LocalisationManager.GetTranslation(2366);
			toggledInput.DisplayInMapper = false;
			break;
		case GateType.EdgeDetect:
			aKey.DisplayName = LocalisationManager.GetTranslation(3808);
			toggledInput.DisplayInMapper = false;
			inverted.DisplayInMapper = true;
			break;
		default:
			aKey.DisplayName = LocalisationManager.GetTranslation(3808);
			bKey.DisplayName = LocalisationManager.GetTranslation(3809);
			toggledInput.DisplayInMapper = true;
			break;
		}
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
	}

	private void UpdateState(bool pressedA, bool pressedB, bool heldA, bool heldB, bool releasedA)
	{
		A = heldA;
		B = heldB;
		switch (gateType)
		{
		case GateType.Counter:
			if (pressedB)
			{
				counter = 0;
				lastCount = 0;
			}
			else if (pressedA)
			{
				lastCount = counter;
				counter++;
			}
			counter %= 4;
			A = counter % 2 == 1;
			B = counter > 1;
			return;
		case GateType.Random:
			if (pressedA)
			{
				aToggled = UnityEngine.Random.Range(0f, 1f) >= 0.5f;
			}
			A = (B = heldA && !pressedA);
			return;
		case GateType.SRLatch:
		{
			bool flag = aToggled;
			if (!flag && (pressedA || heldA))
			{
				flag = true;
			}
			if (flag && (pressedB || heldB))
			{
				flag = false;
			}
			aToggled = flag;
			return;
		}
		case GateType.DLatch:
			if (pressedB || heldB)
			{
				aToggled = pressedA || heldA;
			}
			return;
		case GateType.EdgeDetect:
			B = A;
			if (inverted.IsActive)
			{
				if (releasedA)
				{
					aToggled = true;
				}
			}
			else if (pressedA)
			{
				aToggled = true;
			}
			return;
		}
		if (toggledInput.IsActive)
		{
			if (pressedA)
			{
				aToggled = !aToggled;
			}
			if (pressedB)
			{
				bToggled = !bToggled;
			}
			A = aToggled;
			B = bToggled;
		}
		if (gateType == GateType.NOT)
		{
			B = A;
		}
	}

	public override void EmulationUpdateBlock()
	{
		emuAPressed = aKey.EmulationPressed();
		emuBPressed = bKey.EmulationPressed();
		emuAHeld = aKey.EmulationHeld(true);
		emuBHeld = bKey.EmulationHeld(true);
		emuAReleased = aKey.EmulationReleased();
		UpdateState(emuAPressed, emuBPressed, emuAHeld || aHeld, emuBHeld || bHeld, emuAReleased);
		if (burnoutProne)
		{
			if (!burnedOut)
			{
				bool flag = EvaluateEmulation();
				if (lastEmulate != flag)
				{
					if (framesPulsed >= 5)
					{
						burnedOut = true;
					}
					else
					{
						framesPulsed++;
					}
				}
				else
				{
					ResetBurnOut();
				}
				lastEmulate = flag;
			}
			else if (emuAPressed || emuBPressed || emuAHeld || emuBHeld)
			{
				ResetBurnOut();
			}
		}
		if (!hasSfx)
		{
			return;
		}
		if ((A && !lastA) || (B && !lastB))
		{
			if (sfxFrame < Time.fixedTime)
			{
				sfxFrame = Time.fixedTime + Time.fixedDeltaTime * 2f;
				sfxPlaying = 0;
			}
			if (sfxPlaying < 32)
			{
				bool flag2 = base.GetSubmergedPctMV > 0.9f;
				if (lastSubmerged != flag2)
				{
					sfx.outputAudioMixerGroup = ((!flag2) ? mixer : underwaterMixer);
					lastSubmerged = flag2;
				}
				sfx.PlayOneShot(sfx.clip);
				sfxPlaying++;
			}
		}
		lastA = A;
		lastB = B;
	}

	private void ResetBurnOut()
	{
		burnedOut = false;
		sparked = false;
		framesPulsed = 0;
	}

	public override void SendEmulationUpdateBlock()
	{
		if (burnedOut)
		{
			if (!sparked)
			{
				StopEmulation();
				sparked = true;
				sparks.Play();
				shock.Play();
			}
		}
		else if (EvaluateEmulation())
		{
			StartEmulation();
		}
		else
		{
			StopEmulation();
		}
	}

	public bool EvaluateEmulation()
	{
		switch (gateType)
		{
		case GateType.NOT:
			return !A;
		case GateType.AND:
			return A && B;
		case GateType.OR:
			return A || B;
		case GateType.NAND:
			return !A || !B;
		case GateType.NOR:
			return !A && !B;
		case GateType.XOR:
			return A != B;
		case GateType.XNOR:
			return A == B;
		case GateType.Random:
		case GateType.SRLatch:
		case GateType.DLatch:
			return aToggled;
		case GateType.Counter:
			return counter == 0 && lastCount == 3;
		case GateType.EdgeDetect:
		{
			bool result = aToggled;
			aToggled = false;
			return result;
		}
		default:
			return false;
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (Time.timeScale != 0f)
		{
			aPressed = aKey.IsPressed;
			bPressed = bKey.IsPressed;
			aHeld = aPressed || aKey.IsHeld;
			bHeld = bPressed || bKey.IsHeld;
			aReleased = gateType == GateType.EdgeDetect && !aHeld && aKey.IsReleased;
			UpdateState(aPressed, bPressed, aHeld || emuAHeld, bHeld || emuBHeld, aReleased);
			if (aPressed || bPressed)
			{
				ResetBurnOut();
			}
			angle = (A ? (-90) : 0);
			if (angle != lastAangle)
			{
				leaverA.localRotation = Quaternion.Euler(0f, 0f, angle);
				lastAangle = angle;
			}
			angle = (B ? (-90) : 0);
			if (angle != lastBangle)
			{
				leaverB.localRotation = Quaternion.Euler(0f, 0f, angle);
				lastBangle = angle;
			}
		}
	}

	public void StartEmulation()
	{
		if (!emulating)
		{
			EmulateKeys(true);
		}
	}

	public void StopEmulation()
	{
		if (emulating)
		{
			EmulateKeys(false);
		}
	}

	public void EmulateKeys(bool emulate)
	{
		emulating = emulate;
		VisualController.AssignMaterialColor("_EmissCol", (!emulate) ? Color.black : ledColor);
		EmulateKeys(activationKeys, EmulateKey, emulate);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		StopEmulation();
		if (isSimulating)
		{
			ReferenceMaster.onMachinePostSim = (Action<Machine>)Delegate.Remove(ReferenceMaster.onMachinePostSim, new Action<Machine>(CheckLoop));
		}
	}
}

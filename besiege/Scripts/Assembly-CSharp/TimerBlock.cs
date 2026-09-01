using System.Collections;
using Localisation;
using UnityEngine;
using UnityEngine.Audio;

[AddComponentMenu("Blocks/Block Behaviours/Logic/TimerBlock")]
public class TimerBlock : BlockBehaviour
{
	private enum Phase
	{
		Stopped = 0,
		Prephase = 1,
		Delaying = 2,
		Emulating = 3
	}

	protected const int MAX_SFX = 32;

	public Transform hand;

	public MeshRenderer dial;

	public Color ledColor;

	public AudioSource sfx;

	private bool hasSfx;

	protected AudioMixerGroup mixer;

	protected AudioMixerGroup underwaterMixer;

	protected static int sfxPlaying;

	protected static float sfxFrame;

	protected MSlider waitSlider;

	protected MSlider emulationSlider;

	protected MToggle auto;

	protected MToggle canStop;

	protected MToggle loop;

	protected MToggle holdToActivate;

	protected MKey activateKey;

	protected MKey emulateKey;

	protected MKey[] activationKeys;

	private bool activatePressed;

	private bool emuActivatePressed;

	private bool activateHeld;

	private bool emuActivateHeld;

	private bool lastSubmerged;

	private Phase phase;

	private Phase lastPhase;

	private int timer;

	private int ping;

	private int delay;

	private int emulation;

	public MSlider WaitSlider
	{
		get
		{
			return waitSlider;
		}
	}

	public MSlider EmulationSlider
	{
		get
		{
			return emulationSlider;
		}
	}

	public MToggle Auto
	{
		get
		{
			return auto;
		}
	}

	public MToggle CanStop
	{
		get
		{
			return canStop;
		}
	}

	public MToggle Loop
	{
		get
		{
			return loop;
		}
	}

	public MToggle HoldToActivate
	{
		get
		{
			return holdToActivate;
		}
	}

	public MKey ActivateKey
	{
		get
		{
			return activateKey;
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
		activateKey = AddKey(3775, "activate", ControlScheme.BlockControls.Activate, 0, KeyCode.B);
		emulateKey = AddEmulatorKey(3769, "emulate", ControlScheme.BlockControls.Automate, 0, KeyCode.C);
		auto = AddToggle(3777, "automatic", false);
		holdToActivate = AddToggle(3778, "hold-to-activate", false);
		canStop = AddToggle(3779, "can-stop", false);
		loop = AddToggle(3780, "loop", false);
		waitSlider = AddSliderUnclamped(3781, "wait", 1f, 0f, 60f, string.Empty, "s", true);
		emulationSlider = AddSliderUnclamped(3782, "emulation-time", 1f, 0f, 60f, string.Empty, "s", true);
		UpdateHidden(true);
		auto.Toggled += UpdateHidden;
		canStop.Toggled += UpdateHidden;
		holdToActivate.Toggled += UpdateHidden;
		activationKeys = new MKey[1] { activateKey };
	}

	protected void UpdateHidden(bool unused)
	{
		activateKey.DisplayInMapper = !auto.IsActive;
		activateKey.DisplayName = ((!canStop.IsActive && !holdToActivate.IsActive) ? LocalisationManager.GetTranslation(3775) : LocalisationManager.GetTranslation(3776));
		holdToActivate.DisplayInMapper = !auto.IsActive;
		canStop.DisplayInMapper = !holdToActivate.IsActive && !auto.IsActive;
	}

	public override void OnLoad(XDataHolder data)
	{
		base.OnLoad(data);
		if (isSimulating && auto.IsActive)
		{
			_parentMachine.UnregisterUpdate(this, false);
			Emulate();
		}
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (!auto.IsActive && Time.timeScale != 0f)
		{
			activatePressed = activateKey.IsPressed;
			activateHeld = activateKey.IsHeld;
			CheckKeys(activatePressed, activateHeld || emuActivateHeld);
		}
	}

	public override void EmulationUpdateBlock()
	{
		if (Time.timeScale != 0f && !auto.IsActive)
		{
			emuActivatePressed = activateKey.EmulationPressed();
			emuActivateHeld = activateKey.EmulationHeld(true);
			CheckKeys(emuActivatePressed, emuActivateHeld || activateHeld);
		}
	}

	public override void SendEmulationUpdateBlock()
	{
		if (_parentMachine.isReady)
		{
			RunTimer();
		}
	}

	private void CheckKeys(bool pressed, bool held)
	{
		if (holdToActivate.IsActive)
		{
			if (held)
			{
				if (pressed)
				{
					Emulate();
				}
			}
			else
			{
				StopEmulation();
			}
		}
		else
		{
			if (!pressed)
			{
				return;
			}
			if (phase != Phase.Stopped)
			{
				if (canStop.IsActive)
				{
					StopEmulation();
				}
			}
			else
			{
				Emulate();
			}
		}
	}

	public void Emulate()
	{
		if (phase == Phase.Stopped)
		{
			StartTimer();
		}
	}

	public void StopEmulation()
	{
		if (phase != Phase.Stopped)
		{
			StopAllCoroutines();
			if (phase == Phase.Emulating)
			{
				EmulateKeys(false);
			}
			GoTo(Phase.Stopped);
			lastPhase = Phase.Stopped;
			SetTimerHandRot(0f);
		}
	}

	public int TimeToFrameCount(float time)
	{
		int num = Mathf.CeilToInt(time * 50f);
		return (num < 1) ? 1 : num;
	}

	private void StartTimer()
	{
		GoTo(Phase.Prephase);
	}

	private void RunTimer()
	{
		switch (phase)
		{
		case Phase.Stopped:
			break;
		case Phase.Prephase:
			PrephaseTimer();
			break;
		case Phase.Delaying:
			ElapseDelay();
			break;
		case Phase.Emulating:
			ElapseEmulation();
			break;
		}
	}

	private void PrephaseTimer()
	{
		if (!SimPhysics && base.HasParentMachine && base.ParentMachine.isLocalMachine)
		{
			if (lastPhase != Phase.Prephase)
			{
				ping = TimeToFrameCount((float)BesiegeNetworkManager.Instance.Ping / 1000f);
				lastPhase = Phase.Prephase;
			}
			if (timer < ping)
			{
				timer++;
				return;
			}
			GoTo(Phase.Delaying);
			ElapseDelay();
		}
		else
		{
			GoTo(Phase.Delaying);
			ElapseDelay();
		}
	}

	private void ElapseDelay()
	{
		if (lastPhase != Phase.Delaying)
		{
			bool flag = waitSlider.Value > 0f;
			delay = ((!flag) ? 1 : TimeToFrameCount(waitSlider.Value));
			lastPhase = Phase.Delaying;
			if (flag)
			{
				StartCoroutine(AnimateHand());
			}
		}
		if (timer < delay)
		{
			timer++;
			return;
		}
		GoTo(Phase.Emulating);
		ElapseEmulation();
	}

	private void ElapseEmulation()
	{
		if (lastPhase != Phase.Emulating)
		{
			EmulateKeys(true);
			emulation = ((!(emulationSlider.Value > 0f)) ? 1 : TimeToFrameCount(emulationSlider.Value));
			lastPhase = Phase.Emulating;
		}
		if (timer < emulation)
		{
			timer++;
			return;
		}
		EmulateKeys(false);
		EndCycle();
	}

	private void EndCycle()
	{
		if (loop.IsActive)
		{
			GoTo(Phase.Delaying);
			ElapseDelay();
		}
		else
		{
			GoTo(Phase.Stopped);
			lastPhase = Phase.Stopped;
		}
	}

	private void GoTo(Phase phase)
	{
		timer = 0;
		this.phase = phase;
		if (!hasSfx || delay <= 1 || phase == lastPhase)
		{
			return;
		}
		switch (phase)
		{
		case Phase.Prephase:
			break;
		case Phase.Stopped:
			if (loop.IsActive)
			{
				PlaySFX(0.5f);
			}
			break;
		default:
			PlaySFX(0.5f);
			break;
		}
	}

	private void PlaySFX(float vol)
	{
		if (sfxFrame < Time.fixedTime)
		{
			sfxFrame = Time.fixedTime + Time.fixedDeltaTime * 2f;
			sfxPlaying = 0;
		}
		if (sfxPlaying < 32)
		{
			bool flag = base.GetSubmergedPctMV > 0.9f;
			if (lastSubmerged != flag)
			{
				sfx.outputAudioMixerGroup = ((!flag) ? mixer : underwaterMixer);
				lastSubmerged = flag;
			}
			sfx.PlayOneShot(sfx.clip, vol);
			sfxPlaying++;
		}
	}

	public IEnumerator AnimateHand()
	{
		for (float t = 0f; t < waitSlider.Value; t += Time.deltaTime)
		{
			if (phase != Phase.Delaying)
			{
				SetTimerHandRot(0f);
				yield break;
			}
			float pct = t / waitSlider.Value;
			SetTimerHandRot(pct);
			yield return new WaitForEndOfFrame();
		}
		SetTimerHandRot(0f);
	}

	private void EmulateKeys(bool emulate)
	{
		VisualController.AssignMaterialColor("_EmissCol", (!emulate) ? Color.black : ledColor);
		EmulateKeys(activationKeys, EmulateKey, emulate);
	}

	protected void SetTimerHandRot(float pct)
	{
		hand.localEulerAngles = new Vector3(hand.localEulerAngles.x, (0f - pct) * 360f, hand.localEulerAngles.z);
		if (pct == 0f)
		{
			dial.gameObject.SetActive(false);
			return;
		}
		dial.gameObject.SetActive(true);
		dial.material.SetFloat("_Progress", 1f - pct);
	}

	protected override void OnDisable()
	{
		base.OnDisable();
		if (phase == Phase.Emulating)
		{
			EmulateKeys(false);
		}
	}
}

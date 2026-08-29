using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.VFX;

public class DraculaDarkest1Logic : LevelLogic, ISaveable
{
	public sealed class PuzzleSolvedTimer : Timer
	{
		public int step;

		public override byte getTypeId()
		{
			return 0;
		}

		public PuzzleSolvedTimer()
		{
		}

		public PuzzleSolvedTimer(int step)
		{
			this.step = step;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in step, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			step = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("step: " + $"{step}");
			return stringBuilder.ToString();
		}
	}

	public sealed class WinAnimationTimer : Timer
	{
		public int step;

		public override byte getTypeId()
		{
			return 1;
		}

		public WinAnimationTimer()
		{
		}

		public WinAnimationTimer(int step)
		{
			this.step = step;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in step, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			step = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("step: " + $"{step}");
			return stringBuilder.ToString();
		}
	}

	[DontSave]
	public Ref<Lock, Transform> liarsLock;

	[DontSave]
	public RefArray<Turnable, Transform> liarsTurnablesNeo;

	[DontSave]
	private EventInstance endTurningEvent;

	[DontSave]
	public Switch3D obeliskSwitch;

	private bool puzzleDone;

	[DontSave]
	public MaterialState winObeliskGlow;

	[DontSave]
	public VisualEffect[] winVisualEffects;

	[DontSave]
	public GameObject winCutScene;

	[DontSave]
	public AnimationSampler winAnimationSampler;

	[DontSave]
	public float winTurnOnGlowPoint = 1f;

	[DontSave]
	public float winTurnOffGlowPoint = 3.6f;

	[DebugButton("Win Animation", Tint.Default, PostClickAction.ReturnToGame, 0, new object[] { })]
	private void winAnimation()
	{
		winCutScene.SetActive(value: true);
		VisualEffect[] array = winVisualEffects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Play();
		}
		winAnimationSampler.play();
		game.startTimer(new WinAnimationTimer(0), winTurnOnGlowPoint);
		PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Magic/Darkest_Portal_Activate", winCutScene);
	}

	public override void onInit()
	{
		endTurningEvent = PineFmod.createInstance(liarsTurnablesNeo[0].Get<Turnable>(0).soundTurn.Guid);
		PineFmod.set3DAttributes(endTurningEvent, PineFmod.to3DAttributes(liarsLock.Get<Transform>(0f)));
	}

	public override void onUpdate()
	{
		if (!liarsLock.Get<Lock>(0).isUnlocked || !puzzleDone)
		{
			return;
		}
		foreach (Ref<Turnable, Transform> item in liarsTurnablesNeo)
		{
			item.Get<Transform>(0f).Rotate(Vector3.right * Time.deltaTime * 300f);
		}
		if (endTurningEvent.getPlaybackState(out var state) == RESULT.OK && state != PLAYBACK_STATE.PLAYING)
		{
			PineFmod.start(endTurningEvent);
		}
	}

	public override void onUnlock(Lock targetLock)
	{
		if (!(targetLock == liarsLock.Get<Lock>(0)))
		{
			return;
		}
		foreach (Ref<Turnable, Transform> item in liarsTurnablesNeo)
		{
			item.Get<Turnable>(0).targetable = false;
		}
		game.startTimer(new PuzzleSolvedTimer(0), 0.5f);
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (targetSwitch == obeliskSwitch && game.hasAuthority(targetSwitch))
		{
			game.showExitLevelDialogue();
		}
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in puzzleDone, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		puzzleDone = reader.ReadBoolean();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "puzzleDone",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new PuzzleSolvedTimer(), 
			1 => new WinAnimationTimer(), 
			_ => null, 
		};
	}

	[CompilerGenerated]
	public override void onTimerUpdate(Timer timer)
	{
		if (!(timer is PuzzleSolvedTimer))
		{
			_ = timer is WinAnimationTimer;
		}
	}

	[CompilerGenerated]
	public override void onTimerDone(Timer timer)
	{
		if (timer is PuzzleSolvedTimer timer2)
		{
			onPuzzleSolvedTimerDone(timer2);
		}
		else if (timer is WinAnimationTimer timer3)
		{
			onWinAnimationTimerDone(timer3);
		}
	}

	private void onPuzzleSolvedTimerDone(PuzzleSolvedTimer timer)
	{
		if (timer.step == 0)
		{
			puzzleDone = true;
			game.startTimer(new PuzzleSolvedTimer(1), 1f);
		}
		else
		{
			winAnimation();
		}
	}

	private void onWinAnimationTimerDone(WinAnimationTimer timer)
	{
		if (timer.step == 0)
		{
			float duration = winTurnOffGlowPoint - winTurnOnGlowPoint;
			winObeliskGlow.transitionToDuration("Glowing", duration);
			game.startTimer(new WinAnimationTimer(1), duration);
			return;
		}
		if (timer.step == 1)
		{
			float duration2 = winAnimationSampler.clip.length - winTurnOffGlowPoint;
			winObeliskGlow.transitionToDuration("Glowing", duration2, 0f);
			game.startTimer(new WinAnimationTimer(2), duration2);
			return;
		}
		winCutScene.SetActive(value: false);
		VisualEffect[] array = winVisualEffects;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Stop();
		}
		game.levelCompleted();
	}
}

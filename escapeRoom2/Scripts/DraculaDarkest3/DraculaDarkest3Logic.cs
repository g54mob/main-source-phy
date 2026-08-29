using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.VFX;

public class DraculaDarkest3Logic : LevelLogic, ISaveable
{
	public sealed class WinAnimationTimer : Timer
	{
		public int step;

		public override byte getTypeId()
		{
			return 0;
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

	public sealed class BookcaseDelayTimer : Timer
	{
		public override byte getTypeId()
		{
			return 1;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	[DontSave]
	public Switch3D obeliskSwitch;

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

	[DontSave]
	public Turnable[] bookcaseTurnables;

	[DontSave]
	public MaterialState bookcaseMS;

	[DontSave]
	private readonly int[] bookcaseCorrect = new int[4] { 8, 7, 5, 4 };

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
	}

	public override void onUpdate()
	{
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (targetSwitch == obeliskSwitch && game.hasAuthority(targetSwitch))
		{
			game.showExitLevelDialogue();
		}
	}

	public override void onTurnableMoved(Turnable turnable, MoveEvent moveEvent)
	{
		if (Array.IndexOf(bookcaseTurnables, turnable) != -1 && moveEvent == MoveEvent.Snapped)
		{
			int i = 0;
			if (Array.TrueForAll(bookcaseTurnables, (Turnable x) => x.value == bookcaseCorrect[i++]))
			{
				game.startTimer(new BookcaseDelayTimer(), 1.5f);
			}
		}
	}

	public virtual void save(FastBinaryWriter writer)
	{
	}

	public virtual void load(FastBinaryReader reader)
	{
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new WinAnimationTimer(), 
			1 => new BookcaseDelayTimer(), 
			_ => null, 
		};
	}

	[CompilerGenerated]
	public override void onTimerUpdate(Timer timer)
	{
		if (!(timer is WinAnimationTimer) && timer is BookcaseDelayTimer timer2)
		{
			onBookcaseDelayTimerUpdate(timer2);
		}
	}

	private void onBookcaseDelayTimerUpdate(BookcaseDelayTimer timer)
	{
		bookcaseMS.setWeight("Down", Mathf.PingPong(5f * timer.unitTime, 1f));
	}

	[CompilerGenerated]
	public override void onTimerDone(Timer timer)
	{
		if (timer is WinAnimationTimer timer2)
		{
			onWinAnimationTimerDone(timer2);
		}
		else if (timer is BookcaseDelayTimer timer3)
		{
			onBookcaseDelayTimerDone(timer3);
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

	private void onBookcaseDelayTimerDone(BookcaseDelayTimer timer)
	{
		winAnimation();
	}
}

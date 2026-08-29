using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.VFX;

public class PirateDarkest2Logic : LevelLogic, ISaveable
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

	public sealed class SolvedTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 1;
		}

		public SolvedTimer()
		{
		}

		public SolvedTimer(int index)
		{
			this.index = index;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in index, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			index = reader.ReadInt32();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("index: " + $"{index}");
			return stringBuilder.ToString();
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
	public Slot[] keySlots;

	[DontSave]
	public TweenState[] keyTweens;

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

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (targetSwitch == obeliskSwitch && game.hasAuthority(targetSwitch))
		{
			game.showExitLevelDialogue();
		}
	}

	public override void onSlot(Slot slot)
	{
		if (checkSolution())
		{
			solveKeys();
		}
	}

	private bool checkSolution()
	{
		Slot[] array = keySlots;
		foreach (Slot slot in array)
		{
			if (slot.acceptItems.Length == 0)
			{
				if (!(slot.insertedItem == null))
				{
					return false;
				}
				continue;
			}
			if (slot.insertedItem == null)
			{
				return false;
			}
			Array.Exists(slot.acceptItems, (Item x) => x == slot.insertedItem);
		}
		return true;
	}

	private void solveKeys()
	{
		Slot[] array = keySlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		game.startTimer(new SolvedTimer(0), 0.5f);
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
			1 => new SolvedTimer(), 
			_ => null, 
		};
	}

	[CompilerGenerated]
	public override void onTimerUpdate(Timer timer)
	{
		if (!(timer is WinAnimationTimer))
		{
			_ = timer is SolvedTimer;
		}
	}

	[CompilerGenerated]
	public override void onTimerDone(Timer timer)
	{
		if (timer is WinAnimationTimer timer2)
		{
			onWinAnimationTimerDone(timer2);
		}
		else if (timer is SolvedTimer timer3)
		{
			onSolvedTimerDone(timer3);
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

	private void onSolvedTimerDone(SolvedTimer timer)
	{
		if (timer.index == 0)
		{
			TweenState[] array = keyTweens;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].transitionTo("NewState");
			}
			game.startTimer(new SolvedTimer(1), 1.4f);
		}
		else if (timer.index == 1)
		{
			game.levelCompleted();
		}
	}
}

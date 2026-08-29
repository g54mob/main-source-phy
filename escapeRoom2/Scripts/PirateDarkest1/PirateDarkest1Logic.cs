using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.VFX;

public class PirateDarkest1Logic : LevelLogic, ISaveable
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

	private enum GemColor
	{
		Circle = 0,
		Star = 1,
		Octo = 2,
		Compass = 3,
		None = 4
	}

	[Serializable]
	public class GemPoints
	{
		[DontSave]
		public List<GameObject> points;
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
	private const int gemPointsCount = 20;

	[DontSave]
	private List<GemColor> gemSolution = new List<GemColor>
	{
		GemColor.Compass,
		GemColor.Octo,
		GemColor.Star,
		GemColor.Circle,
		GemColor.Star,
		GemColor.Compass,
		GemColor.Octo,
		GemColor.Circle,
		GemColor.Circle,
		GemColor.Compass,
		GemColor.Compass,
		GemColor.Octo,
		GemColor.Octo,
		GemColor.Star,
		GemColor.Circle,
		GemColor.Star,
		GemColor.Compass,
		GemColor.Compass,
		GemColor.Star,
		GemColor.Star
	};

	private List<GemColor> gemCurrentState = new List<GemColor>();

	private GemColor gemCurrentColor = GemColor.None;

	[DontSave]
	public List<GemPoints> gemPoints;

	[DontSave]
	public SlidableGraph gemSlidableGraph;

	[DontSave]
	public GameObject[] currentColorSign;

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
		foreach (GemPoints gemPoint in gemPoints)
		{
			for (int i = 0; i < gemPoint.points.Count; i++)
			{
				gemPoint.points[i].SetActive(i == 4);
			}
		}
		gemCurrentState = new List<GemColor>();
		for (int j = 0; j < 20; j++)
		{
			gemCurrentState.Add(GemColor.None);
		}
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

	public override void onSlidableGraphReleased(SlidableGraph.OnPieceInteraction context)
	{
		if (context.graph == gemSlidableGraph && checkGemShip())
		{
			winAnimation();
		}
	}

	private bool checkGemShip()
	{
		for (int i = 0; i < 20; i++)
		{
			if (gemCurrentState[i] != gemSolution[i])
			{
				return false;
			}
		}
		return true;
	}

	public override void onSlidableGraphArrivedToNode(SlidableGraph.ToNode context)
	{
		if (!(context.graph == gemSlidableGraph))
		{
			return;
		}
		int num = gemSlidableGraph.nodes.IndexOf(context.toNode);
		if (num == 24)
		{
			for (int i = 0; i < 20; i++)
			{
				gemCurrentColor = GemColor.None;
			}
		}
		if (num >= 20)
		{
			switch (num)
			{
			case 22:
				gemCurrentColor = GemColor.Circle;
				break;
			case 20:
				gemCurrentColor = GemColor.Star;
				break;
			case 21:
				gemCurrentColor = GemColor.Octo;
				break;
			case 23:
				gemCurrentColor = GemColor.Compass;
				break;
			}
			for (int j = 0; j < currentColorSign.Length; j++)
			{
				currentColorSign[j].SetActive(gemCurrentColor == (GemColor)j);
			}
		}
		else
		{
			for (int k = 0; k < 5; k++)
			{
				gemPoints[num].points[k].SetActive(k == (int)gemCurrentColor);
			}
			gemCurrentState[num] = gemCurrentColor;
		}
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.WriteList(gemCurrentState, delegate(FastBinaryWriter w, GemColor e)
		{
			int value2 = (int)e;
			w.Write(in value2, default(FastBinaryWriter.ForPrimitives));
		});
		int value = (int)gemCurrentColor;
		writer.Write(in value, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		gemCurrentState = reader.ReadList((FastBinaryReader r) => (GemColor)r.ReadInt32());
		gemCurrentColor = (GemColor)reader.ReadInt32();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		List<GemColor> list = reader.ReadList((FastBinaryReader r) => (GemColor)r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gemCurrentState[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GemColor gemColor = (GemColor)reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gemCurrentColor",
			fieldValue = $"{gemColor}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		if (id == 0)
		{
			return new WinAnimationTimer();
		}
		return null;
	}

	[CompilerGenerated]
	public override void onTimerUpdate(Timer timer)
	{
		_ = timer is WinAnimationTimer;
	}

	[CompilerGenerated]
	public override void onTimerDone(Timer timer)
	{
		if (timer is WinAnimationTimer timer2)
		{
			onWinAnimationTimerDone(timer2);
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

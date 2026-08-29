using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.VFX;

public class SpaceDarkest4Logic : LevelLogic, ISaveable
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
	public GameObject[] projectionRuinPieces;

	[DontSave]
	private HashSet<GameObject> projectionCylinders = new HashSet<GameObject>();

	[DontSave]
	private HashSet<GameObject> projectionPillars = new HashSet<GameObject>();

	[DontSave]
	private HashSet<GameObject> projectionPyramids = new HashSet<GameObject>();

	[DontSave]
	public MaterialState[] projectionZoomsMs_1;

	[DontSave]
	public MaterialState[] projectionZoomsMs_2;

	[DontSave]
	public MaterialState[] projectionZoomsMs_3;

	[DontSave]
	public MaterialState[] projectionZoomsMs_4;

	[DontSave]
	public MaterialState[] projectionZoomsMs_5;

	[DontSave]
	public MaterialState[] projectionZoomsMs_6;

	[DontSave]
	private int[] projectionZoom_1Nodes = new int[4] { 8, 9, 10, 11 };

	[DontSave]
	private int[] projectionZoom_2Nodes = new int[4] { 0, 1, 2, 3 };

	[DontSave]
	private int[] projectionZoom_3Nodes = new int[3] { 0, 4, 8 };

	[DontSave]
	private int[] projectionZoom_4Nodes = new int[3] { 3, 7, 11 };

	[DontSave]
	private int[] projectionZoom_5Nodes = new int[4] { 5, 4, 6, 7 };

	[DontSave]
	private int[] projectionZoom_6Nodes = new int[3] { 2, 6, 10 };

	[DontSave]
	private int[][] solution = new int[6][]
	{
		new int[7] { 1, 0, 2, 2, 0, 1, 1 },
		new int[7] { 0, 1, 0, 0, 0, 2, 0 },
		new int[7] { 0, 1, 2, 1, 1, 2, 0 },
		new int[7] { 0, 0, 1, 2, 0, 0, 1 },
		new int[7] { 1, 0, 2, 2, 1, 0, 1 },
		new int[7] { 1, 1, 2, 2, 0, 2, 0 }
	};

	[DontSave]
	public SlidableGraph projectionSlidableGraph;

	[DontSave]
	public Zoomable ruinsZoomable;

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

	private void projectionCheckSolution()
	{
		bool flag = true;
		List<int[]> list = new List<int[]> { projectionZoom_1Nodes, projectionZoom_2Nodes, projectionZoom_3Nodes, projectionZoom_4Nodes, projectionZoom_5Nodes, projectionZoom_6Nodes };
		List<MaterialState[]> list2 = new List<MaterialState[]> { projectionZoomsMs_1, projectionZoomsMs_2, projectionZoomsMs_3, projectionZoomsMs_4, projectionZoomsMs_5, projectionZoomsMs_6 };
		Array.ForEach(projectionZoomsMs_1, delegate(MaterialState x)
		{
			x.transitionToDuration("Fade", 0.2f);
		});
		Array.ForEach(projectionZoomsMs_2, delegate(MaterialState x)
		{
			x.transitionToDuration("Fade", 0.2f);
		});
		Array.ForEach(projectionZoomsMs_3, delegate(MaterialState x)
		{
			x.transitionToDuration("Fade", 0.2f);
		});
		Array.ForEach(projectionZoomsMs_4, delegate(MaterialState x)
		{
			x.transitionToDuration("Fade", 0.2f);
		});
		Array.ForEach(projectionZoomsMs_5, delegate(MaterialState x)
		{
			x.transitionToDuration("Fade", 0.2f);
		});
		Array.ForEach(projectionZoomsMs_6, delegate(MaterialState x)
		{
			x.transitionToDuration("Fade", 0.2f);
		});
		for (int num = 0; num < list.Count; num++)
		{
			for (int num2 = 0; num2 < list[num].Length; num2++)
			{
				if (!projectionSlidableGraph.tryGetPiece(projectionSlidableGraph.nodes[list[num][num2]], out var piece) || !(piece.gameObject != null))
				{
					continue;
				}
				if (projectionCylinders.Contains(piece.gameObject))
				{
					list2[num][0].transitionToDuration("Default", 0.2f);
				}
				if (projectionPyramids.Contains(piece.gameObject))
				{
					if (Math.Abs(Vector3.Dot(list2[num][1].transform.right, piece.transform.GetChild(0).right)) > 0.5f)
					{
						list2[num][1].transitionToDuration("Default", 0.2f);
					}
					if (Math.Abs(Vector3.Dot(list2[num][5].transform.right, piece.transform.GetChild(0).right)) < 0.5f)
					{
						list2[num][5].transitionToDuration("Default", 0.2f);
					}
				}
				if (projectionPillars.Contains(piece.gameObject))
				{
					if (Vector3.Dot(list2[num][4].transform.up, piece.transform.GetChild(0).up - piece.transform.GetChild(0).right) > 0.5f)
					{
						list2[num][4].transitionToDuration("Default", 0.2f);
					}
					else
					{
						list2[num][6].transitionToDuration("Default", 0.2f);
					}
					list2[num][2].transitionToDuration("Default", 0.2f);
					list2[num][3].transitionToDuration("Default", 0.2f);
				}
			}
		}
		for (int num3 = 0; num3 < solution.Length; num3++)
		{
			for (int num4 = 0; num4 < solution[num3].Length; num4++)
			{
				int num5 = ((list2[num3][num4].getTargetWeight("Fade") < 0.5f) ? 1 : 0);
				if (solution[num3][num4] != 2 && num5 != solution[num3][num4])
				{
					flag = false;
				}
			}
		}
		if (flag)
		{
			solve();
		}
		void solve()
		{
			ruinsZoomable.targetable = false;
			game.increaseZoomCounter(ruinsZoomable.gameObject);
			projectionSlidableGraph.piecesList.ForEach(delegate(SlidableGraphPiece x)
			{
				x.targetable = false;
			});
			winAnimation();
		}
	}

	public override void onInit()
	{
		projectionCylinders.Add(projectionRuinPieces[0]);
		projectionCylinders.Add(projectionRuinPieces[7]);
		projectionPyramids.Add(projectionRuinPieces[1]);
		projectionPyramids.Add(projectionRuinPieces[5]);
		projectionPyramids.Add(projectionRuinPieces[6]);
		projectionPillars.Add(projectionRuinPieces[2]);
		projectionPillars.Add(projectionRuinPieces[3]);
		projectionPillars.Add(projectionRuinPieces[4]);
		projectionCheckSolution();
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
		if (context.graph == projectionSlidableGraph)
		{
			projectionCheckSolution();
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

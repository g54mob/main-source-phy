using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.VFX;

public class DraculaDarkest2Logic : LevelLogic, ISaveable
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

	public enum KingsPieceType
	{
		King = 0,
		Queen = 1,
		Princess = 2
	}

	public enum KingsColorType
	{
		Orange = 0,
		Yellow = 1,
		Purple = 2,
		Brown = 3,
		GreenTop = 4,
		GreenBottom = 5
	}

	public sealed class PuzzleDelayTimer : Timer
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
	private KingsColorType[][] kingsKingdoms = new KingsColorType[6][]
	{
		new KingsColorType[8]
		{
			KingsColorType.Orange,
			KingsColorType.Orange,
			KingsColorType.GreenTop,
			KingsColorType.GreenTop,
			KingsColorType.GreenTop,
			KingsColorType.GreenTop,
			KingsColorType.Brown,
			KingsColorType.Brown
		},
		new KingsColorType[8]
		{
			KingsColorType.Orange,
			KingsColorType.Orange,
			KingsColorType.GreenTop,
			KingsColorType.GreenTop,
			KingsColorType.GreenTop,
			KingsColorType.GreenTop,
			KingsColorType.Brown,
			KingsColorType.Brown
		},
		new KingsColorType[8]
		{
			KingsColorType.Orange,
			KingsColorType.Orange,
			KingsColorType.Purple,
			KingsColorType.Purple,
			KingsColorType.Purple,
			KingsColorType.Purple,
			KingsColorType.Brown,
			KingsColorType.Brown
		},
		new KingsColorType[8]
		{
			KingsColorType.Yellow,
			KingsColorType.Yellow,
			KingsColorType.Purple,
			KingsColorType.Purple,
			KingsColorType.Purple,
			KingsColorType.Purple,
			KingsColorType.GreenBottom,
			KingsColorType.GreenBottom
		},
		new KingsColorType[8]
		{
			KingsColorType.Yellow,
			KingsColorType.Yellow,
			KingsColorType.Yellow,
			KingsColorType.Purple,
			KingsColorType.Purple,
			KingsColorType.GreenBottom,
			KingsColorType.GreenBottom,
			KingsColorType.GreenBottom
		},
		new KingsColorType[8]
		{
			KingsColorType.Yellow,
			KingsColorType.Yellow,
			KingsColorType.Yellow,
			KingsColorType.Yellow,
			KingsColorType.GreenBottom,
			KingsColorType.GreenBottom,
			KingsColorType.GreenBottom,
			KingsColorType.GreenBottom
		}
	};

	[DontSave]
	public SlidableGraph slidableGraphDarkest;

	[DontSave]
	public MaterialState[] kingsPiecesImages;

	private bool isSolved;

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
		slidableGraphDarkest.isEdgeBlockedPredicate = delegate(GameObject node, GameObject neighbourNode, SlidableGraphPiece piece)
		{
			int num = slidableGraphDarkest.nodes.IndexOf(node);
			int num2 = slidableGraphDarkest.nodes.IndexOf(neighbourNode);
			int num3 = num % 8;
			int num4 = num / 8;
			int num5 = num2 % 8;
			int num6 = num2 / 8;
			int num7 = slidableGraphDarkest.piecesList.IndexOf(piece);
			int dir = -1;
			if (num5 < num3)
			{
				dir = 3;
			}
			else if (num5 > num3)
			{
				dir = 1;
			}
			else if (num6 > num4)
			{
				dir = 2;
			}
			else if (num6 < num4)
			{
				dir = 0;
			}
			List<int> list = GetPositionsOuter(num7, dir);
			for (int i = 0; i < slidableGraphDarkest.piecesList.Count; i++)
			{
				if (i != num7)
				{
					foreach (int position in GetPositions(i))
					{
						if (list.Contains(position))
						{
							return true;
						}
					}
				}
			}
			switch (num7)
			{
			case 0:
				if (num3 == 0 || num5 == 0)
				{
					return true;
				}
				break;
			case 1:
				if (num4 == 0 || num6 == 0)
				{
					return true;
				}
				if (num4 == 5 || num6 == 5)
				{
					return true;
				}
				break;
			case 2:
				if (num3 == 0 || num5 == 0)
				{
					return true;
				}
				if (num3 == 6 || num5 == 6)
				{
					return true;
				}
				if (num3 == 7 || num5 == 7)
				{
					return true;
				}
				break;
			case 3:
				if (num3 == 0 || num5 == 0)
				{
					return true;
				}
				if (num3 == 7 || num5 == 7)
				{
					return true;
				}
				if (num4 == 5 || num6 == 5)
				{
					return true;
				}
				break;
			case 4:
				if (num3 == 0 || num5 == 0)
				{
					return true;
				}
				if (num4 == 5 || num6 == 5)
				{
					return true;
				}
				break;
			}
			return false;
		};
		List<int> GetPositions(int pieceIndex)
		{
			List<int> list = new List<int>();
			int num = -1;
			if (slidableGraphDarkest.tryGetNode(slidableGraphDarkest.piecesList[pieceIndex], out var node))
			{
				num = slidableGraphDarkest.nodes.IndexOf(node);
			}
			if (num != -1)
			{
				switch (pieceIndex)
				{
				case 0:
					list.Add(num);
					list.Add(num - 1);
					break;
				case 1:
					list.Add(num);
					list.Add(num - 8);
					list.Add(num + 8);
					break;
				case 2:
					list.Add(num);
					list.Add(num - 1);
					list.Add(num + 1);
					list.Add(num + 2);
					break;
				case 3:
					list.Add(num);
					list.Add(num - 1);
					list.Add(num + 1);
					list.Add(num + 7);
					list.Add(num + 8);
					list.Add(num + 9);
					break;
				case 4:
					list.Add(num);
					list.Add(num - 1);
					list.Add(num + 7);
					break;
				}
			}
			return list;
		}
		List<int> GetPositionsOuter(int pieceIndex, int dir)
		{
			List<int> list = new List<int>();
			int num = -1;
			if (slidableGraphDarkest.tryGetNode(slidableGraphDarkest.piecesList[pieceIndex], out var node))
			{
				num = slidableGraphDarkest.nodes.IndexOf(node);
			}
			if (num != -1)
			{
				switch (pieceIndex)
				{
				case 0:
					if (num % 8 < 7 && dir == 1)
					{
						list.Add(num + 1);
					}
					if (num % 8 > 1 && dir == 3)
					{
						list.Add(num - 2);
					}
					if (num / 8 > 0 && dir == 0)
					{
						list.Add(num - 8);
					}
					if (num / 8 < 5 && dir == 2)
					{
						list.Add(num + 8);
					}
					if (num / 8 < 5 && num % 8 > 0 && dir == 2)
					{
						list.Add(num + 8 - 1);
					}
					if (num / 8 > 0 && num % 8 > 0 && dir == 0)
					{
						list.Add(num - 8 - 1);
					}
					break;
				case 1:
					if (num % 8 > 0 && dir == 3)
					{
						list.Add(num - 1);
					}
					if (num % 8 < 7 && dir == 1)
					{
						list.Add(num + 1);
					}
					if (num / 8 < 5 && num % 8 > 0 && dir == 3)
					{
						list.Add(num + 7);
					}
					if (num / 8 < 5 && num % 8 < 7 && dir == 1)
					{
						list.Add(num + 9);
					}
					if (num / 8 > 0 && num % 8 < 7 && dir == 1)
					{
						list.Add(num - 7);
					}
					if (num / 8 > 0 && num % 8 > 0 && dir == 3)
					{
						list.Add(num - 9);
					}
					if (num / 8 > 1 && dir == 0)
					{
						list.Add(num - 16);
					}
					if (num / 8 < 4 && dir == 2)
					{
						list.Add(num + 16);
					}
					break;
				case 2:
					if (num / 8 > 0 && dir == 0)
					{
						list.Add(num - 8);
					}
					if (num / 8 < 5 && dir == 2)
					{
						list.Add(num + 8);
					}
					if (num / 8 > 0 && num % 8 > 0 && dir == 0)
					{
						list.Add(num - 9);
					}
					if (num / 8 > 0 && num % 8 < 7 && dir == 0)
					{
						list.Add(num - 7);
					}
					if (num / 8 > 0 && num % 8 < 6 && dir == 0)
					{
						list.Add(num - 6);
					}
					if (num / 8 < 5 && num % 8 > 0 && dir == 2)
					{
						list.Add(num + 7);
					}
					if (num / 8 < 5 && num % 8 < 7 && dir == 2)
					{
						list.Add(num + 9);
					}
					if (num / 8 < 5 && num % 8 < 6 && dir == 2)
					{
						list.Add(num + 10);
					}
					if (num % 8 > 1 && dir == 3)
					{
						list.Add(num - 2);
					}
					if (num % 8 < 5 && dir == 1)
					{
						list.Add(num + 3);
					}
					break;
				case 3:
					if (num / 8 > 0 && num % 8 > 0 && dir == 0)
					{
						list.Add(num - 9);
					}
					if (num / 8 > 0 && dir == 0)
					{
						list.Add(num - 8);
					}
					if (num / 8 > 0 && num % 8 < 7 && dir == 0)
					{
						list.Add(num - 7);
					}
					if (num / 8 < 4 && num % 8 > 0 && dir == 2)
					{
						list.Add(num + 15);
					}
					if (num / 8 < 4 && dir == 2)
					{
						list.Add(num + 16);
					}
					if (num / 8 < 4 && num % 8 < 7 && dir == 2)
					{
						list.Add(num + 17);
					}
					if (num % 8 > 1 && dir == 3)
					{
						list.Add(num - 2);
					}
					if (num % 8 < 6 && dir == 1)
					{
						list.Add(num + 2);
					}
					if (num / 8 < 5 && num % 8 > 1 && dir == 3)
					{
						list.Add(num + 6);
					}
					if (num / 8 < 5 && num % 8 < 6 && dir == 1)
					{
						list.Add(num + 10);
					}
					break;
				case 4:
					if (num % 8 < 7 && dir == 1)
					{
						list.Add(num + 1);
					}
					if (num / 8 > 0 && dir == 0)
					{
						list.Add(num - 8);
					}
					if (num / 8 < 5 && (dir == 2 || dir == 1))
					{
						list.Add(num + 8);
					}
					if (num / 8 > 0 && num % 8 > 0 && dir == 0)
					{
						list.Add(num - 9);
					}
					if (num % 8 > 1 && dir == 3)
					{
						list.Add(num - 2);
					}
					if (num / 8 < 5 && num % 8 > 1 && dir == 3)
					{
						list.Add(num + 6);
					}
					if (num / 8 < 4 && num % 8 > 0 && dir == 2)
					{
						list.Add(num + 15);
					}
					break;
				}
			}
			return list;
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
		if (!isSolved && checkKingsGameSolution())
		{
			isSolved = true;
			game.startTimer(new PuzzleDelayTimer(), 1.5f);
		}
	}

	private bool checkKingsGameSolution()
	{
		Dictionary<(KingsPieceType, KingsColorType), MaterialState> dict = new Dictionary<(KingsPieceType, KingsColorType), MaterialState>();
		int num = -1;
		if (slidableGraphDarkest.tryGetNode(slidableGraphDarkest.piecesList[0], out var node))
		{
			num = slidableGraphDarkest.nodes.IndexOf(node);
		}
		int num2 = num / 8;
		int num3 = num % 8;
		countPiece(KingsPieceType.Princess, kingsKingdoms[num2][num3 - 1], kingsPiecesImages[0]);
		countPiece(KingsPieceType.Princess, kingsKingdoms[num2][num3], kingsPiecesImages[1]);
		int num4 = -1;
		if (slidableGraphDarkest.tryGetNode(slidableGraphDarkest.piecesList[1], out var node2))
		{
			num4 = slidableGraphDarkest.nodes.IndexOf(node2);
		}
		int num5 = num4 / 8;
		int num6 = num4 % 8;
		countPiece(KingsPieceType.King, kingsKingdoms[num5 - 1][num6], kingsPiecesImages[2]);
		countPiece(KingsPieceType.King, kingsKingdoms[num5 + 1][num6], kingsPiecesImages[3]);
		countPiece(KingsPieceType.Queen, kingsKingdoms[num5][num6], kingsPiecesImages[4]);
		int num7 = -1;
		if (slidableGraphDarkest.tryGetNode(slidableGraphDarkest.piecesList[2], out var node3))
		{
			num7 = slidableGraphDarkest.nodes.IndexOf(node3);
		}
		int num8 = num7 / 8;
		int num9 = num7 % 8;
		countPiece(KingsPieceType.King, kingsKingdoms[num8][num9], kingsPiecesImages[5]);
		countPiece(KingsPieceType.Queen, kingsKingdoms[num8][num9 + 1], kingsPiecesImages[6]);
		countPiece(KingsPieceType.Princess, kingsKingdoms[num8][num9 + 2], kingsPiecesImages[7]);
		countPiece(KingsPieceType.Princess, kingsKingdoms[num8][num9 - 1], kingsPiecesImages[8]);
		int num10 = -1;
		if (slidableGraphDarkest.tryGetNode(slidableGraphDarkest.piecesList[3], out var node4))
		{
			num10 = slidableGraphDarkest.nodes.IndexOf(node4);
		}
		int num11 = num10 / 8;
		int num12 = num10 % 8;
		countPiece(KingsPieceType.King, kingsKingdoms[num11][num12 - 1], kingsPiecesImages[9]);
		countPiece(KingsPieceType.King, kingsKingdoms[num11 + 1][num12], kingsPiecesImages[10]);
		countPiece(KingsPieceType.Queen, kingsKingdoms[num11 + 1][num12 - 1], kingsPiecesImages[11]);
		countPiece(KingsPieceType.Queen, kingsKingdoms[num11][num12 + 1], kingsPiecesImages[12]);
		countPiece(KingsPieceType.Princess, kingsKingdoms[num11 + 1][num12 + 1], kingsPiecesImages[13]);
		countPiece(KingsPieceType.Princess, kingsKingdoms[num11][num12], kingsPiecesImages[14]);
		int num13 = -1;
		if (slidableGraphDarkest.tryGetNode(slidableGraphDarkest.piecesList[4], out var node5))
		{
			num13 = slidableGraphDarkest.nodes.IndexOf(node5);
		}
		int num14 = num13 / 8;
		int num15 = num13 % 8;
		countPiece(KingsPieceType.King, kingsKingdoms[num14][num15 - 1], kingsPiecesImages[15]);
		countPiece(KingsPieceType.Queen, kingsKingdoms[num14 + 1][num15 - 1], kingsPiecesImages[16]);
		countPiece(KingsPieceType.Queen, kingsKingdoms[num14][num15], kingsPiecesImages[17]);
		MaterialState[] array = kingsPiecesImages;
		foreach (MaterialState materialState in array)
		{
			materialState.transitionTo(dict.ContainsValue(materialState) ? "Glow" : "Default");
		}
		return dict.Count == 18;
		void countPiece(KingsPieceType sign, KingsColorType color, MaterialState state)
		{
			if (!dict.ContainsKey((sign, color)))
			{
				dict[(sign, color)] = state;
			}
			else
			{
				dict[(sign, color)] = null;
			}
		}
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in isSolved, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		isSolved = reader.ReadBoolean();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isSolved",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new WinAnimationTimer(), 
			1 => new PuzzleDelayTimer(), 
			_ => null, 
		};
	}

	[CompilerGenerated]
	public override void onTimerUpdate(Timer timer)
	{
		if (!(timer is WinAnimationTimer) && timer is PuzzleDelayTimer timer2)
		{
			onPuzzleDelayTimerUpdate(timer2);
		}
	}

	private void onPuzzleDelayTimerUpdate(PuzzleDelayTimer timer)
	{
		MaterialState[] array = kingsPiecesImages;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].setWeight("Glow", Mathf.PingPong(5f * timer.unitTime, 1f));
		}
	}

	[CompilerGenerated]
	public override void onTimerDone(Timer timer)
	{
		if (timer is WinAnimationTimer timer2)
		{
			onWinAnimationTimerDone(timer2);
		}
		else if (timer is PuzzleDelayTimer timer3)
		{
			onPuzzleDelayTimerDone(timer3);
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

	private void onPuzzleDelayTimerDone(PuzzleDelayTimer timer)
	{
		winAnimation();
	}
}

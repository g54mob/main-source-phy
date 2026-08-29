using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.VFX;

public class SpaceDarkest1Logic : LevelLogic, ISaveable
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

	public sealed class CrateBlinkingTimer : Timer
	{
		public TweenState tween;

		public SwapperPiece swapperPiece;

		public override byte getTypeId()
		{
			return 1;
		}

		public CrateBlinkingTimer()
		{
		}

		public CrateBlinkingTimer(TweenState tween, SwapperPiece swapperPiece)
		{
			this.tween = tween;
			this.swapperPiece = swapperPiece;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(tween);
			writer.WriteComponent(swapperPiece);
		}

		public override void readData(FastBinaryReader reader)
		{
			tween = reader.ReadComponent<TweenState>();
			swapperPiece = reader.ReadComponent<SwapperPiece>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("tween: " + $"{tween}");
			stringBuilder.Append("swapperPiece: " + $"{swapperPiece}");
			return stringBuilder.ToString();
		}
	}

	public sealed class SolveCrateTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 2;
		}

		public SolveCrateTimer()
		{
		}

		public SolveCrateTimer(int index)
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

	public class CratePuzzleSegmentInfo
	{
		public int row;

		public int column;

		public int startingRow;

		public int startingColumn;

		public float rotation;

		public Vector3 startingLocalPosition;

		public Quaternion startingLocalRotation;

		public CratePuzzleType type;
	}

	public enum CratePuzzleType
	{
		Square = 0,
		OneNotch = 1,
		FourNotches = 2
	}

	public class CratePuzzleInstruction
	{
		public SwapperPiece piece;

		public SwapperPiece swappedEmpty;

		public int row;

		public int column;

		public int move;

		public int rotate;
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

	private List<CratePuzzleSegmentInfo> crateSegmentsInfo;

	private List<CratePuzzleInstruction> currentCratePuzzleInstructions;

	private bool isCrateSolved;

	private SwapperPiece crateFirstSelected;

	[DontSave]
	public RefArray<GameObject, Transform> crateSegmentObjects;

	[DontSave]
	public Swapper crateSwapper;

	[DontSave]
	public Switch3D crateUndo;

	[DontSave]
	public GameObject[] crateSlotBorders;

	[DontSave]
	public SwapperPiece[] crateSwaps;

	[DontSave]
	public SwapperPiece[] crateEmptySwaps;

	[DontSave]
	public TweenState[] crateSwapsTweens;

	[DontSave]
	public TweenState[] crateBordersTweens;

	[DontSave]
	public Transform[] crateSegmentSolutions;

	[DontSave]
	public Zoomable crateZoom;

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
		crateSegmentsInfo = new List<CratePuzzleSegmentInfo>();
		for (int i = 0; i < crateSegmentObjects.Length; i++)
		{
			crateSegmentsInfo.Add(new CratePuzzleSegmentInfo
			{
				row = ((i >= 3) ? ((i < 6) ? 1 : 2) : 0),
				column = i % 3,
				rotation = 0f,
				startingLocalPosition = crateSegmentObjects[i].Get<Transform>(0f).localPosition,
				startingLocalRotation = crateSegmentObjects[i].Get<Transform>(0f).localRotation,
				startingRow = ((i >= 3) ? ((i < 6) ? 1 : 2) : 0),
				startingColumn = i % 3
			});
		}
		crateSegmentsInfo[0].type = CratePuzzleType.FourNotches;
		crateSegmentsInfo[1].type = CratePuzzleType.Square;
		crateSegmentsInfo[2].type = CratePuzzleType.OneNotch;
		crateSegmentsInfo[3].type = CratePuzzleType.OneNotch;
		crateSegmentsInfo[4].type = CratePuzzleType.OneNotch;
		crateSegmentsInfo[5].type = CratePuzzleType.Square;
		crateSegmentsInfo[6].type = CratePuzzleType.Square;
		crateSegmentsInfo[7].type = CratePuzzleType.OneNotch;
		crateSegmentsInfo[8].type = CratePuzzleType.Square;
		currentCratePuzzleInstructions = new List<CratePuzzleInstruction>();
		crateUndo.targetable = false;
	}

	public override void onSwitch3D(Switch3D switch3D, Switch3DEvent switchEvent)
	{
		onCrateSwitch3D(switch3D, switchEvent);
		if (switch3D == obeliskSwitch && game.hasAuthority(switch3D))
		{
			game.showExitLevelDialogue();
		}
	}

	public override void onSwapper(Swapper swapper, Swapper.SwapperEvent swapperEvent)
	{
		onCrateSwapper(swapperEvent);
	}

	private void onCrateSwitch3D(Switch3D target, Switch3DEvent switchEvent)
	{
		if (target == crateUndo && switchEvent == Switch3DEvent.Start)
		{
			onCrateUndo();
		}
	}

	private void onCrateSwapper(Swapper.SwapperEvent swapperEvent)
	{
		if (swapperEvent.isReset)
		{
			onCrateReset();
		}
		else if (swapperEvent.didSwap)
		{
			onCrateSwapperSwap(swapperEvent.first, swapperEvent.second);
		}
		else
		{
			onCrateSwapperSelected(swapperEvent.getCurrentSelected);
		}
	}

	private void onCrateSwapperSelected(SwapperPiece piece)
	{
		if (crateFirstSelected != null)
		{
			return;
		}
		crateFirstSelected = piece;
		crateUndo.targetable = false;
		float speed = 2f;
		int num = Array.IndexOf(crateSwaps, piece);
		crateSwapsTweens[num].transitionTo("Down", speed);
		for (int i = 0; i < crateSwaps.Length; i++)
		{
			if (i != num)
			{
				crateSwaps[i].targetable = false;
				crateSwapsTweens[i].transitionTo("Down", speed, 0f);
			}
		}
		int j;
		for (j = 0; j < crateEmptySwaps.Length; j++)
		{
			bool flag = currentCratePuzzleInstructions.Exists((CratePuzzleInstruction x) => x.swappedEmpty == crateEmptySwaps[j]);
			crateBordersTweens[j].transitionTo("Down", speed, flag ? 0f : 1f);
			crateEmptySwaps[j].targetable = !flag;
		}
	}

	private void onCrateReset()
	{
		crateHighlightInstructions();
		crateFirstSelected = null;
	}

	private void onCrateSwapperSwap(SwapperPiece first, SwapperPiece second)
	{
		crateUndo.targetable = true;
		crateFirstSelected = null;
		int num = Array.IndexOf(crateSwaps, first);
		int num2 = Array.IndexOf(crateEmptySwaps, second);
		bool flag = num < 3;
		bool flag2 = num == 3;
		CratePuzzleInstruction obj = new CratePuzzleInstruction
		{
			piece = first,
			swappedEmpty = second
		};
		int row;
		switch (num2)
		{
		default:
			row = -1;
			break;
		case 5:
		case 9:
			row = 2;
			break;
		case 4:
		case 10:
			row = 1;
			break;
		case 3:
		case 11:
			row = 0;
			break;
		}
		obj.row = row;
		int column;
		switch (num2)
		{
		default:
			column = -1;
			break;
		case 2:
		case 6:
			column = 2;
			break;
		case 1:
		case 7:
			column = 1;
			break;
		case 0:
		case 8:
			column = 0;
			break;
		}
		obj.column = column;
		obj.move = (flag ? ((num2 <= 8 && num2 >= 3) ? (-1) : ((num2 < 3 || num2 > 8) ? 1 : 0)) : 0);
		obj.rotate = ((!flag) ? (flag2 ? 1 : (-1)) : 0);
		CratePuzzleInstruction cratePuzzleInstruction = obj;
		currentCratePuzzleInstructions.Add(cratePuzzleInstruction);
		crateRunInstruction(cratePuzzleInstruction);
		cratePuzzleInstruction.piece.targetable = false;
		cratePuzzleInstruction.swappedEmpty.targetable = false;
		Quaternion localRotation = first.transform.localRotation;
		first.transform.localRotation = second.transform.localRotation;
		second.transform.localRotation = localRotation;
		if (checkCrateSolved())
		{
			solveCrate();
		}
		crateHighlightInstructions();
	}

	private void crateHighlightInstructions()
	{
		crateUndo.targetable = currentCratePuzzleInstructions.Count > 0;
		int i;
		for (i = 0; i < crateSwaps.Length; i++)
		{
			bool flag = currentCratePuzzleInstructions.Exists((CratePuzzleInstruction x) => x.piece == crateSwaps[i]);
			crateSwaps[i].targetable = !flag;
			crateSwapsTweens[i].transitionTo("Down", 2f, (!flag) ? 1f : 0f);
		}
		for (int num = 0; num < crateEmptySwaps.Length; num++)
		{
			crateEmptySwaps[num].targetable = false;
			crateBordersTweens[num].transitionTo("Down", 2f, 0f);
		}
	}

	private void onCrateUndo()
	{
		int num = currentCratePuzzleInstructions.Count - 1;
		if (num >= 0 && num < currentCratePuzzleInstructions.Count)
		{
			CratePuzzleInstruction cratePuzzleInstruction = currentCratePuzzleInstructions[num];
			currentCratePuzzleInstructions.RemoveAt(num);
			crateRunInstruction(cratePuzzleInstruction, reverse: true);
			Transform obj = cratePuzzleInstruction.piece.transform;
			Transform transform = cratePuzzleInstruction.swappedEmpty.transform;
			Vector3 position = cratePuzzleInstruction.swappedEmpty.transform.position;
			Vector3 position2 = cratePuzzleInstruction.piece.transform.position;
			Vector3 vector = (obj.position = position);
			vector = (transform.position = position2);
			Transform obj2 = cratePuzzleInstruction.piece.transform;
			transform = cratePuzzleInstruction.swappedEmpty.transform;
			Quaternion rotation = cratePuzzleInstruction.swappedEmpty.transform.rotation;
			Quaternion rotation2 = cratePuzzleInstruction.piece.transform.rotation;
			Quaternion quaternion = (obj2.rotation = rotation);
			quaternion = (transform.rotation = rotation2);
			SwapperPiece swapperPiece = ((cratePuzzleInstruction.piece.swappedPiece != null) ? cratePuzzleInstruction.piece.swappedPiece : cratePuzzleInstruction.piece);
			SwapperPiece swapperPiece2 = ((cratePuzzleInstruction.swappedEmpty.swappedPiece != null) ? cratePuzzleInstruction.swappedEmpty.swappedPiece : cratePuzzleInstruction.swappedEmpty);
			cratePuzzleInstruction.swappedEmpty.swappedPiece = ((cratePuzzleInstruction.swappedEmpty != swapperPiece) ? swapperPiece : null);
			cratePuzzleInstruction.piece.swappedPiece = ((cratePuzzleInstruction.piece != swapperPiece2) ? swapperPiece2 : null);
			cratePuzzleInstruction.piece.targetable = true;
			cratePuzzleInstruction.swappedEmpty.targetable = true;
			if (currentCratePuzzleInstructions.Count == 0)
			{
				crateUndo.targetable = false;
			}
			crateHighlightInstructions();
		}
	}

	private void crateRunInstruction(CratePuzzleInstruction instruction, bool reverse = false)
	{
		int num = instruction.move;
		int num2 = instruction.rotate;
		if (reverse)
		{
			num *= -1;
			num2 *= -1;
		}
		for (int i = 0; i < crateSegmentsInfo.Count; i++)
		{
			if (instruction.row == crateSegmentsInfo[i].row)
			{
				crateSegmentsInfo[i].column = (crateSegmentsInfo[i].column + num + 3) % 3;
				crateSegmentsInfo[i].rotation = (crateSegmentsInfo[i].rotation + (float)num2 + 4f) % 4f;
			}
			if (instruction.column == crateSegmentsInfo[i].column)
			{
				crateSegmentsInfo[i].row = (crateSegmentsInfo[i].row + num + 3) % 3;
				crateSegmentsInfo[i].rotation = (crateSegmentsInfo[i].rotation + (float)num2 + 4f) % 4f;
			}
			moveSegment(i);
			rotateSegment(i);
		}
		void moveSegment(int index)
		{
			CratePuzzleSegmentInfo segment = crateSegmentsInfo[index];
			CratePuzzleSegmentInfo cratePuzzleSegmentInfo = crateSegmentsInfo.Find((CratePuzzleSegmentInfo x) => x.startingRow == segment.row && x.startingColumn == segment.column);
			if (cratePuzzleSegmentInfo == null)
			{
				Debug.LogError($"Destination Segment is null for: {crateSegmentObjects[index].Get<GameObject>(0).name} -> {segment.row}, {segment.column}");
			}
			else
			{
				crateSegmentObjects[index].Get<Transform>(0f).localPosition = cratePuzzleSegmentInfo.startingLocalPosition;
			}
		}
		void rotateSegment(int index)
		{
			CratePuzzleSegmentInfo cratePuzzleSegmentInfo = crateSegmentsInfo[index];
			crateSegmentObjects[index].Get<Transform>(0f).localRotation = cratePuzzleSegmentInfo.startingLocalRotation * Quaternion.Euler(90f * cratePuzzleSegmentInfo.rotation * Vector3.forward);
		}
	}

	private bool checkCrateSolved()
	{
		for (int i = 0; i < crateSegmentSolutions.Length; i++)
		{
			int num = -1;
			for (int j = 0; j < crateSegmentSolutions.Length; j++)
			{
				if (!(Vector3.Distance(crateSegmentSolutions[j].position, crateSegmentObjects[i].Get<Transform>(0f).position) > 0.01f))
				{
					if (crateSegmentsInfo[j].type != crateSegmentsInfo[i].type)
					{
						return false;
					}
					num = j;
					break;
				}
			}
			if (num < 0)
			{
				return false;
			}
			float num2 = 5f;
			if (crateSegmentsInfo[i].type != CratePuzzleType.Square && crateSegmentsInfo[i].type != CratePuzzleType.FourNotches && crateSegmentsInfo[i].type == CratePuzzleType.OneNotch && !(Quaternion.Angle(crateSegmentSolutions[num].rotation, crateSegmentObjects[i].Get<Transform>(0f).rotation) <= num2))
			{
				return false;
			}
		}
		return true;
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveCrate()
	{
		if (!isCrateSolved)
		{
			isCrateSolved = true;
			SwapperPiece[] array = crateSwaps;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			array = crateEmptySwaps;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			crateUndo.targetable = false;
			game.startTimer(new SolveCrateTimer(0), 0.5f);
		}
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.WriteList(crateSegmentsInfo, delegate(FastBinaryWriter w, CratePuzzleSegmentInfo e)
		{
			w.WriteCratePuzzleSegmentInfo(e);
		});
		writer.WriteList(currentCratePuzzleInstructions, delegate(FastBinaryWriter w, CratePuzzleInstruction e)
		{
			w.WriteCratePuzzleInstruction(e);
		});
		writer.Write(in isCrateSolved, default(FastBinaryWriter.ForPrimitives));
		writer.WriteComponent(crateFirstSelected);
	}

	public virtual void load(FastBinaryReader reader)
	{
		crateSegmentsInfo = reader.ReadList((FastBinaryReader r) => r.ReadCratePuzzleSegmentInfo());
		currentCratePuzzleInstructions = reader.ReadList((FastBinaryReader r) => r.ReadCratePuzzleInstruction());
		isCrateSolved = reader.ReadBoolean();
		crateFirstSelected = reader.ReadComponent<SwapperPiece>();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		List<CratePuzzleSegmentInfo> list = reader.ReadList((FastBinaryReader r) => r.ReadCratePuzzleSegmentInfo());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crateSegmentsInfo[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<CratePuzzleInstruction> list2 = reader.ReadList((FastBinaryReader r) => r.ReadCratePuzzleInstruction());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentCratePuzzleInstructions[" + ((list2 == null) ? string.Empty : list2.Count.ToString()) + "]",
			fieldValue = (((list2 == null) ? "null" : string.Join(", ", list2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isCrateSolved",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		SwapperPiece arg = reader.ReadComponent<SwapperPiece>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "crateFirstSelected",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new WinAnimationTimer(), 
			1 => new CrateBlinkingTimer(), 
			2 => new SolveCrateTimer(), 
			_ => null, 
		};
	}

	[CompilerGenerated]
	public override void onTimerUpdate(Timer timer)
	{
		if (!(timer is WinAnimationTimer) && !(timer is CrateBlinkingTimer))
		{
			_ = timer is SolveCrateTimer;
		}
	}

	[CompilerGenerated]
	public override void onTimerDone(Timer timer)
	{
		if (timer is WinAnimationTimer timer2)
		{
			onWinAnimationTimerDone(timer2);
		}
		else if (timer is CrateBlinkingTimer timer3)
		{
			onCrateBlinkingTimerDone(timer3);
		}
		else if (timer is SolveCrateTimer timer4)
		{
			onSolveCrateTimerDone(timer4);
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

	private void onCrateBlinkingTimerDone(CrateBlinkingTimer timer)
	{
		if (timer.swapperPiece.targetable)
		{
			bool flag = timer.tween.findStateByName("Down").targetWeight < 0.1f;
			float weight = (flag ? 1f : 0f);
			timer.tween.transitionTo("Down", 2f, weight);
			game.startTimer(new CrateBlinkingTimer(timer.tween, timer.swapperPiece), flag ? 1.3f : 0.6f);
		}
	}

	private void onSolveCrateTimerDone(SolveCrateTimer timer)
	{
		int index = timer.index;
		if (index == 0)
		{
			crateZoom.targetable = false;
			game.increaseZoomCounter(crateZoom.gameObject);
			game.startTimer(new SolveCrateTimer(1), 1.1f);
		}
		if (index == 1)
		{
			winAnimation();
		}
	}
}

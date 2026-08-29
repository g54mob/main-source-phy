using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using FMOD;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.VFX;

public class DraculaDarkest4Logic : LevelLogic, ISaveable
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

	[Serializable]
	public class VampireObject
	{
		[DontSave]
		public GameObject worldObject;

		[DontSave]
		public MaterialState highlightState;

		[DontSave]
		public Texture eyeLetter;
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
	public RefArray<Turnable, Transform> puzzleTurnables;

	[DontSave]
	private EventInstance endTurningEvent;

	[DontSave]
	public Switch3D obeliskSwitch;

	private bool puzzleDone;

	[DontSave]
	private int[][] puzzleCorrect0 = new int[2][]
	{
		new int[4] { 4, 4, 1, 5 },
		new int[4] { 5, 4, 1, 4 }
	};

	[DontSave]
	private int[][] puzzleCorrect1 = new int[2][]
	{
		new int[3] { 2, 3, 5 },
		new int[3] { 5, 2, 2 }
	};

	[DontSave]
	private int[][] puzzleCorrect2 = new int[2][]
	{
		new int[3] { 9, 3, 0 },
		new int[3] { 0, 2, 9 }
	};

	[DontSave]
	private int[][] puzzleCorrect3 = new int[6][]
	{
		new int[3] { 1, 4, 9 },
		new int[3] { 1, 0, 9 },
		new int[3] { 1, 1, 9 },
		new int[3] { 9, 4, 1 },
		new int[3] { 9, 0, 1 },
		new int[3] { 9, 1, 1 }
	};

	private int eyeLastSelectedIndex = -1;

	private int eyeCurrentSelectedIndex = -1;

	private float eyeAlpha;

	[DontSave]
	public List<VampireObject> vampireObjects;

	[DontSave]
	public Item eye;

	[DontSave]
	public MeshRenderer eyeMR;

	[DontSave]
	private Material eyeMRMat;

	private bool wasSelected;

	[DontSave]
	public GameObject eyeBeam;

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
		endTurningEvent = PineFmod.createInstance(puzzleTurnables[0].Get<Turnable>(0).soundTurn.Guid);
		PineFmod.set3DAttributes(endTurningEvent, PineFmod.to3DAttributes(puzzleTurnables[0].Get<Transform>(0f)));
		eyeMRMat = eyeMR.materials[1];
		foreach (VampireObject vampireObject in vampireObjects)
		{
			if (vampireObject.highlightState != null)
			{
				vampireObject.highlightState.transitionTo("Transparent", 10f);
			}
		}
	}

	public override void onUpdate()
	{
		if (puzzleDone)
		{
			foreach (Ref<Turnable, Transform> puzzleTurnable in puzzleTurnables)
			{
				puzzleTurnable.Get<Transform>(0f).Rotate(Vector3.forward * Time.deltaTime * 300f);
			}
			if (endTurningEvent.getPlaybackState(out var state) == RESULT.OK && state != PLAYBACK_STATE.PLAYING)
			{
				PineFmod.start(endTurningEvent);
			}
		}
		eyeCurrentSelectedIndex = -1;
		if (game.isSelectedInPCMode(eye.gameObject) && !game.isInTopZoom(eye.gameObject))
		{
			wasSelected = true;
			VampireObject vampireObject = null;
			foreach (VampireObject vampireObject2 in vampireObjects)
			{
				if (vampireObject == null)
				{
					Collider[] componentsInChildren = vampireObject2.worldObject.GetComponentsInChildren<Collider>();
					for (int i = 0; i < componentsInChildren.Length; i++)
					{
						if (componentsInChildren[i].Raycast(game.playerViewRay, out var hitInfo, 10f) && Game.inReach(game.playerViewRay.origin, hitInfo.point, 2.5f))
						{
							vampireObject = vampireObject2;
							break;
						}
					}
				}
				if (vampireObject2 == vampireObject)
				{
					eyeCurrentSelectedIndex = vampireObjects.IndexOf(vampireObject2);
					if (vampireObject2.highlightState != null)
					{
						vampireObject2.highlightState.transitionTo("Default", 10f);
					}
				}
				else if (vampireObject2.highlightState != null)
				{
					vampireObject2.highlightState.transitionTo("Faded", 10f);
				}
			}
		}
		else if (wasSelected)
		{
			wasSelected = false;
			foreach (VampireObject vampireObject3 in vampireObjects)
			{
				if (vampireObject3.highlightState != null)
				{
					vampireObject3.highlightState.transitionTo("Transparent", 10f);
				}
			}
		}
		if (eyeCurrentSelectedIndex != eyeLastSelectedIndex)
		{
			eyeAlpha = Mathf.MoveTowards(eyeAlpha, 0f, 4f * Time.deltaTime);
			if (eyeAlpha == 0f)
			{
				eyeLastSelectedIndex = eyeCurrentSelectedIndex;
				if (eyeLastSelectedIndex != -1)
				{
					eyeMRMat.SetTexture("_EmissiveColorMap", vampireObjects[eyeLastSelectedIndex].eyeLetter);
					PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Air/Short_Air_Woosh", vampireObjects[eyeLastSelectedIndex].worldObject);
				}
			}
		}
		else if (eyeLastSelectedIndex != -1)
		{
			eyeAlpha = Mathf.MoveTowards(eyeAlpha, 1f, 6f * Time.deltaTime);
		}
		eyeMRMat.SetColor("_EmissiveColor", 6f * new Color(eyeAlpha, eyeAlpha, eyeAlpha, 1f));
	}

	public override void onTurnableMoved(Turnable turnable, MoveEvent moveEvent)
	{
		if (puzzleDone || moveEvent != MoveEvent.Snapped)
		{
			return;
		}
		bool flag = true;
		bool flag2 = false;
		for (int i = 0; i < puzzleCorrect0.Length; i++)
		{
			bool flag3 = true;
			for (int j = 0; j < puzzleCorrect0[i].Length; j++)
			{
				if (puzzleCorrect0[i][j] != puzzleTurnables[j].Get<Turnable>(0).value)
				{
					flag3 = false;
					break;
				}
			}
			if (flag3)
			{
				flag2 = true;
				break;
			}
		}
		if (!flag2)
		{
			flag = false;
		}
		flag2 = false;
		for (int k = 0; k < puzzleCorrect1.Length; k++)
		{
			bool flag4 = true;
			for (int l = 0; l < puzzleCorrect1[k].Length; l++)
			{
				if (puzzleCorrect1[k][l] != puzzleTurnables[l + 4].Get<Turnable>(0).value)
				{
					flag4 = false;
					break;
				}
			}
			if (flag4)
			{
				flag2 = true;
				break;
			}
		}
		if (!flag2)
		{
			flag = false;
		}
		flag2 = false;
		for (int m = 0; m < puzzleCorrect2.Length; m++)
		{
			bool flag5 = true;
			for (int n = 0; n < puzzleCorrect2[m].Length; n++)
			{
				if (puzzleCorrect2[m][n] != puzzleTurnables[n + 7].Get<Turnable>(0).value)
				{
					flag5 = false;
					break;
				}
			}
			if (flag5)
			{
				flag2 = true;
				break;
			}
		}
		if (!flag2)
		{
			flag = false;
		}
		flag2 = false;
		for (int num = 0; num < puzzleCorrect3.Length; num++)
		{
			bool flag6 = true;
			for (int num2 = 0; num2 < puzzleCorrect3[num].Length; num2++)
			{
				if (puzzleCorrect3[num][num2] != puzzleTurnables[num2 + 10].Get<Turnable>(0).value)
				{
					flag6 = false;
					break;
				}
			}
			if (flag6)
			{
				flag2 = true;
				break;
			}
		}
		if (!flag2)
		{
			flag = false;
		}
		if (!flag || !puzzleTurnables[0].Get<Turnable>(0).targetable)
		{
			return;
		}
		foreach (Ref<Turnable, Transform> puzzleTurnable in puzzleTurnables)
		{
			puzzleTurnable.Get<Turnable>(0).targetable = false;
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
		writer.Write(in eyeLastSelectedIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in eyeCurrentSelectedIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in eyeAlpha, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in wasSelected, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		puzzleDone = reader.ReadBoolean();
		eyeLastSelectedIndex = reader.ReadInt32();
		eyeCurrentSelectedIndex = reader.ReadInt32();
		eyeAlpha = reader.ReadSingle();
		wasSelected = reader.ReadBoolean();
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
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "eyeLastSelectedIndex",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "eyeCurrentSelectedIndex",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num3 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "eyeAlpha",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wasSelected",
			fieldValue = $"{flag2}",
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

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.VFX;

public class SpaceDarkest2Logic : LevelLogic, ISaveable
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

	public GameObject[] redSolutions;

	public GameObject[] greenSolutions;

	public GameObject[] zeros;

	public GameObject[] ones;

	public Switch3D[] numberSwitches;

	private int[,] centerTargets = new int[5, 5]
	{
		{ 2, 2, 2, 2, 2 },
		{ 2, 2, 1, 2, 3 },
		{ 3, 3, 2, 3, 3 },
		{ 1, 2, 3, 3, 1 },
		{ 1, 1, 1, 2, 1 }
	};

	private int[,] gridValues = new int[6, 6];

	public MaterialState winObeliskGlow;

	public VisualEffect[] winVisualEffects;

	public GameObject winCutScene;

	public AnimationSampler winAnimationSampler;

	public float winTurnOnGlowPoint = 1f;

	public float winTurnOffGlowPoint = 5f;

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

	public override void onSwitch3D(Switch3D switch3D, Switch3DEvent switchEvent)
	{
		int num = Array.IndexOf(numberSwitches, switch3D);
		if (num != -1 && switchEvent == Switch3DEvent.Start)
		{
			int num2 = 6;
			int num3 = num / num2;
			int num4 = num % num2;
			gridValues[num3, num4] = (gridValues[num3, num4] + 1) % 3;
			if (gridValues[num3, num4] == 0)
			{
				ones[6 * num3 + num4].SetActive(value: false);
			}
			if (gridValues[num3, num4] == 1)
			{
				zeros[6 * num3 + num4].SetActive(value: true);
			}
			if (gridValues[num3, num4] == 2)
			{
				zeros[6 * num3 + num4].SetActive(value: false);
				ones[6 * num3 + num4].SetActive(value: true);
			}
			bool flag = true;
			for (int i = 0; i < 5; i++)
			{
				for (int j = 0; j < 5; j++)
				{
					int num5 = 5 * j + i;
					if (isCenterCorrect(i, j))
					{
						if (!greenSolutions[num5].activeSelf)
						{
							PineFmod.playOneShotSound("event:/Sound Effects/03 Interactable/Buttons & Switches/Digital_Button_04");
						}
						greenSolutions[num5].SetActive(value: true);
						redSolutions[num5].SetActive(value: false);
						continue;
					}
					if (greenSolutions[num5].activeSelf)
					{
						PineFmod.playOneShotSound("event:/Sound Effects/03 Interactable/Buttons & Switches/Digital_Button_04");
					}
					flag = false;
					greenSolutions[num5].SetActive(value: false);
					redSolutions[num5].SetActive(value: true);
				}
			}
			if (flag)
			{
				winAnimation();
			}
		}
		if (switch3D == obeliskSwitch && game.hasAuthority(switch3D))
		{
			game.showExitLevelDialogue();
		}
	}

	public bool isCenterCorrect(int x, int y)
	{
		int num = centerTargets[y, x];
		int num2 = gridValues[y, x + 1];
		int num3 = gridValues[y, x];
		int num4 = gridValues[y + 1, x + 1];
		int num5 = gridValues[y + 1, x];
		if (num2 == 0 || num3 == 0 || num4 == 0 || num5 == 0)
		{
			return false;
		}
		int num6 = num2 + num3 + num4 + num5 - 4;
		if (x == 0 && y == 0)
		{
			Debug.Log(num2 + " " + num3 + " " + num4 + " " + num5);
		}
		return num6 == num;
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.WriteArray(redSolutions, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteArray(greenSolutions, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteArray(zeros, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteArray(ones, delegate(FastBinaryWriter w, GameObject e)
		{
			w.WriteGameObject(e);
		});
		writer.WriteArray(numberSwitches, delegate(FastBinaryWriter w, Switch3D e)
		{
			w.WriteComponent(e);
		});
		writer.WriteArray2D(centerTargets, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteArray2D(gridValues, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteComponent(winObeliskGlow);
		writer.WriteArray(winVisualEffects, delegate(FastBinaryWriter w, VisualEffect e)
		{
			w.WriteComponent(e);
		});
		writer.WriteGameObject(winCutScene);
		writer.WriteComponent(winAnimationSampler);
		writer.Write(in winTurnOnGlowPoint, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in winTurnOffGlowPoint, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		redSolutions = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		greenSolutions = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		zeros = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		ones = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		numberSwitches = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Switch3D>());
		centerTargets = reader.ReadArray2D((FastBinaryReader r) => r.ReadInt32());
		gridValues = reader.ReadArray2D((FastBinaryReader r) => r.ReadInt32());
		winObeliskGlow = reader.ReadComponent<MaterialState>();
		winVisualEffects = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<VisualEffect>());
		winCutScene = reader.ReadGameObject();
		winAnimationSampler = reader.ReadComponent<AnimationSampler>();
		winTurnOnGlowPoint = reader.ReadSingle();
		winTurnOffGlowPoint = reader.ReadSingle();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		GameObject[] array = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "redSolutions[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array2 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "greenSolutions[" + ((array2 == null) ? string.Empty : array2.Length.ToString()) + "]",
			fieldValue = (((array2 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array3 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "zeros[" + ((array3 == null) ? string.Empty : array3.Length.ToString()) + "]",
			fieldValue = (((array3 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject[] array4 = reader.ReadArray((FastBinaryReader r) => r.ReadGameObject());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "ones[" + ((array4 == null) ? string.Empty : array4.Length.ToString()) + "]",
			fieldValue = (((array4 == null) ? "null" : string.Join(", ", (IEnumerable<GameObject>)array4)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		Switch3D[] array5 = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<Switch3D>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "numberSwitches[" + ((array5 == null) ? string.Empty : array5.Length.ToString()) + "]",
			fieldValue = (((array5 == null) ? "null" : string.Join(", ", (IEnumerable<Switch3D>)array5)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[,] array6 = reader.ReadArray2D((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "centerTargets[" + ((array6 == null) ? string.Empty : array6.Length.ToString()) + "]",
			fieldValue = (((array6 == null) ? "null" : "NOT SUPPORTED") ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int[,] array7 = reader.ReadArray2D((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "gridValues[" + ((array7 == null) ? string.Empty : array7.Length.ToString()) + "]",
			fieldValue = (((array7 == null) ? "null" : "NOT SUPPORTED") ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		MaterialState arg = reader.ReadComponent<MaterialState>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "winObeliskGlow",
			fieldValue = $"{arg}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		VisualEffect[] array8 = reader.ReadArray((FastBinaryReader r) => r.ReadComponent<VisualEffect>());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "winVisualEffects[" + ((array8 == null) ? string.Empty : array8.Length.ToString()) + "]",
			fieldValue = (((array8 == null) ? "null" : string.Join(", ", (IEnumerable<VisualEffect>)array8)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		GameObject arg2 = reader.ReadGameObject();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "winCutScene",
			fieldValue = $"{arg2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		AnimationSampler arg3 = reader.ReadComponent<AnimationSampler>();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "winAnimationSampler",
			fieldValue = $"{arg3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "winTurnOnGlowPoint",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num2 = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "winTurnOffGlowPoint",
			fieldValue = $"{num2}",
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

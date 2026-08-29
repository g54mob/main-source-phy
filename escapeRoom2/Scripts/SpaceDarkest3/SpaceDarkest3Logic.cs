using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine;
using UnityEngine.VFX;

public class SpaceDarkest3Logic : LevelLogic, ISaveable
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

	public sealed class CargoTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 1;
		}

		public CargoTimer()
		{
		}

		public CargoTimer(int index)
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

	public enum CargoChemicalType
	{
		Flammable = 0,
		Oxidizing = 1,
		Radioactive = 2,
		Neutral = 3,
		Stars = 4
	}

	public enum CargoTitleType
	{
		Fixed = 0,
		Locked = 1,
		Missing = 2
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

	[Header("Hazmat Cargo")]
	[DontSave]
	public Item[] cargoPieces;

	[DontSave]
	public GameObject[] cargoTitles;

	[DontSave]
	public GameObject[] cargoCorrectTexts;

	[DontSave]
	public GameObject[] cargoErrorTexts;

	[DontSave]
	public MaterialState[] cargoErrorsMaterialStates;

	[DontSave]
	public Slot[] smallBoxSlots;

	[DontSave]
	public Slot[] bigBoxSlots;

	[DontSave]
	public Item[] stars;

	[DontSave]
	public Item[] fires;

	[DontSave]
	public Item[] biohazards;

	private List<CargoChemicalType> cargoChemicalTypes = new List<CargoChemicalType>();

	private List<int> bigCargoButtons = new List<int>();

	private List<int> smallCargoButtons = new List<int>();

	private bool solvedCargo;

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
		for (int i = 0; i < bigBoxSlots.Length; i++)
		{
			bigCargoButtons.Add(-1);
		}
		for (int j = 0; j < smallBoxSlots.Length; j++)
		{
			smallCargoButtons.Add(-1);
		}
		Item[] array = biohazards;
		for (int k = 0; k < array.Length; k++)
		{
			_ = array[k];
			cargoChemicalTypes.Add(CargoChemicalType.Radioactive);
		}
		array = fires;
		for (int k = 0; k < array.Length; k++)
		{
			_ = array[k];
			cargoChemicalTypes.Add(CargoChemicalType.Flammable);
		}
		array = stars;
		for (int k = 0; k < array.Length; k++)
		{
			_ = array[k];
			cargoChemicalTypes.Add(CargoChemicalType.Stars);
		}
		checkCargoSolution(full: false);
	}

	public override void onSlot(Slot targetSlot)
	{
		bool flag = true;
		int num = Array.IndexOf(bigBoxSlots, targetSlot);
		int num2 = Array.IndexOf(smallBoxSlots, targetSlot);
		if (num2 >= 0)
		{
			flag = false;
		}
		if (num >= 0 || num2 >= 0)
		{
			onCargoSlot(targetSlot, flag ? num : num2, flag);
		}
	}

	public override void onRemoveFromSlot(Slot targetSlot, Item item)
	{
		bool flag = true;
		int num = Array.IndexOf(bigBoxSlots, targetSlot);
		int num2 = Array.IndexOf(smallBoxSlots, targetSlot);
		if (num2 >= 0)
		{
			flag = false;
		}
		if (num >= 0 || num2 >= 0)
		{
			onRemoveFromCargoSlot(flag ? num : num2, flag);
		}
	}

	private void onCargoSlot(Slot slot, int slotIndex, bool isBig)
	{
		int num = Array.IndexOf(cargoPieces, slot.insertedItem);
		if (num < 0)
		{
			Debug.LogError("onCargoSlot: couldn't find item " + slot.insertedItem.name);
			return;
		}
		if (isBig)
		{
			bigCargoButtons[slotIndex] = num;
		}
		else
		{
			smallCargoButtons[slotIndex] = num;
		}
		bool flag = true;
		Slot[] array = bigBoxSlots;
		foreach (Slot slot2 in array)
		{
			if (slot2.targetable && slot2.insertedItem == null)
			{
				flag = false;
				break;
			}
		}
		array = smallBoxSlots;
		foreach (Slot slot3 in array)
		{
			if (slot3.targetable && slot3.insertedItem == null)
			{
				flag = false;
				break;
			}
		}
		cargoChangeTitle(flag ? CargoTitleType.Locked : CargoTitleType.Missing);
		checkCargoSolution(flag);
	}

	private void onRemoveFromCargoSlot(int slotIndex, bool isBig)
	{
		if (isBig)
		{
			bigCargoButtons[slotIndex] = -1;
		}
		else
		{
			smallCargoButtons[slotIndex] = -1;
		}
		cargoChangeTitle(CargoTitleType.Missing);
	}

	public void checkCargoSolution(bool full)
	{
		bool[] array = new bool[3]
		{
			checkFlammable(bigCargoButtons, isBig: true) && checkFlammable(smallCargoButtons, isBig: false),
			checkRadioactive(bigCargoButtons, isBig: true) && checkRadioactive(smallCargoButtons, isBig: false),
			checkStars(bigCargoButtons, isBig: true) && checkStars(smallCargoButtons, isBig: false)
		};
		Debug.Log("checkCargoSolution: " + array[0] + ", " + array[1] + ", " + array[2]);
		solvedCargo = true;
		for (int i = 0; i < array.Length; i++)
		{
			cargoErrorsMaterialStates[i].transitionTo("Correct", 10f, array[i] ? 1f : 0f);
			cargoCorrectTexts[i].SetActive(array[i]);
			cargoErrorTexts[i].SetActive(!array[i]);
			if (solvedCargo && !array[i])
			{
				solvedCargo = false;
			}
		}
		if (full && solvedCargo)
		{
			solveCargo();
		}
		bool checkFlammable(List<int> cargoButtons, bool isBig)
		{
			for (int j = 0; j < cargoButtons.Count; j++)
			{
				if (cargoButtons[j] >= 0 && cargoChemicalTypes[cargoButtons[j]] == CargoChemicalType.Flammable)
				{
					foreach (int adjacentIndex in getAdjacentIndices(isBig, j))
					{
						if (cargoButtons[adjacentIndex] >= 0 && cargoChemicalTypes[cargoButtons[adjacentIndex]] == CargoChemicalType.Flammable)
						{
							return false;
						}
					}
				}
			}
			Item[] array2 = fires;
			for (int k = 0; k < array2.Length; k++)
			{
				if (array2[k].slot == null)
				{
					return false;
				}
			}
			return true;
		}
		bool checkRadioactive(List<int> cargoButtons, bool isBig)
		{
			bool[] visited = new bool[cargoButtons.Count];
			for (int j = 0; j < cargoButtons.Count; j++)
			{
				if (cargoButtons[j] >= 0 && cargoChemicalTypes[cargoButtons[j]] == CargoChemicalType.Radioactive && !visited[j])
				{
					int num = getRadioactiveChainLength(cargoButtons, isBig, j, ref visited);
					if (num >= 4)
					{
						Debug.Log("Chain length " + num);
						return false;
					}
				}
				visited[j] = true;
			}
			Item[] array2 = biohazards;
			for (int k = 0; k < array2.Length; k++)
			{
				if (array2[k].slot == null)
				{
					return false;
				}
			}
			return true;
		}
		bool checkStars(List<int> cargoButtons, bool isBig)
		{
			int num = (isBig ? 4 : 3);
			List<List<int>> list = new List<List<int>>
			{
				new List<int> { 0, 0, 0, 0 },
				new List<int> { 0, 0, 0, 0 }
			};
			for (int j = 0; j < cargoButtons.Count; j++)
			{
				if (cargoButtons[j] >= 0 && cargoChemicalTypes[cargoButtons[j]] == CargoChemicalType.Stars)
				{
					int index = j / num;
					list[0][index]++;
					if (list[0][index] > 1)
					{
						return false;
					}
					int index2 = j % num;
					list[1][index2]++;
					if (list[1][index2] > 1)
					{
						return false;
					}
				}
			}
			Item[] array2 = stars;
			for (int k = 0; k < array2.Length; k++)
			{
				if (array2[k].slot == null)
				{
					return false;
				}
			}
			return true;
		}
		static List<int> getAdjacentIndices(bool isBig, int index, bool ignoreDiagonal = false)
		{
			List<int> list = new List<int>();
			int num = (isBig ? 4 : 3);
			int num2 = index / num;
			int num3 = index % num;
			for (int j = -1; j <= 1; j++)
			{
				for (int k = -1; k <= 1; k++)
				{
					if ((j != 0 || k != 0) && (!ignoreDiagonal || Mathf.Abs(j) != Mathf.Abs(k)))
					{
						int num4 = num2 + j;
						int num5 = num3 + k;
						if (num4 >= 0 && num4 < num && num5 >= 0 && num5 < num)
						{
							list.Add(num4 * num + num5);
						}
					}
				}
			}
			return list;
		}
		int getRadioactiveChainLength(List<int> cargoButtons, bool isBig, int index, ref bool[] visited)
		{
			visited[index] = true;
			int num = 1;
			foreach (int adjacentIndex2 in getAdjacentIndices(isBig, index, ignoreDiagonal: true))
			{
				if (cargoButtons[adjacentIndex2] >= 0 && cargoChemicalTypes[cargoButtons[adjacentIndex2]] == CargoChemicalType.Radioactive && !visited[adjacentIndex2])
				{
					num += getRadioactiveChainLength(cargoButtons, isBig, adjacentIndex2, ref visited);
				}
			}
			return num;
		}
	}

	private void cargoChangeTitle(CargoTitleType title)
	{
		for (int i = 0; i < cargoTitles.Length; i++)
		{
			cargoTitles[i].SetActive(i == (int)title);
		}
	}

	[DebugButton(null, Tint.Green, PostClickAction.HideButton, 0, new object[] { })]
	private void solveCargo()
	{
		Debug.Log("Solved cargo");
		Slot[] array = bigBoxSlots;
		foreach (Slot slot in array)
		{
			slot.targetable = false;
			if (slot.insertedItem != null)
			{
				slot.insertedItem.targetable = false;
			}
		}
		array = smallBoxSlots;
		foreach (Slot slot2 in array)
		{
			slot2.targetable = false;
			if (slot2.insertedItem != null)
			{
				slot2.insertedItem.targetable = false;
			}
		}
		cargoChangeTitle(CargoTitleType.Fixed);
		game.startTimer(new CargoTimer(0), 1.2f);
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
		writer.WriteList(cargoChemicalTypes, delegate(FastBinaryWriter w, CargoChemicalType e)
		{
			int value = (int)e;
			w.Write(in value, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(bigCargoButtons, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.WriteList(smallCargoButtons, delegate(FastBinaryWriter w, int e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in solvedCargo, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		cargoChemicalTypes = reader.ReadList((FastBinaryReader r) => (CargoChemicalType)r.ReadInt32());
		bigCargoButtons = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		smallCargoButtons = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		solvedCargo = reader.ReadBoolean();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		List<CargoChemicalType> list = reader.ReadList((FastBinaryReader r) => (CargoChemicalType)r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "cargoChemicalTypes[" + ((list == null) ? string.Empty : list.Count.ToString()) + "]",
			fieldValue = (((list == null) ? "null" : string.Join(", ", list)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list2 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "bigCargoButtons[" + ((list2 == null) ? string.Empty : list2.Count.ToString()) + "]",
			fieldValue = (((list2 == null) ? "null" : string.Join(", ", list2)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		List<int> list3 = reader.ReadList((FastBinaryReader r) => r.ReadInt32());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "smallCargoButtons[" + ((list3 == null) ? string.Empty : list3.Count.ToString()) + "]",
			fieldValue = (((list3 == null) ? "null" : string.Join(", ", list3)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "solvedCargo",
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
			1 => new CargoTimer(), 
			_ => null, 
		};
	}

	[CompilerGenerated]
	public override void onTimerUpdate(Timer timer)
	{
		if (!(timer is WinAnimationTimer))
		{
			_ = timer is CargoTimer;
		}
	}

	[CompilerGenerated]
	public override void onTimerDone(Timer timer)
	{
		if (timer is WinAnimationTimer timer2)
		{
			onWinAnimationTimerDone(timer2);
		}
		else if (timer is CargoTimer timer3)
		{
			onCargoTimerDone(timer3);
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

	private void onCargoTimerDone(CargoTimer timer)
	{
		if (timer.index == 0)
		{
			winAnimation();
		}
	}
}

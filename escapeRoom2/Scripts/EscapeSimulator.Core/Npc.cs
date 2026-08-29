using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Npc : Interactive
{
	[DontSave]
	public Dialogue dialogue;

	[DontSave]
	public bool exitZoomOnDialogueEnd = true;

	[DontSave]
	[HideInInspector]
	public EventReference soundButtonPressed;

	[DontSave]
	[HideInInspector]
	public EventReference soundButtonHover;

	[NonSerialized]
	public bool clientWaitingForResponse;

	[NonSerialized]
	public bool changedOrderIndexInLevelLogic;

	[NonSerialized]
	public int currentLine;

	public override Game.ScreenTargetType getTargetType()
	{
		return Game.ScreenTargetType.NPC;
	}

	protected override Game.PCCrosshair getBaseCursor()
	{
		return Game.PCCrosshair.Talk;
	}

	public override void init()
	{
		for (int i = 0; i < dialogue.dialogueLines.Count; i++)
		{
			dialogue.dialogueLines[i].originalText = dialogue.dialogueLines[i].displayText;
		}
		soundEmitter = PineFmod.addSoundEmitter(base.gameObject);
	}

	public DialogueLine getCurrentLine()
	{
		return dialogue.dialogueLines[currentLine];
	}

	public Choice getChoice(int lineIndex, int choiceIndex)
	{
		return dialogue.dialogueLines[lineIndex].choicesNeo[choiceIndex];
	}

	public bool isLast(int lineIndex)
	{
		return lineIndex == dialogue.dialogueLines.Count - 1;
	}

	public void goTo(int lineIndex)
	{
		changedOrderIndexInLevelLogic = true;
		currentLine = lineIndex;
	}

	public int[] getRandomizedLines(int seed, int start = -1, int end = -1)
	{
		Debug.Log($"Calculating Randomizing lines on npc {base.name} with seed {seed}");
		int[] array = new int[dialogue.dialogueLines.Count];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = i;
		}
		UnityEngine.Random.State state = UnityEngine.Random.state;
		UnityEngine.Random.InitState(seed);
		if (start == -1)
		{
			start = 0;
		}
		if (end == -1)
		{
			end = dialogue.dialogueLines.Count;
		}
		for (int j = start; j < end; j++)
		{
			int num = UnityEngine.Random.Range(j, end);
			ref int reference = ref array[j];
			ref int reference2 = ref array[num];
			int num2 = array[num];
			int num3 = array[j];
			reference = num2;
			reference2 = num3;
		}
		UnityEngine.Random.state = state;
		return array;
	}

	public override void save(FastBinaryWriter writer)
	{
		base.save(writer);
		writer.Write(in clientWaitingForResponse, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in changedOrderIndexInLevelLogic, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentLine, default(FastBinaryWriter.ForPrimitives));
	}

	public override void load(FastBinaryReader reader)
	{
		base.load(reader);
		clientWaitingForResponse = reader.ReadBoolean();
		changedOrderIndexInLevelLogic = reader.ReadBoolean();
		currentLine = reader.ReadInt32();
	}

	public override void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		base.read(reader, saveFileProperties);
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "clientWaitingForResponse",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "changedOrderIndexInLevelLogic",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentLine",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}
}

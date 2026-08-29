using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMOD.Studio;
using FMODUnity;
using TMPro;
using UnityEngine;

public class Tutorial1Logic : LevelLogic, ISaveable
{
	public sealed class ExitDoorDelayTimer : Timer
	{
		public override byte getTypeId()
		{
			return 0;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class Station7Timer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 1;
		}

		public Station7Timer()
		{
		}

		public Station7Timer(int index)
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

	public sealed class TempStation1Timer : Timer
	{
		public override byte getTypeId()
		{
			return 2;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class Station0Timer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 3;
		}

		public Station0Timer()
		{
		}

		public Station0Timer(int index)
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

	public sealed class FootstepsOnTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 4;
		}

		public FootstepsOnTimer()
		{
		}

		public FootstepsOnTimer(int index)
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

	public sealed class FootstepsOffTimer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 5;
		}

		public FootstepsOffTimer()
		{
		}

		public FootstepsOffTimer(int index)
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

	public sealed class Station6Timer : Timer
	{
		public int index;

		public override byte getTypeId()
		{
			return 6;
		}

		public Station6Timer()
		{
		}

		public Station6Timer(int index)
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

	public sealed class Station6DrawerTimer : Timer
	{
		public override byte getTypeId()
		{
			return 7;
		}

		public override void writeData(FastBinaryWriter writer)
		{
		}

		public override void readData(FastBinaryReader reader)
		{
		}
	}

	public sealed class ExitCutsceneTimer : Timer
	{
		public bool on;

		public override byte getTypeId()
		{
			return 8;
		}

		public ExitCutsceneTimer()
		{
		}

		public ExitCutsceneTimer(bool on)
		{
			this.on = on;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in on, default(FastBinaryWriter.ForPrimitives));
		}

		public override void readData(FastBinaryReader reader)
		{
			on = reader.ReadBoolean();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("on: " + $"{on}");
			return stringBuilder.ToString();
		}
	}

	private enum LevelPredicate
	{
		Lights = 0,
		DragStation = 1,
		Station1Turnables = 2,
		Station1Dials = 3,
		Station3Solved = 4,
		Station4Solved = 5,
		Station6Solved = 6,
		Station6Slidable = 7,
		Station6Inventory = 8,
		LevelComplete = 9
	}

	private enum TutorialStation
	{
		Interactives = 0,
		Crouch = 1,
		Items = 2,
		Slots = 3,
		ItemInteract = 4,
		ZoomableInteract = 5,
		Drag = 6,
		Pin = 7,
		Carriable = 8,
		Tool = 9
	}

	private bool[] tutorialStationsSolved;

	[DontSave]
	public List<MaterialState> stationNumbers;

	[DontSave]
	public List<MaterialState> stationNames;

	[DontSave]
	public List<Sequence> stationSolvedSequences;

	[DontSave]
	public List<GameObject> stationSolvedCheckmarks;

	[DontSave]
	public List<StudioEventEmitter> stationSolvedEmitters;

	[DontSave]
	public List<MaterialState> signNumberStates;

	[Header("Station 0")]
	[DontSave]
	public Sequence startDoorSequence;

	[DontSave]
	public Ref<Sequence, Transform> startDoorSequenceRef;

	[DontSave]
	public Trigger station0playerTrigger;

	[DontSave]
	public Ref<Trigger, GameObject> station0playerRef;

	[DontSave]
	public List<GameObject> station0Lights;

	[DontSave]
	public List<MaterialState> station0LightMaterialsStates;

	[DontSave]
	private EventInstance startSoundInstance;

	public float PlayerLookAtDoorAngle = 45f;

	private bool wasFirstFrame;

	[Header("Station 1")]
	[DontSave]
	public RefArray<MaterialState, GameObject> footsteps;

	public bool playFootstepsSound = true;

	[DontSave]
	public Switch3D station1Button;

	[DontSave]
	public Turnable[] station1Turnables;

	[DontSave]
	private int[] station1TurnablesSolution = new int[2] { 2, 5 };

	[DontSave]
	public Dial[] station1Dials;

	[DontSave]
	private int[] station1DialsSolution = new int[2] { 2, 2 };

	[DontSave]
	public Slidable station1Slidable;

	[DontSave]
	public RefArray<TweenState, Transform> station1Doors;

	[DontSave]
	public TweenState station1SlidableTween;

	[Header("Station 2")]
	[DontSave]
	public Switch3D station2CrouchButton;

	[Header("Station 3")]
	[DontSave]
	public List<Item> station3Items;

	[Header("Station 4")]
	[DontSave]
	public List<Slot> station4Slots;

	[DontSave]
	public List<TweenState> station4Tweens;

	[DontSave]
	public GameObject station7NavMeshObstacle;

	[Header("Station 5")]
	[DontSave]
	public Slidable station5SlidableBlocker;

	[DontSave]
	public Item station5SlidableBlockerItem;

	[DontSave]
	public Ref<Item, GameObject> station5SlidableBlockerRef;

	[DontSave]
	public Slidable station5SlidableLid;

	[DontSave]
	public Item station5SlidableLidItem;

	[DontSave]
	public Ref<Item, GameObject> station5SlidableLidRef;

	[DontSave]
	public Transform station5SlidableLidEndOpen;

	[DontSave]
	public Item station5Key;

	[DontSave]
	public Slot station5KeySlot;

	[DontSave]
	public Transform station5MovingSurface;

	[DontSave]
	public Ref<Item, Transform> station5Box;

	[DontSave]
	public Trigger station5Trigger;

	[Header("Station 6")]
	[DontSave]
	public List<Slot> station6Slots;

	[DontSave]
	public Slot station6KeySlot;

	[DontSave]
	public Sequence station6HintSequence;

	[DontSave]
	public List<Item> station6Pieces;

	[DontSave]
	public Ref<Slidable, Transform> station6BoxRef;

	[DontSave]
	public GameObject station6DrawerColliders;

	[DontSave]
	public Item station6Key;

	[DontSave]
	public GameObject station6keyDrawer;

	[DontSave]
	public Trigger station6Trigger;

	[DontSave]
	public Ref<Item, Transform> station6Box;

	[DontSave]
	public Transform station6MovingSurface;

	[DontSave]
	public StudioEventEmitter station6hintTurnSound;

	[DontSave]
	public StudioEventEmitter station6drawerSound;

	private int station6State;

	private bool wasStation6KeyPickedUp;

	[Header("Station 7")]
	[DontSave]
	public List<Trigger> station7Triggers;

	[DontSave]
	public RefArray<Draggable, GameObject, Transform> station7Draggables;

	[DontSave]
	public RefArray<GameObject, Transform> station7DraggableFakes;

	[DontSave]
	public Trigger station7PlayerTrigger;

	[DontSave]
	public List<TweenState> station7Tweens;

	[Header("Station 8")]
	[DontSave]
	public Lock station8Lock;

	[DontSave]
	public List<Turnable> station8Turnables;

	[Header("Station 9")]
	[DontSave]
	public Switch3D station9Button;

	[DontSave]
	public Trigger station9Trigger;

	[DontSave]
	public GameObject station9Obstacle;

	[Header("Station 10")]
	[DontSave]
	public Renderer[] telescopeRenderers;

	[DontSave]
	public MaterialState telescopeMS;

	[DontSave]
	public MaterialState numbersMS;

	[DontSave]
	public Item telescope;

	[DontSave]
	public GameObject ghostLetters;

	[DontSave]
	public Lock station10Lock;

	[DontSave]
	public Turnable[] station10Turnables;

	[Header("Exit Station")]
	[DontSave]
	public List<MaterialState> exitStationIndicators;

	[DontSave]
	public Ref<Switch3D, GameObject> exit;

	[DontSave]
	public Sequence exitSequence;

	[DontSave]
	private EventInstance exitSoundInstance;

	[DontSave]
	public Switch3D doorButtonSwitch;

	[DontSave]
	public TweenState doorButtonTween;

	[DontSave]
	public GameObject endCutscene;

	private bool levelCompleted;

	[Header("Tutorial Texts")]
	[DontSave]
	public TextMeshPro[] tutorialTexts;

	[DontSave]
	public TMP_SpriteAsset textMeshProSprites;

	[DontSave]
	private StringBuilder sharedBuilder = new StringBuilder(128);

	[DontSave]
	private List<(TextMeshPro, string)> displayPanels = new List<(TextMeshPro, string)>(16);

	[DontSave]
	private int lastSceneKeybindRevision = -1;

	[DontSave]
	private int lastLanguage = -1;

	[DontSave]
	private bool didInitialForceInitOfTutorialTexts;

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.Lights, 0.1f, false, () => checkLights());
		game.registerPredicate(LevelPredicate.DragStation, 0.2f, false, () => checkDraggables());
		game.registerPredicate(LevelPredicate.Station1Turnables, 0.2f, false, () => !game.isAnyPlayerInteracting(station1Turnables[0].gameObject) && !game.isAnyPlayerInteracting(station1Turnables[1].gameObject) && station1Turnables[0].value == station1TurnablesSolution[0] && station1Turnables[1].value == station1TurnablesSolution[1]);
		game.registerPredicate(LevelPredicate.Station1Dials, 0.2f, false, () => !game.isAnyPlayerInteracting(station1Dials[0].gameObject) && !game.isAnyPlayerInteracting(station1Dials[1].gameObject) && station1Dials[0].value == station1DialsSolution[0] && station1Dials[1].value == station1DialsSolution[1]);
		game.registerPredicate(LevelPredicate.Station3Solved, 0.3f, false, () => checkStation3());
		game.registerPredicate(LevelPredicate.Station4Solved, 0.3f, false, () => checkStation4());
		game.registerPredicate(LevelPredicate.Station6Solved, 0.3f, false, () => checkStation6());
		game.registerPredicate(LevelPredicate.Station6Slidable, 0.2f, false, () => checkStation6Slidable());
		game.registerPredicate(LevelPredicate.Station6Inventory, 0.4f, false, () => checkStation6Inventory());
		game.registerPredicate(LevelPredicate.LevelComplete, 0.4f, false, () => allStationsSolved());
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		switch (type)
		{
		case 0:
			game.startTimer(new Station0Timer(0), 0.2f);
			break;
		case 1:
			solveStation(TutorialStation.Drag);
			break;
		case 2:
			solveStation1Turnables();
			break;
		case 3:
			solveStation1Dials();
			break;
		case 4:
			solveStation(TutorialStation.Items);
			break;
		case 5:
			solveStation4();
			break;
		case 6:
			solveStation6();
			break;
		case 7:
			solveStation6Slidable();
			break;
		case 8:
			solveStation6Inventory();
			break;
		case 9:
			startEndScreen();
			break;
		}
	}

	public override void onInit()
	{
		tutorialStationsSolved = new bool[Enum.GetNames(typeof(TutorialStation)).Length];
		startSoundInstance = PineFmod.createInstance("event:/Sound Effects/03 Interactable/Doors/Elevator_Door/Elevator_Door_Loop");
		PineFmod.set3DAttributes(startSoundInstance, PineFmod.to3DAttributes(startDoorSequenceRef.Get<Transform>(0f)));
		foreach (GameObject station0Light in station0Lights)
		{
			station0Light.SetActive(value: false);
		}
		foreach (MaterialState station0LightMaterialsState in station0LightMaterialsStates)
		{
			station0LightMaterialsState.setState("Off");
		}
		foreach (Ref<MaterialState, GameObject> footstep in footsteps)
		{
			footstep.Get<MaterialState>(0).setState("Off");
			footstep.Get<GameObject>(0f).SetActive(value: false);
		}
		Turnable[] array = station1Turnables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		Dial[] array2 = station1Dials;
		for (int i = 0; i < array2.Length; i++)
		{
			array2[i].targetable = false;
		}
		station1Slidable.targetable = false;
		station5Key.targetable = false;
		station5SlidableBlockerItem.targetable = false;
		station5SlidableLidItem.targetable = false;
		exit.Get<GameObject>(0f).SetActive(value: false);
		exitSoundInstance = PineFmod.createInstance("event:/Sound Effects/03 Interactable/Doors/Elevator_Door/Elevator_Door_Loop");
		PineFmod.set3DAttributes(exitSoundInstance, PineFmod.to3DAttributes(exit.Get<GameObject>(0f).transform));
		endCutscene.SetActive(value: false);
		TextMeshPro[] array3 = tutorialTexts;
		foreach (TextMeshPro textMeshPro in array3)
		{
			displayPanels.Add((textMeshPro, textMeshPro.text));
		}
	}

	public override void onInitAfterLoad()
	{
		refreshDisplays(force: true);
	}

	private void refreshDisplays(bool force = false)
	{
		if (!force && game.menuOptions.keybindSceneRevision == lastSceneKeybindRevision && !Controller.controllerModeChanged && lastLanguage == PlayerSave.getSettings().language)
		{
			return;
		}
		lastSceneKeybindRevision = game.menuOptions.keybindSceneRevision;
		lastLanguage = PlayerSave.getSettings().language;
		Dictionary<string, string> dictionary;
		if (Controller.isActive())
		{
			dictionary = new Dictionary<string, string>
			{
				{
					"Interaction",
					"<color=#ff1010><sprite name=\"" + glyphControllerB(ControllerButtonActionType.GameQuickInteract) + "\" tint></color>"
				},
				{
					"Crouch",
					"<color=#ff1010><sprite name=\"" + glyphControllerB(ControllerButtonActionType.GameCrouch) + "\" tint></color>"
				},
				{
					"CrouchDark",
					"<color=#130000><sprite name=\"" + glyphControllerB(ControllerButtonActionType.GameCrouch) + "\" tint></color>"
				},
				{
					"Examine",
					"<color=#ff1010><sprite name=\"" + glyphControllerB(ControllerButtonActionType.GameEnterZoom) + "\" tint></color>"
				},
				{
					"Deselect",
					"<color=#ff1010><sprite name=\"" + glyphControllerB(ControllerButtonActionType.GameBack) + "\" tint></color>"
				},
				{
					"InventoryLeft",
					"<color=#ff1010><sprite name=\"" + glyphControllerB(ControllerButtonActionType.GameInventoryLeft) + "\" tint></color>"
				},
				{
					"InventoryRight",
					"<color=#ff1010><sprite name=\"" + glyphControllerB(ControllerButtonActionType.GameInventoryRight) + "\" tint></color>"
				},
				{
					"Look",
					"<size=200%><color=#660d0a><sprite name=\"" + glyphControllerA(ControllerAxisActionType.GameLook) + "\" tint></color></size>"
				},
				{
					"MoveController",
					"<size=200%><color=#660d0a><sprite name=\"" + glyphControllerA(ControllerAxisActionType.GameMove) + "\" tint></color></size>"
				}
			};
		}
		else
		{
			Dictionary<string, string> dictionary2 = new Dictionary<string, string>();
			dictionary2.Add("Interaction", "<color=#ff1010><sprite name=\"mouse_left_tutorial\" tint></color>");
			dictionary2.Add("Crouch", "<color=#ff1010><sprite name=\"" + glyph(KeyBindingAction.Crouch) + "\" tint></color>");
			dictionary2.Add("CrouchDark", "<color=#130000><sprite name=\"" + glyph(KeyBindingAction.Crouch) + "\" tint></color>");
			dictionary2.Add("Number1", "<color=#ff1010><sprite name=\"keyboard_1\" tint></color>");
			dictionary2.Add("Number9", "<color=#ff1010><sprite name=\"keyboard_9\" tint></color>");
			dictionary2.Add("MouseWheel", "<color=#ff1010><sprite name=\"mouse_scroll_tutorial\" tint></color>");
			dictionary2.Add("Examine", "<color=#ff1010><sprite name=\"" + glyph(KeyBindingAction.ExamineInventory) + "\" tint></color>");
			dictionary2.Add("Deselect", "<color=#ff1010><sprite name=\"mouse_right_tutorial\" tint></color>");
			dictionary2.Add("Look", "<size=200%><color=#660d0a><sprite name=\"mouse_move\" tint></color></size>");
			dictionary2.Add("MoveWASD", "<color=#660d0a><sprite name=\"" + glyph(KeyBindingAction.Up) + "\" tint><sprite name=\"" + glyph(KeyBindingAction.Down) + "\" tint><sprite name=\"" + glyph(KeyBindingAction.Left) + "\" tint><sprite name=\"" + glyph(KeyBindingAction.Right) + "\" tint></color>");
			dictionary2.Add("MoveArrows", "<color=#660d0a><sprite name=\"" + glyph(KeyBindingAction.Up, alt: true) + "\" tint><sprite name=\"" + glyph(KeyBindingAction.Down, alt: true) + "\" tint><sprite name=\"" + glyph(KeyBindingAction.Left, alt: true) + "\" tint><sprite name=\"" + glyph(KeyBindingAction.Right, alt: true) + "\" tint></color>");
			dictionary = dictionary2;
		}
		Dictionary<string, string> replacements = dictionary;
		foreach (var displayPanel in displayPanels)
		{
			string text = ((!Controller.isActive()) ? Localization.lookupInDictionary(displayPanel.Item2) : Localization.lookupInDictionary(displayPanel.Item2 + "_controller", Localization.lookupInDictionary(displayPanel.Item2)));
			string input = text;
			displayPanel.Item1.text = replaceKeysEfficient(tutorialRichTextParser(input), replacements);
		}
		string glyph(KeyBindingAction action, bool alt = false)
		{
			(bool exists, MenuOptions.KeyBinding keyBinding) keyBinding = game.menuOptions.getKeyBinding(action);
			bool item = keyBinding.exists;
			MenuOptions.KeyBinding item2 = keyBinding.keyBinding;
			string result = "keyboard_question";
			if (item)
			{
				string text2 = (alt ? item2.keyCodeSecondary : item2.keyCode).ToString().ToLower();
				string text3 = "keyboard_" + text2 switch
				{
					"leftcontrol" => "ctrl", 
					"rightcontrol" => "ctrl", 
					"leftshift" => "shift", 
					"rightshift" => "shift", 
					"leftalt" => "alt", 
					"rightalt" => "alt", 
					"alpha0" => "0", 
					"alpha1" => "1", 
					"alpha2" => "2", 
					"alpha3" => "3", 
					"alpha4" => "4", 
					"alpha5" => "5", 
					"alpha6" => "6", 
					"alpha7" => "7", 
					"alpha8" => "8", 
					"alpha9" => "9", 
					_ => text2, 
				};
				if (textMeshProSprites.GetSpriteIndexFromName(text3) != -1)
				{
					result = text3;
				}
			}
			return result;
		}
		string glyphController(string key)
		{
			string result = "keyboard_question";
			if (SteamInputActionOriginMatcher.lookup.TryGetValue(key, out var value) && textMeshProSprites.GetSpriteIndexFromName(value) != -1)
			{
				result = value;
			}
			return result;
		}
		string glyphControllerA(ControllerAxisActionType axis)
		{
			return glyphController(Controller.getGlyphString(axis));
		}
		string glyphControllerB(ControllerButtonActionType action)
		{
			return glyphController(Controller.getGlyphString(action));
		}
	}

	private string replaceKeysEfficient(string input, Dictionary<string, string> replacements)
	{
		sharedBuilder.Clear();
		int num = 0;
		while (num < input.Length)
		{
			if (input[num] == '[')
			{
				int num2 = num + 1;
				int num3 = input.IndexOf(']', num2);
				if (num3 != -1)
				{
					string text = input.Substring(num2, num3 - num2);
					if (replacements.TryGetValue(text, out var value))
					{
						sharedBuilder.Append(value);
					}
					else
					{
						sharedBuilder.Append('[').Append(text).Append(']');
					}
					num = num3 + 1;
				}
				else
				{
					sharedBuilder.Append(input[num]);
					num++;
				}
			}
			else
			{
				sharedBuilder.Append(input[num]);
				num++;
			}
		}
		return sharedBuilder.ToString();
	}

	private string tutorialRichTextParser(string input)
	{
		sharedBuilder.Clear();
		int num = 0;
		while (num < input.Length)
		{
			if (input[num] != '@')
			{
				num++;
				continue;
			}
			int num2 = num;
			num++;
			int i;
			for (i = num; i < input.Length && input[i] != ':'; i++)
			{
			}
			if (i >= input.Length)
			{
				break;
			}
			int tagLen = i - num2;
			int j;
			for (j = i + 1; j < input.Length && input[j] == ' '; j++)
			{
			}
			int k;
			for (k = j; k < input.Length && (input[k] != '@' || k + 1 >= input.Length || !char.IsLetter(input[k + 1])); k++)
			{
			}
			WriteFormatted(sharedBuilder, input, num2, tagLen, j, k);
			num = k;
		}
		return sharedBuilder.ToString();
		static bool IsMatch(string text, int start, int length, string match)
		{
			if (length != match.Length)
			{
				return false;
			}
			for (int l = 0; l < length; l++)
			{
				if (text[start + l] != match[l])
				{
					return false;
				}
			}
			return true;
		}
		static void WriteFormatted(StringBuilder builder, string text, int tagStart, int length, int valStart, int valEnd)
		{
			if (builder.Length > 0)
			{
				builder.Append('\n');
			}
			if (IsMatch(text, tagStart, length, "@Title"))
			{
				builder.Append("<b><size=110%><align=center>");
				builder.Append(text, valStart, valEnd - valStart);
				builder.Append("</align></size></b>\n");
			}
			else if (IsMatch(text, tagStart, length, "@Subtitle"))
			{
				builder.Append("<b><size=100%><color=#330000><align=center>");
				builder.Append(text, valStart, valEnd - valStart);
				builder.Append("</align></color></size></b>");
			}
			else if (IsMatch(text, tagStart, length, "@Main"))
			{
				builder.Append("<align=center><size=100%>");
				builder.Append(text, valStart, valEnd - valStart);
				builder.Append("</align></size>\n");
			}
			else if (IsMatch(text, tagStart, length, "@Simple"))
			{
				builder.Append("<size=110%><align=center>");
				builder.Append(text, valStart, valEnd - valStart);
				builder.Append("</align></size>\n");
			}
			else if (IsMatch(text, tagStart, length, "@Tip"))
			{
				builder.Append("<size=60%><i>");
				builder.Append(text, valStart, valEnd - valStart);
				builder.Append("</i></size>");
			}
			else if (IsMatch(text, tagStart, length, "@Big"))
			{
				builder.Append("<b><size=150%><align=center>");
				builder.Append(text, valStart, valEnd - valStart);
				builder.Append("</align></size></b>\n");
			}
		}
	}

	public override void onUpdate()
	{
		bool force = false;
		if (game.gameState.current == Game.GameState.Room && Controller.controllerInputSetChanged && !didInitialForceInitOfTutorialTexts)
		{
			didInitialForceInitOfTutorialTexts = true;
			force = true;
		}
		refreshDisplays(force);
		if (!tutorialStationsSolved[1])
		{
			station2CrouchButton.targetable = game.playerViewRay.origin.y > 1f;
		}
	}

	public override void onTool(Item tool, ToolContext context)
	{
		Debug.Log("tool state: " + context.state);
		if (!(tool == telescope))
		{
			return;
		}
		if (context.state == ToolState.Start)
		{
			ghostLetters.SetActive(value: true);
			telescopeMS.transitionToDuration("Invisible");
			numbersMS.transitionToDuration("AppearNumbers");
			Renderer[] array = telescopeRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = false;
			}
		}
		if (context.state == ToolState.End)
		{
			ghostLetters.SetActive(value: false);
			telescopeMS.transitionToDuration("Default", 0.25f);
			numbersMS.transitionToDuration("Default", 0.01f);
			Renderer[] array = telescopeRenderers;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].enabled = true;
			}
		}
	}

	public override void onSwitch3D(Switch3D targetSwitch, Switch3DEvent switchEvent)
	{
		if (targetSwitch == station1Button && switchEvent == Switch3DEvent.On)
		{
			station1Button.targetable = false;
			station1Doors[0].Get<TweenState>(0).transitionTo("Close", 2f);
			station1Doors[1].Get<TweenState>(0).transitionTo("Open", 2f, 1f, playSound: true, 0.5f);
		}
		else if (targetSwitch == station2CrouchButton && switchEvent == Switch3DEvent.On)
		{
			station2CrouchButton.targetable = false;
			solveStation(TutorialStation.Crouch);
		}
		else if (targetSwitch == station9Button && switchEvent == Switch3DEvent.On)
		{
			station9Button.targetable = false;
			solveStation(TutorialStation.Carriable);
		}
		else if (targetSwitch == exit.Get<Switch3D>(0) && switchEvent == Switch3DEvent.Start)
		{
			if (game.hasAuthority(targetSwitch))
			{
				showLeaveDialogue("leaveTutorial", levelCompleted ? "leaveSolvedTutorialMessage" : "leaveTutorialMessage");
			}
		}
		else if (targetSwitch == doorButtonSwitch && switchEvent == Switch3DEvent.On && game.hasAuthority(targetSwitch))
		{
			showLeaveDialogue("leaveTutorial", levelCompleted ? "leaveSolvedTutorialMessage" : "leaveTutorialMessage");
		}
	}

	public override void onTurnableMoved(Turnable turnable, MoveEvent moveEvent)
	{
		if (moveEvent == MoveEvent.Snapped)
		{
			int num = Array.IndexOf(station1Turnables, turnable);
			if (num >= 0 && station1Turnables[num].value == station1TurnablesSolution[num])
			{
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Misc/Small_Bell/Small_Bell", turnable.gameObject);
			}
		}
	}

	public override void onDialMoved(Dial dial, MoveEvent moveEvent)
	{
		if (moveEvent == MoveEvent.Snapped)
		{
			int num = Array.IndexOf(station1Dials, dial);
			if (num >= 0 && station1Dials[num].value == station1DialsSolution[num])
			{
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Misc/Small_Bell/Small_Bell", dial.gameObject);
			}
		}
	}

	public override void onSlot(Slot targetSlot)
	{
		if (station4Slots.Contains(targetSlot) && targetSlot.acceptItems.Contains(targetSlot.insertedItem))
		{
			Debug.Log(station4Slots.IndexOf(targetSlot) + " tt");
			station4Tweens[station4Slots.IndexOf(targetSlot)].transitionTo("Down", 1.5f);
			targetSlot.targetable = false;
			targetSlot.insertedItem.targetable = false;
		}
		else if (targetSlot == station5KeySlot)
		{
			station5KeySlot.targetable = false;
			station5KeySlot.insertedItem.targetable = false;
			if (station5Trigger.state)
			{
				station5Box.Get<Item>(0).hasRigidbody = false;
				game.setParent(station5Box.Get<Transform>(0f), station5MovingSurface);
			}
			solveStation(TutorialStation.ItemInteract);
		}
		else if (targetSlot == station6KeySlot)
		{
			targetSlot.targetable = false;
			targetSlot.insertedItem.targetable = false;
			game.startTimer(new Station6Timer(1), 0.5f);
		}
	}

	public override void onSlidableMoved(Slidable slidable, MoveEvent moveEvent)
	{
		if (slidable == station5SlidableBlocker && moveEvent == MoveEvent.Released && slidable.value > 0.9f)
		{
			station5SlidableBlocker.targetable = false;
			station5SlidableLid.endNode.position = station5SlidableLidEndOpen.position;
			station5SlidableBlockerItem.targetable = true;
			station5SlidableBlockerRef.Get<GameObject>(0f).SetActive(value: false);
		}
		if (slidable == station5SlidableLid && moveEvent == MoveEvent.Released && slidable.value > 0.85f && !station5SlidableBlocker.targetable)
		{
			station5SlidableLid.targetable = false;
			station5Key.targetable = true;
			station5SlidableLidItem.targetable = true;
			station5SlidableLidRef.Get<GameObject>(0f).SetActive(value: false);
		}
		if (slidable == station1Slidable && moveEvent == MoveEvent.Released)
		{
			if (slidable.value > 0.85f)
			{
				station1Slidable.targetable = false;
				station1Doors[3].Get<TweenState>(0).transitionTo("Default", 2f);
				game.startTimer(new FootstepsOnTimer(0), 0.1f);
				solveStation(TutorialStation.Interactives);
			}
			else if (slidable.value < 0.2f)
			{
				slidable.targetable = false;
				station1SlidableTween.transitionTo("Down", 2f);
			}
		}
	}

	public override void onUnlock(Lock targetLock)
	{
		if (targetLock == station8Lock)
		{
			foreach (Turnable station8Turnable in station8Turnables)
			{
				station8Turnable.targetable = false;
			}
			solveStation(TutorialStation.Pin);
		}
		else if (targetLock == station10Lock)
		{
			Turnable[] array = station10Turnables;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = false;
			}
			solveStation(TutorialStation.Tool);
		}
	}

	public override void onTrigger(Trigger trigger, TriggerEvent triggerEvent)
	{
		if (triggerEvent.type != TriggerEventType.Started)
		{
			return;
		}
		Debug.Log("Trigger enter " + game.name + " ", trigger);
		for (int i = 0; i < station7Triggers.Count; i++)
		{
			if (trigger == station7Triggers[i])
			{
				if (game.hasAuthority(station7Draggables[i].Get<Draggable>(0)))
				{
					game.killAllPointers();
				}
				station7Draggables[i].Get<Draggable>(0).targetable = false;
				station7Draggables[i].Get<GameObject>(0f).SetActive(value: false);
				station7DraggableFakes[i].Get<GameObject>(0).SetActive(value: true);
				station7DraggableFakes[i].Get<Transform>(0f).position = station7Draggables[i].Get<Transform>(0u).position;
				game.startTransitionLocal(station7DraggableFakes[i].Get<Transform>(0f), 0.25f, 0f, Vector3.zero);
				game.startTimer(new Station7Timer(i), 0.25f);
			}
		}
		if (trigger == station0playerTrigger)
		{
			station0playerRef.Get<GameObject>(0f).SetActive(value: false);
			startDoorSequence.play(-1f, startDoorSequence.sequenceDuration);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Wood_Door_Push_01", startDoorSequence.gameObject);
		}
	}

	public override void onTweenTransitionDone(TweenState tweenState, string state)
	{
		if (tweenState == station1Doors[1].Get<TweenState>(0) && station1Doors[1].Get<TweenState>(0).findStateByName("Open").weight == 1f)
		{
			Turnable[] array = station1Turnables;
			for (int i = 0; i < array.Length; i++)
			{
				array[i].targetable = true;
			}
		}
		else if (tweenState == station1Doors[2].Get<TweenState>(0) && station1Doors[2].Get<TweenState>(0).findStateByName("Open").weight == 1f)
		{
			Dial[] array2 = station1Dials;
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i].targetable = true;
			}
		}
		else if (tweenState == station1Doors[3].Get<TweenState>(0) && station1Doors[3].Get<TweenState>(0).findStateByName("Open").weight == 1f)
		{
			station1Slidable.targetable = true;
		}
		else if (tweenState == station1SlidableTween)
		{
			float weight = station1SlidableTween.findStateByName("Down").weight;
			if (weight == 1f)
			{
				station1SlidableTween.transitionTo("Down", 2f, 0f);
			}
			else if (weight < 0.2f)
			{
				station1Slidable.targetable = true;
			}
		}
	}

	public override void onMaterialTransitionDone(MaterialState materialState, string state)
	{
		int num = footsteps.FindIndex<MaterialState>((MaterialState x) => x == materialState);
		if (num >= 0 && materialState.findStateByName(state).weight == 1f)
		{
			footsteps[num].Get<GameObject>(0f).SetActive(value: false);
		}
	}

	public override void onAddToInventory(Item item)
	{
		if (station6Key == item && !wasStation6KeyPickedUp)
		{
			wasStation6KeyPickedUp = true;
			game.startTransitionLocal(station6BoxRef.Get<Transform>(0f), 1f, 0f, station6BoxRef.Get<Slidable>(0).startNode.localPosition);
			station6drawerSound.Play();
			game.startTimer(new Station6DrawerTimer(), 1f);
		}
	}

	public override void onSequenceDone(Sequence sequence)
	{
		int num = stationSolvedSequences.IndexOf(sequence);
		if (num >= 0)
		{
			PineFmod.stop(stationSolvedEmitters[num]);
			stationNumbers[num]?.transitionTo("On");
			stationNames[num]?.transitionTo("On");
			signNumberStates[num * 2]?.transitionTo("On", 5f);
			signNumberStates[num * 2 + 1]?.transitionTo("On", 5f);
			exitStationIndicators[num]?.transitionTo("On", 5f);
			stationSolvedCheckmarks[num]?.SetActive(value: true);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Misc/Elevator_Bell/Elevator_Bell", exitStationIndicators[num].gameObject);
		}
		else if (sequence == exitSequence)
		{
			endCutscene.SetActive(value: false);
			exit.Get<GameObject>(0f).SetActive(value: true);
			game.startTimer(new ExitCutsceneTimer(on: false), 1f);
			PineFmod.stop(exitSoundInstance, FMOD.Studio.STOP_MODE.IMMEDIATE);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Elevator_Door/Elevator_Door_Hit", exit.Get<GameObject>(0f));
		}
		else if (sequence == startDoorSequence)
		{
			PineFmod.stop(startSoundInstance, FMOD.Studio.STOP_MODE.IMMEDIATE);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Wood_Door_Slam_02", startDoorSequence.gameObject);
			game.startTimer(new FootstepsOnTimer(0), 0.1f);
		}
		else if (sequence == station6HintSequence)
		{
			station6hintTurnSound.Stop();
		}
	}

	public override void onTimerDone(Timer timer)
	{
		if (timer is Station0Timer timer2)
		{
			onStation0Timer(timer2);
		}
		if (timer is Station6Timer timer3)
		{
			onStation6Timer(timer3);
		}
		if (timer is Station6DrawerTimer)
		{
			onStation6DrawerTimer();
		}
		if (timer is Station7Timer timer4)
		{
			onStation7Timer(timer4);
		}
		if (timer is FootstepsOnTimer timer5)
		{
			onFootstepsOnTimer(timer5);
		}
		if (timer is FootstepsOffTimer timer6)
		{
			onFootstepsOffTimer(timer6);
		}
		if (timer is TempStation1Timer)
		{
			onTempStation1Timer();
		}
		if (timer is ExitDoorDelayTimer)
		{
			onExitDoorDelayTimer();
		}
		if (timer is ExitCutsceneTimer timer7)
		{
			onExitCutSceneTimer(timer7);
		}
	}

	public void onOptionDialogClick(string id)
	{
		if (id == "levelLogic_yes")
		{
			game.requestOrLoadLobby();
		}
	}

	private void solveStation(TutorialStation station)
	{
		tutorialStationsSolved[(int)station] = true;
		stationSolvedSequences[(int)station].play(-1f, stationSolvedSequences[(int)station].sequenceDuration, 1.5f);
		PineFmod.play(stationSolvedEmitters[(int)station]);
		switch (station)
		{
		case TutorialStation.Drag:
			station7NavMeshObstacle.SetActive(value: true);
			break;
		case TutorialStation.Carriable:
			if (station9Trigger.playersInTriggerLastEvent.Contains(game.localPlayerData.id))
			{
				game.teleportPlayer(station9Trigger.transform.position + Vector3.right * 0.65f);
			}
			station9Obstacle.SetActive(value: true);
			break;
		}
		Debug.Log($"Station {station} solved!");
	}

	[DebugButton(null, Tint.Default, PostClickAction.ReturnToGame, 0, new object[] { })]
	private void startEndScreen()
	{
		if (!levelCompleted)
		{
			game.startTimer(new ExitCutsceneTimer(on: true), 2.8f);
		}
		game.startTimer(new ExitDoorDelayTimer(), 3.5f);
	}

	private bool isStationSolved(TutorialStation station)
	{
		return tutorialStationsSolved[(int)station];
	}

	private bool allStationsSolved()
	{
		if (tutorialStationsSolved.Length == 0)
		{
			return false;
		}
		bool flag = true;
		for (int i = 0; i < tutorialStationsSolved.Length; i++)
		{
			flag &= isStationSolved((TutorialStation)i);
		}
		return flag;
	}

	private bool checkLights()
	{
		foreach (Game.GamePlayerData allPlayer in game.getAllPlayers())
		{
			if (Vector3.Angle(allPlayer.lastTransformPose.forward, startDoorSequence.transform.position - allPlayer.lastTransformPose.position) <= PlayerLookAtDoorAngle)
			{
				return true;
			}
		}
		return false;
	}

	private void onStation0Timer(Station0Timer timer)
	{
		int index = timer.index;
		if (index < 3)
		{
			if (index < station0Lights.Count)
			{
				station0Lights[index].SetActive(value: true);
			}
			if (index < station0LightMaterialsStates.Count)
			{
				station0LightMaterialsStates[index].transitionTo("Off", 5f, 0f);
				PineFmod.playOneShotSoundAttached("event:/Sound Effects/05 Misc/Electricity/Light_On", station0LightMaterialsStates[index].gameObject);
			}
			game.startTimer(new Station0Timer(index + 1), 0.6f);
		}
	}

	private void solveStation1Turnables()
	{
		Turnable[] array = station1Turnables;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		station1Doors[1].Get<TweenState>(0).transitionTo("Default", 2f);
		Game obj = game;
		Transform obj2 = station1Doors[2].Get<Transform>(0f);
		Quaternion? rotation = Quaternion.Euler(0f, 179.99f, 0f);
		obj.startTransitionLocal(obj2, 0.5f, 0.5f, null, rotation);
		game.startTimer(new TempStation1Timer(), 1f);
	}

	private void solveStation1Dials()
	{
		Dial[] array = station1Dials;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = false;
		}
		Game obj = game;
		Transform obj2 = station1Doors[2].Get<Transform>(0f);
		Quaternion? rotation = Quaternion.identity;
		obj.startTransitionLocal(obj2, 0.5f, 0f, null, rotation);
		station1Doors[3].Get<TweenState>(0).transitionTo("Open", 2f, 1f, playSound: true, 0.5f);
	}

	private void onFootstepsOnTimer(FootstepsOnTimer timer)
	{
		int index = timer.index;
		footsteps[index].Get<GameObject>(0f).SetActive(value: true);
		footsteps[index].Get<MaterialState>(0).transitionTo("Off", 3f, 0f);
		if (playFootstepsSound)
		{
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/02 Footsteps/Tutorial/Parquet_1", footsteps[index].Get<GameObject>(0f));
		}
		if (index < footsteps.Length - 1)
		{
			game.startTimer(new FootstepsOnTimer(index + 1), 0.2f);
		}
		else if (index == footsteps.Length - 1)
		{
			if (playFootstepsSound)
			{
				playFootstepsSound = false;
			}
			game.startTimer(new FootstepsOffTimer(0), 0.2f);
		}
	}

	private void onFootstepsOffTimer(FootstepsOffTimer timer)
	{
		int index = timer.index;
		footsteps[index].Get<MaterialState>(0).transitionTo("Off", 3f);
		if (index < footsteps.Length - 1)
		{
			game.startTimer(new FootstepsOffTimer(index + 1), 0.2f);
		}
		else if (!isStationSolved(TutorialStation.Interactives) && index == footsteps.Length - 1)
		{
			game.startTimer(new FootstepsOnTimer(0), 0.2f);
		}
	}

	private void onTempStation1Timer()
	{
		Dial[] array = station1Dials;
		for (int i = 0; i < array.Length; i++)
		{
			array[i].targetable = true;
		}
	}

	private void onExitDoorDelayTimer()
	{
		if (!levelCompleted)
		{
			levelCompleted = true;
			exitSequence.play(-1f, exitSequence.sequenceDuration);
			PineFmod.start(exitSoundInstance);
			PineFmod.playOneShotSoundAttached("event:/Sound Effects/03 Interactable/Doors/Elevator_Door/Elevator_Door_Start", exit.Get<GameObject>(0f));
		}
		else
		{
			showLeaveDialogue("finishTutorial", "finishTutorialMessage");
		}
	}

	private void onExitCutSceneTimer(ExitCutsceneTimer timer)
	{
		if (timer.on)
		{
			endCutscene.SetActive(value: true);
		}
		else
		{
			showLeaveDialogue("finishTutorial", "finishTutorialMessage");
		}
	}

	private bool checkStation3()
	{
		foreach (Item station3Item in station3Items)
		{
			if (station3Item.slot == null && !game.isInAnyPlayerInventory(station3Item.gameObject))
			{
				return false;
			}
		}
		return true;
	}

	private bool checkStation4()
	{
		bool flag = true;
		foreach (Slot station4Slot in station4Slots)
		{
			flag &= station4Slot.acceptItems.Contains(station4Slot.insertedItem);
		}
		return flag;
	}

	private void solveStation4()
	{
		foreach (Slot station4Slot in station4Slots)
		{
			station4Slot.targetable = false;
			station4Slot.insertedItem.targetable = false;
		}
		solveStation(TutorialStation.Slots);
	}

	private bool checkStation6()
	{
		bool flag = true;
		foreach (Slot station6Slot in station6Slots)
		{
			flag &= station6Slot.acceptItems.Contains(station6Slot.insertedItem);
		}
		return flag;
	}

	private void solveStation6()
	{
		foreach (Slot station6Slot in station6Slots)
		{
			station6Slot.targetable = false;
			station6Slot.insertedItem.targetable = false;
		}
		station6keyDrawer.SetActive(value: true);
		game.startTransitionLocal(station6BoxRef.Get<Transform>(0f), 1f, 0f, station6BoxRef.Get<Slidable>(0).endNode.localPosition);
		station6drawerSound.Play();
		game.startTimer(new Station6Timer(0), 1f);
	}

	private bool checkStation6Slidable()
	{
		Slidable slidable = station6BoxRef.Get<Slidable>(0);
		if (!game.isAnyPlayerInteracting(slidable.gameObject) && slidable.value > 0.7f)
		{
			return station6State < 1;
		}
		return false;
	}

	private void solveStation6Slidable()
	{
		station6State = 1;
		station6HintSequence.play(-1f, station6State, 2f);
		station6hintTurnSound.Play();
		station6BoxRef.Get<Slidable>(0).targetable = false;
		game.startTransitionLocal(station6BoxRef.Get<Transform>(0f), 0.3f, 0f, station6BoxRef.Get<Slidable>(0).endNode.localPosition);
		station6DrawerColliders.SetActive(value: true);
		station6drawerSound.Play();
		game.startTimer(new Station6DrawerTimer(), 0.3f);
	}

	private bool checkStation6Inventory()
	{
		if (station6State >= 2)
		{
			return false;
		}
		foreach (Item station6Piece in station6Pieces)
		{
			if (station6Piece.slot == null && !game.isInAnyPlayerInventory(station6Piece.gameObject))
			{
				return false;
			}
		}
		return true;
	}

	private void solveStation6Inventory()
	{
		station6State = 2;
		station6HintSequence.play(-1f, station6State, 2f);
		station6hintTurnSound.Play();
		station6Slots.ForEach(delegate(Slot x)
		{
			x.targetable = true;
		});
		game.startTransitionLocal(station6BoxRef.Get<Transform>(0f), 1.3f, 0f, station6BoxRef.Get<Slidable>(0).startNode.localPosition);
		station6drawerSound.Play();
		game.startTimer(new Station6DrawerTimer(), 1.3f);
	}

	private void onStation6Timer(Station6Timer timer)
	{
		switch (timer.index)
		{
		case 0:
			station6State = 3;
			station6HintSequence.play(-1f, station6State, 2f);
			station6hintTurnSound.Play();
			station6KeySlot.targetable = true;
			station6drawerSound.Stop();
			break;
		case 1:
			if (station6Trigger.state)
			{
				station6Box.Get<Item>(0).hasRigidbody = false;
				game.setParent(station6Box.Get<Transform>(0f), station6MovingSurface);
			}
			solveStation(TutorialStation.ZoomableInteract);
			break;
		}
	}

	private void onStation6DrawerTimer()
	{
		station6drawerSound.Stop();
	}

	private bool checkDraggables()
	{
		if (station7Tweens.Count((TweenState t) => t.findStateByName("Down").weight > 0.9f) >= station7Tweens.Count && station7Draggables.Count((Ref<Draggable, GameObject, Transform> d) => game.isAnyPlayerInteracting(d.Get<GameObject>(0f))) == 0 && !station7PlayerTrigger.state)
		{
			return true;
		}
		return false;
	}

	private void onStation7Timer(Station7Timer timer)
	{
		int index = timer.index;
		station7Tweens[index].transitionTo("Down", 1.5f);
	}

	private void showLeaveDialogue(string title, string message)
	{
		VisualControl visualControl = new VisualControl("levelLogic_yes", ControllerButtonActionType.UIConfirmPrimary, "%yes%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
		visualControl.addKeybindAction(KeyBindingAction.Chat, game.menuOptions);
		VisualControl visualControl2 = new VisualControl("", ControllerButtonActionType.UIBack, "%no%", VisualControl.ControlType.Button, VisualControl.Position.BotCenter);
		visualControl2.addEscape();
		game.optionDialog.show(title, message, onOptionDialogClick, visualControl, visualControl2);
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.WriteArray(tutorialStationsSolved, delegate(FastBinaryWriter w, bool e)
		{
			w.Write(in e, default(FastBinaryWriter.ForPrimitives));
		});
		writer.Write(in PlayerLookAtDoorAngle, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in wasFirstFrame, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in playFootstepsSound, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in station6State, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in wasStation6KeyPickedUp, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in levelCompleted, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		tutorialStationsSolved = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		PlayerLookAtDoorAngle = reader.ReadSingle();
		wasFirstFrame = reader.ReadBoolean();
		playFootstepsSound = reader.ReadBoolean();
		station6State = reader.ReadInt32();
		wasStation6KeyPickedUp = reader.ReadBoolean();
		levelCompleted = reader.ReadBoolean();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool[] array = reader.ReadArray((FastBinaryReader r) => r.ReadBoolean());
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "tutorialStationsSolved[" + ((array == null) ? string.Empty : array.Length.ToString()) + "]",
			fieldValue = (((array == null) ? "null" : string.Join(", ", array)) ?? ""),
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		float num = reader.ReadSingle();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "PlayerLookAtDoorAngle",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wasFirstFrame",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag2 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "playFootstepsSound",
			fieldValue = $"{flag2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "station6State",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag3 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "wasStation6KeyPickedUp",
			fieldValue = $"{flag3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		bool flag4 = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "levelCompleted",
			fieldValue = $"{flag4}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new ExitDoorDelayTimer(), 
			1 => new Station7Timer(), 
			2 => new TempStation1Timer(), 
			3 => new Station0Timer(), 
			4 => new FootstepsOnTimer(), 
			5 => new FootstepsOffTimer(), 
			6 => new Station6Timer(), 
			7 => new Station6DrawerTimer(), 
			8 => new ExitCutsceneTimer(), 
			_ => null, 
		};
	}
}

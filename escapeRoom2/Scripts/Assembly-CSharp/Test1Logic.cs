using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Test1Logic : LevelLogic, ISaveable
{
	private enum LevelPredicate
	{
		PlacementOfCubes = 0,
		PlacementOfCubesTimed = 1,
		PlacementOfCubesRepeatable = 2
	}

	private enum RPCs
	{
		BlueSlotRPC = 0
	}

	public sealed class DisableCutSceneTimer : Timer
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

	public sealed class StartTimerButtonTimer : Timer
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

	public sealed class DataTimer : Timer
	{
		public int someInt;

		public int[] someIntArray;

		public string someString;

		public override byte getTypeId()
		{
			return 2;
		}

		public DataTimer()
		{
		}

		public DataTimer(int someInt, int[] someIntArray, string someString)
		{
			this.someInt = someInt;
			this.someIntArray = someIntArray;
			this.someString = someString;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.Write(in someInt, default(FastBinaryWriter.ForPrimitives));
			writer.WriteArray(someIntArray, delegate(FastBinaryWriter w, int e)
			{
				w.Write(in e, default(FastBinaryWriter.ForPrimitives));
			});
			writer.Write(someString);
		}

		public override void readData(FastBinaryReader reader)
		{
			someInt = reader.ReadInt32();
			someIntArray = reader.ReadArray((FastBinaryReader r) => r.ReadInt32());
			someString = reader.ReadString();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.AppendLine("someInt: " + $"{someInt}");
			stringBuilder.AppendLine("someIntArray: " + ToStringHelper.Stringify(someIntArray, (int e) => $"{e}"));
			stringBuilder.Append("someString: " + ToStringHelper.Stringify(someString));
			return stringBuilder.ToString();
		}
	}

	[DontSave]
	public GameObject cutScene;

	[DontSave]
	public Switch3D cutSceneButton;

	[DontSave]
	public MaterialState buttonMaterialState;

	[DontSave]
	public Switch3D changeMaterialStateButton;

	public bool isButtonMaterialStateGreen;

	[DontSave]
	public Switch3D startTimerButton;

	[DontSave]
	public Transform startTimerTarget;

	[DontSave]
	public Transform startTimerTargetStartPosition;

	[DontSave]
	public Transform startTimerTargetEndPosition;

	[DontSave]
	public Switch3D startSequenceButton;

	[DontSave]
	public Sequence sequence;

	[DontSave]
	public Slot blueSlot;

	[DontSave]
	public Lock turnablesLock;

	[DontSave]
	public Transform turnablesLockCubeTransform;

	[DontSave]
	public TMP_Text slidableTextTMP;

	[DontSave]
	public Slidable slidableTop;

	[DontSave]
	public Slidable slidableBot;

	[DontSave]
	public Slidable slidableBezier;

	[DontSave]
	public LineRenderer slidableBezierLine;

	[DontSave]
	public SlidableGraph curvedSlidableGraph;

	[DontSave]
	public LineRenderer curvedSlidableGraphLineTemplate;

	[DontSave]
	public Switch3D curvedSlidableGraphResetButton;

	[DontSave]
	public Switch3D curvedSlidableGraphMoveButton;

	[DontSave]
	public Switch3D curvedSlidableGraphSwitch;

	[DontSave]
	public Switch3D togglePlayerVisibilitySwitch;

	[DontSave]
	public Switch3D finishBtn;

	[DontSave]
	public Switch3D itemSwitch;

	[DontSave]
	public Item itemToGetFromSwitch;

	[DontSave]
	public Switch3D testSwitch;

	[DontSave]
	public Trigger testTrigger;

	public int triggerEnterCounter;

	[DontSave]
	public GameObject singlePlayerOnlyObject;

	[Header("Images")]
	public int currentImageSpriteIndex;

	public int currentSpriteRendererSpriteIndex;

	[DontSave]
	public List<Sprite> imageSprites;

	[DontSave]
	public List<Sprite> spriteRendererSprites;

	[DontSave]
	public Image image;

	[DontSave]
	public SpriteRenderer spriteRenderer;

	[DontSave]
	public Switch3D changeSpritesSwitch;

	[DontSave]
	public Transform redItem;

	[DontSave]
	public Transform blueItem;

	[DontSave]
	public Transform greenItem;

	[DontSave]
	public Transform redPlace;

	[DontSave]
	public Transform bluePlace;

	[DontSave]
	public Transform greenPlace;

	[DontSave]
	public Turnable turnable1;

	[DontSave]
	public Turnable turnable2;

	[DontSave]
	private int turnableEventId;

	[DontSave]
	private bool onUpdateInvoked;

	public override void onInit()
	{
		game.setParent(startTimerTargetStartPosition, startTimerTarget.parent);
		game.setParent(startTimerTargetEndPosition, startTimerTarget.parent);
		initBezierSlidableLine();
		initBezierSlidableGraphLines();
		singlePlayerOnlyObject.SetActive(game.getPlayerCount() == 1);
	}

	public override void onInitAfterLoad()
	{
		Debug.Log("onInitAfterLoad");
	}

	private void initBezierSlidableLine()
	{
		Vector3 startPoint = slidableBezier.startPoint;
		Vector3 startControlPosition = slidableBezier.startControlPosition;
		Vector3 endControlPosition = slidableBezier.endControlPosition;
		Vector3 endPoint = slidableBezier.endPoint;
		List<Vector3> list = new List<Vector3>();
		for (float num = 0f; num <= 1f; num += 0.01f)
		{
			list.Add(Maths.cubicBezier(startPoint, startControlPosition, endControlPosition, endPoint, num));
		}
		slidableBezierLine.positionCount = list.Count;
		slidableBezierLine.SetPositions(list.ToArray());
	}

	private void initBezierSlidableGraphLines()
	{
		foreach (SlidableGraph.Edge edge in curvedSlidableGraph.edges)
		{
			if (edge.startNode == null || edge.endNode == null)
			{
				continue;
			}
			List<Vector3> points = new List<Vector3>();
			LineRenderer lineRenderer = Object.Instantiate(curvedSlidableGraphLineTemplate, curvedSlidableGraph.transform);
			if (edge.isCurved)
			{
				Vector3 startPoint = edge.startPoint;
				Vector3 startControlPosition = edge.startControlPosition;
				Vector3 endControlPosition = edge.endControlPosition;
				Vector3 endPoint = edge.endPoint;
				for (float num = 0f; num <= 1f; num += 0.05f)
				{
					addPointLocalSpace(Maths.cubicBezier(startPoint, startControlPosition, endControlPosition, endPoint, num));
				}
				addPointLocalSpace(Maths.cubicBezier(startPoint, startControlPosition, endControlPosition, endPoint, 1f));
			}
			else
			{
				addPointLocalSpace(edge.startPoint);
				addPointLocalSpace(edge.endPoint);
			}
			for (int i = 0; i < points.Count; i++)
			{
				Vector3 position = curvedSlidableGraph.transform.position;
				position.y = 0f;
				Vector3 vector = points[i];
				vector.y = 0f;
				Vector3 vector2 = vector - position;
				points[i] += vector2.normalized * 0.01f;
			}
			lineRenderer.positionCount = points.Count;
			lineRenderer.SetPositions(points.ToArray());
			void addPointLocalSpace(Vector3 point)
			{
				Vector3 item = curvedSlidableGraph.transform.InverseTransformPoint(point);
				points.Add(item);
			}
		}
	}

	public override void onInitPredicates()
	{
		game.registerPredicate(LevelPredicate.PlacementOfCubes, () => isClose(redPlace, redItem), () => isClose(bluePlace, blueItem), () => isClose(greenPlace, greenItem));
		game.registerPredicate(LevelPredicate.PlacementOfCubesTimed, 5f, false, () => isClose(redPlace, redItem), () => isClose(bluePlace, blueItem), () => isClose(greenPlace, greenItem));
		game.registerPredicate(LevelPredicate.PlacementOfCubesRepeatable, 5f, true, () => isClose(redPlace, redItem), () => isClose(bluePlace, blueItem), () => isClose(greenPlace, greenItem));
		static bool isClose(Transform a, Transform b)
		{
			if (UnityUtils.closeEnough(a.position, b.position, 0.25f) && a.gameObject.activeInHierarchy)
			{
				return b.gameObject.activeInHierarchy;
			}
			return false;
		}
	}

	public override void onLevelPredicateDone(int type, int doneCounter)
	{
		switch (type)
		{
		case 0:
			Debug.Log("Completed Placement of cubes predicate!");
			break;
		case 1:
			Debug.Log("Completed Placement of cubes timed predicate!");
			break;
		case 2:
			Debug.Log($"+ Completed Placement of cubes repeatable predicate! Done count: {doneCounter}");
			break;
		}
	}

	public override void onLevelPredicateUndone(int type)
	{
		if (type == 2)
		{
			Debug.Log("- Undone Placement of cubes repeatable predicate!");
		}
	}

	public override void onUpdate()
	{
		if (!onUpdateInvoked)
		{
			onUpdateInvoked = true;
			Debug.Log(string.Format("[{0}] Frame: {1}", "onUpdate", Time.frameCount));
		}
	}

	public override void onTrackable(Trackable trackable, TrackableEvent trackableEvent)
	{
		Debug.Log($"[{game.name}] Trackable {trackable.name} event: {trackableEvent.type} diff: {trackableEvent.diff}");
	}

	public override void onLookable(Lookable lookable, bool isActivated)
	{
		Debug.Log("[" + game.name + "] Lookable " + lookable.name + " is " + (isActivated ? "activated" : "deactivated"));
	}

	public override void onTrigger(Trigger trigger, TriggerEvent triggerEvent)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.AppendLine($"[{game.name}] Trigger {trigger} called event: {triggerEvent.type}");
		stringBuilder.AppendLine("FULL STATE:");
		stringBuilder.AppendLine("\tPlayers in trigger: " + string.Join(", ", triggerEvent.playersInTrigger));
		stringBuilder.AppendLine("\tInteractives in trigger: " + string.Join(", ", triggerEvent.interactivesInTrigger));
		stringBuilder.AppendLine("DELTA STATE:");
		stringBuilder.AppendLine("\tPlayers entered this event: " + string.Join(", ", triggerEvent.playersEnteredThisEvent));
		stringBuilder.AppendLine("\tInteractives entered this event: " + string.Join(", ", triggerEvent.interactivesEnteredThisEvent));
		stringBuilder.AppendLine("\tPlayers left this event: " + string.Join(", ", triggerEvent.playersLeftThisEvent));
		stringBuilder.AppendLine("\tInteractives left this event: " + string.Join(", ", triggerEvent.interactivesLeftThisEvent));
		if (trigger == testTrigger && triggerEvent.type == TriggerEventType.Enter)
		{
			triggerEnterCounter++;
			Debug.Log($"Trigger enter (in: {triggerEvent.interactivesInTrigger.Count}) counter: {triggerEnterCounter}");
		}
		if (trigger == testTrigger && triggerEvent.type == TriggerEventType.Exit)
		{
			triggerEnterCounter--;
			Debug.Log($"Trigger exit (in: {triggerEvent.interactivesInTrigger.Count}) counter: {triggerEnterCounter}");
		}
	}

	public override void onPickUpCarriableItem(Item item)
	{
		Debug.Log("carriable start " + item.name);
	}

	public override void onDropCarriableItem(Item item)
	{
		Debug.Log("carriable drop " + item.name);
	}

	public override void onRPCCalled(int type)
	{
		if (type == 0)
		{
			Debug.Log("Called Blue Slot RPC!");
		}
	}

	public override void onSwitch3D(Switch3D switch3D, Switch3DEvent switchEvent)
	{
		if (switch3D == testSwitch)
		{
			Debug.Log($"Has authority on switch: {game.session.getUsername(switch3D.authorityPlayerId)} | Event: {switchEvent}");
		}
		if (switchEvent == Switch3DEvent.On && switch3D == finishBtn)
		{
			game.levelCompleted();
		}
		if (switchEvent == Switch3DEvent.Start)
		{
			if (switch3D == togglePlayerVisibilitySwitch)
			{
				game.toggleOtherPlayersVisibility();
			}
			if (switch3D == changeMaterialStateButton)
			{
				Debug.Log($"Deferred call test: {changeMaterialStateButton} with targetable = {changeMaterialStateButton.targetable}");
				changeMaterialStateButton.targetable = false;
			}
		}
		if (switch3D == cutSceneButton)
		{
			cutScene.SetActive(value: true);
			game.startTimer(new DisableCutSceneTimer(), 2f);
		}
		else if (switch3D == changeMaterialStateButton)
		{
			buttonMaterialState.transitionTo("Green", 0.5f, (!isButtonMaterialStateGreen) ? 1 : 0);
			isButtonMaterialStateGreen = !isButtonMaterialStateGreen;
		}
		else if (switch3D == startTimerButton && switchEvent == Switch3DEvent.Start)
		{
			if (game.getTimers<StartTimerButtonTimer>().Count > 0)
			{
				game.cancelTimers<StartTimerButtonTimer>();
				game.cancelTimers<DataTimer>();
			}
			game.startTimer(new StartTimerButtonTimer(), 5f);
			game.startTimer(new DataTimer(1337, new int[4] { 1, 3, 3, 7 }, "1337"), 3f);
		}
		else if (switch3D == startSequenceButton)
		{
			sequence.play();
		}
		else if (switch3D == itemSwitch)
		{
			game.addItemToInventory(itemToGetFromSwitch.gameObject);
		}
		else if (switch3D == changeSpritesSwitch && switchEvent == Switch3DEvent.Start)
		{
			currentImageSpriteIndex = (currentImageSpriteIndex + 1) % imageSprites.Count;
			image.sprite = imageSprites[currentImageSpriteIndex];
			currentSpriteRendererSpriteIndex = (currentSpriteRendererSpriteIndex + 1) % spriteRendererSprites.Count;
			spriteRenderer.sprite = spriteRendererSprites[currentSpriteRendererSpriteIndex];
		}
		else if (switch3D == curvedSlidableGraphResetButton && switchEvent == Switch3DEvent.Start)
		{
			curvedSlidableGraph.reset();
		}
		else if (switch3D == curvedSlidableGraphMoveButton && switchEvent == Switch3DEvent.Start)
		{
			game.startSwitch(curvedSlidableGraphSwitch);
		}
	}

	public override void onAddToInventory(Item item)
	{
		Debug.Log($"{item.name} added to inventory {game.name} {game.hasAuthority(item.gameObject)}");
	}

	public override void onSlidableMoved(Slidable slidable, MoveEvent moveEvent)
	{
		if (slidable == slidableTop)
		{
			Debug.Log($"{game.name} Slider top at {slidable.value} (target priority: {slidable.targetPriority}). Event: {moveEvent}");
			slidable.targetPriority++;
			slidableTextTMP.text = slidable.value.ToString(CultureInfo.InvariantCulture);
			slidableTextTMP.color = Color.Lerp(Color.white, Color.deepSkyBlue, slidable.value);
		}
		if (slidable == slidableBot)
		{
			Debug.Log($"{game.name} Slider bot at {slidable.value}. Event: {moveEvent}");
		}
	}

	public override void onSlot(Slot slot)
	{
		Debug.Log(string.Format("[{0}] {1} | Slot: {2} | Item: {3}", game.name, "onSlot", slot, slot.insertedItem));
		if (slot == blueSlot)
		{
			game.callRPC(RPCs.BlueSlotRPC);
		}
	}

	public override void onRemoveFromSlot(Slot slot, Item item)
	{
		Debug.Log(string.Format("[{0}] {1} | Slot: {2} | Item: {3}", game.name, "onRemoveFromSlot", slot, item));
	}

	public override void onPose(NetPlayerId playerId, CharacterPose characterPose, CharacterPoseState poseEvent)
	{
		Debug.Log(string.Format("[{0}] {1} | PlayerId: {2} | isLocal: {3} | Pose: {4} | Event: {5}", game.name, "onPose", playerId, game.localPlayerData.id == playerId, characterPose, poseEvent));
	}

	public override void onSlidableGraphArrivedToNode(SlidableGraph.ToNode context)
	{
		Debug.Log(game.name + " onSlidableGraphArrivedToNode");
	}

	public override void onSlidableGraphArrivedToEdge(SlidableGraph.ToEdge context)
	{
		Debug.Log(game.name + " onSlidableGraphArrivedToEdge");
	}

	public override void onSlidableGraphMovedOnEdge(SlidableGraph.OnEdge context)
	{
		Debug.Log(game.name + " onSlidableGraphMovedOnEdge");
	}

	public override void onSlidableGraphReleased(SlidableGraph.OnPieceInteraction context)
	{
		Debug.Log(game.name + " onSlidableGraphReleased");
	}

	public override void onSlidableGraphBegin(SlidableGraph.OnPieceInteraction context)
	{
		Debug.Log(game.name + " onSlidableGraphBegin");
	}

	public override void onTurnableMoved(Turnable turnable, MoveEvent moveEvent)
	{
		if (turnable == turnable1 || turnable == turnable2)
		{
			if (moveEvent == MoveEvent.Released)
			{
				Debug.Log($"[{turnableEventId++}] T1: {turnable1.value} | T2: {turnable2.value}");
			}
		}
		else
		{
			Debug.Log($"[{game.name}] [{moveEvent}] Turnable {turnable.name} has value of {turnable.value} ");
		}
	}

	public override void onDialMoved(Dial dial, MoveEvent moveEvent)
	{
		Debug.Log($"[{game.name}] [{moveEvent}] Dial {dial.name} has value of {dial.value} ");
	}

	public override void onRotatableMoved(Rotatable rotatable, MoveEvent moveEvent)
	{
		Debug.Log($"[{game.name}] [{moveEvent}] Rotatable {rotatable.name} has value of {rotatable.transform.eulerAngles} ");
	}

	public override void onJoystickMoved(Joystick joystick, MoveEvent moveEvent)
	{
		Debug.Log($"[{game.name}] [{moveEvent}] Joystick {joystick.name} has value of {joystick.currentRotation} ");
	}

	public override void onSequenceDone(Sequence sequence)
	{
		Debug.Log($"Sequence '{sequence}' done.", sequence);
		if (!(sequence.GetComponent<Trashcan>() != null))
		{
			sequence.sequenceTime = 0f;
			sequence.targetSequenceTime = 0f;
			sequence.syncVisuals();
		}
	}

	public override void onUnlock(Lock targetLock)
	{
		if (targetLock == turnablesLock)
		{
			Debug.Log("Turnables lock unlocked!");
			Game obj = game;
			Transform obj2 = turnablesLockCubeTransform;
			Quaternion? rotation = Quaternion.Euler(0f, 0f, -45f);
			obj.startTransitionLocal(obj2, 3f, 0f, null, rotation);
		}
	}

	public override void onJigsaw(Jigsaw jigsaw, JigsawPiece piece, JigsawEvent jigsawEvent)
	{
		Debug.Log(string.Format("{0} {1} | Jigsaw: {2} | Piece: {3} | Event: {4}", game.name, "onJigsaw", jigsaw, piece, jigsawEvent), piece);
	}

	public override void onConvertToSinglePlayer()
	{
		Debug.Log("Converting level to single player!");
		singlePlayerOnlyObject.SetActive(value: true);
	}

	public virtual void save(FastBinaryWriter writer)
	{
		writer.Write(in isButtonMaterialStateGreen, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in triggerEnterCounter, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentImageSpriteIndex, default(FastBinaryWriter.ForPrimitives));
		writer.Write(in currentSpriteRendererSpriteIndex, default(FastBinaryWriter.ForPrimitives));
	}

	public virtual void load(FastBinaryReader reader)
	{
		isButtonMaterialStateGreen = reader.ReadBoolean();
		triggerEnterCounter = reader.ReadInt32();
		currentImageSpriteIndex = reader.ReadInt32();
		currentSpriteRendererSpriteIndex = reader.ReadInt32();
	}

	public virtual void read(FastBinaryReader reader, List<SaveFileProperty> saveFileProperties)
	{
		int position = reader.Position;
		bool flag = reader.ReadBoolean();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "isButtonMaterialStateGreen",
			fieldValue = $"{flag}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "triggerEnterCounter",
			fieldValue = $"{num}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num2 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentImageSpriteIndex",
			fieldValue = $"{num2}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
		position = reader.Position;
		int num3 = reader.ReadInt32();
		saveFileProperties.Add(new SaveFileProperty
		{
			fieldObject = this,
			fieldName = "currentSpriteRendererSpriteIndex",
			fieldValue = $"{num3}",
			fieldOffset = position,
			fieldSize = reader.Position - position
		});
	}

	public override Timer getTimer(byte id)
	{
		return id switch
		{
			0 => new DisableCutSceneTimer(), 
			1 => new StartTimerButtonTimer(), 
			2 => new DataTimer(), 
			_ => null, 
		};
	}

	[CompilerGenerated]
	public override void onTimerUpdate(Timer timer)
	{
		if (!(timer is DisableCutSceneTimer))
		{
			if (timer is StartTimerButtonTimer timer2)
			{
				onStartTimerButtonTimerUpdate(timer2);
			}
			else
			{
				_ = timer is DataTimer;
			}
		}
	}

	private void onStartTimerButtonTimerUpdate(StartTimerButtonTimer timer)
	{
		Vector3 position = startTimerTargetStartPosition.position;
		Vector3 position2 = startTimerTargetEndPosition.position;
		startTimerTarget.position = Vector3.Lerp(position, position2, timer.unitTime);
		if (timer.unitTime >= 0.5f)
		{
			game.cancelTimers<StartTimerButtonTimer>();
		}
	}

	[CompilerGenerated]
	public override void onTimerDone(Timer timer)
	{
		if (!(timer is DisableCutSceneTimer) && !(timer is StartTimerButtonTimer) && timer is DataTimer timer2)
		{
			onDataTimerDone(timer2);
		}
	}

	private void onDataTimerDone(DataTimer timer)
	{
		Debug.Log(string.Format("Timer with data: {0} | {1} | {2}", timer.someInt, string.Join(", ", timer.someIntArray), timer.someString));
	}
}

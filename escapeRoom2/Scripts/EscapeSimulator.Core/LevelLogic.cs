using System;
using UnityEngine;

public class LevelLogic : MonoBehaviour
{
	[NonSerialized]
	public Game game;

	public virtual void onInit()
	{
	}

	public virtual void onInitHints()
	{
	}

	public virtual void onInitPredicates()
	{
	}

	public virtual void onInitAfterLoad()
	{
	}

	public virtual void onUpdate()
	{
	}

	public virtual void onFixedUpdate()
	{
	}

	public virtual void onLateUpdate()
	{
	}

	public virtual void onSlidableMoved(Slidable slidable, MoveEvent moveEvent)
	{
	}

	public virtual void onHorizontalSliderMoved(HorizontalSlider slider, int pointIndex, MoveEvent moveEvent)
	{
	}

	public virtual void onTurnableMoved(Turnable turnable, MoveEvent moveEvent)
	{
	}

	public virtual void onTrackable(Trackable trackable, TrackableEvent trackableEvent)
	{
	}

	public virtual void onDialMoved(Dial dial, MoveEvent moveEvent)
	{
	}

	public virtual void onJoystickMoved(Joystick joystick, MoveEvent moveEvent)
	{
	}

	public virtual void onRotatableMoved(Rotatable rotatable, MoveEvent moveEvent)
	{
	}

	public virtual void onSwitch3D(Switch3D switch3D, Switch3DEvent switchEvent)
	{
	}

	public virtual void onJigsaw(Jigsaw jigsaw, JigsawPiece piece, JigsawEvent jigsawEvent)
	{
	}

	public virtual void onSlot(Slot slot)
	{
	}

	public virtual void onRemoveFromSlot(Slot slot, Item item)
	{
	}

	public virtual void onSwapper(Swapper swapper, Swapper.SwapperEvent swapperEvent)
	{
	}

	public virtual void onSlidableGraphArrivedToNode(SlidableGraph.ToNode context)
	{
	}

	public virtual void onSlidableGraphArrivedToEdge(SlidableGraph.ToEdge context)
	{
	}

	public virtual void onSlidableGraphMovedOnEdge(SlidableGraph.OnEdge context)
	{
	}

	public virtual void onSlidableGraphBegin(SlidableGraph.OnPieceInteraction context)
	{
	}

	public virtual void onSlidableGraphReleased(SlidableGraph.OnPieceInteraction context)
	{
	}

	public virtual void onSurfaceSlotPlace(SurfaceSlot.Placement placement)
	{
	}

	public virtual void onPose(NetPlayerId playerId, CharacterPose characterPose, CharacterPoseState poseEvent)
	{
	}

	public virtual void onAddToInventory(Item item)
	{
	}

	public virtual void onPickUpCarriableItem(Item item)
	{
	}

	public virtual void onDropCarriableItem(Item item)
	{
	}

	public virtual void onLookable(Lookable lookable, bool isActivated)
	{
	}

	public virtual void onUnlock(Lock targetLock)
	{
	}

	public virtual void onTrigger(Trigger trigger, TriggerEvent triggerEvent)
	{
	}

	public virtual void onTool(Item tool, ToolContext context)
	{
	}

	public virtual void onTimerUpdate(Timer timer)
	{
	}

	public virtual void onTimerDone(Timer timer)
	{
	}

	public virtual void onAnyPlayerZoomedIn(Interactive interactive)
	{
	}

	public virtual void onAllPlayersZoomedOut(Interactive interactive)
	{
	}

	public virtual void onZoomEnter(GameObject zoomedItem)
	{
	}

	public virtual void onZoomLeave(GameObject zoomedItem)
	{
	}

	public virtual void onChatCommand(string message)
	{
	}

	public virtual void onRoulette(Roulette roulette, int index)
	{
	}

	public virtual void onConvertToSinglePlayer()
	{
	}

	public virtual void onLevelPredicateDone(int type, int doneCounter)
	{
	}

	public virtual void onLevelPredicateUndone(int type)
	{
	}

	public virtual void onRPCCalled(int type)
	{
	}

	public virtual void onRemoveFromInventory(Item item)
	{
	}

	public virtual void onTweenTransitionDone(TweenState tweenState, string state)
	{
	}

	public virtual void onMaterialTransitionDone(MaterialState tweenState, string state)
	{
	}

	public virtual void onSequenceDone(Sequence sequence)
	{
	}

	public virtual void onCustomModeLeave(CustomMode mode)
	{
	}

	public virtual void onNpcStart(Npc npc)
	{
	}

	public virtual void onNpcExit(Npc npc)
	{
	}

	public virtual void onNpcChoiceSelected(Npc npc, int lineIndex, int choiceIndex)
	{
	}

	public virtual void onPacket(Packet packet)
	{
	}

	public virtual int getPacketCount()
	{
		return 0;
	}

	public virtual Packet getPacket(byte id)
	{
		return null;
	}

	public virtual Timer getTimer(byte id)
	{
		return null;
	}
}

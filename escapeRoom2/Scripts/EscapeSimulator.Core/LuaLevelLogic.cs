using System.Text;
using UnityEngine;

public class LuaLevelLogic : LevelLogic
{
	public enum LocalPlayerTriggerState
	{
		NotProcessing = 0,
		NotLocalPlayer = 1,
		LocalPlayer = 2
	}

	public sealed class EditorTimer : Timer
	{
		public EditorDelay delay;

		public override byte getTypeId()
		{
			return 0;
		}

		public EditorTimer()
		{
		}

		public EditorTimer(EditorDelay delay)
		{
			this.delay = delay;
		}

		public override void writeData(FastBinaryWriter writer)
		{
			writer.WriteComponent(delay);
		}

		public override void readData(FastBinaryReader reader)
		{
			delay = reader.ReadComponent<EditorDelay>();
		}

		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString()).AppendLine();
			stringBuilder.Append("delay: " + $"{delay}");
			return stringBuilder.ToString();
		}
	}

	public override void onInit()
	{
		game.callLevelLogicLuaFunction("onInit", (LuaLinks _) => true);
	}

	public override void onUpdate()
	{
		game.callLevelLogicLuaFunction("onUpdate", (LuaLinks _) => true);
	}

	public override void onSwitch3D(Switch3D switch3D, Switch3DEvent switchEvent)
	{
		game.callLevelLogicLuaFunction("onSwitch3D", (LuaLinks links) => links.linkedSwitches.Contains(switch3D), switch3D, switchEvent);
	}

	public override void onSlot(Slot slot)
	{
		game.callLevelLogicLuaFunction("onSlot", (LuaLinks links) => links.linkedSlots.Contains(slot), slot);
	}

	public override void onRemoveFromSlot(Slot slot, Item item)
	{
		game.callLevelLogicLuaFunction("onRemoveFromSlot", (LuaLinks links) => links.linkedSlots.Contains(slot), slot, item);
	}

	public override void onUnlock(Lock targetLock)
	{
		game.callLevelLogicLuaFunction("onUnlock", (LuaLinks links) => links.linkedLocks.Contains(targetLock), targetLock);
	}

	public override void onTrigger(Trigger trigger, TriggerEvent triggerEvent)
	{
		game.luaLocalPlayerTriggerState = LocalPlayerTriggerState.NotLocalPlayer;
		if (trigger.canPlayerTrigger)
		{
			if (triggerEvent.playersEnteredThisEvent.Contains(game.localPlayerData.id))
			{
				game.luaLocalPlayerTriggerState = LocalPlayerTriggerState.LocalPlayer;
			}
			if (triggerEvent.playersLeftThisEvent.Contains(game.localPlayerData.id))
			{
				game.luaLocalPlayerTriggerState = LocalPlayerTriggerState.LocalPlayer;
			}
		}
		game.callLevelLogicLuaFunction("onTrigger", (LuaLinks links) => links.linkedTriggers.Contains(trigger), trigger, triggerEvent);
		game.luaLocalPlayerTriggerState = LocalPlayerTriggerState.NotProcessing;
	}

	public override void onRoulette(Roulette roulette, int index)
	{
		game.callLevelLogicLuaFunction("onRoulette", (LuaLinks links) => links.linkedRoulettes.Contains(roulette), roulette, index);
	}

	public override void onRotatableMoved(Rotatable rotatable, MoveEvent moveEvent)
	{
		game.callLevelLogicLuaFunction("onRotatableMoved", (LuaLinks links) => links.linkedRotatables.Contains(rotatable), rotatable, moveEvent);
	}

	public override void onLookable(Lookable lookable, bool isActivated)
	{
		game.callLevelLogicLuaFunction("onLookable", (LuaLinks links) => links.linkedLookables.Contains(lookable), lookable, isActivated);
	}

	public override void onTimerDone(Timer timer)
	{
		EditorTimer editorTimer = timer as EditorTimer;
		if (editorTimer != null)
		{
			game.callLevelLogicLuaFunction("onDelay", (LuaLinks links) => links.linkedDelays.Contains(editorTimer.delay), editorTimer.delay);
		}
		else
		{
			Debug.LogError(string.Format("{0}: Unknown timer type {1}.", "LuaLevelLogic", timer?.GetType()));
		}
	}

	public override Timer getTimer(byte id)
	{
		if (id == 0)
		{
			return new EditorTimer();
		}
		return null;
	}
}

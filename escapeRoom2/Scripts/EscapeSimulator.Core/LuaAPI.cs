using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class LuaAPI
{
	public static string executingScriptName;

	private Game game;

	[Preserve]
	public Game.GamePlayerData getLocalPlayerData => game.localPlayerData;

	[Preserve]
	public Ray getPlayerViewRay => getLocalPlayerData.playerCameraRay;

	[Preserve]
	public Transform getPlayerRig => game.playerRig;

	[Preserve]
	public Transform getHeadPOV => game.headPov;

	[Preserve]
	public bool isProcessingPacket => game.isProcessingPacket;

	public void init(Game game)
	{
		this.game = game;
	}

	[Preserve]
	public void log(string message, bool isError = false)
	{
		string text = (string.IsNullOrEmpty(executingScriptName) ? "" : ("[" + executingScriptName + "] "));
		if (isError)
		{
			Debug.LogError(text + message);
		}
		else
		{
			Debug.Log(text + message);
		}
		if (game.isTestingPlaymodeInEditor)
		{
			game.addChatMessageLocal(message, text, isError ? ChatMessage.Type.CodeDebugError : ChatMessage.Type.CodeDebug);
		}
	}

	[Preserve]
	public void levelNote(string data)
	{
		Debug.Log(data);
		game.addChatMessageLocal(data, "", ChatMessage.Type.LevelNote);
	}

	[Preserve]
	public void setLockValue(Lock targetLock, int value, int index)
	{
		bool flag = index == -400 || index == -500;
		index--;
		if (targetLock != null)
		{
			if (index < 0 && !flag)
			{
				log("Index for locks have to be greater or equal to 1 ", isError: true);
			}
			else if (targetLock.currentValues.Length > index)
			{
				LockArgument lockArgument = new LockArgument
				{
					targetLock = targetLock,
					targetIndex = ((!flag) ? index : (index + 1))
				};
				game.handleLockValue(new LockArgument[1] { lockArgument }, value);
			}
			else
			{
				log("Index on lock " + targetLock.GetComponent<PropInstance>().scriptName + " has to be lower or equal than " + targetLock.currentValues.Length, isError: true);
			}
		}
		else
		{
			log("Trying to set a value on a non existant lock.", isError: true);
		}
	}

	[Preserve]
	public Vector2 vector2(float x, float y)
	{
		return new Vector2(x, y);
	}

	[Preserve]
	public Vector3 vector3(float x, float y, float z)
	{
		return new Vector3(x, y, z);
	}

	[Preserve]
	public float inverseLerp(Vector3 a, Vector3 b, Vector3 point)
	{
		return UnityUtils.inverseLerp(a, b, point);
	}

	[Preserve]
	public float map(float fromSource, float toSource, float fromTarget, float toTarget, float point)
	{
		return UnityUtils.map(fromSource, toSource, fromTarget, toTarget, point);
	}

	[Preserve]
	public float getPercentBetween(float min, float max, float point)
	{
		return UnityUtils.getPercentBetween(point, min, max);
	}

	[Preserve]
	public Transform getMainPlayer()
	{
		Transform result = game.playerCollider.transform;
		if (game.netPlayers != null && game.session.hostPlayerId != null)
		{
			Game.GamePlayerData gamePlayerData = game.netPlayers.Find((Game.GamePlayerData x) => x.id == game.session.hostPlayerId);
			result = ((gamePlayerData == null || !(gamePlayerData.selectedCharacter != null)) ? game.playerCollider.transform : gamePlayerData.selectedCharacter.transform);
		}
		return result;
	}

	[Preserve]
	public Transform getLocalPlayer()
	{
		return game.playerCollider.transform;
	}

	[Preserve]
	public Transform getClosestPlayer(Vector3 position)
	{
		Transform transform = game.playerCollider.transform;
		if (game.netPlayers != null && game.session.hostPlayerId != null)
		{
			float num = Vector3.Distance(position, transform.transform.position);
			foreach (Game.GamePlayerData netPlayer in game.netPlayers)
			{
				float num2 = Vector3.Distance(position, netPlayer.selectedCharacter.transform.position);
				if (num2 < num)
				{
					transform = netPlayer.selectedCharacter.transform;
					num = num2;
				}
			}
		}
		return transform;
	}

	[Preserve]
	public void teleport(GameObject teleportObject)
	{
		if (!teleportObject.TryGetComponent<Teleport>(out var component))
		{
			log("Teleport object does not have a Teleport component.", isError: true);
			return;
		}
		if (component.teleportAll || !game.isProcessingPacket)
		{
			if (component.changeRotation)
			{
				game.teleportPlayer(component.transform.position, component.transform.rotation.eulerAngles);
			}
			else
			{
				game.teleportPlayer(component.transform.position);
			}
			game.localPlayerData.speedWalking = component.walkingSpeed;
			game.localPlayerData.speedRunning = component.runningSpeed;
		}
		if (component.teleportAll)
		{
			foreach (Game.GamePlayerData netPlayer in game.netPlayers)
			{
				netPlayer.speedWalking = component.walkingSpeed;
				netPlayer.speedRunning = component.runningSpeed;
			}
			return;
		}
		if (game.isProcessingPacket)
		{
			Game.GamePlayerData gamePlayerData = game.netPlayers.Find((Game.GamePlayerData x) => x.id == game.processingPacketPlayerId);
			gamePlayerData.speedWalking = component.walkingSpeed;
			gamePlayerData.speedRunning = component.runningSpeed;
		}
	}

	[Preserve]
	public void teleportPlayer(Transform targetPlayer, Vector3 location)
	{
		if (targetPlayer == getLocalPlayer())
		{
			game.teleportPlayer(location);
		}
	}

	[Preserve]
	public void teleportPlayer(Transform targetPlayer, Vector3 location, Vector3 rotation)
	{
		if (targetPlayer == getLocalPlayer())
		{
			game.teleportPlayer(location, rotation);
		}
	}

	[Preserve]
	public void toggleActivator(ActivatorComponent activator)
	{
		if (game.isHost())
		{
			game.handleActivatorComponent(activator.gameObject);
		}
	}

	[Preserve]
	public void add(ref GameObject[] array, GameObject newObject)
	{
		Array.Resize(ref array, array.Length + 1);
		array[array.Length - 1] = newObject;
	}

	[Preserve]
	public void remove(ref GameObject[] array, GameObject toRemove)
	{
		for (int i = Array.IndexOf(array, toRemove); i < array.Length - 1; i++)
		{
			array[i] = array[i + 1];
		}
		Array.Resize(ref array, array.Length - 1);
	}

	[Preserve]
	public bool contains(GameObject[] array, GameObject lookup)
	{
		return UnityUtils.contains(array, lookup);
	}

	[Preserve]
	public bool contains(Component[] array, Component lookup)
	{
		return UnityUtils.contains(array, lookup);
	}

	[Preserve]
	public void playSound(GameObject sound)
	{
		game.playSound(sound);
	}

	[Preserve]
	public void handleRoulette(GameObject roulette)
	{
		game.handleRoulette(roulette.GetComponent<Roulette>());
	}

	[Preserve]
	public void handleDelay(GameObject delay)
	{
		game.handleDelay(delay.GetComponent<EditorDelay>());
	}

	[Preserve]
	public void handleFinish(GameObject finishGO)
	{
		game.levelCompleted(finishGO.GetComponent<Finish>());
	}

	[Preserve]
	public void callLuaFunction(GameObject luaExecutorGO)
	{
		LuaExecutor component = luaExecutorGO.GetComponent<LuaExecutor>();
		if (component.canBeTriggered && !string.IsNullOrEmpty(component.functionToCall))
		{
			if (component.lua.Globals[component.functionToCall] == null)
			{
				log("Lua function " + component.functionToCall + " could not be found.", isError: true);
			}
			else
			{
				game.callLuaFunction(component.lua, component.functionToCall);
			}
		}
	}

	[Preserve]
	public void openLink(GameObject link)
	{
		game.handleOpenLink(link);
	}

	[Preserve]
	public void handleSwitch3D(GameObject switch3D)
	{
		game.handleSwitch3DStart(switch3D.GetComponent<Switch3D>());
	}

	[Preserve]
	public void handleActivator(GameObject activator)
	{
		game.handleActivatorComponent(activator);
	}

	[Preserve]
	public void handlePuzzle(GameObject puzzle)
	{
		game.handleEditorPuzzle(puzzle);
	}

	[Preserve]
	public void activatePostProcessing(GameObject pp)
	{
		game.activateRoomEditorPostProcessing(pp);
	}

	[Preserve]
	public void activateSkybox(GameObject skybox)
	{
		game.activateRoomEditorSky(skybox);
	}

	[Preserve]
	public void activateClouds(GameObject clouds)
	{
		game.activateRoomEditorClouds(clouds);
	}

	[Preserve]
	public void activateFog(GameObject skybox)
	{
		game.activateRoomEditorFog(skybox);
	}

	[Preserve]
	public void activateOcean(GameObject ocean)
	{
		game.activateRoomEditorOcean(ocean);
	}

	[Preserve]
	public void startSwitch(Switch3D switch3D)
	{
		game.startSwitch(switch3D);
	}

	[Preserve]
	public void removeItemFromSlot(Item item)
	{
		game.removeItemFromSlot(item);
	}

	[Preserve]
	public ItemState getItemState(Item item)
	{
		return game.getItemState(item);
	}

	[Preserve]
	public void screenShakeDefault(float duration, float intensity, float roughness)
	{
		game.screenShakeDefault(duration, intensity, roughness);
	}

	[Preserve]
	public void setPointerReach(float newReach)
	{
		game.setPointerReach(newReach);
	}

	[Preserve]
	public void resetPointerReach()
	{
		game.resetPointerReach();
	}

	[Preserve]
	public Vector3 getCameraPosition()
	{
		return game.getCameraPosition();
	}

	[Preserve]
	public Quaternion getCameraRotation()
	{
		return game.getCameraRotation();
	}

	[Preserve]
	public Vector3 cameraTransformPoint(Vector3 point)
	{
		return game.cameraTransformPoint(point);
	}

	[Preserve]
	public Vector3 getCameraForward()
	{
		return game.getCameraForward();
	}

	[Preserve]
	public Vector3 getCameraRight()
	{
		return game.getCameraRight();
	}

	[Preserve]
	public Vector3 getCameraUp()
	{
		return game.getCameraUp();
	}

	[Preserve]
	public bool isItemPinned(GameObject item)
	{
		return game.isItemPinned(item);
	}

	[Preserve]
	public Slot isInSlot(Item item)
	{
		return game.isInSlot(item);
	}

	[Preserve]
	public bool isInInventory(GameObject go)
	{
		return game.isInInventory(go);
	}

	[Preserve]
	public void addItemToInventory(GameObject item, bool returnBorrowed, int preferredInventoryIndex, float transitionOriginScaleModifier, bool shouldRemoveFromSlotIfNeeded)
	{
		game.addItemToInventory(item, returnBorrowed, preferredInventoryIndex, transitionOriginScaleModifier, shouldRemoveFromSlotIfNeeded);
	}

	[Preserve]
	public bool hasAuthority(GameObject gameObject)
	{
		return game.hasAuthority(gameObject);
	}

	[Preserve]
	public bool isHost()
	{
		return game.isHost();
	}

	[Preserve]
	public int getPlayerCount()
	{
		return game.getPlayerCount();
	}

	[Preserve]
	public List<Game.GamePlayerData> getAllPlayers()
	{
		return game.getAllPlayers();
	}

	[Preserve]
	public int getSyncedRandomSeed()
	{
		return game.getSyncedRandomSeed();
	}

	[Preserve]
	public bool isAnyPlayerZoomIn(GameObject gameObject)
	{
		return game.isAnyPlayerZoomIn(gameObject);
	}

	[Preserve]
	public bool isInAnyPlayerInventory(GameObject gameObject)
	{
		return game.isInAnyPlayerInventory(gameObject);
	}

	[Preserve]
	public Game.GamePlayerData getPlayerWithItemInInventory(GameObject gameObject)
	{
		return game.getPlayerWithItemInInventory(gameObject);
	}

	[Preserve]
	public bool isInAnyOtherPlayerInventory(GameObject gameObject)
	{
		return game.isInAnyOtherPlayerInventory(gameObject);
	}

	[Preserve]
	public bool isAnyOtherPlayerHolding(Item item)
	{
		return game.isAnyOtherPlayerHolding(item);
	}

	[Preserve]
	public bool isAnyPlayerHolding(Item item)
	{
		return game.isAnyPlayerHolding(item);
	}

	[Preserve]
	public bool isAnyPlayerInteracting(GameObject gameObject)
	{
		return game.isAnyPlayerInteracting(gameObject);
	}

	[Preserve]
	public bool isLocalPlayerInteracting(GameObject gameObject)
	{
		return game.isLocalPlayerInteracting(gameObject);
	}

	[Preserve]
	public bool isAnyOtherPlayerInteracting(GameObject gameObject)
	{
		return game.isAnyOtherPlayerInteracting(gameObject);
	}

	[Preserve]
	public void setOtherPlayersInvisible()
	{
		game.setOtherPlayersInvisible();
	}

	[Preserve]
	public void setOtherPlayersVisible()
	{
		game.setOtherPlayersVisible();
	}

	[Preserve]
	public void toggleOtherPlayersVisibility()
	{
		game.toggleOtherPlayersVisibility();
	}

	[Preserve]
	public void removeSelectedItem(GameObject gameObject)
	{
		game.removeSelectedItem(gameObject);
	}

	[Preserve]
	public bool isInTopZoom(GameObject objectToCheck)
	{
		return game.isInTopZoom(objectToCheck);
	}

	[Preserve]
	public void increaseZoomCounter(GameObject interactiveGameObject)
	{
		game.increaseZoomCounter(interactiveGameObject);
	}

	[Preserve]
	public void increaseZoomCounter(Interactive interactive)
	{
		game.increaseZoomCounter(interactive);
	}
}

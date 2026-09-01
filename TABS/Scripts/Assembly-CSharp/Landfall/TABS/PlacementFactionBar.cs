using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DM;
using Landfall.TABS.GameMode;
using Landfall.TABS.GameState;
using Landfall.TABS.Workshop;
using Landfall.TABS_Input;
using ModIO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Landfall.TABS
{
	public class PlacementFactionBar : MonoBehaviour
	{
		public bool saveFactionBar;

		public GameObject prefab;

		public FactionButton expandFactionButton;

		public GameObject resetButton;

		public SimpleStateAnimation factionBarAnimator;

		public Transform RealObjectParent;

		public Transform FollowTransformParent;

		public ExpandedFactionUI expandedFactionUI;

		public Action<FactionButton> AddedFactionButton;

		public Action OnFactionBarChanged;

		private List<FactionButton> factionButtons = new List<FactionButton>();

		private IPlacementUI placementUI;

		private GameModeService gameModeService;

		private PlayerActions playerActions;

		private int cycleIndex;

		private void Awake()
		{
			placementUI = GetComponentInParent<IPlacementUI>();
			gameModeService = ServiceLocator.GetService<GameModeService>();
			playerActions = PlayerActions.Instance;
		}

		public void Spawn(Faction[] factions, bool allowEdit = true)
		{
			bool flag = true;
			if (!base.isActiveAndEnabled)
			{
				return;
			}
			if (gameModeService != null && gameModeService.CurrentGameMode is OnlineMultiplayerGameMode)
			{
				flag = false;
				factions = ContentDatabase.Instance().GetDefaultHotbarFactions().ToArray();
			}
			factionButtons = new List<FactionButton>();
			if (factions == null || factions.Length == 0)
			{
				FixExpandButton(allowEdit && flag);
				return;
			}
			int num = 0;
			foreach (Faction faction in factions)
			{
				if (!(faction == null) && (flag || (!faction.IsCustom && !faction.IsModFaction)) && faction.ValidateFactionUnitPropsAndWeapons())
				{
					FactionButton item = SpawnFaction(faction, num);
					factionButtons.Add(item);
					num++;
				}
			}
			bool flag2 = ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_RESTRICT_UNITS").currentValue == 1;
			FixExpandButton((allowEdit || flag2) && flag);
			OnFactionBarChanged?.Invoke();
			StartCoroutine(SelectFirstFaction());
		}

		public void Spawn()
		{
			Faction[] array = null;
			bool allowEdit = true;
			if (CampaignPlayerDataHolder.CurrentGameModeState == GameModeState.Campaign)
			{
				Faction[] array2 = ((ServiceLocator.GetService<GlobalSettingsHandler>().GetSettingsInstance("GAMEPLAY_RESTRICT_UNITS").currentValue == 1) ? GetFactions() : CampaignPlayerDataHolder.GetCurrentLevel().AllowedFactions);
				if (array2 == null || array2.Length == 0)
				{
					array = GetFactions();
				}
				else
				{
					array = array2.Where((Faction faction) => faction != null && ContentDatabase.Instance().GetFaction(faction.Entity.GUID) != null).ToArray();
					allowEdit = false;
				}
			}
			else
			{
				array = GetFactions();
			}
			Spawn(array, allowEdit);
			Faction[] GetFactions()
			{
				Faction[] factionBar = ServiceLocator.GetService<ISaveLoaderService>().GetFactionBar();
				List<int> enabledMods = LocalUser.EnabledModIds;
				return factionBar.Where((Faction faction) => faction != null && (faction.modID == 0 || enabledMods.Contains(faction.modID))).ToArray();
			}
		}

		public bool IsRoomForMoreShards()
		{
			float width = GetComponent<RectTransform>().rect.width;
			float width2 = prefab.GetComponent<RectTransform>().rect.width;
			float spacing = FollowTransformParent.GetComponent<HorizontalLayoutGroup>().spacing;
			int num = (int)(width / (width2 + spacing)) - 5;
			if (factionButtons.Count > num)
			{
				return false;
			}
			return true;
		}

		public Faction[] GetFactionsOnBar()
		{
			if (factionButtons == null || factionButtons.Count <= 0)
			{
				return new Faction[0];
			}
			Faction[] array = new Faction[factionButtons.Count];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = factionButtons[i].GetFaction();
			}
			return array;
		}

		public void AddFactionShard(Faction faction)
		{
			FactionButton factionButton = SpawnFaction(faction, factionButtons.Count);
			RegisterFactionButton(factionButton);
			expandFactionButton.SetFollowTransformLast();
		}

		public void RemoveFactionShard(Faction faction)
		{
			FactionButton factionButton = GetFactionButton(faction);
			UnregisterButton(factionButton);
			factionButton.DestoryTarget();
			UnityEngine.Object.Destroy(factionButton.gameObject);
		}

		private FactionButton GetFactionButton(Faction faction)
		{
			for (int i = 0; i < factionButtons.Count; i++)
			{
				if (factionButtons[i].GetFaction() == faction)
				{
					return factionButtons[i];
				}
			}
			return null;
		}

		private void FixExpandButton(bool active)
		{
			if (expandFactionButton.enabled)
			{
				Transform newFollowTransform = GetNewFollowTransform();
				FactionButton.FactionButtonData factionButtonData = new FactionButton.FactionButtonData(newFollowTransform.transform);
				FactionButton factionButton = expandFactionButton;
				factionButton.gameObject.SetActive(active);
				factionButton.SetupFaction(factionButtonData, expandedFactionUI);
				factionButton.SetPoistionDelayed(newFollowTransform.transform);
				factionButton.SetFollowTransform(newFollowTransform.transform);
			}
		}

		public void Clear()
		{
			Debug.Log("Clearing Factions");
			foreach (Transform item in FollowTransformParent)
			{
				UnityEngine.Object.Destroy(item.gameObject);
			}
			foreach (Transform item2 in RealObjectParent)
			{
				if (item2.gameObject != expandFactionButton.gameObject)
				{
					UnityEngine.Object.Destroy(item2.gameObject);
				}
			}
		}

		public FactionButton SpawnFaction(Faction faction, int index)
		{
			if (faction == null)
			{
				return null;
			}
			Transform newFollowTransform = GetNewFollowTransform();
			bool unlocked = true;
			if (gameModeService.IsCurrentBaseGameModeType<CampaignGameMode>())
			{
				TABSCampaignLevelAsset currentLevel = CampaignPlayerDataHolder.GetCurrentLevel();
				if (currentLevel != null)
				{
					unlocked = false;
					UnitBlueprint[] units = faction.Units;
					foreach (UnitBlueprint unit in units)
					{
						if (currentLevel.IsAllowed(unit))
						{
							unlocked = true;
							break;
						}
					}
				}
			}
			FactionButton.FactionButtonData factionButtonData = new FactionButton.FactionButtonData(faction, placementUI, unlocked, index, newFollowTransform.transform);
			FactionButton component = UnityEngine.Object.Instantiate(prefab, RealObjectParent).GetComponent<FactionButton>();
			component.SetupFaction(factionButtonData, expandedFactionUI);
			component.SetPoistionDelayed(newFollowTransform.transform);
			component.SetFollowTransform(newFollowTransform.transform);
			AddedFactionButton?.Invoke(component);
			return component;
		}

		private FactionButton SpawnExpandButton()
		{
			Transform newFollowTransform = GetNewFollowTransform();
			FactionButton.FactionButtonData factionButtonData = new FactionButton.FactionButtonData(newFollowTransform.transform);
			FactionButton component = UnityEngine.Object.Instantiate(prefab, RealObjectParent).GetComponent<FactionButton>();
			component.SetupFaction(factionButtonData, expandedFactionUI);
			component.SetPoistionDelayed(newFollowTransform.transform);
			component.SetFollowTransform(newFollowTransform.transform);
			return component;
		}

		public Transform GetNewFollowTransform()
		{
			Image image = new GameObject("Follow Transform").AddComponent<Image>();
			image.GetComponent<RectTransform>().sizeDelta = prefab.GetComponent<RectTransform>().sizeDelta;
			image.GetComponent<RectTransform>().pivot = prefab.GetComponent<RectTransform>().pivot;
			image.transform.SetParent(FollowTransformParent);
			image.enabled = false;
			return image.transform;
		}

		public float GetFactionButtonY()
		{
			if (factionButtons.Count > 0)
			{
				return factionButtons[0].transform.position.y;
			}
			return expandFactionButton.transform.position.y;
		}

		public int GetIndexForPos(Vector2 pos)
		{
			for (int i = 0; i < factionButtons.Count; i++)
			{
				if (pos.x < factionButtons[i].transform.position.x)
				{
					return i;
				}
			}
			return factionButtons.Count;
		}

		public void AddForce(Vector3 point, float strength)
		{
			float num = Mathf.Clamp(Time.unscaledDeltaTime, 0f, 0.02f);
			for (int i = 0; i < factionButtons.Count; i++)
			{
				Vector3 normalized = (factionButtons[i].transform.position - point).normalized;
				float num2 = Vector3.Distance(factionButtons[i].transform.position, point);
				float num3 = 1700f;
				float num4 = (num3 - num2) / num3;
				factionButtons[i].AddForce(normalized * strength * 8000f * num4 * num);
			}
		}

		public void RegisterAddFactionButtonCallback(Action<FactionButton> action)
		{
			AddedFactionButton = action;
		}

		public void RegisterFactionButton(FactionButton factionButton)
		{
			Canvas.ForceUpdateCanvases();
			factionButtons.Add(factionButton);
			factionButtons.Sort((FactionButton button01, FactionButton button02) => button01.GetFollowTransform().position.x.CompareTo(button02.GetFollowTransform().position.x));
			if (saveFactionBar)
			{
				SaveFactionBar();
			}
			AddedFactionButton?.Invoke(factionButton);
			OnFactionBarChanged?.Invoke();
		}

		public void UnregisterButton(FactionButton factionButton)
		{
			factionButtons.Remove(factionButton);
			factionButtons.Sort((FactionButton button01, FactionButton button02) => button01.transform.position.x.CompareTo(button02.transform.position.x));
			if (saveFactionBar)
			{
				SaveFactionBar();
			}
			OnFactionBarChanged?.Invoke();
		}

		private void SaveFactionBar()
		{
			Faction[] factionsOnBar = GetFactionsOnBar();
			ServiceLocator.GetService<ISaveLoaderService>().SetFactionBar(factionsOnBar);
		}

		public void ResetDefaultFactions()
		{
			Clear();
			Spawn(ContentDatabase.Instance().GetDefaultHotbarFactions().ToArray());
			if (saveFactionBar)
			{
				SaveFactionBar();
			}
		}

		public bool IsFactionOnBar(Faction faction)
		{
			Faction[] factionsOnBar = GetFactionsOnBar();
			if (factionsOnBar == null || factionsOnBar.Length == 0)
			{
				return false;
			}
			for (int i = 0; i < factionsOnBar.Length; i++)
			{
				if (factionsOnBar[i] == faction)
				{
					return true;
				}
			}
			return false;
		}

		private void Update()
		{
			BaseGameMode currentGameMode = gameModeService.CurrentGameMode;
			if (currentGameMode != null && currentGameMode.GameStateManager?.GameState == Landfall.TABS.GameState.GameState.PlacementState)
			{
				bool flag = factionBarAnimator.m_state == SimpleStateAnimation.State.State01;
				if (playerActions.m_cycleTabsUp.WasPressed && (flag || expandedFactionUI.open))
				{
					CycleFactionButton(1);
				}
				if (playerActions.m_cycleTabsDown.WasPressed && (flag || expandedFactionUI.open))
				{
					CycleFactionButton(-1);
				}
				if (playerActions.m_resetFactions.WasPressed && expandedFactionUI.open && resetButton != null && resetButton.activeSelf)
				{
					ResetDefaultFactions();
				}
			}
		}

		public void CycleFactionButton(int increase)
		{
			if (factionButtons.Count <= 0)
			{
				return;
			}
			cycleIndex = Mod(cycleIndex + increase, factionButtons.Count);
			FactionButton factionButton = factionButtons[cycleIndex];
			if (!(factionButton == null))
			{
				if (factionButton.faction == null)
				{
					CycleFactionButton(increase);
					return;
				}
				factionButton.OnPointerEnter(new PointerEventData(EventSystem.current));
				factionButton.PointerSelect();
			}
		}

		private IEnumerator SelectFirstFaction()
		{
			if (factionButtons.Count > 0)
			{
				FactionButton factionButton = factionButtons[0];
				if (!(factionButton == null))
				{
					yield return 1;
					factionButton.PointerSelect();
				}
			}
		}

		private int Mod(int x, int m)
		{
			if (m == 0)
			{
				return 0;
			}
			return (x % m + m) % m;
		}
	}
}

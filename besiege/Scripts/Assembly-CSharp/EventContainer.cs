using System.Collections.Generic;
using InternalModding.Events;
using Localisation;
using UnityEngine;

public class EventContainer
{
	public enum EventType
	{
		Progress = 0,
		Wait = 1,
		Repeat = 2,
		Transform = 3,
		GameWin = 4,
		Reset = 5,
		Activate = 6,
		Deactivate = 7,
		Variable = 8,
		Random = 9,
		RespawnMachine = 10,
		SetRespawn = 11,
		ReloadMachine = 12,
		Approach = 13,
		Pursue = 14,
		FactionCharge = 15,
		Flee = 16,
		Strafe = 17,
		WalkAround = 18,
		Stationary = 19,
		Modded = 20
	}

	public class LevelProgressEvent : EventContainer
	{
		public float progress;

		public LevelProgressEvent()
		{
			progress = 10f;
		}

		public override EventContainer Clone()
		{
			LevelProgressEvent levelProgressEvent = new LevelProgressEvent();
			levelProgressEvent.progress = progress;
			return levelProgressEvent;
		}

		public override void Load(string[] stringData)
		{
			progress = float.Parse(stringData[EntityEvent.LoadOffset]);
			int num = EntityEvent.LoadOffset + 1;
			if (num < stringData.Length)
			{
				string s = stringData[num];
				int result;
				if (int.TryParse(s, out result))
				{
					entityEvent.team = (MPTeam)result;
					return;
				}
				if (BesiegeLogFilter.logWarn)
				{
					Debug.LogWarning("Team was not in the correct format!");
				}
				entityEvent.team = MPTeam.None;
			}
			else if (BesiegeLogFilter.logWarn)
			{
				Debug.LogWarning("Level progress event missing team reference!");
			}
		}

		public override string Save()
		{
			int team = (int)entityEvent.team;
			return progress + "|" + team;
		}

		public override string SaveLoadValue()
		{
			int loadTeam = (int)entityEvent._loadTeam;
			return progress + "|" + loadTeam;
		}
	}

	public class GameWinEvent : EventContainer
	{
		public enum WinType
		{
			Health = 0,
			Progress = 1,
			Variable = 2
		}

		private static Dictionary<string, WinType> winTypeLookup = new Dictionary<string, WinType>
		{
			{
				"Health",
				WinType.Health
			},
			{
				"Progress",
				WinType.Progress
			},
			{
				"Variable",
				WinType.Variable
			}
		};

		public WinType winType;

		public string varName;

		public GameWinEvent()
		{
			winType = WinType.Progress;
			varName = LocalisationManager.GetTranslation(2938);
		}

		public override EventContainer Clone()
		{
			GameWinEvent gameWinEvent = new GameWinEvent();
			gameWinEvent.winType = winType;
			gameWinEvent.varName = varName;
			return gameWinEvent;
		}

		public override void Load(string[] stringData)
		{
			if (!winTypeLookup.TryGetValue(stringData[EntityEvent.LoadOffset], out winType))
			{
				Debug.LogError("Couldn't find win type '" + stringData[EntityEvent.LoadOffset] + "'!");
			}
			else if (winType == WinType.Variable)
			{
				varName = stringData[EntityEvent.LoadOffset + 1];
			}
		}

		public override string Save()
		{
			return string.Concat(winType, (winType != WinType.Variable) ? string.Empty : ("|" + varName));
		}
	}

	public class WaitEvent : EventContainer
	{
		public float waitTime;

		public bool displayCountDown;

		public int icon;

		protected float currentTime;

		public WaitEvent()
		{
			waitTime = 1f;
			displayCountDown = false;
			icon = 0;
		}

		public override void Reset()
		{
			currentTime = 0f;
			isDone = false;
		}

		public override bool IsProgressEvent()
		{
			return displayCountDown;
		}

		public override void SetProgress(float progress, float timeCorrection)
		{
			currentTime = Mathf.Min(progress * waitTime + timeCorrection, waitTime);
		}

		public override float GetProgress()
		{
			return (!(waitTime > 0f)) ? 0f : (currentTime / waitTime);
		}

		public override EventContainer Clone()
		{
			WaitEvent waitEvent = new WaitEvent();
			waitEvent.waitTime = waitTime;
			waitEvent.displayCountDown = displayCountDown;
			waitEvent.icon = icon;
			return waitEvent;
		}

		public override void Execute()
		{
			if (!isDone)
			{
				if (displayCountDown)
				{
					CustomLevel.Instance.ShowWaitEvent(this);
				}
				UpdateEvent(currentTime);
			}
		}

		public override float UpdateEvent(float delta)
		{
			currentTime += delta;
			isDone = currentTime >= waitTime;
			if (isDone)
			{
				Stop();
			}
			return (!isDone) ? 0f : (currentTime - waitTime);
		}

		public override void Stop()
		{
			base.Stop();
			if (displayCountDown)
			{
				CustomLevel.Instance.HideWaitEvent(this);
			}
		}

		public override void Load(string[] stringData)
		{
			waitTime = float.Parse(stringData[EntityEvent.LoadOffset]);
			displayCountDown = EntityEvent.LoadOffset + 1 < stringData.Length && int.Parse(stringData[EntityEvent.LoadOffset + 1]) == 1;
			icon = ((EntityEvent.LoadOffset + 2 < stringData.Length) ? int.Parse(stringData[EntityEvent.LoadOffset + 2]) : 0);
		}

		public override string Save()
		{
			return waitTime.ToString() + "|" + (displayCountDown ? 1 : 0).ToString() + "|" + icon;
		}
	}

	public class RepeatEvent : WaitEvent
	{
		public float repeatCount;

		private int currentCount;

		public bool keepRepeating = true;

		public EntityLogic logic;

		public RepeatEvent()
		{
			repeatCount = float.PositiveInfinity;
			ResetCurrentCount();
		}

		public override void Reset()
		{
		}

		public void ResetCurrentCount()
		{
			currentCount = 0;
			keepRepeating = true;
		}

		public override EventContainer Clone()
		{
			RepeatEvent repeatEvent = new RepeatEvent();
			repeatEvent.repeatCount = repeatCount;
			return repeatEvent;
		}

		public override void Execute()
		{
			if (keepRepeating)
			{
				currentCount++;
			}
			isDone = true;
			UpdateEvent(0f);
			if ((float)currentCount >= repeatCount)
			{
				currentCount = 0;
				keepRepeating = false;
			}
			else if (!keepRepeating)
			{
				keepRepeating = true;
			}
		}

		public override float UpdateEvent(float delta)
		{
			return delta;
		}

		public override void Load(string[] stringData)
		{
			if (!float.TryParse(stringData[EntityEvent.LoadOffset], out repeatCount))
			{
				Debug.LogWarning("Couldn't load variable value!");
			}
			if (repeatCount < 1f)
			{
				repeatCount = float.PositiveInfinity;
			}
		}

		public override string Save()
		{
			return repeatCount.ToString();
		}
	}

	public class PickContainer : EventContainer
	{
		public List<TriggerTarget> pickTargets = new List<TriggerTarget>();

		protected int loadOffset;

		public override EventContainer Clone()
		{
			return new PickContainer();
		}

		public override void Load(string[] stringData)
		{
			entityEvent.entityList.Clear();
			for (int i = EntityEvent.LoadOffset + loadOffset; i < stringData.Length; i++)
			{
				long result;
				if (long.TryParse(stringData[i], out result))
				{
					entityEvent.entityList.Add(result);
				}
			}
		}

		public override string Save()
		{
			string text = string.Empty;
			for (int i = 0; i < entityEvent.entityList.Count; i++)
			{
				text += entityEvent.entityList[i];
				if (i < entityEvent.entityList.Count - 1)
				{
					text += "|";
				}
			}
			return text;
		}

		public override string SaveLoadValue()
		{
			string text = string.Empty;
			for (int i = 0; i < entityEvent._loadEntityList.Count; i++)
			{
				text += entityEvent._loadEntityList[i];
				if (i < entityEvent._loadEntityList.Count - 1)
				{
					text += "|";
				}
			}
			return text;
		}
	}

	public class EntityResetEvent : PickContainer
	{
	}

	public class EntityActivateEvent : PickContainer
	{
	}

	public class EntityDeactivateEvent : PickContainer
	{
	}

	public class RespawnMachineEvent : PickContainer
	{
	}

	public class SetRespawnEvent : PickContainer
	{
		public long zoneTarget;

		public SetRespawnEvent()
		{
			zoneTarget = LevelPrefab.UNASSIGNED_ID;
		}

		public override EventContainer Clone()
		{
			SetRespawnEvent setRespawnEvent = new SetRespawnEvent();
			setRespawnEvent.zoneTarget = zoneTarget;
			return setRespawnEvent;
		}

		public override void Load(string[] stringData)
		{
			if (!long.TryParse(stringData[EntityEvent.LoadOffset], out zoneTarget))
			{
				Debug.LogWarning("Couldn't load variable spawn zone entity ID!");
			}
			loadOffset = 1;
			base.Load(stringData);
		}

		public override string Save()
		{
			return zoneTarget + "|" + base.Save();
		}

		public override string SaveLoadValue()
		{
			return zoneTarget + "|" + base.SaveLoadValue();
		}
	}

	public enum VarModifyType
	{
		Add = 0,
		Subtract = 1,
		Set = 2
	}

	public class VariableEvent : PickContainer
	{
		public float val;

		public string key;

		public VarModifyType modifyType;

		public VariableEvent()
		{
			key = LocalisationManager.GetTranslation(2938);
			modifyType = VarModifyType.Set;
			val = 1f;
		}

		public override EventContainer Clone()
		{
			VariableEvent variableEvent = new VariableEvent();
			variableEvent.val = val;
			variableEvent.key = key;
			variableEvent.modifyType = modifyType;
			return variableEvent;
		}

		public override void Load(string[] stringData)
		{
			key = stringData[EntityEvent.LoadOffset];
			int result;
			if (!int.TryParse(stringData[EntityEvent.LoadOffset + 1], out result))
			{
				Debug.LogWarning("Couldn't load variable modify type!");
			}
			modifyType = (VarModifyType)result;
			if (!float.TryParse(stringData[EntityEvent.LoadOffset + 2], out val))
			{
				Debug.LogWarning("Couldn't load variable value!");
			}
			loadOffset = 3;
			base.Load(stringData);
		}

		public override string Save()
		{
			int num = (int)modifyType;
			return key + "|" + num + "|" + val + "|" + base.Save();
		}

		public override string SaveLoadValue()
		{
			int num = (int)modifyType;
			return key + "|" + num + "|" + val + "|" + base.SaveLoadValue();
		}
	}

	public class RandomEvent : PickContainer
	{
		public int min;

		public int max;

		public string key;

		public VarModifyType modifyType;

		public int randomVal;

		public RandomEvent()
		{
			key = LocalisationManager.GetTranslation(2938);
			modifyType = VarModifyType.Set;
			min = 0;
			max = 1;
		}

		public override EventContainer Clone()
		{
			RandomEvent randomEvent = new RandomEvent();
			randomEvent.min = min;
			randomEvent.max = max;
			randomEvent.key = key;
			randomEvent.modifyType = modifyType;
			return randomEvent;
		}

		public override void Load(string[] stringData)
		{
			key = stringData[EntityEvent.LoadOffset];
			int result;
			if (!int.TryParse(stringData[EntityEvent.LoadOffset + 1], out result))
			{
				Debug.LogWarning("Couldn't load variable modify type!");
			}
			modifyType = (VarModifyType)result;
			if (stringData.Length <= EntityEvent.LoadOffset + 3)
			{
				Debug.LogError("No min/max random values specified!");
				return;
			}
			if (!int.TryParse(stringData[EntityEvent.LoadOffset + 2], out min))
			{
				Debug.LogWarning("Couldn't load random min value!");
			}
			if (!int.TryParse(stringData[EntityEvent.LoadOffset + 3], out max))
			{
				Debug.LogWarning("Couldn't load random max value!");
			}
			loadOffset = 4;
			base.Load(stringData);
		}

		public override string Save()
		{
			int num = (int)modifyType;
			return key + "|" + num + "|" + min + "|" + max + "|" + base.Save();
		}

		public override string SaveLoadValue()
		{
			int num = (int)modifyType;
			return key + "|" + num + "|" + min + "|" + max + "|" + base.SaveLoadValue();
		}
	}

	public class ReloadMachineEvent : PickContainer
	{
		public float reloadValue;

		public ReloadAmmoType ammoType;

		public ReloadAmmoType lastAmmoType;

		public bool setAmmo;

		public bool eachBlock;

		public ReloadMachineEvent()
		{
			reloadValue = 10f;
			ammoType = ReloadAmmoType.All;
			setAmmo = false;
			eachBlock = false;
		}

		public override int EventDataSize()
		{
			return (ammoType == ReloadAmmoType.Random) ? 1 : 0;
		}

		public override EventContainer Clone()
		{
			ReloadMachineEvent reloadMachineEvent = new ReloadMachineEvent();
			reloadMachineEvent.reloadValue = reloadValue;
			reloadMachineEvent.ammoType = ammoType;
			reloadMachineEvent.setAmmo = setAmmo;
			reloadMachineEvent.eachBlock = eachBlock;
			return reloadMachineEvent;
		}

		public override void EncodeEventData(byte[] data, int offset)
		{
			data[offset] = (byte)lastAmmoType;
		}

		public override void DecodeEventData(byte[] data, int offset)
		{
			lastAmmoType = (ReloadAmmoType)data[offset];
		}

		public override void Load(string[] stringData)
		{
			if (!float.TryParse(stringData[EntityEvent.LoadOffset], out reloadValue))
			{
				Debug.LogWarning("Couldn't load reload multiplier!");
			}
			ammoType = ReloadAmmoType.All;
			loadOffset = 1;
			if (EntityEvent.LoadOffset + 1 < stringData.Length)
			{
				int result = 0;
				if (int.TryParse(stringData[EntityEvent.LoadOffset + 1], out result))
				{
					ammoType = (ReloadAmmoType)result;
					setAmmo = EntityEvent.LoadOffset + 2 < stringData.Length && int.Parse(stringData[EntityEvent.LoadOffset + 2]) == 1;
					eachBlock = EntityEvent.LoadOffset + 3 < stringData.Length && int.Parse(stringData[EntityEvent.LoadOffset + 3]) == 1;
					loadOffset = 4;
				}
			}
			base.Load(stringData);
		}

		public override string Save()
		{
			return reloadValue + "|" + (int)ammoType + "|" + (setAmmo ? 1 : 0) + "|" + (eachBlock ? 1 : 0) + "|" + base.Save();
		}

		public override string SaveLoadValue()
		{
			return reloadValue + "|" + (int)ammoType + "|" + (setAmmo ? 1 : 0) + "|" + (eachBlock ? 1 : 0) + "|" + base.SaveLoadValue();
		}
	}

	public class ValueEvent : EventContainer
	{
		public float value;

		public override EventContainer Clone()
		{
			ValueEvent valueEvent = new ValueEvent();
			valueEvent.value = value;
			return valueEvent;
		}

		public override void Execute()
		{
		}

		public override void Load(string[] stringData)
		{
			value = float.Parse(stringData[EntityEvent.LoadOffset]);
		}

		public override string Save()
		{
			return value.ToString();
		}
	}

	public class EntityBehaviourEvent : ValueEvent
	{
		public float activationDistance;

		public float speed;

		public bool attack;

		public EntityBehaviourEvent()
		{
			activationDistance = 20f;
			speed = 5f;
			attack = false;
		}

		public override EventContainer Clone()
		{
			EntityBehaviourEvent entityBehaviourEvent = new EntityBehaviourEvent();
			entityBehaviourEvent.activationDistance = activationDistance;
			entityBehaviourEvent.speed = speed;
			entityBehaviourEvent.attack = attack;
			return entityBehaviourEvent;
		}

		public override void Execute()
		{
		}

		public override void Load(string[] stringData)
		{
			float result;
			if (float.TryParse(stringData[EntityEvent.LoadOffset], out result))
			{
				activationDistance = result;
			}
			if (float.TryParse(stringData[EntityEvent.LoadOffset + 1], out result))
			{
				speed = result;
			}
			int num = EntityEvent.LoadOffset + 2;
			if (num < stringData.Length)
			{
				bool result2;
				if (bool.TryParse(stringData[num], out result2))
				{
					attack = result2;
				}
			}
			else if (BesiegeLogFilter.logWarn)
			{
				Debug.LogWarning("stringData is out of bounds for attack parameter!");
			}
		}

		public override string Save()
		{
			string empty = string.Empty;
			string text = empty;
			return text + activationDistance + "|" + speed + "|" + attack;
		}

		public override string SaveLoadValue()
		{
			string empty = string.Empty;
			string text = empty;
			return text + activationDistance + "|" + speed + "|" + attack;
		}
	}

	public class TransformEvent : PickContainer
	{
		public enum TransformRotationType
		{
			AroundWorldAxis = 0,
			AroundLocalAxis = 1,
			SetRotation = 2
		}

		public enum TransformPositionType
		{
			WorldPosition = 0,
			WorldDirection = 1,
			LocalDirection = 2
		}

		public enum TransformType
		{
			Instant = 0,
			Lerp = 1,
			Force = 2
		}

		public struct fromTransform
		{
			public Transform transform;

			public Vector3 fromPosition;

			public Quaternion fromRotation;

			public Vector3 toPosition;

			public Quaternion toRotation;

			public Vector3 currentPosition;

			public Quaternion currentRotation;

			public Vector3 forceDir;

			public float power;

			public Vector3 torqueDir;

			public float rPower;

			public float toPositionMag;

			public Vector3 toPositionNorm;

			public LevelEntity entity;

			public GenericEntity entityBehaviour;

			public bool isDone;

			public bool physActive;

			public bool setupDone;

			private bool wasKinematic;

			private bool[] childrenKinematic;

			private bool[] childrenInterpolated;

			private bool usedInterpolation;

			public float oldMaxAngularVelocity;

			public void Setup(LevelEntity e, TransformEvent transformEvent, bool overrideFrom)
			{
				entity = e;
				entityBehaviour = e.EntityBehaviour;
				if (entity == null || entity.transform == null)
				{
					isDone = true;
					if (entity == null)
					{
						Debug.LogError("TransformEvent.Setup(): LevelEntity is null");
					}
					else
					{
						Debug.LogError("TransformEvent.Setup(): entity did not have a transform");
					}
					return;
				}
				transform = entity.transform;
				if (overrideFrom)
				{
					currentPosition = (fromPosition = ((!entityBehaviour.useAccurateTransform) ? transform.position : entityBehaviour.accuratePosition));
					currentRotation = (fromRotation = ((!entityBehaviour.useAccurateTransform) ? transform.rotation : entityBehaviour.accurateRotation));
				}
				else
				{
					currentPosition = transform.position;
					currentRotation = transform.rotation;
				}
				physActive = entityBehaviour.PhysicsEnabled;
				power = 50f;
				rPower = transformEvent.eulerAngles.magnitude;
				isDone = false;
				toPosition = transformEvent.position;
				toRotation = transformEvent.CreateRotation(ref this, 1f);
				e.SetLerping(false);
				childrenKinematic = new bool[entityBehaviour.childBodies.Length];
				childrenInterpolated = new bool[entityBehaviour.childBodies.Length];
				for (int i = 0; i < childrenKinematic.Length; i++)
				{
					if (!(entityBehaviour.childBodies[i] == null))
					{
						childrenKinematic[i] = entityBehaviour.childBodies[i].isKinematic;
						childrenInterpolated[i] = entityBehaviour.childBodies[i].interpolation == RigidbodyInterpolation.Interpolate;
					}
				}
				if (transformEvent.transformType == TransformType.Lerp)
				{
					wasKinematic = true;
					if (!entityBehaviour.noRigidbody)
					{
						wasKinematic = entityBehaviour.Rigidbody.isKinematic;
						entityBehaviour.Rigidbody.isKinematic = true;
						entityBehaviour.isKinematic = true;
						usedInterpolation = entityBehaviour.Rigidbody.interpolation == RigidbodyInterpolation.Interpolate;
						for (int j = 1; j < entityBehaviour.childBodies.Length; j++)
						{
							if (!(entityBehaviour.childBodies[j] == null))
							{
								entityBehaviour.childBodies[j].interpolation = RigidbodyInterpolation.None;
								entityBehaviour.childBodies[j].isKinematic = true;
							}
						}
					}
					else
					{
						for (int k = 0; k < entityBehaviour.childBodies.Length; k++)
						{
							if (!(entityBehaviour.childBodies[k] == null))
							{
								entityBehaviour.childBodies[k].interpolation = RigidbodyInterpolation.None;
								entityBehaviour.childBodies[k].isKinematic = true;
							}
						}
					}
					e.SetLerping(true);
				}
				else if (transformEvent.transformType == TransformType.Force)
				{
					toPositionMag = toPosition.magnitude;
					toPositionNorm = toPosition.normalized;
					if (!entityBehaviour.noRigidbody)
					{
						transform = entityBehaviour.Rigidbody.transform;
						oldMaxAngularVelocity = entityBehaviour.Rigidbody.maxAngularVelocity;
						entityBehaviour.Rigidbody.maxAngularVelocity = float.PositiveInfinity;
					}
					power *= (toPosition - fromPosition).magnitude;
				}
				else if (transformEvent.transformType == TransformType.Instant)
				{
					wasKinematic = true;
					if (!entityBehaviour.noRigidbody)
					{
						wasKinematic = entityBehaviour.Rigidbody.isKinematic;
						entityBehaviour.Rigidbody.isKinematic = true;
						entityBehaviour.isKinematic = true;
						usedInterpolation = entityBehaviour.Rigidbody.interpolation == RigidbodyInterpolation.Interpolate;
						entityBehaviour.Rigidbody.interpolation = RigidbodyInterpolation.None;
					}
				}
				setupDone = true;
			}

			public void Complete(TransformType transformType)
			{
				LevelEntity levelEntity = entityBehaviour.entity;
				int num = 0;
				if (!entityBehaviour.noRigidbody)
				{
					if (transformType != TransformType.Force)
					{
						if (!wasKinematic)
						{
							entityBehaviour.Rigidbody.isKinematic = false;
							entityBehaviour.Rigidbody.velocity = Vector3.zero;
							entityBehaviour.isKinematic = false;
							levelEntity.changedPos = (levelEntity.changedRot = false);
						}
						entityBehaviour.Rigidbody.interpolation = (usedInterpolation ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None);
						entityBehaviour.accuratePosition = currentPosition;
						entityBehaviour.accurateRotation = toRotation;
						if (entityBehaviour.gameObject.activeInHierarchy && !entityBehaviour.useAccurateTransform)
						{
							entityBehaviour.StartCoroutine(entityBehaviour.AfterTransformEvent());
						}
						entityBehaviour.useAccurateTransform = true;
					}
					num = 1;
				}
				for (int i = num; i < entityBehaviour.childBodies.Length; i++)
				{
					if (!(entityBehaviour.childBodies[i] == null))
					{
						entityBehaviour.childBodies[i].interpolation = (childrenInterpolated[i] ? RigidbodyInterpolation.Interpolate : RigidbodyInterpolation.None);
						entityBehaviour.childBodies[i].isKinematic = childrenKinematic[i];
					}
				}
				entityBehaviour.currentTransformEvent = null;
				levelEntity.SetLerping(false);
				isDone = true;
			}

			public void ChangeLevelEntity(LevelEntity e)
			{
				entity = e;
				entityBehaviour = e.EntityBehaviour;
				transform = entity.transform;
				isDone = true;
			}
		}

		public delegate void Transformer(ref fromTransform info, float delta);

		private static Dictionary<string, TransformRotationType> rotationTypeLookup = new Dictionary<string, TransformRotationType>
		{
			{
				"AroundWorldAxis",
				TransformRotationType.AroundWorldAxis
			},
			{
				"AroundLocalAxis",
				TransformRotationType.AroundLocalAxis
			},
			{
				"SetRotation",
				TransformRotationType.SetRotation
			}
		};

		private static Dictionary<string, TransformPositionType> positionTypeLookup = new Dictionary<string, TransformPositionType>
		{
			{
				"WorldDirection",
				TransformPositionType.WorldDirection
			},
			{
				"LocalDirection",
				TransformPositionType.LocalDirection
			},
			{
				"WorldPosition",
				TransformPositionType.WorldPosition
			}
		};

		public Vector3 position;

		public Quaternion rotation;

		public Vector3 eulerAngles;

		public float lerpTime = 1f;

		public TransformRotationType rotationType;

		public TransformPositionType positionType = TransformPositionType.WorldDirection;

		public bool ignorePosition;

		public bool ignoreRotation;

		private float currentTime;

		public Transformer transformer;

		private static Dictionary<string, TransformType> transformTypeLookup = new Dictionary<string, TransformType>
		{
			{
				"Instant",
				TransformType.Instant
			},
			{
				"Lerp",
				TransformType.Lerp
			},
			{
				"Force",
				TransformType.Force
			}
		};

		public TransformType transformType;

		public fromTransform[] entityList;

		private bool isInitialized;

		private float positionScale;

		private float progress;

		private float excess;

		private Quaternion tempRotation = Quaternion.identity;

		public TransformEvent()
		{
			entityList = new fromTransform[0];
			transformType = TransformType.Instant;
			rotationType = TransformRotationType.AroundWorldAxis;
			positionType = TransformPositionType.WorldDirection;
			lerpTime = 1f;
			eulerAngles = Vector3.zero;
			useFixedUpdate = true;
		}

		public override void Reset()
		{
			currentTime = 0f;
			isDone = false;
		}

		public override EventContainer Clone()
		{
			TransformEvent transformEvent = new TransformEvent();
			transformEvent.transformType = transformType;
			transformEvent.rotationType = rotationType;
			transformEvent.positionType = positionType;
			transformEvent.position = position;
			transformEvent.rotation = rotation;
			transformEvent.lerpTime = lerpTime;
			transformEvent.eulerAngles = eulerAngles;
			return transformEvent;
		}

		public override void Execute()
		{
			if (lerpTime <= 0f)
			{
				lerpTime = 0.01f;
			}
			if (transformType != TransformType.Instant)
			{
				positionScale = 1f / lerpTime;
				return;
			}
			positionScale = 1f;
			UpdateEvent(0f);
		}

		public override void Stop()
		{
			base.Stop();
			for (int i = 0; i < entityList.Length; i++)
			{
				fromTransform fromTransform2 = entityList[i];
				if (!(fromTransform2.entity == null) && !(fromTransform2.entityBehaviour == null) && fromTransform2.entityBehaviour.currentTransformEvent != null && !fromTransform2.isDone)
				{
					fromTransform2.Complete(transformType);
				}
			}
		}

		public override float UpdateEvent(float delta)
		{
			if (!isInitialized)
			{
				isDone = true;
				return delta;
			}
			float num = ((!(currentTime <= lerpTime)) ? 0f : (lerpTime - currentTime));
			excess = ((!(delta > num)) ? 0f : (delta - num));
			delta = ((!(delta > num)) ? delta : num);
			currentTime += delta;
			progress = currentTime / lerpTime;
			for (int i = 0; i < entityList.Length; i++)
			{
				fromTransform info = entityList[i];
				if (info.isDone || !info.setupDone)
				{
					continue;
				}
				if (!ValidateTransformEvent(info))
				{
					entityList[i].isDone = true;
					continue;
				}
				transformer(ref info, delta);
				if (info.entityBehaviour.updateOnTransformEvent)
				{
					info.entityBehaviour.UpdateOnTransformEvent();
				}
				entityList[i] = info;
			}
			if (transformType != TransformType.Instant)
			{
				isDone = currentTime >= lerpTime;
			}
			else
			{
				isDone = true;
			}
			return excess;
		}

		public override void Load(string[] stringData)
		{
			position = Vector3Parse(stringData[EntityEvent.LoadOffset]);
			eulerAngles = Vector3Parse(stringData[EntityEvent.LoadOffset + 1]);
			if (!transformTypeLookup.TryGetValue(stringData[EntityEvent.LoadOffset + 2], out transformType))
			{
				Debug.LogError("Couldn't find transform type '" + stringData[EntityEvent.LoadOffset + 2] + "'!");
			}
			if (!positionTypeLookup.TryGetValue(stringData[EntityEvent.LoadOffset + 3], out positionType))
			{
				Debug.LogError("Couldn't find TransformPositionTypeLookup type '" + stringData[EntityEvent.LoadOffset + 3] + "'!");
			}
			float result;
			if (float.TryParse(stringData[EntityEvent.LoadOffset + 4], out result))
			{
				lerpTime = result;
			}
			if (!rotationTypeLookup.TryGetValue(stringData[EntityEvent.LoadOffset + 5], out rotationType))
			{
				Debug.LogError("Couldn't find TransformRotationType type '" + stringData[EntityEvent.LoadOffset + 5] + "'!");
			}
			loadOffset = 6;
			base.Load(stringData);
		}

		public override bool IsProgressEvent()
		{
			return transformType == TransformType.Lerp || transformType == TransformType.Instant;
		}

		public override float GetProgress()
		{
			return (!(lerpTime > 0f)) ? 0f : (currentTime / lerpTime);
		}

		public override void SetProgress(float p, float timeCorrection)
		{
			if (StatMaster.isHosting || StatMaster.isLocalSim)
			{
				return;
			}
			currentTime = Mathf.Min(p * lerpTime + timeCorrection, lerpTime);
			for (int i = 0; i < entityList.Length; i++)
			{
				if (transformType == TransformType.Lerp)
				{
					fromTransform fromTransform2 = entityList[i];
					if (!ignorePosition)
					{
						Vector3 currentPosition = new Vector3(fromTransform2.fromPosition.x, fromTransform2.fromPosition.y, fromTransform2.fromPosition.z);
						entityList[i].currentPosition = currentPosition;
						if (fromTransform2.entity.hasLerpTarget)
						{
							fromTransform2.entity.lerpTransformTarget.position = currentPosition;
						}
						else if (fromTransform2.entityBehaviour.noRigidbody)
						{
							fromTransform2.transform.position = currentPosition;
						}
						else
						{
							fromTransform2.entityBehaviour.Rigidbody.MovePosition(currentPosition);
						}
					}
					if (!ignoreRotation)
					{
						Quaternion quaternion = new Quaternion(fromTransform2.fromRotation.x, fromTransform2.fromRotation.y, fromTransform2.fromRotation.z, fromTransform2.fromRotation.w);
						entityList[i].currentRotation = quaternion;
						if (fromTransform2.entity.hasLerpTarget)
						{
							fromTransform2.entity.lerpTransformTarget.rotation = quaternion;
						}
						else if (fromTransform2.entityBehaviour.noRigidbody)
						{
							fromTransform2.transform.rotation = quaternion;
						}
						else
						{
							fromTransform2.entityBehaviour.Rigidbody.MoveRotation(quaternion);
						}
					}
					if (currentTime > 0f)
					{
						float num = 0f;
						float num2 = 0.1f;
						if (!ignorePosition && !ignoreRotation)
						{
							while (num + num2 < currentTime)
							{
								num += num2;
								progress = num / lerpTime;
								AdjustPosition(ref entityList[i], num2);
								entityList[i].currentRotation = CreateRotation(ref entityList[i], progress);
							}
						}
						progress = currentTime / lerpTime;
						bool activeInHierarchy = fromTransform2.entityBehaviour.gameObject.activeInHierarchy;
						if (!ignorePosition)
						{
							Vector3 vector = (entityList[i].currentPosition = AdjustPosition(ref entityList[i], currentTime - num));
							if (fromTransform2.entity.hasLerpTarget)
							{
								fromTransform2.entity.lerpTransformTarget.position = vector;
							}
							else if (fromTransform2.entityBehaviour.noRigidbody || !activeInHierarchy)
							{
								fromTransform2.transform.position = vector;
							}
							else
							{
								fromTransform2.entityBehaviour.Rigidbody.MovePosition(vector);
							}
						}
						if (!ignoreRotation)
						{
							Quaternion rot = (entityList[i].currentRotation = CreateRotation(ref entityList[i], progress));
							if (fromTransform2.entity.hasLerpTarget)
							{
								fromTransform2.entity.lerpTransformTarget.rotation = rot;
							}
							else if (fromTransform2.entityBehaviour.noRigidbody || !activeInHierarchy)
							{
								fromTransform2.transform.rotation = rot;
							}
							else
							{
								fromTransform2.entityBehaviour.Rigidbody.MoveRotation(rot);
							}
						}
					}
					else if (fromTransform2.entity.hasLerpTarget)
					{
						Transform lerpTransformTarget = fromTransform2.entity.lerpTransformTarget;
						lerpTransformTarget.position = ((!ignorePosition) ? fromTransform2.fromPosition : fromTransform2.currentPosition);
						lerpTransformTarget.rotation = ((!ignoreRotation) ? fromTransform2.fromRotation : fromTransform2.currentRotation);
					}
					LevelEntity entity = fromTransform2.entity;
					if (entity.hasLerpTarget)
					{
						entity.SetData(!ignorePosition, entityList[i].currentPosition, !ignoreRotation, entityList[i].currentRotation);
					}
				}
				else if (transformType == TransformType.Instant)
				{
					UpdateEvent(0f);
					isDone = true;
				}
				else
				{
					Debug.LogError("Unsupported progress type: " + transformType);
				}
			}
		}

		public override int EventDataSize()
		{
			return 1 + (((!ignorePosition) ? 6 : 0) + ((!ignoreRotation) ? 7 : 0)) * entityList.Length;
		}

		public override void EncodeEventData(byte[] data, int offset)
		{
			data[offset] = (byte)entityList.Length;
			offset++;
			for (int i = 0; i < entityList.Length; i++)
			{
				fromTransform fromTransform2 = entityList[i];
				if (!ignorePosition)
				{
					NetworkCompression.CompressPosition(fromTransform2.fromPosition, data, offset);
					offset += 6;
				}
				if (!ignoreRotation)
				{
					NetworkCompression.CompressRotation(fromTransform2.fromRotation, data, offset);
					offset += 7;
				}
			}
		}

		public override void DecodeEventData(byte[] data, int offset)
		{
			int num = data[offset];
			offset++;
			if (num != entityList.Length)
			{
				Debug.LogWarning("Transform entityList is the wrong size!");
			}
			for (int i = 0; i < num; i++)
			{
				Vector3 vec = default(Vector3);
				Quaternion rot = default(Quaternion);
				if (!ignorePosition)
				{
					NetworkCompression.DecompressPosition(data, offset, out vec);
					offset += 6;
				}
				if (!ignoreRotation)
				{
					NetworkCompression.DecompressRotation(data, offset, out rot);
					offset += 7;
				}
				if (i < entityList.Length)
				{
					if (!ignorePosition)
					{
						entityList[i].fromPosition = vec;
					}
					if (!ignoreRotation)
					{
						entityList[i].fromRotation = rot;
					}
				}
				else
				{
					Debug.LogWarning("Entity out of bounds when decoding Transform event!");
				}
			}
		}

		public override string Save()
		{
			string empty = string.Empty;
			string text = empty;
			return string.Concat(text, LevelEditor.SaveVector3(position), "|", LevelEditor.SaveVector3(eulerAngles), "|", transformType, "|", positionType, "|", lerpTime, "|", rotationType, "|", base.Save());
		}

		public override string SaveLoadValue()
		{
			string empty = string.Empty;
			string text = empty;
			return string.Concat(text, LevelEditor.SaveVector3(position), "|", LevelEditor.SaveVector3(eulerAngles), "|", transformType, "|", positionType, "|", lerpTime, "|", rotationType, "|", base.SaveLoadValue());
		}

		private bool ValidateTransformEvent(fromTransform current)
		{
			if (current.entity.isDestroyed)
			{
				return false;
			}
			TransformEvent currentTransformEvent = current.entityBehaviour.currentTransformEvent;
			if (currentTransformEvent == null)
			{
				currentTransformEvent = (current.entityBehaviour.currentTransformEvent = this);
			}
			else if (currentTransformEvent != this)
			{
				if (((currentTransformEvent.ignorePosition == ignorePosition && ignorePosition) || currentTransformEvent.ignorePosition != ignorePosition) && ((currentTransformEvent.ignoreRotation == ignoreRotation && ignoreRotation) || currentTransformEvent.ignoreRotation != ignoreRotation))
				{
					return true;
				}
				if (StatMaster.Mode.levelEdit)
				{
					string arg = ((currentTransformEvent.ignorePosition != ignorePosition || ignorePosition) ? LocalisationManager.GetTranslation(3333) : LocalisationManager.GetTranslation(3332));
					string text = string.Format(LocalisationManager.GetTranslation(3274), arg, current.entityBehaviour.name);
					SingleInstanceFindOnly<GenericUIPopup>.Instance.Show(text, 3f);
				}
				return false;
			}
			return true;
		}

		public void InstantTransform(ref fromTransform info, float delta)
		{
			if (!ignorePosition)
			{
				info.transform.position = (info.toPosition = AdjustPosition(ref info, 1f));
				info.entity.changedPos = true;
			}
			if (!ignoreRotation)
			{
				info.transform.rotation = info.toRotation;
				info.entity.changedRot = true;
			}
			info.Complete(transformType);
		}

		public void LerpTransform(ref fromTransform info, float delta)
		{
			bool activeInHierarchy = info.entityBehaviour.gameObject.activeInHierarchy;
			if (!ignorePosition)
			{
				if (info.entity.hasLerpTarget)
				{
					info.entity.lerpTransformTarget.position = AdjustPosition(ref info, delta);
				}
				else
				{
					if (info.entityBehaviour.noRigidbody || !activeInHierarchy)
					{
						info.transform.position = AdjustPosition(ref info, delta);
					}
					else
					{
						info.entityBehaviour.Rigidbody.MovePosition(AdjustPosition(ref info, delta));
					}
					info.entity.changedPos = true;
				}
			}
			if (!ignoreRotation)
			{
				info.currentRotation = CreateRotation(ref info, progress);
				if (info.entity.hasLerpTarget)
				{
					info.entity.lerpTransformTarget.rotation = info.currentRotation;
				}
				else
				{
					if (info.entityBehaviour.noRigidbody || !activeInHierarchy)
					{
						info.transform.rotation = info.currentRotation;
					}
					else
					{
						info.entityBehaviour.Rigidbody.MoveRotation(info.currentRotation);
					}
					info.entity.changedRot = true;
				}
			}
			if (currentTime >= lerpTime)
			{
				info.Complete(transformType);
			}
		}

		public void ForceTransform(ref fromTransform info, float delta)
		{
			if (!info.entityBehaviour.SimPhysics)
			{
				return;
			}
			if (!ignorePosition && !info.entityBehaviour.noRigidbody)
			{
				Rigidbody rigidbody = info.entityBehaviour.Rigidbody;
				switch (positionType)
				{
				case TransformPositionType.WorldDirection:
					rigidbody.velocity = position;
					break;
				case TransformPositionType.LocalDirection:
					rigidbody.velocity = info.transform.rotation * position;
					break;
				case TransformPositionType.WorldPosition:
				{
					Vector3 vector = info.transform.position;
					rigidbody.velocity = (info.toPosition - vector).normalized * info.power * (delta / lerpTime);
					if (CompareVectors(vector, info.toPositionMag, info.toPositionNorm, 1f, 0.98f))
					{
						ignorePosition = true;
					}
					break;
				}
				}
			}
			if (!ignoreRotation)
			{
				float angle;
				if (rotationType == TransformRotationType.SetRotation)
				{
					tempRotation = ((!(info.transform.rotation == new Quaternion(0f, 0f, 0f, -1f))) ? info.transform.rotation : Quaternion.identity);
					(info.toRotation * Quaternion.Inverse(tempRotation)).ToAngleAxis(out angle, out info.torqueDir);
				}
				else
				{
					info.toRotation.ToAngleAxis(out angle, out info.torqueDir);
				}
				if (!info.entityBehaviour.noRigidbody)
				{
					info.entityBehaviour.Rigidbody.angularVelocity = info.torqueDir * info.rPower * delta;
					if (rotationType == TransformRotationType.SetRotation && CompareQuaternion(info.transform.rotation, info.toRotation, 0.9999f))
					{
						ignoreRotation = true;
						info.entityBehaviour.Rigidbody.angularVelocity = Vector3.zero;
					}
				}
			}
			if ((ignorePosition && ignoreRotation) || currentTime >= lerpTime)
			{
				info.Complete(transformType);
				if (!info.entityBehaviour.noRigidbody)
				{
					info.entityBehaviour.Rigidbody.maxAngularVelocity = info.oldMaxAngularVelocity;
				}
			}
		}

		private Vector3 AdjustPosition(ref fromTransform info, float delta)
		{
			switch (positionType)
			{
			case TransformPositionType.WorldDirection:
				info.currentPosition += position * positionScale * delta;
				break;
			case TransformPositionType.LocalDirection:
				info.currentPosition += ((!ignoreRotation) ? info.currentRotation : info.transform.rotation) * position * positionScale * delta;
				break;
			case TransformPositionType.WorldPosition:
				info.currentPosition = ((transformType != TransformType.Instant) ? Vector3.Lerp(info.fromPosition, position, progress) : position);
				break;
			default:
				info.currentPosition = Vector3.zero;
				break;
			}
			return info.currentPosition;
		}

		private Quaternion CreateRotation(ref fromTransform info, float delta)
		{
			Quaternion quaternion;
			if (rotationType == TransformRotationType.SetRotation && transformType != TransformType.Force)
			{
				quaternion = Quaternion.Lerp(info.fromRotation, Quaternion.Euler(eulerAngles), delta);
			}
			else
			{
				Vector3 euler = eulerAngles * Mathf.Clamp01(delta);
				quaternion = Quaternion.Euler(euler);
			}
			switch (rotationType)
			{
			case TransformRotationType.AroundWorldAxis:
				info.toRotation = quaternion * info.fromRotation;
				break;
			case TransformRotationType.AroundLocalAxis:
				info.toRotation = info.fromRotation * quaternion;
				break;
			case TransformRotationType.SetRotation:
				info.toRotation = quaternion;
				break;
			}
			if (transformType == TransformType.Force)
			{
				if (info.toRotation == new Quaternion(0f, 0f, 0f, -1f) && !eulerAngles.Equals(Vector3.zero))
				{
					info.toRotation = Quaternion.Euler(eulerAngles.normalized * 10f);
				}
				if (rotationType != TransformRotationType.SetRotation)
				{
					info.toRotation *= Quaternion.Inverse(info.fromRotation);
				}
			}
			return info.toRotation;
		}

		public void Init(int count)
		{
			isInitialized = true;
			entityList = new fromTransform[count];
			for (int i = 0; i < entityList.Length; i++)
			{
				entityList[i] = default(fromTransform);
				entityList[i].setupDone = false;
			}
			ignorePosition = positionType != TransformPositionType.WorldPosition && position.Equals(Vector3.zero);
			ignoreRotation = rotationType != TransformRotationType.SetRotation && eulerAngles.Equals(Vector3.zero);
			switch (transformType)
			{
			case TransformType.Instant:
				transformer = InstantTransform;
				break;
			case TransformType.Lerp:
				transformer = LerpTransform;
				break;
			case TransformType.Force:
				transformer = ForceTransform;
				break;
			}
		}
	}

	private static Dictionary<string, EventType> typeLookup = new Dictionary<string, EventType>
	{
		{
			"Progress",
			EventType.Progress
		},
		{
			"GameWin",
			EventType.GameWin
		},
		{
			"Wait",
			EventType.Wait
		},
		{
			"Activate",
			EventType.Activate
		},
		{
			"Deactivate",
			EventType.Deactivate
		},
		{
			"Reset",
			EventType.Reset
		},
		{
			"RespawnMachine",
			EventType.RespawnMachine
		},
		{
			"SetRespawn",
			EventType.SetRespawn
		},
		{
			"ReloadMachine",
			EventType.ReloadMachine
		},
		{
			"Transform",
			EventType.Transform
		},
		{
			"Variable",
			EventType.Variable
		},
		{
			"Random",
			EventType.Random
		},
		{
			"Repeat",
			EventType.Repeat
		},
		{
			"Pursue",
			EventType.Pursue
		},
		{
			"FactionCharge",
			EventType.FactionCharge
		},
		{
			"Flee",
			EventType.Flee
		},
		{
			"Strafe",
			EventType.Strafe
		},
		{
			"WalkAround",
			EventType.WalkAround
		},
		{
			"Stationary",
			EventType.Stationary
		},
		{
			"Modded",
			EventType.Modded
		}
	};

	private static EventType[] baseEvents = new EventType[11]
	{
		EventType.Progress,
		EventType.GameWin,
		EventType.Wait,
		EventType.Activate,
		EventType.Deactivate,
		EventType.Reset,
		EventType.Transform,
		EventType.Variable,
		EventType.Random,
		EventType.Repeat,
		EventType.Modded
	};

	private static EventType[] machineEvents = new EventType[3]
	{
		EventType.RespawnMachine,
		EventType.SetRespawn,
		EventType.ReloadMachine
	};

	private static EventType[] behaviourEvents = new EventType[6]
	{
		EventType.Pursue,
		EventType.FactionCharge,
		EventType.Flee,
		EventType.Strafe,
		EventType.WalkAround,
		EventType.Stationary
	};

	public bool isDone;

	public bool useFixedUpdate;

	public EntityEvent entityEvent;

	public static bool IsPickEvent(EventType e)
	{
		return GetDefault(e) is PickContainer;
	}

	public static bool IsBehaviourEvent(EventType e)
	{
		return ContainsEvent(e, behaviourEvents);
	}

	public static bool IsMachineEvent(EventType e)
	{
		return ContainsEvent(e, machineEvents);
	}

	public virtual void SetProgress(float progress, float timeCorrection)
	{
	}

	public virtual float GetProgress()
	{
		return 0f;
	}

	public virtual int EventDataSize()
	{
		return 0;
	}

	public virtual void EncodeEventData(byte[] data, int offset)
	{
	}

	public virtual void DecodeEventData(byte[] data, int offset)
	{
	}

	public virtual bool IsProgressEvent()
	{
		return false;
	}

	public virtual void Stop()
	{
		isDone = true;
	}

	public static bool SanatizeKey(string key, out string result)
	{
		result = key;
		if (result.Contains("|"))
		{
			do
			{
				result = result.Replace("|", string.Empty);
			}
			while (result.Contains("|"));
			if (result.Length == 0)
			{
				result = null;
				return false;
			}
		}
		return true;
	}

	public static List<EventType> GetEvents(TriggerType triggerType)
	{
		List<EventType> list = new List<EventType>();
		if (triggerType == TriggerType.Behaviour)
		{
			list.AddRange(behaviourEvents);
		}
		else
		{
			list.AddRange(baseEvents);
			list.InsertRange(6, machineEvents);
		}
		if (SingleInstanceFindOnly<EventLoader>.Instance.LoadedEvents.Count == 0)
		{
			list.Remove(EventType.Modded);
		}
		return list;
	}

	private static bool ContainsEvent(EventType e, EventType[] eventList)
	{
		for (int i = 0; i < eventList.Length; i++)
		{
			if (eventList[i] == e)
			{
				return true;
			}
		}
		return false;
	}

	public static EventContainer GetDefault(EventType e)
	{
		switch (e)
		{
		case EventType.Progress:
			return new LevelProgressEvent();
		case EventType.Wait:
			return new WaitEvent();
		case EventType.Repeat:
			return new RepeatEvent();
		case EventType.Transform:
			return new TransformEvent();
		case EventType.GameWin:
			return new GameWinEvent();
		case EventType.Reset:
			return new EntityResetEvent();
		case EventType.Activate:
			return new EntityActivateEvent();
		case EventType.Deactivate:
			return new EntityDeactivateEvent();
		case EventType.Variable:
			return new VariableEvent();
		case EventType.Random:
			return new RandomEvent();
		case EventType.RespawnMachine:
			return new RespawnMachineEvent();
		case EventType.SetRespawn:
			return new SetRespawnEvent();
		case EventType.ReloadMachine:
			return new ReloadMachineEvent();
		case EventType.Modded:
			return new ModdedEventContainer();
		case EventType.Pursue:
		case EventType.FactionCharge:
		case EventType.Flee:
		case EventType.Strafe:
		case EventType.WalkAround:
		case EventType.Stationary:
			return new EntityBehaviourEvent();
		default:
			return null;
		}
	}

	public virtual EventContainer Clone()
	{
		return null;
	}

	public virtual void Load(string[] stringData)
	{
	}

	public virtual string Save()
	{
		return string.Empty;
	}

	public virtual string SaveLoadValue()
	{
		return Save();
	}

	public static EventType GetEvent(string evt)
	{
		EventType value;
		if (!typeLookup.TryGetValue(evt, out value))
		{
			Debug.LogError("Unknown event container: " + evt);
			return EventType.Progress;
		}
		return value;
	}

	public virtual void Reset()
	{
	}

	public virtual void Execute()
	{
		isDone = true;
	}

	public virtual float UpdateEvent(float delta)
	{
		return delta;
	}

	private Vector3 Vector3Parse(string s)
	{
		s = s.Trim();
		if (s.StartsWith("(") && s.EndsWith(")"))
		{
			s = s.Substring(1, s.Length - 2);
		}
		string[] array = s.Split(',');
		float result;
		if (!float.TryParse(array[0], out result))
		{
			Debug.LogError("Error parsing X! input='" + s + "' x='" + array[0] + "'");
			result = 0f;
		}
		float result2;
		if (!float.TryParse(array[1], out result2))
		{
			Debug.LogError("Error parsing Y! input='" + s + "' y='" + array[1] + "'");
			result2 = 0f;
		}
		float result3;
		if (!float.TryParse(array[2], out result3))
		{
			Debug.LogError("Error parsing Z! input='" + s + "' z='" + array[2] + "'");
			result3 = 0f;
		}
		return new Vector3(result, result2, result3);
	}

	private bool CompareVectors(Vector3 a, float bMagnitude, Vector3 bNormal, float errorMagin, float angleError)
	{
		float num = a.magnitude - bMagnitude;
		if (num >= errorMagin || num <= 0f - errorMagin)
		{
			return false;
		}
		float num2 = Vector3.Dot(a.normalized, bNormal);
		return num2 >= angleError;
	}

	private bool CompareQuaternion(Quaternion a, Quaternion b, float angleError)
	{
		float num = Quaternion.Dot(a, b);
		return num >= angleError;
	}
}

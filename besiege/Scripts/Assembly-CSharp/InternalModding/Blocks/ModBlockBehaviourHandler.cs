using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Loading;
using Modding;
using Modding.Blocks;
using Modding.Modules;
using Modding.Serialization;
using UnityEngine;
using mattmc3.dotmore.Collections.Generic;

namespace InternalModding.Blocks
{
	public class ModBlockBehaviourHandler : BlockBehaviour, ILimitsDisplay
	{
		public BlockScript blockScript;

		[Obsolete("audioSource is obsolete, use ReferenceMaster.PlayFlip()", false)]
		public AudioSource audioSource;

		private FireController fireController;

		private bool hasBurned;

		private bool doused;

		private bool sentBurntOutMessage;

		private bool started;

		private ModBlockBehaviour[] behaviours;

		internal ModdedBlock moddedBlock;

		private Dictionary<string, MapperType> moduleMapperTypes;

		private PlayerMachine _machine;

		public bool CanFlip;

		internal Transform directionArrow;

		public bool HasBehaviours;

		public override bool CanGlow
		{
			get
			{
				return Prefab.hasBVC && VisualController.canBeHeated;
			}
		}

		public bool IsBurning
		{
			get
			{
				return (bool)fireController && fireController.onFire;
			}
		}

		public bool HasBurnedOut
		{
			get
			{
				return !IsBurning && !doused && hasBurned;
			}
		}

		public PlayerMachine Machine
		{
			get
			{
				if (_machine == null || _machine.InternalObject == null)
				{
					return _machine = PlayerMachine.From(base.ParentMachine);
				}
				return _machine;
			}
		}

		public Transform DirectionArrow
		{
			get
			{
				return directionArrow;
			}
		}

		public override void OnSave(XDataHolder data)
		{
			base.OnSave(data);
			data.Write("flipped", Flipped);
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour modBlockBehaviour in array)
			{
				modBlockBehaviour.OnSave(data);
			}
		}

		public override void OnLoad(XDataHolder data)
		{
			base.OnLoad(data);
			if (data.HasKey("flipped"))
			{
				Flipped = data.ReadBool("flipped");
				PostFlip(false, false);
			}
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour modBlockBehaviour in array)
			{
				modBlockBehaviour.OnLoad(data);
			}
		}

		public void OnModuleReload()
		{
			if (behaviours == null)
			{
				return;
			}
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour modBlockBehaviour in array)
			{
				IModuleBehaviour moduleBehaviour = modBlockBehaviour as IModuleBehaviour;
				if (moduleBehaviour != null)
				{
					moduleBehaviour.OnReload();
				}
			}
		}

		protected override void Awake()
		{
			base.Awake();
			if (!isSimulating)
			{
				hasMyBounds = Prefab.hasMyBounds;
				if (VisualController != null)
				{
					VisualController.meshFiltery = VisualController.MeshFilter;
					VisualController.isSimulating = isSimulating;
					VisualController.GetShortRenderer(out VisualController.shortVisRen);
					VisualController.heating = new BlockVisualController.Heating();
					VisualController.heating.glowCol = Prefab.heatGlowColor;
					VisualController.heating.lerpSpeed = Prefab.heatLerpSpeed;
					VisualController.heating.colToSet = Prefab.heatColorName;
					VisualController.burning = new BlockVisualController.Burning();
					VisualController.burning.Color = Prefab.burnColor;
				}
			}
			moddedBlock = ModIds.GetBlockByEffectiveId(Prefab.ID);
			if (!HasBehaviours)
			{
				BlockModule[] modules = moddedBlock.Modules;
				List<ModBlockBehaviour> list = new List<ModBlockBehaviour>();
				BlockModule[] array = modules;
				foreach (BlockModule blockModule in array)
				{
					ModBlockBehaviour modBlockBehaviour = (ModBlockBehaviour)base.gameObject.AddComponent(CustomModules.GetBlockBehaviourType(blockModule));
					IModuleBehaviour moduleBehaviour = (IModuleBehaviour)modBlockBehaviour;
					moduleBehaviour.RawModule = blockModule;
					moduleBehaviour.ModuleGuid = blockModule.Guid;
					list.Add(modBlockBehaviour);
				}
				list.Add(blockScript);
				behaviours = list.ToArray();
				HasBehaviours = true;
			}
			else
			{
				behaviours = GetComponents<ModBlockBehaviour>();
				ModBlockBehaviour[] array2 = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour2 in array2)
				{
					IModuleBehaviour moduleBehaviour2 = modBlockBehaviour2 as IModuleBehaviour;
					if (moduleBehaviour2 != null)
					{
						moduleBehaviour2.RawModule = moddedBlock.Modules.First((BlockModule m) => m.Guid == moduleBehaviour2.ModuleGuid);
					}
				}
			}
			moduleMapperTypes = new Dictionary<string, MapperType>();
			MapperTypeDefinition[] array3 = moddedBlock.ModuleMapperTypes;
			foreach (MapperTypeDefinition mapperTypeDefinition in array3)
			{
				MapperType value = mapperTypeDefinition.Create(this);
				moduleMapperTypes[mapperTypeDefinition.Key] = value;
			}
			bool flag = false;
			ModBlockBehaviour[] array4 = behaviours;
			foreach (ModBlockBehaviour modBlockBehaviour3 in array4)
			{
				modBlockBehaviour3.handler = this;
				modBlockBehaviour3.BlockId = Prefab.ID;
				ModdingUtil.PerformCallback(modBlockBehaviour3.SafeAwake);
				if (!flag && modBlockBehaviour3.EmulatesAnyKeys)
				{
					flag = true;
					Prefab.EmulatesAnyKeys = true;
				}
			}
		}

		protected override void Start()
		{
			base.Start();
			started = true;
			if (isSimulating)
			{
				ModBlockBehaviour[] array = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour in array)
				{
					ModdingUtil.PerformCallback(modBlockBehaviour.OnSimulateStart);
				}
			}
			if (base.transform.parent == base.ParentMachine.BuildingMachine)
			{
				ModBlockBehaviour[] array2 = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour2 in array2)
				{
					ModdingUtil.PerformCallback(modBlockBehaviour2.OnBlockPlaced);
				}
			}
			else
			{
				fireController = GetComponent<FireController>();
				if (fireController != null)
				{
					fireController.sendDousedMessage = fireController.sendDousedMessage.Append(base.transform).ToArray();
					fireController.sendKillMessage = fireController.sendKillMessage.Append(base.transform).ToArray();
				}
			}
		}

		public override void OnReloadAmmo(ref int units, ReloadAmmoType type, bool setAmmo, bool eachBlock, bool playAnim = true)
		{
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour modBlockBehaviour in array)
			{
				modBlockBehaviour.OnReloadAmmo(ref units, type, setAmmo, eachBlock);
			}
		}

		public override bool OnFlip(bool sound, bool isUndo)
		{
			bool flag = blockScript.OnFlip();
			if (flag && sound)
			{
				ReferenceMaster.PlayFlip();
			}
			return flag;
		}

		public MapperType GetMapperReference(MapperTypeReference reference)
		{
			if (!moduleMapperTypes.ContainsKey(reference.Key))
			{
				throw new ArgumentException("No mapper type with key " + reference.Key + " defined!");
			}
			return moduleMapperTypes[reference.Key];
		}

		public ModResource GetResource(ResourceReference reference)
		{
			return ModResource.Get(reference, moddedBlock.Info.Mod);
		}

		protected override void OnDisable()
		{
			base.OnDisable();
			if ((bool)base.ParentMachine && isSimulating)
			{
				ModBlockBehaviour[] array = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour in array)
				{
					ModdingUtil.PerformCallback(modBlockBehaviour.OnSimulateStop);
				}
			}
		}

		public override void UpdateBlock()
		{
			base.UpdateBlock();
			if (!started)
			{
				return;
			}
			if ((bool)fireController && HasBurnedOut && !sentBurntOutMessage)
			{
				sentBurntOutMessage = true;
				ModBlockBehaviour[] array = behaviours;
				ModBlockBehaviour behaviour;
				for (int i = 0; i < array.Length; i++)
				{
					behaviour = array[i];
					ModdingUtil.PerformCallback(delegate
					{
						behaviour.OnStopBurning(false);
					});
				}
			}
			if (isSimulating)
			{
				ModBlockBehaviour[] array2 = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour in array2)
				{
					ModdingUtil.PerformCallback(modBlockBehaviour.SimulateUpdateAlways);
				}
				if (SimPhysics)
				{
					ModBlockBehaviour[] array3 = behaviours;
					foreach (ModBlockBehaviour modBlockBehaviour2 in array3)
					{
						ModdingUtil.PerformCallback(modBlockBehaviour2.SimulateUpdateHost);
					}
				}
				else
				{
					ModBlockBehaviour[] array4 = behaviours;
					foreach (ModBlockBehaviour modBlockBehaviour3 in array4)
					{
						ModdingUtil.PerformCallback(modBlockBehaviour3.SimulateUpdateClient);
					}
				}
			}
			else
			{
				ModBlockBehaviour[] array5 = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour4 in array5)
				{
					ModdingUtil.PerformCallback(modBlockBehaviour4.BuildingUpdate);
				}
			}
		}

		public override void FixedUpdateBlock()
		{
			base.FixedUpdateBlock();
			if (!started)
			{
				return;
			}
			if (isSimulating)
			{
				ModBlockBehaviour[] array = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour in array)
				{
					ModdingUtil.PerformCallback(modBlockBehaviour.SimulateFixedUpdateAlways);
				}
				if (SimPhysics)
				{
					ModBlockBehaviour[] array2 = behaviours;
					foreach (ModBlockBehaviour modBlockBehaviour2 in array2)
					{
						ModdingUtil.PerformCallback(modBlockBehaviour2.SimulateFixedUpdateHost);
					}
				}
				else
				{
					ModBlockBehaviour[] array3 = behaviours;
					foreach (ModBlockBehaviour modBlockBehaviour3 in array3)
					{
						ModdingUtil.PerformCallback(modBlockBehaviour3.SimulateFixedUpdateClient);
					}
				}
			}
			else
			{
				ModBlockBehaviour[] array4 = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour4 in array4)
				{
					ModdingUtil.PerformCallback(modBlockBehaviour4.BuildingFixedUpdate);
				}
			}
		}

		public override void LateUpdateBlock()
		{
			base.LateUpdateBlock();
			if (!started)
			{
				return;
			}
			if (isSimulating)
			{
				ModBlockBehaviour[] array = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour in array)
				{
					ModdingUtil.PerformCallback(modBlockBehaviour.SimulateLateUpdateAlways);
				}
				if (SimPhysics)
				{
					ModBlockBehaviour[] array2 = behaviours;
					foreach (ModBlockBehaviour modBlockBehaviour2 in array2)
					{
						ModdingUtil.PerformCallback(modBlockBehaviour2.SimulateLateUpdateHost);
					}
				}
				else
				{
					ModBlockBehaviour[] array3 = behaviours;
					foreach (ModBlockBehaviour modBlockBehaviour3 in array3)
					{
						ModdingUtil.PerformCallback(modBlockBehaviour3.SimulateLateUpdateClient);
					}
				}
			}
			else
			{
				ModBlockBehaviour[] array4 = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour4 in array4)
				{
					ModdingUtil.PerformCallback(modBlockBehaviour4.BuildingLateUpdate);
				}
			}
		}

		public override void EmulationUpdateBlock()
		{
			base.EmulationUpdateBlock();
			if (started && isSimulating)
			{
				ModBlockBehaviour[] array = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour in array)
				{
					ModdingUtil.PerformCallback(modBlockBehaviour.KeyEmulationUpdate);
				}
			}
		}

		public override void SendEmulationUpdateBlock()
		{
			base.SendEmulationUpdateBlock();
			if (started && isSimulating && SimPhysics)
			{
				ModBlockBehaviour[] array = behaviours;
				foreach (ModBlockBehaviour modBlockBehaviour in array)
				{
					ModdingUtil.PerformCallback(modBlockBehaviour.SendKeyEmulationUpdateHost);
				}
			}
		}

		public virtual Transform GetLimitsDisplay()
		{
			return VisualController.Block.MeshRenderer.transform;
		}

		public void Kill()
		{
			doused = false;
			hasBurned = true;
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour modBlockBehaviour in array)
			{
				ModdingUtil.PerformCallback(modBlockBehaviour.OnStartBurning);
			}
		}

		public void Doused()
		{
			doused = true;
			ModBlockBehaviour[] array = behaviours;
			ModBlockBehaviour behaviour;
			for (int i = 0; i < array.Length; i++)
			{
				behaviour = array[i];
				ModdingUtil.PerformCallback(delegate
				{
					behaviour.OnStopBurning(true);
				});
			}
		}

		public new void EmulateKeys(MKey[] activationKeys, MKey emulateKey, bool emulate)
		{
			base.EmulateKeys(activationKeys, emulateKey, emulate);
		}

		public void OnCollisionEnter(Collision collision)
		{
			if (!isSimulating)
			{
				return;
			}
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour behaviour in array)
			{
				ModdingUtil.PerformCallback(delegate
				{
					behaviour.OnSimulateCollisionEnter(collision);
				});
			}
		}

		public void OnCollisionStay(Collision collision)
		{
			if (!isSimulating)
			{
				return;
			}
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour behaviour in array)
			{
				ModdingUtil.PerformCallback(delegate
				{
					behaviour.OnSimulateCollisionStay(collision);
				});
			}
		}

		public void OnCollisionExit(Collision collision)
		{
			if (!isSimulating)
			{
				return;
			}
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour behaviour in array)
			{
				ModdingUtil.PerformCallback(delegate
				{
					behaviour.OnSimulateCollisionExit(collision);
				});
			}
		}

		public void OnTriggerEnter(Collider other)
		{
			if (!isSimulating)
			{
				return;
			}
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour behaviour in array)
			{
				ModdingUtil.PerformCallback(delegate
				{
					behaviour.OnSimulateTriggerEnter(other);
				});
			}
		}

		public void OnTriggerStay(Collider other)
		{
			if (!isSimulating)
			{
				return;
			}
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour behaviour in array)
			{
				ModdingUtil.PerformCallback(delegate
				{
					behaviour.OnSimulateTriggerStay(other);
				});
			}
		}

		public void OnTriggerExit(Collider other)
		{
			if (!isSimulating)
			{
				return;
			}
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour behaviour in array)
			{
				ModdingUtil.PerformCallback(delegate
				{
					behaviour.OnSimulateTriggerExit(other);
				});
			}
		}

		public void OnParticleCollision(GameObject other)
		{
			if (!isSimulating)
			{
				return;
			}
			ModBlockBehaviour[] array = behaviours;
			foreach (ModBlockBehaviour behaviour in array)
			{
				ModdingUtil.PerformCallback(delegate
				{
					behaviour.OnSimulateParticleCollision(other);
				});
			}
		}
	}
}

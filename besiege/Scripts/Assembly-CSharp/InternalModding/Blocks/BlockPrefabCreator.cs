using System;
using System.Collections.Generic;
using System.Linq;
using InternalModding.Common;
using InternalModding.Misc;
using Modding;
using Modding.Serialization;
using UnityEngine;
using cakeslice;
using mattmc3.dotmore.Collections.Generic;

namespace InternalModding.Blocks
{
	public static class BlockPrefabCreator
	{
		public static void CreateGhost(ModdedBlock blockInfo)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<BlockLoader>.Instance.GhostTemplate);
			gameObject.name = "Modded: " + blockInfo.Name;
			gameObject.SetActive(false);
			SetVis(blockInfo, gameObject, true);
			Transform transform = gameObject.transform.FindChild("HammerPos");
			transform.localPosition = blockInfo.GhostInfo.Hammer.Position;
			transform.localEulerAngles = blockInfo.GhostInfo.Hammer.Rotation;
			SetupColliders(blockInfo, gameObject, true);
			SetupArrow(blockInfo, gameObject, true);
			blockInfo.Ghost = gameObject;
		}

		public static void CreatePrefab(ModdedBlock blockInfo)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<BlockLoader>.Instance.BlockTemplate);
			gameObject.name = string.Concat(blockInfo.Info.Mod.Info.Id, "-", blockInfo.LocalId);
			gameObject.SetActive(false);
			Rigidbody component = gameObject.GetComponent<Rigidbody>();
			component.mass = blockInfo.Mass;
			SetupBehaviour(blockInfo, gameObject);
			SetupAddingPoints(blockInfo, gameObject);
			SetupBlockHealthBar(blockInfo, gameObject);
			SetupFire(blockInfo, gameObject);
			BlockBehaviour component2 = gameObject.GetComponent<BlockBehaviour>();
			if (blockInfo.CanFreeze)
			{
				component2.iceTag = gameObject.AddComponent<IceTag>();
			}
			SetVis(blockInfo, gameObject);
			SetupColliders(blockInfo, gameObject);
			SetupArrow(blockInfo, gameObject, false);
			blockInfo.Prefab = gameObject;
		}

		public static void CreateStrippedPrefab(ModdedBlock blockInfo)
		{
			GameObject prefab = blockInfo.Prefab;
			GameObject gameObject = UnityEngine.Object.Instantiate(prefab);
			gameObject.name = prefab.name;
			Predicate<Transform> predicate = delegate(Transform o)
			{
				do
				{
					if (blockInfo.KeepWhenStripped.Contains(o.name))
					{
						return false;
					}
				}
				while ((o = o.parent) != null);
				return true;
			};
			Type[] array = new Type[10]
			{
				typeof(Joint),
				typeof(ConstantForce),
				typeof(Rigidbody),
				typeof(MyBounds),
				typeof(TriggerSetJointBase),
				typeof(DestroyJointIfNull),
				typeof(SetColliderIterationCount),
				typeof(Outline),
				typeof(DestroyJointIfNull),
				typeof(BlockDamageType)
			};
			Type[] array2 = array;
			foreach (Type type in array2)
			{
				Component[] componentsInChildren = gameObject.GetComponentsInChildren(type, true);
				Component[] array3 = componentsInChildren;
				foreach (Component component in array3)
				{
					if (predicate(component.transform))
					{
						UnityEngine.Object.DestroyImmediate(component);
					}
				}
			}
			Collider[] componentsInChildren2 = gameObject.GetComponentsInChildren<Collider>();
			Collider[] array4 = componentsInChildren2;
			foreach (Collider collider in array4)
			{
				if (collider.isTrigger && predicate(collider.transform))
				{
					UnityEngine.Object.DestroyImmediate(collider);
				}
			}
			ModBlockBehaviourHandler component2 = gameObject.GetComponent<ModBlockBehaviourHandler>();
			component2.stripped = true;
			component2.noRigidbody = true;
			component2.moddedBlock = blockInfo;
			component2.Prefab = prefab.GetComponent<ModBlockBehaviourHandler>().Prefab;
			NetworkBlock networkBlock = component2.gameObject.AddComponent<NetworkBlock>();
			networkBlock.blockBehaviour = component2;
			networkBlock.isBlock = true;
			component2.SetNetworkBlock(networkBlock);
			networkBlock.FetchComponents();
			blockInfo.StrippedPrefab = gameObject;
		}

		public static void UpdatePrefab(ModdedBlock block)
		{
			UpdateObject(block, block.Prefab, false, false);
			UpdateObject(block, block.Ghost, false, true);
			object obj;
			if (!StatMaster.isMP)
			{
				obj = new Machine[1] { Machine.Active() };
			}
			else
			{
				IEnumerable<Machine> enumerable = ((IEnumerable<PlayerData>)Playerlist.Players).Select((Func<PlayerData, Machine>)((PlayerData p) => p.machine));
				obj = enumerable;
			}
			IEnumerable<Machine> enumerable2 = (IEnumerable<Machine>)obj;
			foreach (Machine item in enumerable2)
			{
				if (item == null)
				{
					continue;
				}
				foreach (BlockBehaviour item2 in item.BuildingBlocks.Union(item.SimulationBlocks))
				{
					if (item2.BlockID == block.Id)
					{
						UpdateObject(block, item2.gameObject, item2.isSimulating, false);
					}
				}
			}
		}

		private static void UpdateObject(ModdedBlock block, GameObject obj, bool simVersion, bool ghost)
		{
			SetVis(block, obj, ghost);
			SetupColliders(block, obj, ghost);
			if (!simVersion && !ghost)
			{
				SetupAddingPoints(block, obj);
			}
			if (!ghost)
			{
				SetupBlockHealthBar(block, obj);
				SetupFire(block, obj);
			}
			if (!simVersion || ghost)
			{
				SetupArrow(block, obj, ghost);
			}
			if (!ghost)
			{
				obj.GetComponent<Rigidbody>().mass = block.Mass;
				ModBlockBehaviourHandler component = obj.GetComponent<ModBlockBehaviourHandler>();
				component.OnModuleReload();
				if (block.Density != 0f)
				{
					component.density = (component.originalDensity = block.Density);
				}
			}
		}

		public static void SetVis(ModdedBlock blockInfo, GameObject blockObj, bool ghost = false)
		{
			GameObject go = blockObj.transform.FindChild("Vis").gameObject;
			bool flag = false;
			if (blockInfo.PrefabRegistered)
			{
				flag = ((!ghost) ? (!blockObj.GetComponent<BlockVisualController>().selectedSkin.isDefault) : (!PrefabMaster.BlockPrefabs[blockInfo.Id].SelectedSkin.isDefault));
			}
			if (flag)
			{
				go.transform.localPosition = blockInfo.MeshReference.Position;
				go.transform.localRotation = Quaternion.Euler(blockInfo.MeshReference.Rotation);
				go.transform.localScale = blockInfo.MeshReference.Scale;
				return;
			}
			MeshFilter component = go.GetComponent<MeshFilter>();
			component.mesh = null;
			go.transform.localScale = new UnityEngine.Vector3(3f, 3f, 3f);
			if (blockInfo.Mesh == null || !blockInfo.Mesh.Loaded)
			{
				GameObject gameObject = new GameObject("Placeholder Vis");
				gameObject.transform.parent = blockObj.transform;
				foreach (ModCollider collider in blockInfo.Colliders)
				{
					collider.CreateVisual(gameObject.transform);
				}
			}
			Material origMaterial = UnityEngine.Object.Instantiate(((!ghost) ? SingleInstanceFindOnly<BlockLoader>.Instance.BlockTemplate : SingleInstanceFindOnly<BlockLoader>.Instance.GhostTemplate).transform.FindChild("Vis").GetComponent<MeshRenderer>().sharedMaterial);
			MeshRenderer component2 = go.GetComponent<MeshRenderer>();
			component2.material = SingleInstanceFindOnly<BlockLoader>.Instance.LoadingMaterial;
			if (ghost)
			{
				GhostMaterialController component3 = blockObj.GetComponent<GhostMaterialController>();
				component3.startingMaterials = new Material[1] { origMaterial };
				component3.originalMaterials = new Material[1] { origMaterial };
				component3.visFilter = go.GetComponent<MeshFilter>();
				if (blockInfo.Texture != null)
				{
					blockInfo.Texture.SetOnObject(go, origMaterial, delegate(GameObject obj)
					{
						Renderer renderer = obj.GetComponent<Renderer>();
						GhostMaterialController component4 = obj.transform.parent.GetComponent<GhostMaterialController>();
						if (component4 != null)
						{
							int num = component4.renderers.ToList().FindIndex((Renderer r) => r == renderer);
							if (num >= 0 && num < component4.renderers.Length)
							{
								component4.originalMaterials[num] = renderer.material;
								if (component4.startingMaterials != null)
								{
									component4.startingMaterials[num] = new Material(renderer.material);
								}
							}
						}
					});
				}
				if (blockInfo.Mesh != null)
				{
					blockInfo.Mesh.SetOnObject(go, blockInfo.MeshReference, delegate
					{
						Transform transform = go.transform.parent.FindChild("Placeholder Vis");
						if ((bool)transform)
						{
							UnityEngine.Object.Destroy(transform.gameObject);
						}
					});
				}
			}
			else
			{
				if (blockInfo.Texture != null)
				{
					blockInfo.Texture.SetOnObject(go, origMaterial, null, delegate
					{
						origMaterial.mainTexture = (Texture2D)blockInfo.Texture;
					});
				}
				if (blockInfo.Mesh != null)
				{
					blockInfo.Mesh.SetOnObject(go, blockInfo.MeshReference, delegate
					{
						Transform transform = go.transform.parent.FindChild("Placeholder Vis");
						if ((bool)transform)
						{
							UnityEngine.Object.Destroy(transform.gameObject);
						}
					});
				}
			}
			if (blockInfo.Texture != null)
			{
				blockInfo.Texture.OnLoad += delegate
				{
					BlockSkinLoader.SkinPack.Skin defaultOfType = BlockSkinLoader.SkinPack.Skin.GetDefaultOfType((BlockType)blockInfo.Id);
					if (defaultOfType != null)
					{
						defaultOfType.ForceMaterial(new Material(origMaterial));
						defaultOfType.ForceGhostMaterial(null);
						defaultOfType.texture = (Texture2D)blockInfo.Texture;
						defaultOfType.DoneLoading();
					}
					if (blockInfo.BlockPrefab != null)
					{
						foreach (BlockSkinLoader.SkinPack.Skin availableSkin in blockInfo.BlockPrefab.AvailableSkins)
						{
							if (availableSkin != defaultOfType)
							{
								availableSkin.ForceMaterial(null);
								availableSkin.ForceGhostMaterial(null);
							}
						}
					}
				};
			}
			if (blockInfo.Mesh == null)
			{
				return;
			}
			blockInfo.Mesh.OnLoad += delegate
			{
				BlockSkinLoader.SkinPack.Skin defaultOfType = BlockSkinLoader.SkinPack.Skin.GetDefaultOfType((BlockType)blockInfo.Id);
				if (defaultOfType != null)
				{
					defaultOfType.mesh = blockInfo.Mesh;
					defaultOfType.DoneLoading();
				}
			};
		}

		private static void SetupAddingPoints(ModdedBlock blockInfo, GameObject block)
		{
			BlockBehaviour component = block.GetComponent<BlockBehaviour>();
			if (blockInfo.AddingPointJoints != null)
			{
				List<GameObject> myAddingPoints = new List<GameObject>();
				foreach (Transform item in block.transform)
				{
					if (item.name == "Adding Point" || item.name == "StickyJointTrigger")
					{
						myAddingPoints.Add(item.gameObject);
					}
				}
				component.DestroyOnSimulate = (from o in component.DestroyOnSimulate.AsEnumerable()
					where !myAddingPoints.Contains(o)
					select o).ToArray();
				foreach (GameObject item2 in myAddingPoints)
				{
					UnityEngine.Object.DestroyImmediate(item2);
				}
				blockInfo.AddingPointJoints.Clear();
			}
			blockInfo.AddingPointJoints = new List<GameObject>(blockInfo.AddingPoints.Count + 1);
			GameObject joint2Template = SingleInstanceFindOnly<BlockLoader>.Instance.Joint2Template;
			GameObject gameObject = block.transform.FindChild("TriggerForJoint").gameObject;
			BasePoint basePoint = blockInfo.BasePoint;
			if (basePoint.HasAddingPoint)
			{
				GameObject gameObject2 = new GameObject("Adding Point");
				gameObject2.transform.parent = block.transform;
				gameObject2.transform.localPosition = new UnityEngine.Vector3(0f, 0f, 0.5f);
				gameObject2.transform.localEulerAngles = new UnityEngine.Vector3(90f, 0f, 0f);
				BoxCollider boxCollider = gameObject2.AddComponent<BoxCollider>();
				boxCollider.isTrigger = true;
				boxCollider.center = new UnityEngine.Vector3(0f, -0.58f, 0f);
				boxCollider.size = new UnityEngine.Vector3(0.6f, 0f, 0.6f);
				gameObject2.layer = 12;
				if (blockInfo.DebugVisuals)
				{
					GameObject gameObject3 = GameObject.CreatePrimitive(PrimitiveType.Cube);
					gameObject3.name = "Vis";
					gameObject3.layer = 25;
					gameObject3.transform.parent = gameObject2.transform;
					gameObject3.transform.localPosition = boxCollider.center;
					gameObject3.transform.localEulerAngles = UnityEngine.Vector3.zero;
					gameObject3.transform.localScale = boxCollider.size;
					gameObject3.GetComponent<Renderer>().sharedMaterial = SingleInstanceFindOnly<BlockLoader>.Instance.AddingPointVisualMaterial;
					UnityEngine.Object.DestroyImmediate(gameObject3.GetComponent<Collider>());
				}
				blockInfo.AddingPointJoints.Add(gameObject2);
			}
			ConfigurableJoint component2 = block.GetComponent<ConfigurableJoint>();
			component2.breakForce = basePoint.BreakForce;
			component2.breakTorque = basePoint.BreakForce;
			if (basePoint.Sticky)
			{
				gameObject.transform.localScale = new UnityEngine.Vector3(basePoint.Radius, basePoint.Radius, basePoint.Radius);
				if (basePoint.HasMotion)
				{
					component2.angularXMotion = basePoint.MotionX;
					component2.angularYMotion = basePoint.MotionY;
					component2.angularZMotion = basePoint.MotionZ;
					gameObject.tag = "MechanicalTag";
				}
			}
			else
			{
				UnityEngine.Object.Destroy(block.GetComponent<ConfigurableJoint>());
				UnityEngine.Object.Destroy(gameObject);
			}
			foreach (AddingPoint addingPoint in blockInfo.AddingPoints)
			{
				GameObject gameObject4 = new GameObject("Adding Point");
				gameObject4.transform.parent = block.transform;
				gameObject4.transform.localPosition = addingPoint.Position;
				gameObject4.transform.localEulerAngles = addingPoint.Rotation;
				gameObject4.transform.position -= gameObject4.transform.forward * 0.5f;
				BoxCollider boxCollider2 = gameObject4.AddComponent<BoxCollider>();
				boxCollider2.isTrigger = true;
				boxCollider2.center = new UnityEngine.Vector3(0f, 0f, 0.5f);
				boxCollider2.size = new UnityEngine.Vector3(0.6f, 0.6f, 0f);
				gameObject4.layer = 12;
				blockInfo.AddingPointJoints.Add(gameObject4);
				GameObject gameObject5 = new GameObject("occluder");
				gameObject5.layer = 21;
				gameObject5.transform.parent = gameObject4.transform;
				gameObject5.transform.localPosition = UnityEngine.Vector3.zero;
				gameObject5.transform.localEulerAngles = UnityEngine.Vector3.zero;
				gameObject5.transform.localScale = UnityEngine.Vector3.one;
				boxCollider2 = gameObject5.AddComponent<BoxCollider>();
				boxCollider2.isTrigger = true;
				boxCollider2.center = new UnityEngine.Vector3(0f, 0f, 0.4945f);
				boxCollider2.size = new UnityEngine.Vector3(0.6f, 0.6f, 0f);
				if (blockInfo.DebugVisuals)
				{
					GameObject gameObject6 = GameObject.CreatePrimitive(PrimitiveType.Cube);
					gameObject6.name = "Vis";
					gameObject6.layer = 25;
					gameObject6.transform.parent = gameObject4.transform;
					gameObject6.transform.localPosition = boxCollider2.center;
					gameObject6.transform.localEulerAngles = UnityEngine.Vector3.zero;
					gameObject6.transform.localScale = boxCollider2.size;
					gameObject6.GetComponent<Renderer>().sharedMaterial = SingleInstanceFindOnly<BlockLoader>.Instance.AddingPointVisualMaterial;
					UnityEngine.Object.DestroyImmediate(gameObject6.GetComponent<Collider>());
				}
				if (addingPoint.Sticky)
				{
					GameObject gameObject7 = new GameObject("temp");
					gameObject7.transform.parent = gameObject4.transform;
					gameObject7.transform.localPosition = new UnityEngine.Vector3(0f, 0f, 0f);
					GameObject gameObject8 = (GameObject)UnityEngine.Object.Instantiate(joint2Template, gameObject7.transform.position, Quaternion.identity);
					gameObject8.name = "StickyJointTrigger";
					gameObject8.GetComponent<SphereCollider>().radius = addingPoint.Radius;
					gameObject8.transform.parent = block.transform;
					gameObject8.transform.localEulerAngles = addingPoint.Rotation;
					TriggerSetJoint2 component3 = gameObject8.GetComponent<TriggerSetJoint2>();
					component3.parentBody = block.GetComponent<Rigidbody>();
					gameObject8.GetComponent<SphereCollider>().center = boxCollider2.center;
					UnityEngine.Object.Destroy(gameObject7);
				}
			}
			GameObject[] destroyOnSimulate = component.DestroyOnSimulate;
			component.DestroyOnSimulate = new GameObject[destroyOnSimulate.Length + blockInfo.AddingPointJoints.Count];
			for (int num = 0; num < destroyOnSimulate.Length; num++)
			{
				component.DestroyOnSimulate[num] = destroyOnSimulate[num];
			}
			for (int num2 = 0; num2 < blockInfo.AddingPointJoints.Count; num2++)
			{
				component.DestroyOnSimulate[num2 + destroyOnSimulate.Length] = blockInfo.AddingPointJoints[num2];
			}
		}

		private static void SetupBlockHealthBar(ModdedBlock blockInfo, GameObject block)
		{
			BlockHealthBar blockHealthBar = block.GetComponent<BlockHealthBar>();
			if (blockInfo.CanTakeDamage)
			{
				if (blockHealthBar == null)
				{
					blockHealthBar = block.AddComponent<BlockHealthBar>();
				}
				blockHealthBar.health = blockInfo.Health;
				blockInfo.BlockPrefab.hasHealthBar = true;
				block.GetComponent<BlockBehaviour>().BlockHealth = blockHealthBar;
			}
			else if (blockHealthBar != null)
			{
				UnityEngine.Object.Destroy(blockHealthBar);
			}
		}

		private static void SetupColliders(ModdedBlock blockInfo, GameObject block, bool ghost = false)
		{
			MyBounds component = block.GetComponent<MyBounds>();
			if (!ghost)
			{
				component.childColliders.Clear();
			}
			Transform transform = block.transform.FindChild("Colliders");
			if ((bool)transform)
			{
				UnityEngine.Object.DestroyImmediate(transform.gameObject);
			}
			transform = new GameObject("Colliders").transform;
			transform.SetParent(block.transform, false);
			Transform transform2 = block.transform.FindChild("Collider Vis");
			if ((bool)transform2)
			{
				UnityEngine.Object.DestroyImmediate(transform2.gameObject);
			}
			if (blockInfo.DebugVisuals)
			{
				transform2 = new GameObject("Collider Vis").transform;
				transform2.SetParent(block.transform, false);
			}
			int layer = (ghost ? 2 : 0);
			foreach (ModCollider item in (!ghost) ? blockInfo.Colliders : blockInfo.GhostColliders)
			{
				Collider collider = item.CreateCollider(transform);
				if (!item.LayerSpecified)
				{
					collider.gameObject.layer = layer;
				}
				if (!ghost && blockInfo.DebugVisuals)
				{
					item.CreateVisual(transform2);
				}
				if (ghost)
				{
					collider.isTrigger = true;
				}
				else
				{
					component.childColliders.Add(collider);
				}
			}
		}

		private static void SetupFire(ModdedBlock blockInfo, GameObject block)
		{
			BlockBehaviour component = block.GetComponent<BlockBehaviour>();
			component.fireTag = block.GetComponent<FireTag>();
			blockInfo.FireInteraction.SetOnObject(block, block.transform.FindChild("FireController"), block.transform.FindChild("Fire"), component, blockInfo.DebugVisuals);
		}

		private static void SetupBehaviour(ModdedBlock blockInfo, GameObject blockObj)
		{
			Type type = null;
			if (!string.IsNullOrEmpty(blockInfo.ScriptName))
			{
				List<Type> typesByName = blockInfo.Info.Mod.GetTypesByName(blockInfo.ScriptName);
				if (typesByName.Count > 1)
				{
					MLog.Error("[Blocks] Too many types named " + blockInfo.ScriptName + " in mod assemblies!");
				}
				else if (typesByName.Count < 1)
				{
					MLog.Error("[Blocks] No type named " + blockInfo.ScriptName + " in mod assemblies!");
				}
				else
				{
					type = typesByName.First();
				}
			}
			if (type != null && !type.IsSubclassOf(typeof(BlockScript)))
			{
				MLog.Error("[Blocks] Type " + blockInfo.ScriptName + " does not extend BlockScript!");
				type = null;
			}
			if (type == null)
			{
				type = typeof(BlockScript);
			}
			blockInfo.ScriptType = type;
			BlockPrefab blockPrefab = new BlockPrefab();
			blockPrefab.name = blockInfo.Name;
			blockPrefab.ghost = blockInfo.Ghost;
			blockPrefab.nameKeywords = blockInfo.SearchKeywords.Append(blockInfo.Info.Mod.Info.Author).ToArray();
			blockPrefab.ID = blockInfo.Id;
			blockPrefab.Type = (BlockType)blockInfo.Id;
			blockPrefab.SetBreakForce = true;
			blockPrefab.hasMyBounds = true;
			blockPrefab.SetColliderIterations = false;
			blockPrefab.SetVelocityIterations = false;
			blockPrefab.IterationCount = 10;
			blockPrefab.VelocityIterationCount = 10;
			blockPrefab.VisualController = blockObj.GetComponent<BlockVisualController>();
			blockPrefab.hasBVC = true;
			blockPrefab.blockDamageSetting = DamageIgnoreSetting.JointOnly;
			blockPrefab.blockDamageTypes = new MachineDamageType[2]
			{
				MachineDamageType.JointBreak,
				MachineDamageType.ClusterLeave
			};
			blockPrefab.hasDamageType = blockInfo.HasDamageType;
			blockPrefab.myDamageType = blockInfo.DamageType;
			blockPrefab.RegisterBuildUpdate = true;
			blockPrefab.RegisterBuildFixedUpdate = true;
			blockPrefab.RegisterBuildLateUpdate = true;
			blockPrefab.RegisterSimUpdate = true;
			blockPrefab.RegisterSimFixedUpdate = true;
			blockPrefab.RegisterSimLateUpdate = true;
			blockPrefab.RegisterEmulationUpdate = true;
			blockPrefab.canBurn = blockInfo.CanBurn;
			blockPrefab.canFreeze = blockInfo.CanFreeze;
			blockPrefab.clusterBaseCandidate = true;
			blockPrefab.hasMeshFilter = true;
			blockPrefab.hasArrow = blockInfo.ArrowSpecified;
			blockPrefab.heatLerpSpeed = 2f;
			blockPrefab.heatGlowColor = new Color(1f, 0.26f, 0f);
			blockPrefab.heatColorName = "_EmissCol";
			blockPrefab.burnColor = new Color(0.2f, 0.2f, 0.2f, 1f);
			BlockPrefab prefab = (blockInfo.BlockPrefab = blockPrefab);
			ModBlockBehaviourHandler modBlockBehaviourHandler = blockObj.AddComponent<ModBlockBehaviourHandler>();
			modBlockBehaviourHandler.Prefab = prefab;
			modBlockBehaviourHandler.infoType = BasicInfo.BasicInfoType.Block;
			modBlockBehaviourHandler.moddedBlock = blockInfo;
			modBlockBehaviourHandler.density = (modBlockBehaviourHandler.originalDensity = blockInfo.Density);
			modBlockBehaviourHandler.DestroyOnClient = new GameObject[0];
			modBlockBehaviourHandler.DestroyOnSimulate = new GameObject[2]
			{
				blockObj.transform.FindChild("Occluder").gameObject,
				blockObj.transform.FindChild("DirectionArrow").gameObject
			};
			TriggerSetJoint[] componentsInChildren = modBlockBehaviourHandler.GetComponentsInChildren<TriggerSetJoint>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				componentsInChildren[i].block = modBlockBehaviourHandler;
			}
			if (blockInfo.HasDamageType)
			{
				blockObj.AddComponent<BlockDamageType>();
			}
			BlockVisualController component = modBlockBehaviourHandler.GetComponent<BlockVisualController>();
			component.Block = modBlockBehaviourHandler;
			modBlockBehaviourHandler.VisualController = component;
			modBlockBehaviourHandler.myBounds = blockObj.GetComponent<MyBounds>();
			modBlockBehaviourHandler.MeshRenderer = component.renderers[0];
			modBlockBehaviourHandler.Rigidbody = blockObj.GetComponent<Rigidbody>();
			modBlockBehaviourHandler.audioSource = blockObj.GetComponent<AudioSource>();
			modBlockBehaviourHandler.blockJoint = blockObj.GetComponent<Joint>();
			modBlockBehaviourHandler.CanFlip = blockInfo.CanFlip;
			BlockScript blockScript = (BlockScript)blockObj.AddComponent(type);
			blockScript.handler = modBlockBehaviourHandler;
			modBlockBehaviourHandler.blockScript = blockScript;
		}

		private static void SetupArrow(ModdedBlock info, GameObject go, bool ghost)
		{
			Transform transform = go.transform.FindChild("DirectionArrow");
			if (transform == null)
			{
				return;
			}
			if (info.ArrowSpecified)
			{
				info.Arrow.SetOnTransform(transform);
				if (!ghost)
				{
					go.GetComponent<ModBlockBehaviourHandler>().directionArrow = transform;
					return;
				}
				transform.localRotation = Quaternion.Euler(info.Arrow.Rotation + new UnityEngine.Vector3(-180f, 0f, 0f));
				Modding.Serialization.Vector3 scale = info.Arrow.Scale;
				scale.y *= -1f;
				transform.localScale = scale;
			}
			else
			{
				transform.gameObject.SetActive(false);
			}
		}
	}
}

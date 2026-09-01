using System.Collections.Generic;
using InternalModding.Blocks;
using Modding.Blocks;
using Modding.Mapper;
using UnityEngine;

namespace Modding
{
	public class ModBlockBehaviour : MonoBehaviour
	{
		private PlayerMachine _machine;

		[SerializeField]
		internal ModBlockBehaviourHandler handler;

		public virtual bool EmulatesAnyKeys
		{
			get
			{
				return false;
			}
		}

		public bool IsBurning
		{
			get
			{
				return handler.IsBurning;
			}
		}

		public bool HasBurnedOut
		{
			get
			{
				return handler.HasBurnedOut;
			}
		}

		public bool IsFrozen
		{
			get
			{
				return (bool)handler.iceTag && handler.iceTag.frozen;
			}
		}

		public bool IsDestroyed
		{
			get
			{
				return handler.IsDestroyed;
			}
		}

		public bool HasRigidbody
		{
			get
			{
				return !handler.noRigidbody;
			}
		}

		public Rigidbody Rigidbody
		{
			get
			{
				return handler.Rigidbody;
			}
		}

		public BlockBehaviour BlockBehaviour
		{
			get
			{
				return handler;
			}
		}

		public BlockVisualController VisualController
		{
			get
			{
				return handler.VisualController;
			}
		}

		public Renderer Renderer
		{
			get
			{
				return handler.VisualController.renderers[0];
			}
		}

		public Transform MainVis
		{
			get
			{
				return handler.VisualController.Block.MeshRenderer.transform;
			}
		}

		public bool ShowDebugVisuals
		{
			get
			{
				return handler.moddedBlock.DebugVisuals;
			}
		}

		public bool Flipped
		{
			get
			{
				return handler.Flipped;
			}
		}

		public int BlockId { get; internal set; }

		public bool SimPhysics
		{
			get
			{
				return handler.SimPhysics;
			}
		}

		public bool IsSimulating
		{
			get
			{
				return handler.isSimulating;
			}
		}

		public bool IsStripped
		{
			get
			{
				return handler.stripped;
			}
		}

		public PlayerMachine Machine
		{
			get
			{
				return handler.Machine;
			}
		}

		public bool CanFlip
		{
			get
			{
				return handler.CanFlip;
			}
		}

		public Transform DirectionArrow
		{
			get
			{
				return handler.DirectionArrow;
			}
		}

		public virtual void SafeAwake()
		{
		}

		public virtual void OnPrefabCreation()
		{
		}

		public virtual void OnBlockPlaced()
		{
		}

		public virtual void BuildingUpdate()
		{
		}

		public virtual void SimulateUpdateAlways()
		{
		}

		public virtual void SimulateUpdateHost()
		{
		}

		public virtual void SimulateUpdateClient()
		{
		}

		public virtual void BuildingFixedUpdate()
		{
		}

		public virtual void SimulateFixedUpdateAlways()
		{
		}

		public virtual void SimulateFixedUpdateHost()
		{
		}

		public virtual void SimulateFixedUpdateClient()
		{
		}

		public virtual void BuildingLateUpdate()
		{
		}

		public virtual void SimulateLateUpdateAlways()
		{
		}

		public virtual void SimulateLateUpdateHost()
		{
		}

		public virtual void SimulateLateUpdateClient()
		{
		}

		public virtual void KeyEmulationUpdate()
		{
		}

		public virtual void SendKeyEmulationUpdateHost()
		{
		}

		public virtual void OnSimulateStart()
		{
		}

		public virtual void OnSimulateStop()
		{
		}

		public virtual void OnStartBurning()
		{
		}

		public virtual void OnStopBurning(bool doused)
		{
		}

		public virtual void OnSimulateCollisionEnter(Collision collision)
		{
		}

		public virtual void OnSimulateCollisionStay(Collision collision)
		{
		}

		public virtual void OnSimulateCollisionExit(Collision collision)
		{
		}

		public virtual void OnSimulateTriggerEnter(Collider collision)
		{
		}

		public virtual void OnSimulateTriggerStay(Collider collision)
		{
		}

		public virtual void OnSimulateTriggerExit(Collider collision)
		{
		}

		public virtual void OnSimulateParticleCollision(GameObject other)
		{
		}

		public virtual void OnSave(XDataHolder data)
		{
		}

		public virtual void OnLoad(XDataHolder data)
		{
		}

		public virtual void OnReloadAmmo(ref int units, ReloadAmmoType type, bool setAmmo, bool eachBlock)
		{
		}

		public MKey AddKey(string displayName, string key, KeyCode defaultKey)
		{
			return handler.AddKey(displayName, key, defaultKey);
		}

		public MKey AddKey(MKey key)
		{
			return handler.AddKey(key);
		}

		public MKey AddEmulatorKey(string displayName, string key, KeyCode defaultKey)
		{
			return handler.AddEmulatorKey(displayName, key, defaultKey);
		}

		public void EmulateKeys(MKey[] activationKeys, MKey emulateKey, bool emulate)
		{
			if (!EmulatesAnyKeys)
			{
				Debug.LogError("Block " + handler.moddedBlock.Name + " uses EmulateKeys but does not set EmulatesAnyKeys property!\nThis will not work correctly!");
			}
			handler.EmulateKeys(activationKeys, emulateKey, emulate);
		}

		public MTeam AddTeam(string displayName, string key, MPTeam defaultTeam)
		{
			return handler.AddTeam(displayName, key, defaultTeam);
		}

		public MTeam AddTeam(MTeam team)
		{
			return handler.AddTeam(team);
		}

		public MText AddText(string displayName, string key, string defaultText)
		{
			return handler.AddText(displayName, key, defaultText);
		}

		public MText AddText(MText text)
		{
			return handler.AddText(text);
		}

		public MValue AddValue(string displayName, string key, float defaultValue)
		{
			return handler.AddValue(displayName, key, defaultValue);
		}

		public MValue AddValue(string displayName, string key, float defaultValue, float min, float max)
		{
			return handler.AddValue(displayName, key, defaultValue, min, max);
		}

		public MValue AddValue(MValue valueHolder)
		{
			return handler.AddValue(valueHolder);
		}

		public MSlider AddSlider(string displayName, string key, float defaultValue, float min, float max)
		{
			return handler.AddSlider(displayName, key, defaultValue, min, max, string.Empty);
		}

		public MSlider AddSliderUnclamped(string displayName, string key, float defaultValue, float min, float max)
		{
			return handler.AddSliderUnclamped(displayName, key, defaultValue, min, max, string.Empty);
		}

		public MSlider AddSlider(MSlider slider)
		{
			return handler.AddSlider(slider);
		}

		public MColourSlider AddColourSlider(string displayName, string key, Color defaultValue, bool snapColors)
		{
			return handler.AddColourSlider(displayName, key, defaultValue, snapColors);
		}

		public MColourSlider AddColourSlider(MColourSlider slider)
		{
			return handler.AddColourSlider(slider);
		}

		public MMenu AddMenu(string key, int defaultIndex, List<string> items, bool footerMenu = false)
		{
			return handler.AddMenu(key, defaultIndex, items, footerMenu);
		}

		public MMenu AddMenu(MMenu menu)
		{
			return handler.AddMenu(menu);
		}

		public MToggle AddToggle(string displayName, string key, bool defaultValue)
		{
			return handler.AddToggle(displayName, key, defaultValue);
		}

		public MToggle AddToggle(string displayName, string key, string tooltipText, bool defaultValue)
		{
			return handler.AddToggle(displayName, key, tooltipText, defaultValue);
		}

		public MToggle AddToggle(MToggle toggle)
		{
			return handler.AddToggle(toggle);
		}

		public MLimits AddLimits(string displayName, string key, float defaultMin, float defaultMax, float highestAngle, FauxTransform iconInfo, bool enabled = true)
		{
			MLimits limits = new MLimits(displayName, key, defaultMin, defaultMax, highestAngle, iconInfo, handler);
			return AddLimits(limits, enabled);
		}

		public MLimits AddLimits(string displayName, string key, float defaultMin, float defaultMax, float highestAngle, FauxTransform iconInfo, ILimitsDisplay limitsDisplay, bool enabled = true)
		{
			MLimits limits = new MLimits(displayName, key, defaultMin, defaultMax, highestAngle, iconInfo, limitsDisplay);
			return AddLimits(limits, enabled);
		}

		public MLimits AddLimits(MLimits limits, bool enabled = true)
		{
			return handler.AddLimits(limits, enabled);
		}

		public MCustom<T> AddCustom<T>(MCustom<T> custom)
		{
			return handler.AddCustom(custom);
		}
	}
}

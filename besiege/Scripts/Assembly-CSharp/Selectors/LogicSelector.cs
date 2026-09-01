using BlockMapperInternal;
using UnityEngine;

namespace Selectors
{
	public class LogicSelector : Selector, IWidgetContainer
	{
		[SerializeField]
		private ContainerDetails container;

		[SerializeField]
		private UIButton chainIcon;

		private GameObject chainGO;

		private Transform chainTransform;

		private MeshRenderer chainRenderer;

		[SerializeField]
		private Texture chain;

		[SerializeField]
		private Texture chainBroken;

		private WidgetController headerController;

		private WidgetController varController;

		private WidgetController keyChangeController;

		private WidgetController machineDamageController;

		private WidgetController targetController;

		private WidgetController addTargetController;

		private WidgetController eventController;

		private WidgetController addEventController;

		private WidgetController moddedController;

		private UIButton addTargetButton;

		private UIButton addEventButton;

		private EditLogicHandler logicHandler;

		private bool hasHandler;

		private bool updateCallback;

		public override MapperType MapperType
		{
			get
			{
				return Logic;
			}
			set
			{
				if (updateCallback)
				{
					if (Logic != null)
					{
						Logic.LogicChanged -= OnLogicChange;
					}
					updateCallback = false;
				}
				Logic = (MLogic)value;
				if (Logic != null)
				{
					Logic.LogicChanged += OnLogicChange;
					updateCallback = true;
				}
			}
		}

		public MLogic Logic { get; set; }

		public EntityLogic Value
		{
			get
			{
				return Logic.Value;
			}
			set
			{
				Logic.Value = value;
			}
		}

		public void RefreshPickFields()
		{
			for (int i = 0; i < targetController.ContainerCount; i++)
			{
				TargetPicker targetPicker = targetController.containers[i].widget as TargetPicker;
				targetPicker.Refresh();
			}
			for (int j = 0; j < eventController.ContainerCount; j++)
			{
				LogicEventWidget logicEventWidget = eventController.containers[j].widget as LogicEventWidget;
				logicEventWidget.RefreshPicker();
			}
		}

		public float TopValue()
		{
			return base.transform.position.y;
		}

		public float ZValue()
		{
			return base.transform.position.z;
		}

		private void OnLogicChange()
		{
			int num = 0;
			bool flag = Value.IsTrigger();
			bool flag2 = Value.triggerType == TriggerType.MachineDamage;
			bool flag3 = Value.triggerType == TriggerType.Variable;
			bool flag4 = Value.IsKeyChange();
			bool flag5 = Value.triggerType == TriggerType.Modded;
			float currentEndPosition = headerController.CurrentEndPosition;
			if (flag)
			{
				bool flag6 = false;
				bool flag7 = Value.targets.Count > 1;
				if (Value.targets.Count != targetController.ContainerCount)
				{
					flag6 = true;
					targetController.Clear();
					for (num = 0; num < Value.targets.Count; num++)
					{
						targetController.RegisterToggle(Value);
					}
				}
				if (chainGO.activeSelf != flag7)
				{
					chainGO.SetActive(flag7);
				}
				if (flag7)
				{
					UpdateChainIcon();
				}
				if (flag6)
				{
					targetController.Display(this, currentEndPosition);
					if (flag7)
					{
						Vector3 position = chainTransform.position;
						float num2 = 0f;
						for (int i = 0; i < targetController.containers.Count; i++)
						{
							num2 += targetController.containers[i].transform.position.y;
						}
						num2 /= 1f * (float)targetController.containers.Count;
						chainTransform.position = new Vector3(position.x, num2, position.z);
					}
				}
				else
				{
					targetController.UpdateDisplay(this, currentEndPosition);
				}
				if (addTargetController.isHidden)
				{
					addTargetController.Show();
				}
				if (addTargetController.ContainerCount > 0)
				{
					addTargetController.UpdateDisplay(this, targetController.EndPosition);
				}
				else
				{
					addTargetController.Display(this, targetController.EndPosition);
				}
				UpdateTargetCallback();
			}
			else
			{
				targetController.Clear();
				if (!addTargetController.isHidden)
				{
					addTargetController.Hide();
				}
			}
			if (flag2)
			{
				if (machineDamageController.isHidden)
				{
					machineDamageController.Show();
				}
				if (machineDamageController.ContainerCount > 0)
				{
					machineDamageController.UpdateDisplay(this, currentEndPosition);
				}
				else
				{
					machineDamageController.Display(this, currentEndPosition);
				}
			}
			else if (!machineDamageController.isHidden)
			{
				machineDamageController.Hide();
			}
			if (flag3)
			{
				if (varController.isHidden)
				{
					varController.Show();
				}
				if (varController.ContainerCount > 0)
				{
					varController.UpdateDisplay(this, currentEndPosition);
				}
				else
				{
					varController.Display(this, currentEndPosition);
				}
			}
			else if (!varController.isHidden)
			{
				varController.Hide();
			}
			if (flag5)
			{
				if (moddedController.isHidden)
				{
					moddedController.Show();
				}
				if (moddedController.ContainerCount > 0)
				{
					moddedController.UpdateDisplay(this, currentEndPosition);
				}
				else
				{
					moddedController.Display(this, currentEndPosition);
				}
			}
			else if (!moddedController.isHidden)
			{
				moddedController.Hide();
			}
			if (flag4)
			{
				if (keyChangeController.isHidden)
				{
					keyChangeController.Show();
				}
				if (keyChangeController.ContainerCount > 0)
				{
					keyChangeController.UpdateDisplay(this, currentEndPosition);
				}
				else
				{
					keyChangeController.Display(this, currentEndPosition);
				}
			}
			else if (!keyChangeController.isHidden)
			{
				keyChangeController.Hide();
			}
			bool flag8 = false;
			if (Value.events.Count != eventController.ContainerCount)
			{
				flag8 = true;
				eventController.Clear();
				for (num = 0; num < Value.events.Count; num++)
				{
					eventController.RegisterToggle(Value);
				}
			}
			float startPosition = (flag ? addTargetController.EndPosition : (flag2 ? machineDamageController.EndPosition : (flag3 ? varController.EndPosition : ((!flag4) ? currentEndPosition : keyChangeController.EndPosition))));
			if (flag5)
			{
				startPosition = moddedController.EndPosition;
			}
			if (flag8)
			{
				eventController.Display(this, startPosition);
			}
			else
			{
				eventController.UpdateDisplay(this, startPosition);
			}
			addEventController.UpdateDisplay(this, eventController.EndPosition);
			float num3 = 1f / base.transform.lossyScale.y;
			float num4 = addEventController.EndPosition * num3;
			Transform transform = container.Background.transform;
			float x = transform.localScale.x;
			if (x != num4)
			{
				transform.localScale = new Vector3(transform.localScale.x, num4, transform.localScale.z);
				BlockMapper currentInstance = BlockMapper.CurrentInstance;
				if (currentInstance != null)
				{
					currentInstance.IsDirty = true;
				}
			}
		}

		private void Awake()
		{
			chainRenderer = chainIcon.GetComponent<MeshRenderer>();
			chainGO = chainIcon.gameObject;
			chainTransform = chainIcon.transform;
			chainIcon.Click += ToggleAllTargets;
			string text = "Prefabs/BlockMapper/LevelEditor/";
			headerController = new WidgetController(text + "LogicHeader");
			machineDamageController = new WidgetController(text + "MachineDamageContainer");
			varController = new WidgetController(text + "VarContainer");
			keyChangeController = new WidgetController(text + "KeyChangeContainer");
			targetController = new WidgetController(text + "TargetContainer");
			addTargetController = new WidgetController(text + "AddTargetContainer");
			eventController = new WidgetController(text + "EventContainer");
			addEventController = new WidgetController(text + "AddEventContainer");
			moddedController = new WidgetController(text + "ModdedTriggerContainer");
			logicHandler = EditLogicHandler.Instance;
			hasHandler = logicHandler != null;
		}

		public void OnDisable()
		{
			if (updateCallback)
			{
				if (Logic != null)
				{
					Logic.LogicChanged -= OnLogicChange;
				}
				updateCallback = false;
			}
		}

		private void ToggleAllTargets()
		{
			Value.allTargets = !Value.allTargets;
			UpdateChainIcon();
			if (hasHandler)
			{
				logicHandler.OnEditLogic(Value);
			}
		}

		private void UpdateChainIcon()
		{
			chainRenderer.material.mainTexture = ((!Value.allTargets) ? chainBroken : chain);
		}

		public override void Init()
		{
			base.Init();
			RefreshWidgetList();
			Rebuild();
		}

		public override void ResetToPool()
		{
			if (updateCallback)
			{
				if (Logic != null)
				{
					Logic.LogicChanged -= OnLogicChange;
				}
				updateCallback = false;
			}
			Clear();
			base.ResetToPool();
		}

		public void Clear()
		{
			headerController.Clear();
			varController.Clear();
			keyChangeController.Clear();
			machineDamageController.Clear();
			targetController.Clear();
			addTargetController.Clear();
			eventController.Clear();
			addEventController.Clear();
			moddedController.Clear();
		}

		private void RefreshWidgetList()
		{
			Clear();
			headerController.RegisterToggle(Value);
			addTargetController.RegisterToggle();
			varController.RegisterToggle(Value);
			keyChangeController.RegisterToggle(Value);
			machineDamageController.RegisterToggle(Value);
			addEventController.RegisterToggle();
			moddedController.RegisterToggle(Value);
			for (int i = 0; i < Value.targets.Count; i++)
			{
				targetController.RegisterToggle(Value);
			}
			for (int i = 0; i < Value.events.Count; i++)
			{
				eventController.RegisterToggle(Value);
			}
		}

		public void OnRemove()
		{
			if (hasHandler)
			{
				logicHandler.OnRemoveLogic(Value);
			}
		}

		public void OnRemoveTarget(int index)
		{
			if (hasHandler && Value.targets.Count > index)
			{
				logicHandler.OnRemoveTarget(Value, Value.targets[index]);
			}
		}

		public void OnRemoveEvent(int index)
		{
			if (hasHandler && Value.events.Count > index)
			{
				logicHandler.OnRemoveEvent(Value, Value.events[index]);
			}
		}

		public void OnMoveEvent(int index, bool isDown)
		{
			if (hasHandler && Value.events.Count > index)
			{
				logicHandler.OnMoveEvent(Value, Value.events[index], isDown);
			}
		}

		public void OnSortBehaviour(int index)
		{
			if (hasHandler && Value.events.Count > index)
			{
				logicHandler.OnSortBehaviour(Value, Value.events[index]);
			}
		}

		private void OnAddTarget()
		{
			if (hasHandler)
			{
				logicHandler.OnAddTarget(Value);
			}
		}

		private void OnAddEvent()
		{
			if (hasHandler)
			{
				logicHandler.OnAddEvent(Value);
			}
		}

		private void UpdateTargetCallback()
		{
			UIButton componentInChildren = addTargetController.Widget.GetComponentInChildren<UIButton>();
			if (componentInChildren != addTargetButton)
			{
				if (addTargetButton != null)
				{
					addTargetButton.Click -= OnAddTarget;
				}
				addTargetButton = componentInChildren;
				addTargetButton.Click += OnAddTarget;
			}
		}

		private void Rebuild()
		{
			StatMaster.Mode.pickMode = StatMaster.Mode.PickMode.None;
			headerController.Display(this, 0f);
			bool flag = Value.IsTrigger();
			bool flag2 = Value.triggerType == TriggerType.MachineDamage;
			bool flag3 = Value.triggerType == TriggerType.Variable;
			bool flag4 = Value.IsKeyChange();
			bool flag5 = Value.triggerType == TriggerType.Modded;
			if (flag)
			{
				targetController.Display(this, headerController.EndPosition);
				addTargetController.Display(this, targetController.EndPosition);
				if (targetController.containers.Count > 1)
				{
					if (!chainGO.activeSelf)
					{
						chainGO.SetActive(true);
					}
					UpdateChainIcon();
				}
				else if (chainGO.activeSelf)
				{
					chainGO.SetActive(false);
				}
				UpdateTargetCallback();
				if (chainGO.activeSelf)
				{
					Vector3 position = chainTransform.position;
					float num = 0f;
					for (int i = 0; i < targetController.containers.Count; i++)
					{
						num += targetController.containers[i].transform.position.y;
					}
					num /= 1f * (float)targetController.containers.Count;
					chainTransform.position = new Vector3(position.x, num, position.z);
				}
			}
			else
			{
				if (flag2)
				{
					machineDamageController.Display(this, headerController.EndPosition);
				}
				else if (flag3)
				{
					varController.Display(this, headerController.EndPosition);
				}
				else if (flag4)
				{
					keyChangeController.Display(this, headerController.EndPosition);
				}
				else if (flag5)
				{
					moddedController.Display(this, headerController.EndPosition);
				}
				if (chainGO.activeSelf)
				{
					chainGO.SetActive(false);
				}
			}
			WidgetController widgetController = (flag ? addTargetController : (flag2 ? machineDamageController : (flag3 ? varController : ((!flag4) ? headerController : keyChangeController))));
			if (flag5)
			{
				widgetController = moddedController;
			}
			eventController.Display(this, widgetController.EndPosition);
			addEventController.Display(this, eventController.EndPosition);
			UIButton componentInChildren = addEventController.Widget.GetComponentInChildren<UIButton>();
			if (addEventButton != componentInChildren)
			{
				addEventButton = componentInChildren;
				addEventButton.ResetDelegates();
				addEventButton.Click += OnAddEvent;
			}
			float num2 = 1f / base.transform.lossyScale.y;
			float y = addEventController.EndPosition * num2;
			Transform transform = container.Background.transform;
			transform.localScale = new Vector3(transform.localScale.x, y, transform.localScale.z);
		}
	}
}

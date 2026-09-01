using System.Collections.Generic;
using System.Linq;
using InternalModding.Loading;
using InternalModding.Mods;
using Modding;
using Ordered;
using UnityEngine;

namespace InternalModding.Events
{
	public class ModdedEventDisplay : PickEventDisplay
	{
		public static bool IsInitializing;

		public DynamicText Title;

		public MeshRenderer Icon;

		public UIButton NextModded;

		public UIButton PrevModded;

		public float TitleHeight = 0.3f;

		public float PropertyLineHeight = 0.48f;

		private ModdedEventContainer eventContainer;

		private float StartY = -0.58f;

		private System.Collections.Generic.Dictionary<EventProperty, Component> Displays;

		private int LineCount;

		protected override void Awake()
		{
			base.Awake();
			NextModded.Click += OnNextModded;
			PrevModded.Click += OnPrevModded;
			ModReloading.OnModReload += OnModReload;
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();
			ModReloading.OnModReload -= OnModReload;
		}

		public override void Init(LogicEventWidget parentWidget, EntityLogic inLogic, EntityEvent inEvent)
		{
			eventContainer = inEvent.eventData as ModdedEventContainer;
			if (eventContainer.Event == null)
			{
				parentWidget.OnRemove();
				return;
			}
			IsInitializing = true;
			if (Displays == null)
			{
				Displays = new System.Collections.Generic.Dictionary<EventProperty, Component>();
			}
			else
			{
				foreach (Component value in Displays.Values)
				{
					Object.Destroy(value.gameObject);
				}
				Displays.Clear();
			}
			List<EventProperty> list = eventContainer.Properties.Values.OrderBy((EventProperty p) => p.Row).ToList();
			Ordered.Dictionary<Transform, float> dictionary = new Ordered.Dictionary<Transform, float>();
			int num = 0;
			LineCount = 0;
			IEnumerable<EventProperty.Picker> source = list.OfType<EventProperty.Picker>();
			if (source.Count() != 0)
			{
				mode = source.ElementAt(0).Mode;
			}
			float num2 = StartY;
			Vector3 localPosition;
			for (int num3 = 0; num3 < list.Count; num3++)
			{
				EventProperty eventProperty = list[num3];
				if (eventProperty is EventProperty.Picker)
				{
					continue;
				}
				eventProperty.SetEvent(null, null);
				Component component = eventProperty.CreateDisplay();
				Displays.Add(eventProperty, component);
				Transform transform = component.transform;
				transform.parent = base.transform;
				dictionary.Add(transform, eventProperty.X);
				if (num3 != list.Count - 1 && list[num3 + 1].Row == num)
				{
					continue;
				}
				foreach (KeyValuePair<Transform, float> item in dictionary)
				{
					localPosition = item.Key.localPosition;
					localPosition.x = ((dictionary.Count != 1) ? (item.Value + 0.22f) : 0.22f);
					localPosition.y = num2;
					localPosition.z = -0.1f;
					item.Key.localPosition = localPosition;
				}
				num2 -= PropertyLineHeight;
				dictionary.Clear();
				LineCount++;
				num++;
			}
			foreach (EventProperty item2 in list)
			{
				item2.SetEvent(inLogic, inEvent);
			}
			localPosition = pickWidgetPosition;
			localPosition.y = num2;
			pickWidgetPosition = localPosition;
			base.Init(parentWidget, inLogic, inEvent);
			IsInitializing = false;
		}

		public override void ToggleHover(bool toggle)
		{
			base.ToggleHover(toggle);
			NextModded.gameObject.SetActive(toggle);
			PrevModded.gameObject.SetActive(toggle);
		}

		public override void UpdateVisual()
		{
			base.UpdateVisual();
			if (eventContainer.Event != null)
			{
				if (!eventContainer.Event.HasPicker)
				{
					ResetToPool();
				}
				Title.SetText(eventContainer.Event.Name);
				Texture2D mainTexture = ((eventContainer.Event.Icon == null) ? SingleInstanceFindOnly<EventLoader>.Instance.DefaultIcon : ((Texture2D)eventContainer.Event.Icon));
				Icon.material.mainTexture = mainTexture;
				UpdateBackground();
				UpdateBottomLine();
			}
		}

		protected override void UpdateBackground()
		{
			backgroundTransform.localScale = new Vector3(backgroundTransform.localScale.x, TitleHeight + (defaultHeight - pickSpacer) + (float)pickWidgets.Count * pickSpacer + (float)LineCount * PropertyLineHeight, backgroundTransform.localScale.z);
		}

		private void OnNextModded()
		{
			ModdedEvent moddedEvent = SingleInstanceFindOnly<EventLoader>.Instance.NextEvent(eventContainer.Event);
			eventContainer.Event = moddedEvent;
			UpdateVisual();
			BlockMapper.CurrentInstance.IsDirty = true;
			OnEditEvent();
		}

		private void OnPrevModded()
		{
			ModdedEvent moddedEvent = SingleInstanceFindOnly<EventLoader>.Instance.PreviousEvent(eventContainer.Event);
			eventContainer.Event = moddedEvent;
			UpdateVisual();
			BlockMapper.CurrentInstance.IsDirty = true;
			OnEditEvent();
		}

		private void OnModReload(ModContainer mod, ModInfo newInfo)
		{
			UpdateVisual();
			BlockMapper.CurrentInstance.IsDirty = true;
			OnEditEvent();
		}
	}
}

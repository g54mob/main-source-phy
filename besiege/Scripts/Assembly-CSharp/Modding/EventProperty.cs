using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Xml.Serialization;
using InternalModding.Events;
using InternalModding.Mods;
using Modding.Levels;
using Modding.Serialization;
using Selectors;
using UnityEngine;

namespace Modding
{
	[Serializable]
	public abstract class EventProperty : Element
	{
		[Serializable]
		public class Picker : EventProperty
		{
			[XmlIgnore]
			public List<Entity> Entities;

			[XmlAttribute("mode")]
			public StatMaster.Mode.PickMode Mode;

			public Picker()
			{
				Entities = new List<Entity>();
			}

			internal override string Save()
			{
				return Mode.ToString();
			}

			internal override void Load(string data)
			{
				Mode = (StatMaster.Mode.PickMode)(int)Enum.Parse(typeof(StatMaster.Mode.PickMode), data);
			}

			internal override EventProperty CreateInstance()
			{
				return CopyAttributes(new Picker
				{
					Mode = Mode
				});
			}

			internal override UnityEngine.Component CreateDisplay()
			{
				return null;
			}

			internal override void UpdateDisplay(UnityEngine.Component c)
			{
			}
		}

		[Serializable]
		public class Icon : EventProperty
		{
			[RequireToValidate]
			[XmlElement("Icon")]
			public ResourceReference IconReference;

			[XmlIgnore]
			public ModTexture IconTexture;

			public Icon()
			{
				nameRequired = false;
			}

			internal override string Save()
			{
				return string.Empty;
			}

			internal override void Load(string data)
			{
			}

			internal override EventProperty CreateInstance()
			{
				return CopyAttributes(new Icon
				{
					IconReference = IconReference,
					IconTexture = IconTexture
				});
			}

			internal override void LoadAssets(ModContainer mod)
			{
				IconTexture = (ModTexture)ModResource.Get(IconReference, mod);
			}

			internal override UnityEngine.Component CreateDisplay()
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<EventLoader>.Instance.IconPrefab);
				MeshRenderer mr = gameObject.GetComponent<MeshRenderer>();
				IconTexture.OnLoad += delegate
				{
					if (mr != null)
					{
						mr.material.mainTexture = (Texture2D)IconTexture;
					}
				};
				return mr;
			}

			internal override void UpdateDisplay(UnityEngine.Component c)
			{
			}
		}

		[Serializable]
		public class Text : EventProperty
		{
			[XmlText]
			public string DisplayText;

			[XmlAttribute("fontSize")]
			[DefaultValue(0.15f)]
			public float FontSize = 0.15f;

			public Text()
			{
				nameRequired = false;
			}

			internal override string Save()
			{
				return string.Empty;
			}

			internal override void Load(string data)
			{
			}

			internal override EventProperty CreateInstance()
			{
				return CopyAttributes(new Text
				{
					DisplayText = DisplayText,
					FontSize = FontSize
				});
			}

			internal override UnityEngine.Component CreateDisplay()
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<EventLoader>.Instance.TextPrefab);
				DynamicText component = gameObject.GetComponent<DynamicText>();
				component.SetText(DisplayText);
				component.size = FontSize;
				return gameObject.transform;
			}

			internal override void UpdateDisplay(UnityEngine.Component c)
			{
			}
		}

		[Serializable]
		public class TextInput : EventProperty
		{
			private string _text = string.Empty;

			[DefaultValue("")]
			[XmlAttribute("default")]
			public string DefaultText;

			[XmlAttribute("title")]
			public string Title;

			[XmlAttribute("maxChars")]
			[DefaultValue(16)]
			public int MaxChars = 16;

			[XmlIgnore]
			public new string Text
			{
				get
				{
					return _text;
				}
				set
				{
					_text = value;
					OnEdit();
				}
			}

			internal override string Save()
			{
				return Text;
			}

			internal override void Load(string data)
			{
				Text = data;
			}

			internal override EventProperty CreateInstance()
			{
				return CopyAttributes(new TextInput
				{
					Title = Title,
					MaxChars = MaxChars,
					DefaultText = DefaultText,
					Text = DefaultText
				});
			}

			internal override UnityEngine.Component CreateDisplay()
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<EventLoader>.Instance.TextInputPrefab);
				DynamicText component = gameObject.transform.FindChild("Title").GetComponent<DynamicText>();
				component.SetText(Title);
				TextHolder component2 = gameObject.GetComponent<TextHolder>();
				component2.CharLimit = MaxChars;
				component2.SetText(Text);
				component2.TextChanged += delegate(string newText)
				{
					Text = newText;
				};
				return component2;
			}

			internal override void UpdateDisplay(UnityEngine.Component c)
			{
				TextHolder textHolder = (TextHolder)c;
				textHolder.SetText(Text);
			}
		}

		public class NumberInput : EventProperty
		{
			private float _value;

			[XmlAttribute("title")]
			public string Title;

			[DefaultValue(0f)]
			[XmlAttribute("default")]
			public float DefaultValue;

			[XmlIgnore]
			public bool DefaultValueSpecified;

			[XmlAttribute("maxChars")]
			[DefaultValue(6)]
			public int CharLimit = 6;

			[XmlAttribute("prefix")]
			[DefaultValue("")]
			public string Prefix = string.Empty;

			[DefaultValue("")]
			[XmlAttribute("suffix")]
			public string Suffix = string.Empty;

			[XmlAttribute("decimals")]
			[DefaultValue(2)]
			public int Decimals = 2;

			[DefaultValue(2)]
			[XmlAttribute("maxDecimals")]
			public int MaxDecimals = 2;

			[DefaultValue(false)]
			[XmlAttribute("splitThousands")]
			public bool SplitThousand;

			[XmlAttribute("negativeNumbers")]
			[DefaultValue(true)]
			public bool NegativeNumbers = true;

			[XmlAttribute("maxValue")]
			[DefaultValue(float.PositiveInfinity)]
			public float MaxValue = float.PositiveInfinity;

			[XmlAttribute("minValue")]
			[DefaultValue(float.NegativeInfinity)]
			public float MinValue = float.NegativeInfinity;

			[XmlIgnore]
			public float Value
			{
				get
				{
					return _value;
				}
				set
				{
					_value = value;
					OnEdit();
				}
			}

			internal override string Save()
			{
				return Value.ToString(StaticSettings.Culture);
			}

			internal override void Load(string data)
			{
				float result;
				if (float.TryParse(data, NumberStyles.Float | NumberStyles.AllowThousands, StaticSettings.Culture, out result))
				{
					Value = result;
					return;
				}
				Debug.LogError("Error loading ValueHolder, can't parse float: " + data);
				Value = ((!DefaultValueSpecified) ? MinValue : DefaultValue);
			}

			internal override EventProperty CreateInstance()
			{
				return CopyAttributes(new NumberInput
				{
					Title = Title,
					CharLimit = CharLimit,
					Prefix = Prefix,
					Suffix = Suffix,
					Decimals = Decimals,
					MaxDecimals = MaxDecimals,
					SplitThousand = SplitThousand,
					NegativeNumbers = NegativeNumbers,
					MaxValue = MaxValue,
					MinValue = MinValue,
					DefaultValue = DefaultValue,
					DefaultValueSpecified = DefaultValueSpecified,
					Value = ((!DefaultValueSpecified) ? MinValue : DefaultValue)
				});
			}

			internal override UnityEngine.Component CreateDisplay()
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<EventLoader>.Instance.NumberInputPrefab);
				DynamicText component = gameObject.transform.FindChild("Title").GetComponent<DynamicText>();
				component.SetText(Title);
				ValueHolder component2 = gameObject.GetComponent<ValueHolder>();
				component2.CharLimit = CharLimit;
				component2.prefix = Prefix;
				component2.suffix = Suffix;
				component2.Decimals = Decimals;
				component2.MaxDecimals = MaxDecimals;
				component2.splitThousands = SplitThousand;
				component2.negativeNumbers = NegativeNumbers;
				component2.maxValue = MaxValue;
				component2.minValue = MinValue;
				component2.SetValue(Value);
				component2.ValueChanged += delegate(float newValue)
				{
					Value = newValue;
				};
				return component2;
			}

			internal override void UpdateDisplay(UnityEngine.Component c)
			{
				ValueHolder valueHolder = (ValueHolder)c;
				valueHolder.SetText(Value);
			}
		}

		[Serializable]
		public class Choice : EventProperty
		{
			[Serializable]
			public class Option : Element
			{
				[XmlText]
				public string Text;

				[XmlAttribute("index")]
				public int Index;

				[XmlIgnore]
				public bool IndexSpecified;
			}

			private int _currentIndex;

			[RequireToValidate]
			[XmlElement("Option")]
			public Option[] Options;

			[XmlAttribute("default")]
			public int Default;

			[XmlIgnore]
			public bool DefaultSpecified;

			[XmlIgnore]
			public int CurrentIndex
			{
				get
				{
					return _currentIndex;
				}
				set
				{
					_currentIndex = value;
					OnEdit();
				}
			}

			internal override string Save()
			{
				return Options[CurrentIndex].Index.ToString(StaticSettings.Culture);
			}

			internal override void Load(string data)
			{
				int index = int.Parse(data);
				SetIndex(index);
			}

			public void SetIndex(int index)
			{
				CurrentIndex = -1;
				for (int i = 0; i < Options.Length; i++)
				{
					if (index == Options[i].Index)
					{
						CurrentIndex = i;
						break;
					}
				}
				if (CurrentIndex == -1)
				{
					Debug.LogError("Error setting Choice index: Index " + index + " not found.");
					CurrentIndex = 0;
				}
			}

			internal override EventProperty CreateInstance()
			{
				return CopyAttributes(new Choice
				{
					Name = Name,
					Row = Row,
					Options = Options,
					Default = Default,
					DefaultSpecified = DefaultSpecified,
					CurrentIndex = (DefaultSpecified ? Default : 0)
				});
			}

			internal override UnityEngine.Component CreateDisplay()
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<EventLoader>.Instance.ChoicePrefab);
				ChoiceHolder component = gameObject.GetComponent<ChoiceHolder>();
				component.Options = Options.Select((Option o) => o.Text).ToArray();
				component.SetChoice(CurrentIndex);
				component.OptionChanged += delegate(int newChoice)
				{
					CurrentIndex = newChoice;
				};
				return component;
			}

			internal override void UpdateDisplay(UnityEngine.Component c)
			{
				ChoiceHolder choiceHolder = (ChoiceHolder)c;
				choiceHolder.SetChoiceNoEvent(CurrentIndex);
			}
		}

		public class Toggle : EventProperty
		{
			private bool _value;

			[XmlAttribute("default")]
			public bool Default;

			[XmlIgnore]
			public bool DefaultSpecified;

			[RequireToValidate]
			[XmlElement("Icon")]
			public ResourceReference IconReference;

			[XmlIgnore]
			public new ModTexture Icon;

			[XmlIgnore]
			public bool Value
			{
				get
				{
					return _value;
				}
				set
				{
					_value = value;
					OnEdit();
				}
			}

			internal override string Save()
			{
				return Value.ToString();
			}

			internal override void Load(string data)
			{
				bool result;
				if (!bool.TryParse(data, out result))
				{
					Debug.LogError("Can't parse value of Toggle property!");
					result = DefaultSpecified && Default;
				}
				Value = result;
			}

			internal override EventProperty CreateInstance()
			{
				return CopyAttributes(new Toggle
				{
					Name = Name,
					Row = Row,
					Default = Default,
					DefaultSpecified = DefaultSpecified,
					Value = (DefaultSpecified && Default),
					IconReference = IconReference,
					Icon = Icon
				});
			}

			internal override void LoadAssets(ModContainer mod)
			{
				Icon = (ModTexture)ModResource.Get(IconReference, mod);
			}

			internal override UnityEngine.Component CreateDisplay()
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<EventLoader>.Instance.TogglePrefab);
				MeshRenderer icon = gameObject.transform.FindChild("Icon").GetComponent<MeshRenderer>();
				Icon.OnLoad += delegate
				{
					if (icon != null)
					{
						icon.material.mainTexture = (Texture2D)Icon;
					}
				};
				ToggleHolder component = gameObject.GetComponent<ToggleHolder>();
				component.SetValue(Value);
				component.ValueChanged += delegate(bool newValue)
				{
					Value = newValue;
				};
				return component;
			}

			internal override void UpdateDisplay(UnityEngine.Component c)
			{
				ToggleHolder toggleHolder = (ToggleHolder)c;
				toggleHolder.SetValueNoEvent(Value);
			}
		}

		public class TeamButton : EventProperty
		{
			private MPTeam _team;

			public MPTeam Team
			{
				get
				{
					return _team;
				}
				set
				{
					_team = value;
					OnEdit();
				}
			}

			internal override string Save()
			{
				return Team.ToString();
			}

			internal override void Load(string data)
			{
				try
				{
					Team = (MPTeam)(int)Enum.Parse(typeof(MPTeam), data);
				}
				catch (ArgumentException)
				{
					Debug.LogError("Could not load MPTeam value: " + data);
					Team = MPTeam.None;
				}
			}

			internal override EventProperty CreateInstance()
			{
				return CopyAttributes(new TeamButton());
			}

			internal override UnityEngine.Component CreateDisplay()
			{
				GameObject gameObject = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<EventLoader>.Instance.TeamButtonPrefab);
				TeamButtonHolder component = gameObject.GetComponent<TeamButtonHolder>();
				component.SetValue(Team);
				component.TeamChanged += delegate(MPTeam newTeam)
				{
					Team = newTeam;
				};
				return component;
			}

			internal override void UpdateDisplay(UnityEngine.Component c)
			{
				TeamButtonHolder teamButtonHolder = (TeamButtonHolder)c;
				teamButtonHolder.SetValue(Team);
			}
		}

		[XmlAttribute("name")]
		[DefaultValue("")]
		public string Name;

		protected bool nameRequired = true;

		[XmlIgnore]
		public bool XSpecified;

		[XmlIgnore]
		public int Row;

		protected EntityLogic Logic;

		protected EntityEvent Event;

		[DefaultValue(0f)]
		[XmlAttribute("x")]
		public float X { get; set; }

		internal abstract string Save();

		internal abstract void Load(string data);

		internal abstract EventProperty CreateInstance();

		internal abstract UnityEngine.Component CreateDisplay();

		internal abstract void UpdateDisplay(UnityEngine.Component display);

		internal virtual EventProperty SetEvent(EntityLogic logic, EntityEvent evt)
		{
			Logic = logic;
			Event = evt;
			return this;
		}

		protected virtual void OnEdit()
		{
			if (Logic != null && Event != null && !ModdedEventDisplay.IsInitializing)
			{
				EditLogicHandler.Instance.OnEditEvent(Logic, Event);
			}
		}

		internal virtual void LoadAssets(ModContainer mod)
		{
		}

		protected EventProperty CopyAttributes(EventProperty dst)
		{
			dst.Name = Name;
			dst.X = X;
			dst.XSpecified = XSpecified;
			dst.Row = Row;
			return dst;
		}

		protected override bool Validate(string elementName)
		{
			if (!base.Validate(elementName))
			{
				return false;
			}
			if (nameRequired && string.IsNullOrEmpty(Name))
			{
				return MissingAttribute(elementName, "name");
			}
			return true;
		}

		public override string ToString()
		{
			return Save();
		}
	}
}

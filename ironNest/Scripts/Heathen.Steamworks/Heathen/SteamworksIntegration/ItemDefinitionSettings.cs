using System;
using System.Collections.Generic;
using Steamworks;
using UnityEngine;
using UnityEngine.Serialization;

namespace Heathen.SteamworksIntegration
{
	[Serializable]
	[HelpURL("https://kb.heathen.group/steamworks/features/inventory")]
	public class ItemDefinitionSettings
	{
		[Serializable]
		public class LanguageVariant
		{
			public LanguageCodes language;

			public string value;
		}

		[Serializable]
		public class LanguageVariantNode
		{
			[HideInInspector]
			public string node;

			public string value;

			public List<LanguageVariant> variants;

			public string GetSimpleValue()
			{
				return null;
			}

			public override string ToString()
			{
				return null;
			}

			public void Populate(SteamItemDef_t itemDefId)
			{
			}
		}

		[Serializable]
		public class Bundle
		{
			[Serializable]
			public struct Entry
			{
				[SerializeReference]
				public ItemDefinitionSettings item;

				public int count;

				public bool Valid => false;

				public override string ToString()
				{
					return null;
				}
			}

			public List<Entry> entries;

			public bool Valid => false;

			public override string ToString()
			{
				return null;
			}
		}

		[Serializable]
		public class PromoRule
		{
			[Serializable]
			public struct PlayedEntry
			{
				public AppId_t app;

				public uint minutes;
			}

			public List<AppId_t> owns;

			public List<string> achievements;

			public List<PlayedEntry> played;

			public bool manual;

			public bool Valid => false;

			public override string ToString()
			{
				return null;
			}

			public void Populate(SteamItemDef_t itemDefId)
			{
			}
		}

		[Serializable]
		public struct ExchangeRecipe
		{
			[Serializable]
			public struct Material
			{
				[Serializable]
				public struct ItemDefDescriptor
				{
					public int item;

					public uint count;

					public override string ToString()
					{
						return null;
					}
				}

				[Serializable]
				public struct ItemTagDescriptor
				{
					public string name;

					public string value;

					public uint count;

					public override string ToString()
					{
						return null;
					}
				}

				public ItemDefDescriptor item;

				public ItemTagDescriptor tag;

				public bool Valid => false;

				public override string ToString()
				{
					return null;
				}
			}

			public List<Material> materials;

			public bool Valid => false;

			public string GetSchema()
			{
				return null;
			}
		}

		[Serializable]
		public class ExchangeCollection
		{
			public List<ExchangeRecipe> recipe;

			public override string ToString()
			{
				return null;
			}
		}

		[Serializable]
		public struct Price
		{
			[Serializable]
			public struct Value
			{
				public string currency;

				public uint value;

				public bool Valid => false;

				public override string ToString()
				{
					return null;
				}
			}

			[Serializable]
			public struct PriceList
			{
				[Serializable]
				public struct PriceCollection
				{
					public Value[] values;

					public bool Valid => false;

					public override string ToString()
					{
						return null;
					}
				}

				[Serializable]
				public struct ChangeCollection
				{
					public string fromDate;

					public string untilDate;

					public PriceCollection prices;

					public bool Valid => false;

					public override string ToString()
					{
						return null;
					}
				}

				public PriceCollection original;

				public ChangeCollection[] changes;

				public bool Valid => false;

				public override string ToString()
				{
					return null;
				}
			}

			public uint version;

			public bool usePricing;

			public bool useCategory;

			public ValvePriceCategories category;

			public PriceList priceList;

			public string Node => null;

			public bool Valid => false;

			public override string ToString()
			{
				return null;
			}

			public void Populate(SteamItemDef_t itemDefId)
			{
			}
		}

		[Serializable]
		public struct Colour
		{
			public bool use;

			public Color color;

			public override string ToString()
			{
				return null;
			}
		}

		[Serializable]
		public class TagCollection
		{
			public List<ItemTag> tags;

			public override string ToString()
			{
				return null;
			}

			public void Populate(SteamItemDef_t itemDefId)
			{
			}
		}

		[Serializable]
		public struct TagGeneratorValue
		{
			public string value;

			public uint weight;

			public bool Valid => false;

			public override string ToString()
			{
				return null;
			}
		}

		[Serializable]
		public class TagGeneratorValues
		{
			public List<TagGeneratorValue> values;

			public bool Valid => false;

			public override string ToString()
			{
				return null;
			}

			public void Populate(SteamItemDef_t itemDefId)
			{
			}
		}

		[Serializable]
		public class ExtendedSchema
		{
			[Serializable]
			public struct Entry
			{
				public string node;

				public string value;

				public override string ToString()
				{
					return null;
				}
			}

			public List<Entry> entries;

			public override string ToString()
			{
				return null;
			}
		}

		[SerializeField]
		public int id;

		[FormerlySerializedAs("item_type")]
		[SerializeField]
		public InventoryItemType itemType;

		[FormerlySerializedAs("item_name")]
		[SerializeField]
		public LanguageVariantNode itemName;

		[FormerlySerializedAs("item_description")]
		[SerializeField]
		public LanguageVariantNode itemDescription;

		[FormerlySerializedAs("item_display_type")]
		[SerializeField]
		public LanguageVariantNode itemDisplayType;

		[FormerlySerializedAs("itemBackgroundColor")]
		[FormerlySerializedAs("item_background_color")]
		[SerializeField]
		public Colour itemBackgroundColour;

		[FormerlySerializedAs("itemNameColor")]
		[FormerlySerializedAs("item_name_color")]
		[SerializeField]
		public Colour itemNameColour;

		[FormerlySerializedAs("item_icon_url")]
		[SerializeField]
		public string itemIconURL;

		[FormerlySerializedAs("item_icon_url_large")]
		[SerializeField]
		public string itemIconURLLarge;

		[FormerlySerializedAs("item_marketable")]
		[SerializeField]
		public bool itemMarketable;

		[FormerlySerializedAs("item_tradable")]
		[SerializeField]
		public bool itemTradable;

		[FormerlySerializedAs("item_store_tags")]
		[SerializeField]
		public List<string> itemStoreTags;

		[FormerlySerializedAs("item_store_images")]
		[SerializeField]
		public List<string> itemStoreImages;

		private ItemChangedEvent _eventChanged;

		public ItemData Data
		{
			get
			{
				return default(ItemData);
			}
			set
			{
			}
		}

		public List<ItemDetail> Details => null;

		public long TotalQuantity => 0L;

		public string DisplayName => null;

		public bool HasPrice => false;

		public Currency.Code CurrencyCode => default(Currency.Code);

		public string CurrencySymbol => null;

		public ulong CurrentPrice => 0uL;

		public ulong BasePrice => 0uL;

		public ItemChangedEvent EventChanged => null;

		public InventoryItemType Type => default(InventoryItemType);

		public string Name
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public string Description => null;

		public string DisplayType => null;

		public SteamItemDef_t Id
		{
			get
			{
				return default(SteamItemDef_t);
			}
			set
			{
			}
		}

		public Colour BackgroundColour => default(Colour);

		public Colour NameColour => default(Colour);

		public string IconUrl => null;

		public string IconUrlLarge => null;

		public bool Marketable => false;

		public bool Tradable => false;

		public string[] StoreTags => null;

		public string[] StoreImages => null;
	}
}

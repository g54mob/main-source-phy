using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	public class CGModuleSlot
	{
		protected List<CGModuleSlot> mLinkedSlots;

		public CGModule Module { get; internal set; }

		public SlotInfo Info { get; internal set; }

		public Vector2 Origin { get; set; }

		public Rect DropZone { get; set; }

		public bool IsLinked
		{
			get
			{
				if (LinkedSlots != null)
				{
					return LinkedSlots.Count > 0;
				}
				return false;
			}
		}

		public bool IsLinkedAndConfigured
		{
			get
			{
				if (!IsLinked)
				{
					return false;
				}
				for (int i = 0; i < LinkedSlots.Count; i++)
				{
					if (!LinkedSlots[i].Module.IsConfigured)
					{
						return false;
					}
				}
				return true;
			}
		}

		public IOnRequestProcessing OnRequestModule => Module as IOnRequestProcessing;

		public IOnRequestPath OnRequestPathModule => Module as IOnRequestPath;

		public IPathProvider PathProvider => Module as IPathProvider;

		public IExternalInput ExternalInput => Module as IExternalInput;

		public List<CGModuleSlot> LinkedSlots
		{
			get
			{
				if (mLinkedSlots == null)
				{
					LoadLinkedSlots();
				}
				return mLinkedSlots ?? new List<CGModuleSlot>();
			}
		}

		public int Count => LinkedSlots.Count;

		public string Name
		{
			get
			{
				if (Info == null)
				{
					return "";
				}
				return Info.Name;
			}
		}

		public bool HasLinkTo(CGModuleSlot other)
		{
			for (int i = 0; i < LinkedSlots.Count; i++)
			{
				if (LinkedSlots[i] == other)
				{
					return true;
				}
			}
			return false;
		}

		public List<CGModule> GetLinkedModules()
		{
			List<CGModule> list = new List<CGModule>();
			for (int i = 0; i < LinkedSlots.Count; i++)
			{
				list.Add(LinkedSlots[i].Module);
			}
			return list;
		}

		public virtual void LinkTo(CGModuleSlot other)
		{
			if ((bool)Module)
			{
				Module.Generator.sortModulesINTERNAL();
				Module.Dirty = true;
			}
			if ((bool)other.Module)
			{
				other.Module.Dirty = true;
			}
		}

		public virtual void UnlinkFrom(CGModuleSlot other)
		{
			if ((bool)Module)
			{
				Module.Generator.sortModulesINTERNAL();
				Module.Dirty = true;
			}
			if ((bool)other.Module)
			{
				other.Module.Dirty = true;
			}
		}

		public virtual void UnlinkAll()
		{
		}

		public void ReInitializeLinkedSlots()
		{
			mLinkedSlots = null;
		}

		public void ReInitializeLinkedTargetModules()
		{
			foreach (CGModule linkedModule in GetLinkedModules())
			{
				if (linkedModule != null)
				{
					linkedModule.ReInitializeLinkedSlots();
				}
			}
		}

		protected virtual void LoadLinkedSlots()
		{
		}

		public static implicit operator bool(CGModuleSlot a)
		{
			return a != null;
		}

		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}: {1}.{2}", GetType().Name, Module.name, Name);
		}
	}
}

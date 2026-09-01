using System;
using System.Collections.Generic;
using UnityEngine;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGModuleOutputSlot : CGModuleSlot
	{
		public CGData[] Data = new CGData[0];

		public CGDataRequestParameter[] LastRequestParameters;

		public OutputSlotInfo OutputInfo => base.Info as OutputSlotInfo;

		public bool HasData
		{
			get
			{
				if (Data != null && Data.Length != 0)
				{
					return Data[0] != null;
				}
				return false;
			}
		}

		protected override void LoadLinkedSlots()
		{
			if (!base.Module.Generator.IsInitialized)
			{
				return;
			}
			base.LoadLinkedSlots();
			mLinkedSlots = new List<CGModuleSlot>();
			foreach (CGModuleLink outputLink in base.Module.GetOutputLinks(this))
			{
				CGModule module = base.Module.Generator.GetModule(outputLink.TargetModuleID, includeOnRequestProcessing: true);
				if ((bool)module)
				{
					CGModuleInputSlot cGModuleInputSlot = module.InputByName[outputLink.TargetSlotName];
					if (!cGModuleInputSlot.Module.GetInputLink(cGModuleInputSlot, this))
					{
						cGModuleInputSlot.Module.InputLinks.Add(new CGModuleLink(cGModuleInputSlot, this));
						cGModuleInputSlot.ReInitializeLinkedSlots();
					}
					if (!mLinkedSlots.Contains(cGModuleInputSlot))
					{
						mLinkedSlots.Add(cGModuleInputSlot);
					}
				}
				else
				{
					base.Module.OutputLinks.Remove(outputLink);
				}
			}
		}

		public override void LinkTo(CGModuleSlot inputSlot)
		{
			if (!HasLinkTo(inputSlot))
			{
				if ((!inputSlot.Info.Array || inputSlot.Info.ArrayType == SlotInfo.SlotArrayType.Hidden) && inputSlot.IsLinked)
				{
					inputSlot.UnlinkAll();
				}
				base.Module.OutputLinks.Add(new CGModuleLink(this, inputSlot));
				inputSlot.Module.InputLinks.Add(new CGModuleLink(inputSlot, this));
				if (!base.LinkedSlots.Contains(inputSlot))
				{
					base.LinkedSlots.Add(inputSlot);
				}
				if (!inputSlot.LinkedSlots.Contains(this))
				{
					inputSlot.LinkedSlots.Add(this);
				}
				base.LinkTo(inputSlot);
			}
		}

		public override void UnlinkFrom(CGModuleSlot inputSlot)
		{
			if (HasLinkTo(inputSlot))
			{
				CGModuleInputSlot inSlot = (CGModuleInputSlot)inputSlot;
				CGModuleLink outputLink = base.Module.GetOutputLink(this, inSlot);
				base.Module.OutputLinks.Remove(outputLink);
				CGModuleLink inputLink = inputSlot.Module.GetInputLink(inSlot, this);
				inputSlot.Module.InputLinks.Remove(inputLink);
				base.LinkedSlots.Remove(inputSlot);
				inputSlot.LinkedSlots.Remove(this);
				base.UnlinkFrom(inputSlot);
			}
		}

		public void ClearData()
		{
			Data = new CGData[0];
		}

		public void SetData<T>(List<T> data) where T : CGData
		{
			if (data == null)
			{
				Data = new CGData[0];
				return;
			}
			if (!base.Info.Array && data.Count > 1)
			{
				Debug.LogWarning("[Curvy] " + base.Module.GetType().Name + " (" + base.Info.Name + ") only supports a single data item! Either avoid calculating unnecessary data or define the slot as an array!");
			}
			CGData[] data2 = data.ToArray();
			Data = data2;
		}

		public void SetData(params CGData[] data)
		{
			Data = ((data == null) ? new CGData[0] : data);
		}

		public T GetData<T>() where T : CGData
		{
			if (Data.Length != 0)
			{
				return Data[0] as T;
			}
			return null;
		}

		public T[] GetAllData<T>() where T : CGData
		{
			return Data as T[];
		}
	}
}

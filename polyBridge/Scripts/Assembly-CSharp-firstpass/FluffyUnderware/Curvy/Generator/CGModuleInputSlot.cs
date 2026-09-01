using System;
using System.Collections.Generic;

namespace FluffyUnderware.Curvy.Generator
{
	[Serializable]
	public class CGModuleInputSlot : CGModuleSlot
	{
		public InputSlotInfo InputInfo => base.Info as InputSlotInfo;

		protected override void LoadLinkedSlots()
		{
			if (!base.Module.Generator.IsInitialized)
			{
				return;
			}
			base.LoadLinkedSlots();
			mLinkedSlots = new List<CGModuleSlot>();
			foreach (CGModuleLink inputLink in base.Module.GetInputLinks(this))
			{
				CGModule module = base.Module.Generator.GetModule(inputLink.TargetModuleID, includeOnRequestProcessing: true);
				if ((bool)module)
				{
					CGModuleOutputSlot cGModuleOutputSlot = module.OutputByName[inputLink.TargetSlotName];
					if (!cGModuleOutputSlot.Module.GetOutputLink(cGModuleOutputSlot, this))
					{
						cGModuleOutputSlot.Module.OutputLinks.Add(new CGModuleLink(cGModuleOutputSlot, this));
						cGModuleOutputSlot.ReInitializeLinkedSlots();
					}
					if (!mLinkedSlots.Contains(cGModuleOutputSlot))
					{
						mLinkedSlots.Add(cGModuleOutputSlot);
					}
				}
				else
				{
					base.Module.InputLinks.Remove(inputLink);
				}
			}
		}

		public override void UnlinkAll()
		{
			foreach (CGModuleSlot item in new List<CGModuleSlot>(base.LinkedSlots))
			{
				UnlinkFrom(item);
			}
		}

		public override void LinkTo(CGModuleSlot outputSlot)
		{
			if (!HasLinkTo(outputSlot))
			{
				base.Module.InputLinks.Add(new CGModuleLink(this, outputSlot));
				outputSlot.Module.OutputLinks.Add(new CGModuleLink(outputSlot, this));
				if (!base.LinkedSlots.Contains(outputSlot))
				{
					base.LinkedSlots.Add(outputSlot);
				}
				if (!outputSlot.LinkedSlots.Contains(this))
				{
					outputSlot.LinkedSlots.Add(this);
				}
				base.LinkTo(outputSlot);
			}
		}

		public override void UnlinkFrom(CGModuleSlot outputSlot)
		{
			if (HasLinkTo(outputSlot))
			{
				CGModuleOutputSlot outSlot = (CGModuleOutputSlot)outputSlot;
				CGModuleLink inputLink = base.Module.GetInputLink(this, outSlot);
				base.Module.InputLinks.Remove(inputLink);
				CGModuleLink outputLink = outputSlot.Module.GetOutputLink(outSlot, this);
				outputSlot.Module.OutputLinks.Remove(outputLink);
				base.LinkedSlots.Remove(outputSlot);
				outputSlot.LinkedSlots.Remove(this);
				base.UnlinkFrom(outputSlot);
			}
		}

		public CGModuleOutputSlot SourceSlot(int index = 0)
		{
			if (index >= base.Count || index < 0)
			{
				return null;
			}
			return (CGModuleOutputSlot)base.LinkedSlots[index];
		}

		public bool CanLinkTo(CGModuleOutputSlot source)
		{
			if (source.Module != base.Module)
			{
				return AreInputAndOutputSlotsCompatible(InputInfo, base.OnRequestModule != null, source.OutputInfo, source.OnRequestModule != null);
			}
			return false;
		}

		public static bool AreInputAndOutputSlotsCompatible(InputSlotInfo inputSlotInfo, bool inputSlotModuleIsOnRequest, OutputSlotInfo outputSlotInfo, bool outputSlotModuleIsOnRequest)
		{
			if (inputSlotInfo.IsValidFrom(outputSlotInfo.DataType))
			{
				if (!outputSlotModuleIsOnRequest || !(inputSlotInfo.RequestDataOnly || inputSlotModuleIsOnRequest))
				{
					if (!outputSlotModuleIsOnRequest)
					{
						return !inputSlotInfo.RequestDataOnly;
					}
					return false;
				}
				return true;
			}
			return false;
		}

		private CGModule SourceModule(int index)
		{
			if (index >= base.Count || index < 0)
			{
				return null;
			}
			return base.LinkedSlots[index].Module;
		}

		public T GetData<T>(params CGDataRequestParameter[] requests) where T : CGData
		{
			CGData[] data = GetData<T>(0, requests);
			if (data != null && data.Length != 0)
			{
				return data[0] as T;
			}
			return null;
		}

		public List<T> GetAllData<T>(params CGDataRequestParameter[] requests) where T : CGData
		{
			List<T> list = new List<T>();
			for (int i = 0; i < base.Count; i++)
			{
				CGData[] data = GetData<T>(i, requests);
				if (data != null)
				{
					if (!base.Info.Array)
					{
						list.Add(data[0] as T);
						break;
					}
					list.Capacity += data.Length;
					for (int j = 0; j < data.Length; j++)
					{
						list.Add(data[j] as T);
					}
				}
			}
			return list;
		}

		private CGData[] GetData<T>(int slotIndex, params CGDataRequestParameter[] requests) where T : CGData
		{
			CGModuleOutputSlot cGModuleOutputSlot = SourceSlot(slotIndex);
			if (cGModuleOutputSlot == null || !cGModuleOutputSlot.Module.Active)
			{
				return new CGData[0];
			}
			if (cGModuleOutputSlot.Module is IOnRequestProcessing)
			{
				bool flag = cGModuleOutputSlot.Data == null || cGModuleOutputSlot.Data.Length == 0;
				if (!flag && cGModuleOutputSlot.LastRequestParameters != null && cGModuleOutputSlot.LastRequestParameters.Length == requests.Length)
				{
					for (int i = 0; i < requests.Length; i++)
					{
						if (!requests[i].Equals(cGModuleOutputSlot.LastRequestParameters[i]))
						{
							flag = true;
							break;
						}
					}
				}
				else
				{
					flag = true;
				}
				if (flag)
				{
					cGModuleOutputSlot.LastRequestParameters = requests;
					cGModuleOutputSlot.Module.UIMessages.Clear();
					cGModuleOutputSlot.SetData(((IOnRequestProcessing)cGModuleOutputSlot.Module).OnSlotDataRequest(this, cGModuleOutputSlot, requests));
				}
			}
			if (!InputInfo.ModifiesData)
			{
				return cGModuleOutputSlot.Data;
			}
			return cloneData<T>(ref cGModuleOutputSlot.Data);
		}

		private static CGData[] cloneData<T>(ref CGData[] source) where T : CGData
		{
			T[] array = new T[source.Length];
			for (int i = 0; i < source.Length; i++)
			{
				array[i] = ((source[i] == null) ? null : source[i].Clone<T>());
			}
			return array;
		}
	}
}

using System;
using UnityEngine;

public class SimBehaviour : MonoBehaviour
{
	[NonSerialized]
	private bool _hasNetBlock;

	[NonSerialized]
	private NetworkBlock _netBlock;

	public BasicInfo basicInfo;

	public bool HasBasicInfo;

	public bool HasParentMachine
	{
		get
		{
			return HasBasicInfo && basicInfo.infoType == BasicInfo.BasicInfoType.Block && basicInfo.HasParentMachine;
		}
	}

	public Machine ParentMachine
	{
		get
		{
			return (!HasBasicInfo || basicInfo.infoType != BasicInfo.BasicInfoType.Block || !basicInfo.HasParentMachine) ? null : basicInfo.ParentMachine;
		}
	}

	public bool SimPhysics
	{
		get
		{
			return HasBasicInfo ? basicInfo.SimPhysics : ((!StatMaster.isClient) ? StatMaster.levelSimulating : StatMaster.isLocalSim);
		}
	}

	public bool isSimulating
	{
		get
		{
			return (!HasBasicInfo) ? StatMaster.levelSimulating : basicInfo.isSimulating;
		}
	}

	public NetworkBlock NetBlock
	{
		get
		{
			if (!_hasNetBlock)
			{
				if (HasBasicInfo)
				{
					_netBlock = basicInfo.NetBlock;
				}
				else if (base.gameObject.activeInHierarchy)
				{
					_netBlock = GetComponentInParent<NetworkBlock>();
				}
				else
				{
					Transform parent = base.transform;
					int num = 0;
					do
					{
						_netBlock = parent.GetComponent<NetworkBlock>();
						parent = base.transform.parent;
						num++;
					}
					while (_netBlock == null && parent != null);
				}
				_hasNetBlock = true;
			}
			return _netBlock;
		}
	}

	protected virtual void Awake()
	{
	}

	protected virtual void Start()
	{
		if (!StatMaster.isMP && !HasBasicInfo && basicInfo != null)
		{
			HasBasicInfo = true;
		}
	}
}

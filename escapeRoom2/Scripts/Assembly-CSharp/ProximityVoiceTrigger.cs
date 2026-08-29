using System.Collections.Generic;
using Photon.Pun;
using Photon.Voice.PUN;
using Photon.Voice.Unity;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class ProximityVoiceTrigger : VoiceComponent
{
	private List<byte> groupsToAdd = new List<byte>();

	private List<byte> groupsToRemove = new List<byte>();

	[SerializeField]
	private byte[] subscribedGroups;

	private PhotonVoiceView photonVoiceView;

	private PhotonView photonView;

	public byte TargetInterestGroup
	{
		get
		{
			if (photonView != null)
			{
				return (byte)photonView.OwnerActorNr;
			}
			return 0;
		}
	}

	protected override void Awake()
	{
		photonVoiceView = GetComponentInParent<PhotonVoiceView>();
		photonView = GetComponentInParent<PhotonView>();
		GetComponent<Collider>().isTrigger = true;
		IsLocalCheck();
	}

	private void ToggleTransmission()
	{
		if (!(photonVoiceView.RecorderInUse != null))
		{
			return;
		}
		byte targetInterestGroup = TargetInterestGroup;
		if (photonVoiceView.RecorderInUse.InterestGroup != targetInterestGroup)
		{
			if (base.Logger.IsInfoEnabled)
			{
				base.Logger.LogInfo("Setting RecorderInUse's InterestGroup to {0}", targetInterestGroup);
			}
			photonVoiceView.RecorderInUse.InterestGroup = targetInterestGroup;
		}
		bool flag = subscribedGroups != null && subscribedGroups.Length != 0;
		if (photonVoiceView.RecorderInUse.TransmitEnabled != flag)
		{
			if (base.Logger.IsInfoEnabled)
			{
				base.Logger.LogInfo("Setting RecorderInUse's TransmitEnabled to {0}", flag);
			}
			photonVoiceView.RecorderInUse.TransmitEnabled = flag;
		}
		photonVoiceView.RecorderInUse.IsRecording = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (!IsLocalCheck())
		{
			return;
		}
		ProximityVoiceTrigger component = other.GetComponent<ProximityVoiceTrigger>();
		if (component != null)
		{
			byte targetInterestGroup = component.TargetInterestGroup;
			if (base.Logger.IsDebugEnabled)
			{
				base.Logger.LogDebug("OnTriggerEnter {0}", targetInterestGroup);
			}
			if (targetInterestGroup != TargetInterestGroup && targetInterestGroup != 0 && !groupsToAdd.Contains(targetInterestGroup))
			{
				groupsToAdd.Add(targetInterestGroup);
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (!IsLocalCheck())
		{
			return;
		}
		ProximityVoiceTrigger component = other.GetComponent<ProximityVoiceTrigger>();
		if (!(component != null))
		{
			return;
		}
		byte targetInterestGroup = component.TargetInterestGroup;
		if (base.Logger.IsDebugEnabled)
		{
			base.Logger.LogDebug("OnTriggerExit {0}", targetInterestGroup);
		}
		if (targetInterestGroup != TargetInterestGroup && targetInterestGroup != 0)
		{
			if (groupsToAdd.Contains(targetInterestGroup))
			{
				groupsToAdd.Remove(targetInterestGroup);
			}
			if (!groupsToRemove.Contains(targetInterestGroup))
			{
				groupsToRemove.Add(targetInterestGroup);
			}
		}
	}

	private void Update()
	{
		if (!PhotonVoiceNetwork.Instance.Client.InRoom)
		{
			subscribedGroups = null;
		}
		else
		{
			if (!IsLocalCheck())
			{
				return;
			}
			if (groupsToAdd.Count > 0 || groupsToRemove.Count > 0)
			{
				byte[] array = null;
				byte[] array2 = null;
				if (groupsToAdd.Count > 0)
				{
					array = groupsToAdd.ToArray();
				}
				if (groupsToRemove.Count > 0)
				{
					array2 = groupsToRemove.ToArray();
				}
				if (base.Logger.IsInfoEnabled)
				{
					base.Logger.LogInfo("client of actor number {0} trying to change groups, to_be_removed#:{1} to_be_added#={2}", TargetInterestGroup, groupsToRemove.Count, groupsToAdd.Count);
				}
				if (PhotonVoiceNetwork.Instance.Client.OpChangeGroups(array2, array))
				{
					if (subscribedGroups != null)
					{
						List<byte> list = new List<byte>();
						for (int i = 0; i < subscribedGroups.Length; i++)
						{
							list.Add(subscribedGroups[i]);
						}
						for (int j = 0; j < groupsToRemove.Count; j++)
						{
							if (list.Contains(groupsToRemove[j]))
							{
								list.Remove(groupsToRemove[j]);
							}
						}
						for (int k = 0; k < groupsToAdd.Count; k++)
						{
							if (!list.Contains(groupsToAdd[k]))
							{
								list.Add(groupsToAdd[k]);
							}
						}
						subscribedGroups = list.ToArray();
					}
					else
					{
						subscribedGroups = array;
					}
					groupsToAdd.Clear();
					groupsToRemove.Clear();
				}
				else if (base.Logger.IsErrorEnabled)
				{
					base.Logger.LogError("Error changing groups");
				}
			}
			ToggleTransmission();
		}
	}

	private bool IsLocalCheck()
	{
		if (photonVoiceView.IsPhotonViewReady)
		{
			if (photonView.IsMine)
			{
				return true;
			}
			if (base.enabled)
			{
				if (base.Logger.IsInfoEnabled)
				{
					base.Logger.LogInfo("Disabling ProximityVoiceTrigger as does not belong to local player, actor number {0}", TargetInterestGroup);
				}
				base.enabled = false;
			}
		}
		return false;
	}
}

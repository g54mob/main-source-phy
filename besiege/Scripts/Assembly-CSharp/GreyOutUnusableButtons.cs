using UnityEngine;

public class GreyOutUnusableButtons : MonoBehaviour
{
	public enum GreyState
	{
		Default = 0,
		Spectator = 1,
		LimitedMachines = 2,
		OneMachine = 3
	}

	public MeshRenderer[] spectatorHidden;

	public MonoBehaviour[] spectatorDeactivated;

	public MeshRenderer[] limitedMachinesHidden;

	public MonoBehaviour[] limitedMachinesDeactivated;

	public MeshRenderer[] oneMachineHidden;

	public MonoBehaviour[] oneMachineDeactivated;

	protected GreyState _state;

	public GreyState state
	{
		get
		{
			return _state;
		}
		set
		{
			_state = value;
			ChangedState(_state);
		}
	}

	private void LateUpdate()
	{
		if (PlayerData.hasLocalPlayer)
		{
			if (PlayerData.localPlayer.isSpectator)
			{
				if (state != GreyState.Spectator)
				{
					state = GreyState.Spectator;
				}
				return;
			}
			if (StatMaster.limitMachines)
			{
				if (LevelEditor.Instance.Settings.AllowedMachines.Count > 1)
				{
					if (state != GreyState.LimitedMachines)
					{
						state = GreyState.LimitedMachines;
					}
				}
				else if (state != GreyState.OneMachine)
				{
					state = GreyState.OneMachine;
				}
				return;
			}
		}
		if (state != GreyState.Default)
		{
			state = GreyState.Default;
		}
	}

	public void ChangedState(GreyState s)
	{
		switch (state)
		{
		case GreyState.Spectator:
		{
			for (int num = 0; num < limitedMachinesHidden.Length; num++)
			{
				limitedMachinesHidden[num].material.SetColor("_TintColor", new Color(1f, 1f, 1f, 0.5f));
			}
			for (int num2 = 0; num2 < oneMachineHidden.Length; num2++)
			{
				oneMachineHidden[num2].material.SetColor("_TintColor", new Color(1f, 1f, 1f, 0.5f));
			}
			for (int num3 = 0; num3 < spectatorHidden.Length; num3++)
			{
				spectatorHidden[num3].material.SetColor("_TintColor", new Color(1f, 1f, 1f, 0.25f));
			}
			for (int num4 = 0; num4 < limitedMachinesDeactivated.Length; num4++)
			{
				limitedMachinesDeactivated[num4].enabled = true;
			}
			for (int num5 = 0; num5 < oneMachineDeactivated.Length; num5++)
			{
				oneMachineDeactivated[num5].enabled = true;
			}
			for (int num6 = 0; num6 < spectatorDeactivated.Length; num6++)
			{
				spectatorDeactivated[num6].enabled = false;
			}
			break;
		}
		case GreyState.LimitedMachines:
		{
			for (int num7 = 0; num7 < oneMachineHidden.Length; num7++)
			{
				oneMachineHidden[num7].material.SetColor("_TintColor", new Color(1f, 1f, 1f, 0.5f));
			}
			for (int num8 = 0; num8 < spectatorHidden.Length; num8++)
			{
				spectatorHidden[num8].material.SetColor("_TintColor", new Color(1f, 1f, 1f, 0.5f));
			}
			for (int num9 = 0; num9 < limitedMachinesHidden.Length; num9++)
			{
				limitedMachinesHidden[num9].material.SetColor("_TintColor", new Color(1f, 1f, 1f, (!LevelEditor.Instance.Settings.AllowModMachines) ? 0.25f : 0.5f));
			}
			for (int num10 = 0; num10 < oneMachineDeactivated.Length; num10++)
			{
				oneMachineDeactivated[num10].enabled = true;
			}
			for (int num11 = 0; num11 < spectatorDeactivated.Length; num11++)
			{
				spectatorDeactivated[num11].enabled = true;
			}
			for (int num12 = 0; num12 < limitedMachinesDeactivated.Length; num12++)
			{
				limitedMachinesDeactivated[num12].enabled = LevelEditor.Instance.Settings.AllowModMachines;
			}
			break;
		}
		case GreyState.OneMachine:
		{
			for (int num13 = 0; num13 < spectatorHidden.Length; num13++)
			{
				spectatorHidden[num13].material.SetColor("_TintColor", new Color(1f, 1f, 1f, 0.5f));
			}
			for (int num14 = 0; num14 < limitedMachinesHidden.Length; num14++)
			{
				limitedMachinesHidden[num14].material.SetColor("_TintColor", new Color(1f, 1f, 1f, (!LevelEditor.Instance.Settings.AllowModMachines) ? 0.25f : 0.5f));
			}
			for (int num15 = 0; num15 < oneMachineHidden.Length; num15++)
			{
				oneMachineHidden[num15].material.SetColor("_TintColor", new Color(1f, 1f, 1f, (!LevelEditor.Instance.Settings.AllowModMachines) ? 0.25f : 0.5f));
			}
			for (int num16 = 0; num16 < spectatorDeactivated.Length; num16++)
			{
				spectatorDeactivated[num16].enabled = true;
			}
			for (int num17 = 0; num17 < limitedMachinesDeactivated.Length; num17++)
			{
				limitedMachinesDeactivated[num17].enabled = LevelEditor.Instance.Settings.AllowModMachines;
			}
			for (int num18 = 0; num18 < oneMachineDeactivated.Length; num18++)
			{
				oneMachineDeactivated[num18].enabled = LevelEditor.Instance.Settings.AllowModMachines;
			}
			break;
		}
		default:
		{
			for (int i = 0; i < spectatorHidden.Length; i++)
			{
				spectatorHidden[i].material.SetColor("_TintColor", new Color(1f, 1f, 1f, 0.5f));
			}
			for (int j = 0; j < limitedMachinesHidden.Length; j++)
			{
				limitedMachinesHidden[j].material.SetColor("_TintColor", new Color(1f, 1f, 1f, 0.5f));
			}
			for (int k = 0; k < oneMachineHidden.Length; k++)
			{
				oneMachineHidden[k].material.SetColor("_TintColor", new Color(1f, 1f, 1f, 0.5f));
			}
			for (int l = 0; l < spectatorDeactivated.Length; l++)
			{
				spectatorDeactivated[l].enabled = true;
			}
			for (int m = 0; m < limitedMachinesDeactivated.Length; m++)
			{
				limitedMachinesDeactivated[m].enabled = true;
			}
			for (int n = 0; n < oneMachineDeactivated.Length; n++)
			{
				oneMachineDeactivated[n].enabled = true;
			}
			break;
		}
		}
	}
}

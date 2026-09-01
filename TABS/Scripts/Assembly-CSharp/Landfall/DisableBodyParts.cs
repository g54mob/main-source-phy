using System.Collections.Generic;
using Landfall.TABS;
using UnityEngine;

namespace Landfall
{
	public class DisableBodyParts : MonoBehaviour
	{
		[SerializeField]
		private Unit unit;

		[Header("Body Parts")]
		[SerializeField]
		private Transform head;

		[SerializeField]
		private Transform hip;

		[SerializeField]
		private Transform torso;

		[SerializeField]
		private Transform armLeft;

		[SerializeField]
		private Transform elbowLeft;

		[SerializeField]
		private Transform armRight;

		[SerializeField]
		private Transform elbowRight;

		[SerializeField]
		private Transform legLeft;

		[SerializeField]
		private Transform kneeLeft;

		[SerializeField]
		private Transform legRight;

		[SerializeField]
		private Transform kneeRight;

		[Header("Sub Body Parts")]
		[SerializeField]
		private Transform wristLeft;

		[SerializeField]
		private Transform wristRight;

		[SerializeField]
		private Transform footLeft;

		[SerializeField]
		private Transform footRight;

		private Dictionary<Transform, Renderer[]> bodyRenderers = new Dictionary<Transform, Renderer[]>();

		private UnitBlueprint unitBlueprint;

		private GlobalSettingsHandler settingsHandler;

		private SettingsInstance hideClothesSetting;

		private void AddBodyRenderers(Transform bodyPart)
		{
			if (!(bodyPart == null))
			{
				bodyRenderers.Add(bodyPart, bodyPart.GetComponentsInChildren<Renderer>(includeInactive: true));
			}
		}

		private void OnHideClothesChanged(int value)
		{
			ProcessAllBodyParts();
		}

		private void ProcessAllBodyParts()
		{
			if (hideClothesSetting != null && hideClothesSetting.currentValue != 0)
			{
				EnableDisableBodyPartRenderers(head, shouldDisable: false);
				EnableDisableBodyPartRenderers(hip, shouldDisable: false);
				EnableDisableBodyPartRenderers(torso, shouldDisable: false);
				EnableDisableBodyPartRenderers(armLeft, shouldDisable: false);
				EnableDisableBodyPartRenderers(elbowLeft, shouldDisable: false);
				EnableDisableBodyPartRenderers(armRight, shouldDisable: false);
				EnableDisableBodyPartRenderers(elbowRight, shouldDisable: false);
				EnableDisableBodyPartRenderers(legLeft, shouldDisable: false);
				EnableDisableBodyPartRenderers(kneeLeft, shouldDisable: false);
				EnableDisableBodyPartRenderers(legRight, shouldDisable: false);
				EnableDisableBodyPartRenderers(kneeRight, shouldDisable: false);
				EnableDisableBodyPartRenderers(wristLeft, shouldDisable: false);
				EnableDisableBodyPartRenderers(wristRight, shouldDisable: false);
				EnableDisableBodyPartRenderers(footLeft, shouldDisable: false);
				EnableDisableBodyPartRenderers(footRight, shouldDisable: false);
			}
			else if (!(unitBlueprint == null))
			{
				EnableDisableBodyPartRenderers(head, unitBlueprint.Head);
				EnableDisableBodyPartRenderers(hip, unitBlueprint.Hip);
				EnableDisableBodyPartRenderers(torso, unitBlueprint.Torso);
				EnableDisableBodyPartRenderers(armLeft, unitBlueprint.ArmLeft);
				EnableDisableBodyPartRenderers(elbowLeft, unitBlueprint.ElbowLeft);
				EnableDisableBodyPartRenderers(armRight, unitBlueprint.ArmRight);
				EnableDisableBodyPartRenderers(elbowRight, unitBlueprint.ElbowRight);
				EnableDisableBodyPartRenderers(legLeft, unitBlueprint.LegLeft);
				EnableDisableBodyPartRenderers(kneeLeft, unitBlueprint.KneeLeft);
				EnableDisableBodyPartRenderers(legRight, unitBlueprint.LegRight);
				EnableDisableBodyPartRenderers(kneeRight, unitBlueprint.KneeRight);
				EnableDisableBodyPartRenderers(wristLeft, unitBlueprint.WristLeft);
				EnableDisableBodyPartRenderers(wristRight, unitBlueprint.WristRight);
				EnableDisableBodyPartRenderers(footLeft, unitBlueprint.FootLeft);
				EnableDisableBodyPartRenderers(footRight, unitBlueprint.FootRight);
			}
		}

		private void EnableDisableBodyPartRenderers(Transform bodyPart, bool shouldDisable)
		{
			if (bodyPart == null)
			{
				return;
			}
			Renderer[] value = null;
			if (bodyRenderers.TryGetValue(bodyPart, out value))
			{
				int i = 0;
				for (int num = value.Length; i < num; i++)
				{
					value[i].enabled = !shouldDisable;
				}
			}
		}
	}
}

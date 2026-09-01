using System.Collections;
using System.Collections.Generic;
using Landfall.TABS;
using Landfall.TABS.AI;
using Landfall.TABS.RuntimeCleanup;
using UnityEngine;

public class CopySelf : MonoBehaviour
{
	public GameObject poof;

	private RuntimeGarbageCollector m_gc;

	private bool done;

	private void Start()
	{
		m_gc = ServiceLocator.GetService<RuntimeGarbageCollector>();
	}

	public void DoEffect()
	{
		if (!done && base.enabled)
		{
			done = true;
			UnitAPI component = base.transform.root.GetComponent<UnitAPI>();
			if ((bool)component)
			{
				component.forceSupressFromWinCondition = true;
			}
			Transform root = base.transform.root;
			Vector3 vector = component.GetComponent<Unit>().data.mainRig.position + root.GetComponentInChildren<DirectionObject>().transform.forward * 3f;
			Dictionary<string, UnitRig.BoneInfo> rigInfo = root.GetComponent<UnitRig>().RigInfo;
			Unit component2 = root.GetComponent<Unit>();
			GameObject gameObject = component2.unitBlueprint.Spawn(root.position, root.rotation, component2.Team)[0];
			gameObject.transform.position += vector - root.GetComponentInChildren<DataHandler>().mainRig.position;
			gameObject.GetComponent<UnitRig>().RigInfo = rigInfo;
			gameObject.GetComponentInChildren<GeneralInput>().hasControl = false;
			gameObject.transform.root.GetComponentInChildren<RigidbodyHolder>().randomizeRigidbodySizes = false;
			DataHandler componentInChildren = gameObject.transform.root.GetComponentInChildren<DataHandler>();
			componentInChildren.health = component2.data.health;
			StartCoroutine(DelaySetMaxHealth(component2, componentInChildren.unit));
			gameObject.transform.root.GetComponentInChildren<Unit>().damageDealt = component2.damageDealt;
			gameObject.transform.root.GetComponentInChildren<WeaponHandler>().SetAttackCounters(component2.WeaponHandler.rightWeapon.internalCounter);
			WeaponHandler weaponHandler = component2.WeaponHandler;
			WeaponHandler weaponHandler2 = gameObject.GetComponent<Unit>().WeaponHandler;
			if ((bool)weaponHandler.rightWeapon)
			{
				weaponHandler2.rightWeapon.transform.localPosition = weaponHandler.rightWeapon.transform.localPosition;
				weaponHandler2.rightWeapon.transform.localRotation = weaponHandler.rightWeapon.transform.localRotation;
				weaponHandler2.rightWeapon.transform.localScale = weaponHandler.rightWeapon.transform.localScale;
			}
			if ((bool)weaponHandler.leftWeapon)
			{
				weaponHandler2.leftWeapon.transform.localPosition = weaponHandler.leftWeapon.transform.localPosition;
				weaponHandler2.leftWeapon.transform.localRotation = weaponHandler.leftWeapon.transform.localRotation;
				weaponHandler2.leftWeapon.transform.localScale = weaponHandler.leftWeapon.transform.localScale;
			}
			PossesionCamera componentInChildren2 = base.transform.root.GetComponentInChildren<PossesionCamera>();
			if ((bool)componentInChildren2)
			{
				Object.Destroy(componentInChildren2);
			}
			if (gameObject.GetComponent<KillAfterSeconds>() == null)
			{
				KillAfterSeconds killAfterSeconds = base.gameObject.transform.root.gameObject.AddComponent<KillAfterSeconds>();
				killAfterSeconds.seconds = 0.25f;
				killAfterSeconds.objectToSpawn = poof;
				killAfterSeconds.spawnObjectOnMainRig = true;
				killAfterSeconds.destroyRoot = true;
				killAfterSeconds.skinnedShape = true;
			}
			TransferPossession component3 = GetComponent<TransferPossession>();
			if (component3 != null)
			{
				component3.Transfer(gameObject);
			}
			m_gc.AddGameObject(gameObject);
		}
	}

	private IEnumerator DelaySetMaxHealth(Unit mine, Unit theirs)
	{
		yield return new WaitForSeconds(0f);
		yield return new WaitForSeconds(0f);
		theirs.data.maxHealth = mine.data.maxHealth;
	}
}

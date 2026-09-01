using Landfall.MonoBatch;
using UnityEngine;

public class DragHandler : BatchedMonobehaviour
{
	private RigidbodyHolder rigidbodyHolder;

	private WeaponHandler weaponHandler;

	private float ragdollControlLastFrame = 1f;

	private DataHandler data;

	protected override void Start()
	{
		base.Start();
		rigidbodyHolder = GetComponent<RigidbodyHolder>();
		weaponHandler = GetComponent<WeaponHandler>();
		data = GetComponent<DataHandler>();
	}

	public override void BatchedUpdate()
	{
		if (Mathf.Approximately(data.ragdollControl, ragdollControlLastFrame))
		{
			return;
		}
		ragdollControlLastFrame = data.ragdollControl;
		if ((bool)rigidbodyHolder)
		{
			rigidbodyHolder.SetColliders(data.ragdollControl > 0.2f);
		}
		int num = rigidbodyHolder.AllRigs.Length;
		for (int i = 0; i < num; i++)
		{
			if ((bool)rigidbodyHolder.AllRigs[i])
			{
				rigidbodyHolder.AllRigs[i].drag = rigidbodyHolder.AllDrags[i].x * data.ragdollControl;
				rigidbodyHolder.AllRigs[i].angularDrag = rigidbodyHolder.AllDrags[i].y * data.ragdollControl;
			}
		}
		if ((bool)weaponHandler)
		{
			if ((bool)weaponHandler.rightWeapon && (bool)weaponHandler.rightWeapon.rigidbody)
			{
				weaponHandler.rightWeapon.rigidbody.drag = weaponHandler.rightWeapon.defaultDrag * data.ragdollControl;
				weaponHandler.rightWeapon.rigidbody.angularDrag = weaponHandler.rightWeapon.defaultAngularDrag * data.ragdollControl;
			}
			if ((bool)weaponHandler.leftWeapon && (bool)weaponHandler.leftWeapon.rigidbody)
			{
				weaponHandler.leftWeapon.rigidbody.drag = weaponHandler.leftWeapon.defaultDrag * data.ragdollControl;
				weaponHandler.leftWeapon.rigidbody.angularDrag = weaponHandler.leftWeapon.defaultAngularDrag * data.ragdollControl;
			}
		}
	}

	public void UpdateDrag()
	{
		ragdollControlLastFrame = -1f;
	}
}

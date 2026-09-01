using System.Collections;
using Landfall.TABS;
using TFBGames;
using UnityEngine;

public class Effect_IceArrow : UnitEffectBase
{
	private const float UpdateEffectColorTimeInterval = 0.6f;

	public float effectAmount = 10f;

	public float dragPerAmount = 1f;

	public float removeDragSpeed = 2f;

	public Color fostColor;

	public UnitColorInstance color;

	public Material iceMaterial;

	public float amount;

	private float healthMultiplier = 1f;

	private RigidbodyHolder rigHolder;

	private WeaponHandler weapons;

	private DataHandler data;

	private DragHandler dragHandler;

	private UnitColorHandler colorHandler;

	private bool done;

	private float timer;

	private INetworkService m_networkService;

	private NetworkBattleController m_networkBattle;

	private NetworkBattleController NetworkBattle
	{
		get
		{
			if (m_networkBattle == null)
			{
				m_networkBattle = ServiceLocator.GetService<NetworkBattleController>();
			}
			return m_networkBattle;
		}
	}

	private void Start()
	{
		m_networkService = ServiceLocator.GetService<INetworkService>();
		m_networkBattle = ServiceLocator.GetService<NetworkBattleController>();
	}

	public override void DoEffect()
	{
		Add(effectAmount);
	}

	public override void Ping()
	{
		Add(effectAmount);
	}

	private void Add(float a)
	{
		if (((bool)data && data.Dead && !base.ShouldSkipDeadTests) || done)
		{
			return;
		}
		if (!rigHolder)
		{
			rigHolder = base.transform.parent.GetComponentInChildren<RigidbodyHolder>();
			if ((bool)rigHolder)
			{
				dragHandler = rigHolder.GetComponent<DragHandler>();
				data = rigHolder.GetComponent<DataHandler>();
				colorHandler = rigHolder.GetComponent<UnitColorHandler>();
				weapons = rigHolder.GetComponent<WeaponHandler>();
				healthMultiplier = 1f / Mathf.Clamp(data.maxHealth * 0.01f, 1f, float.PositiveInfinity);
			}
		}
		if ((!data || !data.Dead || base.ShouldSkipDeadTests) && (bool)rigHolder)
		{
			a *= healthMultiplier;
			for (int i = 0; i < rigHolder.AllDrags.Length; i++)
			{
				rigHolder.AllDrags[i].x += a * dragPerAmount;
				rigHolder.AllDrags[i].y += a * dragPerAmount;
			}
			amount += a;
			if (dragHandler != null)
			{
				dragHandler.UpdateDrag();
			}
			timer = 1.6f;
			if (amount * 0.03f >= 1f)
			{
				Done();
			}
		}
	}

	private void Update()
	{
		if (done || !rigHolder || (data.Dead && !base.ShouldSkipDeadTests))
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < rigHolder.AllDrags.Length; i++)
		{
			if (rigHolder.AllDrags[i].x > rigHolder.defaultDrags[i].x)
			{
				flag = true;
				rigHolder.AllDrags[i].x -= Time.deltaTime * removeDragSpeed;
			}
			if (rigHolder.AllDrags[i].y > rigHolder.defaultDrags[i].y)
			{
				flag = true;
				rigHolder.AllDrags[i].y -= Time.deltaTime * removeDragSpeed;
			}
		}
		if (flag)
		{
			amount -= Time.deltaTime * removeDragSpeed * dragPerAmount;
		}
		timer += Time.deltaTime;
		if (colorHandler != null)
		{
			colorHandler.SetColor(color, Mathf.Clamp(amount * 0.03f, 0f, 1f));
		}
		if (dragHandler != null)
		{
			dragHandler.UpdateDrag();
		}
	}

	private void Done()
	{
		if (!done && (!data || !data.Dead || base.ShouldSkipDeadTests))
		{
			PlaySoundEffect component = GetComponent<PlaySoundEffect>();
			if (component != null)
			{
				component.Go();
			}
			done = true;
			StartCoroutine(DelayFreeze());
		}
	}

	private IEnumerator DelayFreeze()
	{
		yield return new WaitForSeconds(0.1f);
		if (data != null && data.healthHandler != null && data.healthHandler.DiedLocally)
		{
			yield break;
		}
		if (NetworkBattle != null && m_networkService != null && m_networkService.IsClient)
		{
			if (colorHandler != null)
			{
				colorHandler.SetMaterial(iceMaterial);
			}
			base.ShouldSkipDeadTests = false;
			data.healthHandler.DiedLocally = true;
			Object.Destroy(base.gameObject);
		}
		else
		{
			base.ShouldSkipDeadTests = false;
			StandardDeath();
		}
	}

	private void OptimizedDeath()
	{
		if (colorHandler != null)
		{
			colorHandler.SetColor(color, 100f);
		}
		data.healthHandler.Die();
		Object.Destroy(base.gameObject);
	}

	private void StandardDeath()
	{
		Joint[] componentsInChildren = base.transform.root.GetComponentsInChildren<Joint>();
		float num = 0f;
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < componentsInChildren.Length; i++)
		{
			Object.Destroy(componentsInChildren[i]);
		}
		for (int j = 0; j < data.allRigs.AllRigs.Length; j++)
		{
			Rigidbody rigidbody = data.allRigs.AllRigs[j];
			if (!(rigidbody == null))
			{
				num += rigidbody.mass;
				zero += rigidbody.velocity / data.allRigs.AllDrags.Length;
				Object.Destroy(rigidbody);
			}
		}
		if ((bool)colorHandler)
		{
			colorHandler.SetMaterial(iceMaterial);
		}
		if ((bool)weapons)
		{
			if ((bool)weapons.leftWeapon && (bool)weapons.leftWeapon.rigidbody)
			{
				Object.Destroy(weapons.leftWeapon.rigidbody);
			}
			if ((bool)weapons.rightWeapon && (bool)weapons.rightWeapon.rigidbody)
			{
				Object.Destroy(weapons.rightWeapon.rigidbody);
			}
		}
		Rigidbody rigidbody2 = data.transform.root.gameObject.AddComponent<Rigidbody>();
		data.mainRig = rigidbody2;
		rigidbody2.mass = num * 3f;
		rigidbody2.velocity = zero;
		rigidbody2.gameObject.AddComponent<SinkOnDeath>().Sink();
		rigidbody2.drag = 1f;
		rigidbody2.angularDrag = 1f;
		rigidbody2.interpolation = RigidbodyInterpolation.Interpolate;
		Collider component = rigidbody2.GetComponent<Collider>();
		if ((bool)component)
		{
			Object.Destroy(component);
		}
		rigidbody2.ResetCenterOfMass();
		data.healthHandler.Die();
		Object.Destroy(base.gameObject);
		DatabaseID gUID = data.unit.unitBlueprint.Entity.GUID;
		if (gUID.m_modID == -1 && gUID.m_ID == 1610400248)
		{
			ServiceLocator.GetService<AchievementService>().UnlockAchievement("FREEZE_ICE_GIANT");
		}
	}
}

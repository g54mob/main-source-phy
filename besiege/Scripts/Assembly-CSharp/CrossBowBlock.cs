using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[AddComponentMenu("Blocks/Block Behaviours/Crossbow Block")]
public class CrossBowBlock : BlockBehaviour
{
	public GameObject projectile;

	public Transform projectileSpawnPos;

	public float power;

	public int ammo;

	public float recoilPower = 0.1f;

	public float regularDamage;

	public float blockDamage;

	public RandomSoundController randomSoundController;

	public FireController fireController;

	public GameObject placeholderArrow;

	public ReloadAnimation anim;

	private float timebetween;

	private bool autoFire;

	private int poolAmount;

	private AttackScript.Projectile[] projectileArray;

	private List<Transform> spawnedProjectiles;

	private ProjectileScript projectilePrefabScript;

	private int nextArrow;

	private MSlider rateOfFire;

	private MSlider powerSlider;

	private MKey fireKey;

	private MToggle holdToShootToggle;

	private bool useProjManager;

	private int maxAmmo;

	public MSlider RateOfFireSlider
	{
		get
		{
			return rateOfFire;
		}
	}

	public MSlider PowerSlider
	{
		get
		{
			return powerSlider;
		}
	}

	public MToggle HoldToShootToggle
	{
		get
		{
			return holdToShootToggle;
		}
	}

	public MKey FireKey
	{
		get
		{
			return fireKey;
		}
	}

	protected override void Awake()
	{
		base.Awake();
		anim.Awake(this);
		maxAmmo = ammo;
		fireKey = AddKey(2429, "shoot", ControlScheme.BlockControls.Crossbow, 0, KeyCode.C);
		rateOfFire = AddSlider(2440, "rate-of-fire", 1f, 0.25f, 4f, string.Empty);
		holdToShootToggle = AddToggle(2441, "hold-to-fire", true);
		if (!isSimulating || SimPhysics)
		{
			powerSlider = AddSlider(2427, "power", 1.5f, 0.25f, 2f, string.Empty);
		}
		if (!isSimulating)
		{
			return;
		}
		spawnedProjectiles = new List<Transform>();
		rateOfFire.Value = Mathf.Clamp(rateOfFire.Value, 0.1f, 10f);
		useProjManager = StatMaster.isHosting && base.HasParentMachine && !base.ParentMachine.LocalSim && StatMaster.isMP;
		if (SimPhysics && !useProjManager)
		{
			float num = power * powerSlider.Value / 1000f * 4f;
			projectilePrefabScript = this.projectile.GetComponent<ProjectileScript>();
			poolAmount = (int)((num + ((!projectilePrefabScript.useKillTimer) ? 0f : projectilePrefabScript.killTimer)) * rateOfFire.Value);
			poolAmount += 2;
			projectileArray = new AttackScript.Projectile[poolAmount];
			for (int i = 0; i < projectileArray.Length; i++)
			{
				GameObject gameObject = Object.Instantiate(this.projectile, projectileSpawnPos.position, projectileSpawnPos.rotation) as GameObject;
				spawnedProjectiles.Add(gameObject.transform);
				projectileArray[i] = new AttackScript.Projectile(gameObject);
				AttackScript.Projectile projectile = projectileArray[i];
				projectile.transform.parent = GetPhysGoal();
				projectile.projectileScript.attackDamage = regularDamage;
				projectile.projectileScript.blockDamageAmount = blockDamage;
				projectile.projectileScript.hasAttached = !projectilePrefabScript.useKillTimer;
				projectile.gameObject.SetActive(false);
			}
		}
		timebetween = 0f;
	}

	public override void UpdateBlock()
	{
		base.UpdateBlock();
		if (fireController.fireProgress >= 0.95f || BlockHealth.health <= 0f)
		{
			if (placeholderArrow.activeSelf)
			{
				placeholderArrow.SetActive(false);
			}
			return;
		}
		bool flag = fireKey.IsPressed || fireKey.EmulationPressed();
		bool flag2 = fireKey.IsHeld || fireKey.EmulationHeld(true);
		if (flag && !holdToShootToggle.IsActive)
		{
			autoFire = !autoFire;
		}
		if (timebetween <= 0f)
		{
			if (ammo > 0 || (base.HasParentMachine && base.ParentMachine.InfiniteAmmoMode))
			{
				if ((flag2 && holdToShootToggle.IsActive) || autoFire)
				{
					StartCoroutine(FIRE());
					timebetween = 1f / rateOfFire.Value;
				}
				else
				{
					placeholderArrow.SetActive(true);
				}
				return;
			}
			if (placeholderArrow.activeSelf)
			{
				placeholderArrow.SetActive(false);
			}
			if ((flag2 && holdToShootToggle.IsActive) || flag)
			{
				StartCoroutine(Empty());
				timebetween = 1f / rateOfFire.Value;
			}
		}
		else
		{
			timebetween -= Time.deltaTime;
		}
	}

	public override void OnReloadAmmo(ref int units, ReloadAmmoType type, bool setAmmo, bool eachBlock, bool playAnim = true)
	{
		if (type != ReloadAmmoType.All && type != ReloadAmmoType.Arrow)
		{
			return;
		}
		if (setAmmo)
		{
			if (eachBlock || units < maxAmmo)
			{
				ammo = units;
				units = 0;
			}
			else
			{
				units -= maxAmmo;
				ammo = maxAmmo;
			}
			if (playAnim)
			{
				anim.AnimateReload(ammo);
			}
			return;
		}
		int num = 0;
		if (eachBlock || units <= maxAmmo - ammo)
		{
			ammo += units;
			num = units;
			units = 0;
		}
		else
		{
			num = maxAmmo - ammo;
			units -= num;
			ammo = maxAmmo;
		}
		if (playAnim)
		{
			anim.AnimateReload(num);
		}
	}

	private IEnumerator FIRE()
	{
		bool infiniteAmmo = base.HasParentMachine && base.ParentMachine.InfiniteAmmoMode;
		if (infiniteAmmo && !StatMaster.GodTools.HasBeenUsed)
		{
			StatMaster.GodTools.HasBeenUsed = true;
		}
		if (ammo <= 0 && !infiniteAmmo)
		{
			anim.StopReloadAnim();
			yield break;
		}
		base.ParentMachine.hasFiredProjectiles = true;
		ammo--;
		int rand = Random.Range(1, 4);
		for (int i = 0; i < rand; i++)
		{
			yield return new WaitForFixedUpdate();
		}
		if (StatMaster.Rules.DisableProjectiles)
		{
			if (placeholderArrow.activeSelf)
			{
				placeholderArrow.SetActive(false);
			}
			yield break;
		}
		if (SimPhysics)
		{
			AttackScript.Projectile currentProjectile = ((!useProjManager && !projectilePrefabScript.useKillTimer) ? GetProjectileReverse() : GetProjectile());
			if (currentProjectile != null)
			{
				Rigidbody projectileBody = currentProjectile.rigidbody;
				ProjectileScript projectileScript = currentProjectile.projectileScript;
				projectileBody.isKinematic = false;
				projectileScript.enabled = true;
				if (fireController.onFire)
				{
					FireTag fireTag = projectileScript.firecontrol.fireTagCode;
					if (fireTag != null)
					{
						fireTag.Ignite();
					}
				}
				Vector3 randomDir = projectileSpawnPos.forward + Random.insideUnitSphere * 0.01f;
				projectileScript.col.gameObject.layer = 8;
				projectileScript.visObj.enabled = false;
				projectileBody.velocity = Rigidbody.velocity;
				yield return new WaitForFixedUpdate();
				if (placeholderArrow.activeSelf)
				{
					placeholderArrow.SetActive(false);
				}
				projectileBody.MovePosition(projectileSpawnPos.position);
				projectileBody.MoveRotation(projectileSpawnPos.rotation);
				projectileScript.col.gameObject.layer = 0;
				projectileScript.visObj.enabled = true;
				projectileBody.AddForce(power * powerSlider.Value * randomDir);
				Rigidbody.AddForce(power * powerSlider.Value * -randomDir * recoilPower);
			}
		}
		yield return new WaitForSeconds(Random.Range(0f, 0.05f));
		randomSoundController.Play2(0.15f);
	}

	private IEnumerator Empty()
	{
		yield return new WaitForSeconds(Random.Range(0f, 0.1f));
		randomSoundController.Play3();
	}

	private void InitProjectile(AttackScript.Projectile proj)
	{
		proj.transform.parent = GetPhysGoal();
		proj.transform.position = projectileSpawnPos.position;
		proj.transform.rotation = projectileSpawnPos.rotation;
		proj.transform.localScale = Vector3.one;
		if (!proj.gameObject.activeInHierarchy)
		{
			proj.gameObject.SetActive(true);
		}
		else
		{
			proj.projectileScript.OnEnable();
		}
		proj.projectileScript.hasAttached = false;
		if ((bool)proj.gyro)
		{
			proj.gyro.localRotation = Quaternion.identity;
		}
		proj.projectileScript.ownerID = base.ParentMachine.PlayerID;
	}

	private AttackScript.Projectile GetProjectileReverse()
	{
		for (int i = nextArrow; i < projectileArray.Length; i++)
		{
			AttackScript.Projectile projectile = projectileArray[i];
			if ((bool)projectile.gameObject)
			{
				if (projectile.projectileScript.hasAttached)
				{
					projectile.rigidbody.interpolation = RigidbodyInterpolation.None;
					InitProjectile(projectile);
					projectile.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
					nextArrow = i + 1;
					if (nextArrow >= projectileArray.Length)
					{
						nextArrow = 0;
					}
					return projectile;
				}
			}
			else
			{
				GameObject p = Object.Instantiate(this.projectile, projectileSpawnPos.position, projectileSpawnPos.rotation) as GameObject;
				spawnedProjectiles.Add(projectile.transform);
				projectile.NewProjectile(p);
				projectile.transform.parent = GetPhysGoal();
				projectile.projectileScript.ownerID = base.ParentMachine.PlayerID;
				projectile.projectileScript.attackDamage = regularDamage;
				projectile.projectileScript.blockDamageAmount = blockDamage;
				i--;
			}
		}
		for (int j = 0; j < projectileArray.Length; j++)
		{
			AttackScript.Projectile projectile = projectileArray[j];
			if (!projectile.gameObject)
			{
				continue;
			}
			if (projectile.projectileScript.hasAttached)
			{
				InitProjectile(projectile);
				nextArrow = j + 1;
				if (nextArrow >= projectileArray.Length)
				{
					nextArrow = 0;
				}
				return projectile;
			}
			if (j == nextArrow)
			{
				InitProjectile(projectile);
				nextArrow = j + 1;
				if (nextArrow >= projectileArray.Length)
				{
					nextArrow = 0;
				}
				return projectile;
			}
		}
		return null;
	}

	private AttackScript.Projectile GetProjectile()
	{
		if (useProjManager)
		{
			byte[] array = new byte[13];
			int num = 0;
			NetworkCompression.CompressPosition(projectileSpawnPos.position, array, num);
			num += 6;
			NetworkCompression.CompressRotation(projectileSpawnPos.rotation, array, num);
			NetworkAddPiece instance = NetworkAddPiece.Instance;
			Transform transform = ProjectileManager.Instance.Spawn(NetworkProjectileType.CrossbowArrow, instance.frame, base.ParentMachine.PlayerID, array);
			spawnedProjectiles.Add(transform);
			AttackScript.Projectile projectile = new AttackScript.Projectile(transform.gameObject);
			projectile.projectileScript.attackDamage = regularDamage;
			projectile.projectileScript.blockDamageAmount = blockDamage;
			projectile.projectileScript.ownerID = base.ParentMachine.PlayerID;
			return projectile;
		}
		for (int i = 0; i < projectileArray.Length; i++)
		{
			AttackScript.Projectile projectile = projectileArray[i];
			if ((bool)projectile.gameObject)
			{
				if (!projectile.gameObject.activeInHierarchy)
				{
					InitProjectile(projectile);
					return projectile;
				}
				continue;
			}
			GameObject gameObject = Object.Instantiate(this.projectile, projectileSpawnPos.position, projectileSpawnPos.rotation) as GameObject;
			spawnedProjectiles.Add(gameObject.transform);
			projectile.NewProjectile(gameObject);
			projectile.transform.parent = GetPhysGoal();
			projectile.projectileScript.ownerID = base.ParentMachine.PlayerID;
			projectile.projectileScript.attackDamage = regularDamage;
			projectile.projectileScript.blockDamageAmount = blockDamage;
			projectile.gameObject.SetActive(false);
			i--;
		}
		for (int j = 0; j < projectileArray.Length; j++)
		{
			AttackScript.Projectile projectile = projectileArray[j];
			if ((bool)projectile.gameObject)
			{
				if (projectile.projectileScript.hasAttached)
				{
					InitProjectile(projectile);
					return projectile;
				}
				if (j == nextArrow)
				{
					InitProjectile(projectile);
					return projectile;
				}
			}
		}
		return null;
	}

	private Transform GetPhysGoal()
	{
		return ReferenceMaster.physicsGoalInstance;
	}

	protected override void OnDestroy()
	{
		base.OnDestroy();
		if (!SimPhysics)
		{
			return;
		}
		for (int i = 0; i < spawnedProjectiles.Count; i++)
		{
			Transform transform = spawnedProjectiles[i];
			if (transform == null)
			{
				continue;
			}
			GameObject gameObject = transform.gameObject;
			if (gameObject != null)
			{
				if (useProjManager)
				{
					ProjectileManager.Instance.Despawn(transform);
				}
				else
				{
					Object.Destroy(gameObject);
				}
			}
		}
	}
}

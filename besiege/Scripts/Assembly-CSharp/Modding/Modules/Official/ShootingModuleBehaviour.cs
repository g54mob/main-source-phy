using System;
using System.Collections;
using System.Collections.Generic;
using InternalModding.Blocks;
using InternalModding.Misc;
using Modding.Serialization;
using UnityEngine;

namespace Modding.Modules.Official
{
	public class ShootingModuleBehaviour : BlockModuleBehaviour<ShootingModule>, IFireEffect
	{
		public MSlider PowerSlider;

		public MSlider RateOfFire;

		public MKey FireKey;

		public MToggle HoldToShootToggle;

		public int AmmoLeft;

		private AttackScript.Projectile[] projectileArray;

		private List<Transform> spawnedProjectiles;

		private bool useProjManager;

		private int nextProjectile;

		private RandomSoundController soundController;

		private GameObject projectile;

		private ProjectileScript projectilePrefabScript;

		public Transform projectileStart;

		public GameObject projectilePlaceholder;

		private float timeDiff;

		private bool autoFire;

		private bool firing;

		public override void SafeAwake()
		{
			try
			{
				PowerSlider = GetSlider(base.Module.PowerSlider);
				RateOfFire = GetSlider(base.Module.RateOfFireSlider);
				HoldToShootToggle = GetToggle(base.Module.HoldToShootToggle);
				FireKey = GetKey(base.Module.FireKey);
			}
			catch (Exception ex)
			{
				MLog.Error("Could not get all mapper types for Shooting module! Module will be disabled.");
				MLog.Error(ex.ToString());
				UnityEngine.Object.Destroy(this);
				return;
			}
			AmmoLeft = base.Module.DefaultAmmo;
			if (!base.IsSimulating)
			{
				if (base.Module.ShowPlaceholderProjectile)
				{
					projectileStart = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<BlockLoader>.Instance.ModulePrefabs.ShootingDirectionVisual).transform;
					projectileStart.parent = base.transform;
					base.Module.ProjectileStart.SetOnTransform(projectileStart);
					projectileStart.FindChild("Vis").gameObject.SetActive(false);
					projectilePlaceholder = base.Module.GetProjectilePlaceholder(handler);
					projectilePlaceholder.transform.parent = projectileStart;
					projectilePlaceholder.transform.localPosition = UnityEngine.Vector3.zero;
					projectilePlaceholder.transform.localRotation = Quaternion.identity;
					projectilePlaceholder.SetActive(true);
				}
				return;
			}
			if (projectileStart == null)
			{
				projectileStart = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<BlockLoader>.Instance.ModulePrefabs.ShootingDirectionVisual).transform;
				projectileStart.parent = base.transform;
				base.Module.ProjectileStart.SetOnTransform(projectileStart);
			}
			projectileStart.FindChild("Vis").gameObject.SetActive(base.ShowDebugVisuals && !base.Module.ShowPlaceholderProjectile);
			if (base.Module.Sounds != null && base.Module.Sounds.Length > 0)
			{
				Transform transform = UnityEngine.Object.Instantiate(SingleInstanceFindOnly<BlockLoader>.Instance.ModulePrefabs.ShootingSoundControl).transform;
				transform.parent = base.transform;
				transform.localPosition = UnityEngine.Vector3.zero;
				ShootingModuleSoundHolder component = transform.GetComponent<ShootingModuleSoundHolder>();
				soundController = transform.GetComponent<RandomSoundController>();
				List<AudioClip> list = new List<AudioClip>();
				object[] sounds = base.Module.Sounds;
				foreach (object obj in sounds)
				{
					if (obj is ResourceReference)
					{
						ModAudioClip modAudioClip = (ModAudioClip)GetResource((ResourceReference)obj);
						list.Add(modAudioClip);
					}
					else if (obj is ShootingModule.CrossbowSounds)
					{
						list.AddRange(component.CrossBowClips);
					}
					else if (obj is ShootingModule.CannonSound)
					{
						list.Add(component.CannonClip);
					}
					else
					{
						MLog.Error("Unknown sound type!");
					}
				}
				soundController.audioclips2 = list.ToArray();
			}
			this.projectile = CreateProjectile();
			spawnedProjectiles = new List<Transform>();
			useProjManager = StatMaster.isHosting && !base.Machine.InternalObject.LocalSim && StatMaster.isMP;
			if (base.SimPhysics && !useProjManager)
			{
				projectilePrefabScript = this.projectile.GetComponent<ProjectileScript>();
				projectileArray = new AttackScript.Projectile[base.Module.PoolSize];
				for (int j = 0; j < projectileArray.Length; j++)
				{
					GameObject gameObject = (GameObject)UnityEngine.Object.Instantiate(this.projectile, projectileStart.position, projectileStart.rotation);
					spawnedProjectiles.Add(gameObject.transform);
					AttackScript.Projectile projectile = new AttackScript.Projectile(gameObject);
					projectileArray[j] = projectile;
					projectile.transform.parent = GetPhysGoal();
					projectile.projectileScript.attackDamage = base.Module.ProjectileInfo.EntityDamage;
					projectile.projectileScript.blockDamageAmount = base.Module.ProjectileInfo.BlockDamage;
					projectile.projectileScript.hasAttached = !projectilePrefabScript.useKillTimer;
					projectile.projectileScript.disableCollider = false;
					projectile.gameObject.SetActive(false);
				}
			}
			timeDiff = 0f;
		}

		public override void OnReload()
		{
			if ((bool)projectileStart)
			{
				base.Module.ProjectileStart.SetOnTransform(projectileStart);
			}
			AmmoLeft = base.Module.DefaultAmmo;
			if ((bool)projectilePlaceholder)
			{
				bool activeSelf = projectilePlaceholder.activeSelf;
				UnityEngine.Object.Destroy(projectilePlaceholder);
				projectilePlaceholder = base.Module.GetProjectilePlaceholder(handler);
				projectilePlaceholder.transform.parent = projectileStart;
				projectilePlaceholder.transform.localPosition = UnityEngine.Vector3.zero;
				projectilePlaceholder.transform.localRotation = Quaternion.identity;
				projectilePlaceholder.SetActive(activeSelf);
			}
			if (!useProjManager && projectileArray != null)
			{
				AttackScript.Projectile[] array = projectileArray;
				foreach (AttackScript.Projectile projectile in array)
				{
					base.Module.UpdateProjectileObject(projectile.gameObject, handler);
				}
			}
		}

		public override void OnReloadAmmo(ref int units, ReloadAmmoType type, bool setAmmo, bool eachBlock)
		{
			if (type != ReloadAmmoType.All && type != base.Module.AmmoType)
			{
				return;
			}
			if (setAmmo)
			{
				if (eachBlock || units < base.Module.DefaultAmmo)
				{
					AmmoLeft = units;
					units = 0;
				}
				else
				{
					units -= base.Module.DefaultAmmo;
					AmmoLeft = base.Module.DefaultAmmo;
				}
			}
			else if (eachBlock || units <= base.Module.DefaultAmmo - AmmoLeft)
			{
				AmmoLeft += units;
				units = 0;
			}
			else
			{
				units -= base.Module.DefaultAmmo - AmmoLeft;
				AmmoLeft = base.Module.DefaultAmmo;
			}
		}

		public bool OnIgnite(FireTag t, Collider c, bool pyroMode)
		{
			if (!base.Module.TriggeredByFire)
			{
				return false;
			}
			if (firing)
			{
				return false;
			}
			StartCoroutine(Fire());
			return true;
		}

		public override void SimulateUpdateAlways()
		{
			if ((FireKey.IsPressed || FireKey.EmulationPressed()) && !HoldToShootToggle.IsActive)
			{
				autoFire = !autoFire;
			}
			if (timeDiff <= 0f)
			{
				if (AmmoLeft > 0 || base.Machine.InfiniteAmmo)
				{
					if ((bool)projectilePlaceholder && !projectilePlaceholder.activeSelf)
					{
						projectilePlaceholder.SetActive(true);
					}
					if (((FireKey.IsHeld || FireKey.EmulationHeld(true)) && HoldToShootToggle.IsActive) || autoFire)
					{
						StartCoroutine(Fire());
					}
				}
			}
			else
			{
				timeDiff -= Time.deltaTime;
			}
		}

		private IEnumerator Fire()
		{
			if (firing || timeDiff > 0f)
			{
				yield break;
			}
			timeDiff = 1f / RateOfFire.Value;
			bool infiniteAmmo = base.Machine.InfiniteAmmo;
			if (infiniteAmmo && !StatMaster.GodTools.HasBeenUsed)
			{
				StatMaster.GodTools.HasBeenUsed = true;
			}
			if ((AmmoLeft <= 0 && !infiniteAmmo) || StatMaster.Rules.DisableProjectiles)
			{
				yield break;
			}
			firing = true;
			bool waited = false;
			if (base.SimPhysics)
			{
				while (!base.Machine.InternalObject.isReady || Time.timeScale == 0f)
				{
					waited = true;
					yield return null;
				}
				if (waited)
				{
					yield return new WaitForFixedUpdate();
				}
			}
			AmmoLeft--;
			yield return new WaitForSeconds(UnityEngine.Random.Range(0f, 0.05f));
			if ((bool)projectilePlaceholder)
			{
				projectilePlaceholder.SetActive(false);
			}
			if (base.SimPhysics)
			{
				AttackScript.Projectile currentProjectile = ((!useProjManager && !projectilePrefabScript.useKillTimer) ? GetProjectileReverse() : GetProjectile());
				if (currentProjectile != null)
				{
					Rigidbody projectileBody = currentProjectile.rigidbody;
					ProjectileScript projectileScript = currentProjectile.projectileScript;
					projectileBody.isKinematic = false;
					projectileScript.enabled = true;
					if (base.IsBurning)
					{
						FireTag fireTag = projectileScript.firecontrol.fireTagCode;
						if (fireTag != null)
						{
							fireTag.Ignite();
						}
					}
					UnityEngine.Vector3 randomDir = projectileStart.forward + UnityEngine.Random.insideUnitSphere * 0.01f;
					projectileBody.velocity = base.Rigidbody.velocity;
					projectileBody.AddForce(100f * PowerSlider.Value * randomDir);
					UnityEngine.Vector3 recoilForce = 100f * PowerSlider.Value * -randomDir * base.Module.RecoilMultiplier;
					base.Rigidbody.AddForce(recoilForce);
				}
			}
			firing = false;
			if ((bool)soundController)
			{
				soundController.Play2(0.15f);
			}
		}

		private void InitProjectile(AttackScript.Projectile proj)
		{
			proj.transform.parent = GetPhysGoal();
			proj.transform.position = projectileStart.position;
			proj.transform.rotation = projectileStart.rotation;
			proj.transform.localScale = UnityEngine.Vector3.one;
			if (!proj.gameObject.activeInHierarchy)
			{
				proj.gameObject.SetActive(true);
			}
			else
			{
				proj.projectileScript.OnEnable();
			}
			proj.projectileScript.ownerID = handler.ParentMachine.PlayerID;
			if ((bool)proj.gyro)
			{
				proj.gyro.localRotation = Quaternion.identity;
			}
		}

		private GameObject CreateProjectile()
		{
			return base.Module.GetProjectilePrefab(handler);
		}

		private AttackScript.Projectile GetProjectile()
		{
			if (useProjManager)
			{
				byte[] array = new byte[13];
				int num = 0;
				NetworkCompression.CompressPosition(projectileStart.position, array, num);
				num += 6;
				NetworkCompression.CompressRotation(projectileStart.rotation, array, num);
				NetworkAddPiece instance = NetworkAddPiece.Instance;
				Transform transform = ProjectileManager.Instance.Spawn((NetworkProjectileType)base.Module.ProjectileId, instance.frame, base.Machine.Player.NetworkId, array);
				spawnedProjectiles.Add(transform);
				AttackScript.Projectile projectile = new AttackScript.Projectile(transform.gameObject);
				projectile.projectileScript.ownerID = handler.ParentMachine.PlayerID;
				return projectile;
			}
			for (int i = 0; i < projectileArray.Length; i++)
			{
				AttackScript.Projectile projectile = projectileArray[i];
				if (!projectile.gameObject)
				{
					GameObject gameObject = UnityEngine.Object.Instantiate(this.projectile, projectileStart.position, projectileStart.rotation) as GameObject;
					spawnedProjectiles.Add(gameObject.transform);
					projectile.NewProjectile(gameObject);
					projectile.transform.parent = GetPhysGoal();
					projectile.projectileScript.ownerID = handler.ParentMachine.PlayerID;
					projectile.gameObject.SetActive(false);
				}
				if (!projectile.gameObject.activeInHierarchy)
				{
					InitProjectile(projectile);
					return projectile;
				}
			}
			for (int j = 0; j < projectileArray.Length; j++)
			{
				AttackScript.Projectile projectile = projectileArray[j];
				if ((bool)projectile.gameObject && (projectile.projectileScript.hasAttached || j == nextProjectile))
				{
					InitProjectile(projectile);
					return projectile;
				}
			}
			return null;
		}

		private AttackScript.Projectile GetProjectileReverse()
		{
			AttackScript.Projectile projectile;
			for (int i = nextProjectile; i < projectileArray.Length; i++)
			{
				projectile = projectileArray[i];
				if (!projectile.gameObject)
				{
					GameObject p = UnityEngine.Object.Instantiate(this.projectile, projectileStart.position, projectileStart.rotation) as GameObject;
					spawnedProjectiles.Add(projectile.transform);
					projectile.NewProjectile(p);
					projectile.transform.parent = GetPhysGoal();
					projectile.projectileScript.ownerID = handler.ParentMachine.PlayerID;
				}
				if (projectile.projectileScript.hasAttached || !projectile.projectileScript.canAttach)
				{
					projectile.rigidbody.interpolation = RigidbodyInterpolation.None;
					InitProjectile(projectile);
					projectile.rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
					nextProjectile = i + 1;
					if (nextProjectile >= projectileArray.Length)
					{
						nextProjectile = 0;
					}
					return projectile;
				}
			}
			projectile = projectileArray[nextProjectile];
			InitProjectile(projectile);
			nextProjectile++;
			if (nextProjectile >= projectileArray.Length)
			{
				nextProjectile = 0;
			}
			return projectile;
		}

		private Transform GetPhysGoal()
		{
			return ReferenceMaster.physicsGoalInstance;
		}
	}
}

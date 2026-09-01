using UnityEngine;

[AddComponentMenu("VFX/GlobalParticles")]
public class GlobalParticles : MonoBehaviour
{
	public enum ParticleType
	{
		SmallWaterSplash = 0,
		WaterSplash = 1,
		LargeWaterSplash = 2,
		Bubbles = 3,
		BigBubbles = 4,
		BloodBurst = 5,
		BloodBurstWater = 6,
		WakeSplash = 7,
		BloodPoolWater = 8,
		BloodTrailWater = 9,
		SandUnderWater = 10,
		BloodBursthit3 = 11,
		DustBurst = 12,
		DustBursthit = 13,
		Foam = 14,
		Ripple = 15,
		BreakWood = 16,
		BreakStone = 17,
		Spark = 18,
		Marker = 19,
		Marker2 = 20
	}

	public const int MAX_PARTICLES = 10000;

	[Header("General Particles")]
	public ParticleSystem[] smallWaterSplashs;

	public ParticleSystem[] waterSplashs;

	public ParticleSystem[] largeWaterSplashs;

	public ParticleSystem[] bubbles;

	public ParticleSystem[] bigBubbles;

	public ParticleSystem[] sandUnderWater;

	public ParticleSystem[] wakeSplash;

	public ParticleSystem[] foam;

	public ParticleSystem[] ripple;

	public ParticleSystem[] marker;

	public ParticleSystem[] marker2;

	[Header("AI Particles")]
	public ParticleSystem[] bloodBurst;

	public ParticleSystem[] bloodBursthit3;

	public ParticleSystem[] bloodBurstWater;

	public ParticleSystem[] bloodPoolWater;

	public ParticleSystem[] bloodTrailWater;

	public ParticleSystem[] dustBurst;

	public ParticleSystem[] dustBursthit;

	[Header("Breaking Particles")]
	public ParticleSystem[] woodBreak;

	public ParticleSystem[] stoneBreak;

	public ParticleSystem[] spark;

	private static ParticleSystem.EmitParams emitter = default(ParticleSystem.EmitParams);

	private static GlobalParticles instance;

	private static ParticleSystem[] currentSystem;

	private static bool hasInstance = false;

	protected void Start()
	{
		instance = this;
		hasInstance = true;
		WinCondition winCondition = WinCondition.Instance;
		Transform parent = ((!winCondition) ? null : winCondition.transform.FindChild("STATIC"));
		Transform transform = new GameObject("PFXParent").transform;
		transform.parent = parent;
		ResetEmitter();
		for (int i = 0; i < smallWaterSplashs.Length; i++)
		{
			smallWaterSplashs[i] = Object.Instantiate(smallWaterSplashs[i], transform) as ParticleSystem;
			SetupParticles(smallWaterSplashs[i]);
		}
		for (int j = 0; j < waterSplashs.Length; j++)
		{
			waterSplashs[j] = Object.Instantiate(waterSplashs[j], transform) as ParticleSystem;
			SetupParticles(waterSplashs[j]);
		}
		for (int k = 0; k < largeWaterSplashs.Length; k++)
		{
			largeWaterSplashs[k] = Object.Instantiate(largeWaterSplashs[k], transform) as ParticleSystem;
			SetupParticles(largeWaterSplashs[k]);
		}
		for (int l = 0; l < bubbles.Length; l++)
		{
			bubbles[l] = Object.Instantiate(bubbles[l], transform) as ParticleSystem;
			SetupParticles(bubbles[l]);
		}
		for (int m = 0; m < bigBubbles.Length; m++)
		{
			bigBubbles[m] = Object.Instantiate(bigBubbles[m], transform) as ParticleSystem;
			SetupParticles(bigBubbles[m]);
		}
		for (int n = 0; n < bloodBurst.Length; n++)
		{
			bloodBurst[n] = Object.Instantiate(bloodBurst[n], transform) as ParticleSystem;
			ParticleSystem obj = bloodBurst[n];
			Color bloodColor = StatMaster.BloodColor;
			bloodBurst[n].startColor = bloodColor;
			obj.startColor = bloodColor;
			bloodBurst[n].GetComponent<ParticleSystemRenderer>().material.SetColor("_TintColor", Color.white);
			SetupParticles(bloodBurst[n]);
		}
		for (int num = 0; num < bloodBurstWater.Length; num++)
		{
			bloodBurstWater[num] = Object.Instantiate(bloodBurstWater[num], transform) as ParticleSystem;
			SetupParticles(bloodBurstWater[num]);
		}
		for (int num2 = 0; num2 < wakeSplash.Length; num2++)
		{
			wakeSplash[num2] = Object.Instantiate(wakeSplash[num2], transform) as ParticleSystem;
			SetupParticles(wakeSplash[num2]);
		}
		for (int num3 = 0; num3 < sandUnderWater.Length; num3++)
		{
			sandUnderWater[num3] = Object.Instantiate(sandUnderWater[num3], transform) as ParticleSystem;
			SetupParticles(sandUnderWater[num3]);
		}
		for (int num4 = 0; num4 < bloodBursthit3.Length; num4++)
		{
			bloodBursthit3[num4] = Object.Instantiate(bloodBursthit3[num4], transform) as ParticleSystem;
			bloodBursthit3[num4].startColor = StatMaster.BloodColor;
			bloodBursthit3[num4].GetComponent<ParticleSystemRenderer>().material.SetColor("_TintColor", Color.white);
			SetupParticles(bloodBursthit3[num4]);
		}
		Color startColor = Color.red * 0.5f + Color.yellow * 0.4f + Color.blue * 0.1f;
		startColor.a = 1f;
		for (int num5 = 0; num5 < dustBurst.Length; num5++)
		{
			dustBurst[num5] = Object.Instantiate(dustBurst[num5], transform) as ParticleSystem;
			dustBurst[num5].startColor = startColor;
			SetupParticles(dustBurst[num5]);
		}
		for (int num6 = 0; num6 < dustBursthit.Length; num6++)
		{
			dustBursthit[num6] = Object.Instantiate(dustBursthit[num6], transform) as ParticleSystem;
			dustBursthit[num6].startColor = startColor;
			SetupParticles(dustBursthit[num6]);
		}
		for (int num7 = 0; num7 < foam.Length; num7++)
		{
			foam[num7] = Object.Instantiate(foam[num7], transform) as ParticleSystem;
			SetupParticles(foam[num7]);
		}
		for (int num8 = 0; num8 < ripple.Length; num8++)
		{
			ripple[num8] = Object.Instantiate(ripple[num8], transform) as ParticleSystem;
			SetupParticles(ripple[num8]);
		}
		for (int num9 = 0; num9 < woodBreak.Length; num9++)
		{
			woodBreak[num9] = Object.Instantiate(woodBreak[num9], transform) as ParticleSystem;
			SetupParticles(woodBreak[num9]);
		}
		for (int num10 = 0; num10 < stoneBreak.Length; num10++)
		{
			stoneBreak[num10] = Object.Instantiate(stoneBreak[num10], transform) as ParticleSystem;
			SetupParticles(stoneBreak[num10]);
		}
		for (int num11 = 0; num11 < spark.Length; num11++)
		{
			spark[num11] = Object.Instantiate(spark[num11], transform) as ParticleSystem;
			SetupParticles(spark[num11]);
		}
		for (int num12 = 0; num12 < marker.Length; num12++)
		{
			marker[num12] = Object.Instantiate(marker[num12], transform) as ParticleSystem;
			SetupParticles(marker[num12]);
		}
		for (int num13 = 0; num13 < marker2.Length; num13++)
		{
			marker2[num13] = Object.Instantiate(marker2[num13], transform) as ParticleSystem;
			SetupParticles(marker2[num13]);
		}
	}

	private void OnDestroy()
	{
		hasInstance = false;
	}

	public static bool GetParticleSystem(int type, out ParticleSystem[] system)
	{
		system = null;
		if (!hasInstance)
		{
			Debug.LogError("Missing particle system");
			return false;
		}
		switch (type)
		{
		case 0:
			system = instance.smallWaterSplashs;
			return true;
		case 1:
			system = instance.waterSplashs;
			return true;
		case 2:
			system = instance.largeWaterSplashs;
			return true;
		case 3:
			system = instance.bubbles;
			return true;
		case 4:
			system = instance.bigBubbles;
			return true;
		case 5:
			system = instance.bloodBurst;
			return true;
		case 6:
			system = instance.bloodBurstWater;
			return true;
		case 7:
			system = instance.wakeSplash;
			return true;
		case 8:
			system = instance.bloodPoolWater;
			return true;
		case 9:
			system = instance.bloodTrailWater;
			return true;
		case 10:
			system = instance.sandUnderWater;
			return true;
		case 11:
			system = instance.bloodBursthit3;
			return true;
		case 12:
			system = instance.dustBurst;
			return true;
		case 13:
			system = instance.dustBursthit;
			return true;
		case 14:
			system = instance.foam;
			return true;
		case 15:
			system = instance.ripple;
			return true;
		case 16:
			system = instance.woodBreak;
			return true;
		case 17:
			system = instance.stoneBreak;
			return true;
		case 18:
			system = instance.spark;
			return true;
		case 19:
			system = instance.marker;
			return true;
		case 20:
			system = instance.marker2;
			return true;
		default:
			return false;
		}
	}

	public static bool EmitParticleBursts(int type, Vector3 emitPosition, bool applyShapeToPosition = true)
	{
		if (!GetParticleSystem(type, out currentSystem))
		{
			return false;
		}
		if (currentSystem[0].particleCount > 10000)
		{
			return false;
		}
		emitter.applyShapeToPosition = applyShapeToPosition;
		emitter.position = emitPosition;
		int[] array = new int[currentSystem.Length];
		ParticleSystem.Burst[] array2 = new ParticleSystem.Burst[1];
		for (int i = 0; i < currentSystem.Length; i++)
		{
			currentSystem[i].emission.GetBursts(array2);
			array[i] = ((array2.Length <= 0) ? 2 : Random.Range(array2[0].minCount, array2[0].maxCount));
		}
		Emit(array);
		return true;
	}

	public static bool EmitParticleBursts(int type, ParticleSystem.EmitParams customParams)
	{
		if (!GetParticleSystem(type, out currentSystem))
		{
			return false;
		}
		if (currentSystem[0].particleCount > 10000)
		{
			return false;
		}
		int[] array = new int[currentSystem.Length];
		ParticleSystem.Burst[] array2 = new ParticleSystem.Burst[1];
		for (int i = 0; i < currentSystem.Length; i++)
		{
			currentSystem[i].emission.GetBursts(array2);
			array[i] = ((array2.Length <= 0) ? 2 : Random.Range(array2[0].minCount, array2[0].maxCount));
		}
		emitter = customParams;
		Emit(array);
		return true;
	}

	public static bool EmitParticleSet(int type, Vector3 emitPosition, int[] particleAmount, bool applyShapeToPosition = true)
	{
		emitter.applyShapeToPosition = applyShapeToPosition;
		emitter.position = emitPosition;
		if (!GetParticleSystem(type, out currentSystem))
		{
			return false;
		}
		Emit(particleAmount);
		return true;
	}

	public static bool EmitParticleAmount(int type, Vector3 emitPosition, int amount, bool applyShapeToPosition = true)
	{
		if (!GetParticleSystem(type, out currentSystem))
		{
			return false;
		}
		if (currentSystem[0].particleCount > 10000)
		{
			return false;
		}
		emitter.applyShapeToPosition = applyShapeToPosition;
		emitter.position = emitPosition;
		int[] array = new int[currentSystem.Length];
		for (int i = 0; i < array.Length; i++)
		{
			array[i] = amount;
		}
		Emit(array);
		return true;
	}

	public static bool EmitParticle(int type, ParticleSystem.EmitParams customParams, params int[] amount)
	{
		if (!GetParticleSystem(type, out currentSystem))
		{
			return false;
		}
		if (currentSystem[0].particleCount > 10000)
		{
			return false;
		}
		for (int i = 0; i < currentSystem.Length; i++)
		{
			currentSystem[i].Emit(customParams, amount[i]);
		}
		return true;
	}

	private static void Emit(int[] particlesToEmit)
	{
		emitter.randomSeed = (uint)Random.Range(int.MinValue, int.MaxValue);
		for (int i = 0; i < currentSystem.Length; i++)
		{
			currentSystem[i].Emit(emitter, particlesToEmit[i]);
		}
		ResetEmitter();
	}

	private static void ResetEmitter()
	{
		emitter.ResetAngularVelocity();
		emitter.ResetAxisOfRotation();
		emitter.ResetPosition();
		emitter.ResetRotation();
		emitter.ResetStartColor();
		emitter.ResetStartLifetime();
		emitter.ResetStartSize();
		emitter.ResetVelocity();
	}

	private static void SetupParticles(ParticleSystem ps)
	{
		ps.transform.position = new Vector3(-10000f, -10000f, -10000f);
		emitter.position = ps.transform.position;
		ps.Emit(emitter, 1);
	}
}

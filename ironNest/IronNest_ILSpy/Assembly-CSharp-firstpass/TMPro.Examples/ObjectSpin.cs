using System;
using Cpp2ILInjected;
using UnityEngine;

namespace TMPro.Examples;

public class ObjectSpin : MonoBehaviour
{
	public enum MotionType
	{
		Rotation,
		SearchLight,
		Translation
	}

	public MotionType Motion;

	public Vector3 TranslationDistance;

	public float TranslationSpeed;

	public float SpinSpeed;

	public int RotationRange;

	private Transform m_transform;

	private float m_time;

	private Vector3 m_prevPOS;

	private Vector3 m_initial_Rotation;

	private Vector3 m_initial_Position;

	private Color32 m_lightColor;

	private unsafe void Awake()
	{
		//IL_002f: Expected O, but got Ref
		//IL_0042: Expected O, but got F4
		//IL_006f: Expected O, but got F4
		//IL_00ef: Expected O, but got Ref
		Transform transform = base.transform;
		m_transform = transform;
		Quaternion rotation = m_transform.rotation;
		Quaternion rotation2 = default(Quaternion);
		Vector3 vector = Quaternion.Internal_ToEulerRad(ref rotation2);
		object obj = default(object);
		Vector3 vector2 = Quaternion.Internal_MakePositive((Vector3)(&obj));
		m_initial_Rotation = (Vector3)vector2.x;
		_ = vector2.z;
		Vector3 position = m_transform.position;
		m_initial_Position = (Vector3)position.x;
		_ = position.z;
		Cpp2ILHelpers.NoteDecompilerIssue("Method not found @180696670");
		UnityEngine.Object obj2 = default(UnityEngine.Object);
		if (obj2 != null)
		{
			Color color = ((Light)obj2).color;
		}
		float num = default(float);
		Color32 lightColor = (Color)(&num);
		m_lightColor = lightColor;
	}

	private unsafe void Update()
	{
		//IL_0043: Expected O, but got I4
		//IL_019b: Expected O, but got Ref
		//IL_00f2: Expected O, but got Ref
		//IL_0115: Expected O, but got F4
		bool flag = Motion == MotionType.Rotation;
		if (!flag)
		{
			object obj = Motion - 1;
			Vector3 euler = default(Vector3);
			if (!flag)
			{
				if ((nint)obj == 1)
				{
					float deltaTime = Time.deltaTime;
					float num = deltaTime * TranslationSpeed;
					float time = num + m_time;
					m_time = time;
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
					Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033DE70");
					m_transform.position = (Vector3)(&euler);
					Vector3 position = m_transform.position;
					m_prevPOS = (Vector3)position.x;
					_ = position.z;
				}
			}
			else
			{
				float deltaTime2 = Time.deltaTime;
				float num2 = deltaTime2 * SpinSpeed;
				float time2 = num2 + m_time;
				m_time = time2;
				float num3 = (float)m_initial_Rotation * ((float)Math.PI / 180f);
				Cpp2ILHelpers.NoteDecompilerIssue("Method not found @18033E400");
				Quaternion quaternion = Quaternion.Internal_FromEulerRad(ref euler);
				m_transform.rotation = (Quaternion)(&euler);
			}
		}
		else
		{
			float deltaTime3 = Time.deltaTime;
			float yAngle = deltaTime3 * SpinSpeed;
			m_transform.Rotate(0f, yAngle, 0f);
		}
	}

	public ObjectSpin()
	{
		Vector3 translationDistance = default(Vector3);
		TranslationDistance = translationDistance;
		_ = 0;
		TranslationSpeed = 1f;
		SpinSpeed = 5f;
		RotationRange = 15;
		base._002Ector();
	}
}

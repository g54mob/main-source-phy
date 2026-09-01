using System;
using Landfall.TABS.WinConditions;
using UnityEngine;

namespace Landfall.TABS
{
	[Serializable]
	public class UnitLayout
	{
		[Serializable]
		public class Unit
		{
			public float XPosition;

			public float YPosition;

			public float ZPosition;

			public float Rotation;

			[SerializeField]
			private SerializedRuntimeReference m_serializedRuntimeReference;

			[NonSerialized]
			private RuntimeReference m_runtimeReference;

			public DatabaseID unitType;

			public Vector3 Position
			{
				get
				{
					return new Vector3(XPosition, YPosition, ZPosition);
				}
				private set
				{
					XPosition = value.x;
					YPosition = value.y;
					ZPosition = value.z;
				}
			}

			public SerializedRuntimeReference SerializedRuntimeReference => m_serializedRuntimeReference;

			public RuntimeReference RuntimeReference
			{
				get
				{
					return m_runtimeReference;
				}
				set
				{
					m_runtimeReference = value;
					if (m_runtimeReference != null)
					{
						m_serializedRuntimeReference = new SerializedRuntimeReference(m_runtimeReference);
					}
				}
			}

			public void SetPosition(Vector3 pos)
			{
				Position = pos;
			}

			public Unit(Vector3 pos, float rotation, UnitBlueprint bluePrint, RuntimeReference runtimeReference)
			{
				XPosition = pos.x;
				YPosition = pos.y;
				ZPosition = pos.z;
				Rotation = rotation;
				unitType = bluePrint.Entity.GUID;
				RuntimeReference = runtimeReference;
			}

			public Quaternion GetRotation()
			{
				return Quaternion.Euler(0f, Rotation, 0f);
			}
		}

		public Unit[] Units;
	}
}

using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Landfall.TABC
{
	public class BoardManager : SerializedMonoBehaviour
	{
		public static BoardManager instance;

		public Board m_CurrentBoard;

		private void Awake()
		{
			instance = this;
		}

		public int UnitsOnTheBoard()
		{
			return m_CurrentBoard.Units.Count;
		}

		public void AddNewUnit(GameObject unitObject, UnitDataInstance data, int2 pos)
		{
			data.boardPos = pos;
			Board.Unit item = new Board.Unit(unitObject, data, pos, unitObject.GetComponent<UnitData>());
			m_CurrentBoard.Units.Add(item);
		}

		public void RemoveUnit(int2 pos)
		{
			m_CurrentBoard.Units.Remove(GetUnitFromPos(pos));
		}

		public void MoveUnit(GameObject objectToMove, int2 pos)
		{
			Board.Unit unit = UnitFromObject(objectToMove);
			unit.pos = pos;
		}

		private Board.Unit UnitFromObject(GameObject unitObject)
		{
			Board.Unit result = default(Board.Unit);
			for (int i = 0; i < m_CurrentBoard.Units.Count; i++)
			{
				if (unitObject.gameObject == m_CurrentBoard.Units[i].unitObject)
				{
					result = m_CurrentBoard.Units[i];
				}
			}
			return result;
		}

		public BoardHoverInfo MouseIsOverBoard()
		{
			bool isHovered = false;
			BoardHoverInfo boardHoverInfo = new BoardHoverInfo();
			RaycastHit[] array = Physics.RaycastAll(MainCam.instance.cam.ScreenPointToRay(Input.mousePosition), 100f);
			for (int i = 0; i < array.Length; i++)
			{
				BoardData component = array[i].transform.GetComponent<BoardData>();
				if ((bool)component)
				{
					isHovered = true;
					if ((bool)component)
					{
						boardHoverInfo.boardPos = component.WorldToBoardPos(array[i].point);
					}
				}
			}
			boardHoverInfo.isHovered = isHovered;
			return boardHoverInfo;
		}

		public void RespawnUnits()
		{
			for (int i = 0; i < m_CurrentBoard.Units.Count; i++)
			{
				m_CurrentBoard.Units[i].unitData.RespawnUnit();
			}
		}

		public Board.Unit GetUnitFromPos(int2 pos)
		{
			Board.Unit result = default(Board.Unit);
			for (int i = 0; i < m_CurrentBoard.Units.Count; i++)
			{
				if (pos.x == m_CurrentBoard.Units[i].pos.x && pos.y == m_CurrentBoard.Units[i].pos.y)
				{
					result = m_CurrentBoard.Units[i];
				}
			}
			return result;
		}
	}
}

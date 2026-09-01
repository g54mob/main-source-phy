using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Modding.Blocks;
using Modding.Levels;

namespace Modding.Common
{
	public class Player
	{
		private readonly int _hashCode;

		public string Name
		{
			get
			{
				return InternalObject.name;
			}
		}

		public ushort NetworkId
		{
			get
			{
				return InternalObject.networkId;
			}
		}

		public string SteamId
		{
			get
			{
				return InternalObject.platformUserId.ToString();
			}
		}

		public bool IsLocalPlayer
		{
			get
			{
				return InternalObject.isLocalPlayer;
			}
		}

		public bool IsHost
		{
			get
			{
				return NetworkId == 0;
			}
		}

		public bool VoteState
		{
			get
			{
				return InternalObject.voteState;
			}
		}

		public bool IsSpectator
		{
			get
			{
				return InternalObject.isSpectator;
			}
		}

		public bool InLocalSim
		{
			get
			{
				return InternalObject.inLocalSim;
			}
		}

		public MPTeam Team
		{
			get
			{
				return InternalObject.team;
			}
		}

		public PlayerMachine Machine
		{
			get
			{
				return PlayerMachine.From(InternalObject.machine);
			}
		}

		public EntityPrefabInfo ActivePrefab
		{
			get
			{
				return EntityPrefabInfo.From((!IsLocalPlayer) ? InternalObject.activePrefab : LevelEditor.Instance.ActiveObjectBrush);
			}
		}

		public ReadOnlyCollection<Entity> Selection
		{
			get
			{
				if (!IsLocalPlayer)
				{
					return null;
				}
				return LevelEditor.Instance.Selection.Select(Entity.From).ToList().AsReadOnly();
			}
		}

		public int Ping
		{
			get
			{
				return InternalObject.ping;
			}
		}

		public PlayerData InternalObject { get; private set; }

		private Player(PlayerData data)
		{
			InternalObject = data;
			_hashCode = data.GetHashCode();
		}

		public override string ToString()
		{
			return "Player " + Name + "(" + NetworkId + ")";
		}

		protected bool Equals(Player other)
		{
			return object.Equals(InternalObject, other.InternalObject);
		}

		public override bool Equals(object obj)
		{
			if (object.ReferenceEquals(null, obj))
			{
				return false;
			}
			if (object.ReferenceEquals(this, obj))
			{
				return true;
			}
			if (obj.GetType() != GetType())
			{
				return false;
			}
			return Equals((Player)obj);
		}

		public override int GetHashCode()
		{
			return _hashCode;
		}

		public static Player From(ushort networkId)
		{
			return From(Playerlist.GetPlayer(networkId));
		}

		public static Player From(PlayerData data)
		{
			if (data == null)
			{
				return null;
			}
			return new Player(data);
		}

		public static Player GetLocalPlayer()
		{
			return From(PlayerData.localPlayer);
		}

		public static Player GetHost()
		{
			return From(0);
		}

		public static List<Player> GetAllPlayers()
		{
			return Playerlist.Players.Select(From).ToList();
		}

		public static bool operator ==(Player left, Player right)
		{
			return object.Equals(left, right);
		}

		public static bool operator !=(Player left, Player right)
		{
			return !object.Equals(left, right);
		}
	}
}

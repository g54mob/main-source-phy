using Landfall.TABS_Input;
using UnityEngine;

namespace TFBGames
{
	public class PlayerCamera : MonoBehaviour
	{
		private InputService inputService;

		public Player CurrentPlayer { get; private set; }

		public bool IsClone { get; private set; }

		public MainCam MainCam { get; private set; }

		public Camera Camera { get; private set; }

		public CameraAbilityPossess PossessCamera { get; private set; }

		public PlayerActions Actions { get; private set; }

		private void Awake()
		{
			MainCam = base.gameObject.GetComponentInChildren<MainCam>(includeInactive: true);
			Camera = base.gameObject.GetComponentInChildren<Camera>(includeInactive: true);
			PossessCamera = base.gameObject.GetComponentInChildren<CameraAbilityPossess>(includeInactive: true);
			inputService = ServiceLocator.GetService<InputService>();
			SetPlayer(Player.Any);
		}

		public void Initialize(bool isClone)
		{
			IsClone = isClone;
			CurrentPlayer = Player.Any;
		}

		public void SetPlayer(Player player)
		{
			Actions = inputService.GetPlayerActions(player);
			CurrentPlayer = player;
		}
	}
}

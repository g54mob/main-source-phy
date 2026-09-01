using Landfall.TABS_Input;
using TFBGames;
using UnityEngine;

public class GridCellHoverMenu : MonoBehaviour
{
	[SerializeField]
	private GameObject controllerMenu;

	[SerializeField]
	private GameObject keyboardMenu;

	private InputService inputService;

	private void Awake()
	{
		inputService = ServiceLocator.GetService<InputService>();
	}

	private void Start()
	{
		ShowMenu(PlayerActions.Instance.InputType);
	}

	private void ShowMenu(InputType inputType)
	{
		controllerMenu.SetActive(inputType == InputType.Controller);
		keyboardMenu.SetActive(inputType != InputType.Controller);
	}

	private void OnEnable()
	{
		ShowMenu(PlayerActions.Instance.InputType);
		inputService.InputChanged += ShowMenu;
	}

	private void OnDisable()
	{
		inputService.InputChanged -= ShowMenu;
	}
}

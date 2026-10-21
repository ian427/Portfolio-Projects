using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu( fileName = "InputReader", menuName = "Scriptable Objects/Input Reader" )]
public class InputReader : ScriptableObject, GameInput.IPlayerActions, GameInput.IUIActions
{
	// Player events.
	public event UnityAction<Vector2> MoveEvent;
	public event UnityAction<Vector2, bool> AimEvent;
	public event UnityAction RollEventStart; 

	// UI Events

	public event UnityAction<bool> PauseGameEvent;

	private GameInput _gameInput;

	private void OnEnable()
	{
		if ( _gameInput == null )
		{
			_gameInput = new GameInput();
			_gameInput.Player.SetCallbacks( this );
			_gameInput.UI.SetCallbacks( this );
		}

		EnablePlayerInput();
	}

	private void OnDisable()
	{
		DisableAllInput();
	}

	public void EnablePlayerInput()
	{
		_gameInput.Player.Enable();
		_gameInput.UI.Disable();
	}

	public void DisablePlayerInput()
	{
		_gameInput.Player.Disable();
		_gameInput.UI.Enable();
	}

	public void DisableAllInput()
	{
		_gameInput.Player.Disable();
		_gameInput.UI.Disable();
	}

	private bool IsDeviceMouse( InputAction.CallbackContext context ) => context.control.device.name == "Mouse";

	// Player actions. 
	public void OnMove( InputAction.CallbackContext context )
	{
		MoveEvent?.Invoke( context.ReadValue<Vector2>().normalized );
	}

	public void OnAim( InputAction.CallbackContext context )
	{
		AimEvent?.Invoke( context.ReadValue<Vector2>(), IsDeviceMouse( context ) );
	}

	public void OnRotate( InputAction.CallbackContext context )
	{

	}

	public void OnAttack( InputAction.CallbackContext context )
	{

	}

	public void OnRoll( InputAction.CallbackContext context )
	{
		if ( context.action.phase == InputActionPhase.Started )
		{
			RollEventStart?.Invoke();
		}
	}

	public void OnInteract( InputAction.CallbackContext context )
	{

	}

	public void OnPause( InputAction.CallbackContext context )
	{
		DisablePlayerInput();
		PauseGameEvent?.Invoke( true );
	}

	// UI Actions
	public void OnNavigate( InputAction.CallbackContext context )
	{
		
	}

	public void OnUnpause( InputAction.CallbackContext context )
	{
		EnablePlayerInput();
		PauseGameEvent?.Invoke( false );
	}

	public void OnSubmit( InputAction.CallbackContext context )
	{
		
	}

	public void OnCancel( InputAction.CallbackContext context )
	{
		
	}

	public void OnPoint( InputAction.CallbackContext context )
	{
		
	}

	public void OnClick( InputAction.CallbackContext context )
	{
		
	}

	public void OnRightClick( InputAction.CallbackContext context )
	{
		
	}

	public void OnMiddleClick( InputAction.CallbackContext context )
	{
		
	}

	public void OnScrollWheel( InputAction.CallbackContext context )
	{
		
	}

	public void OnTrackedDevicePosition( InputAction.CallbackContext context )
	{
		
	}

	public void OnTrackedDeviceOrientation( InputAction.CallbackContext context )
	{
		
	}
}

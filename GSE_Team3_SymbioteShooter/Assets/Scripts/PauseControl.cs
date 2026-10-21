using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

/// <summary>
/// PauseControl handles when the player hits a key bound to the Pause action in the Action Map.
/// The approach to pausing is not ideal; we set <c>Time.timescale</c> to 0 which works but causes issues
/// things like UI animations will also stop, which we may not want to happen just because we've paused the game.
/// </summary>
public class PauseControl : MonoBehaviour
{
    [SerializeField] private GameObject hudCanvas;
    [SerializeField] private GameObject pauseMenu;

	[SerializeField] private InputReader inputReader;

    public static UnityEvent<bool> OnPause = new UnityEvent<bool>(); 
    
    private bool _isPaused;

	private void OnEnable()
	{
		inputReader.PauseGameEvent += PauseGame;
	}

	private void OnDisable()
	{
		inputReader.PauseGameEvent -= PauseGame;
	}

	private void PauseGame(bool isPaused)
	{
		_isPaused = isPaused;

		if ( _isPaused )
		{
			hudCanvas.SetActive( false );
			pauseMenu.SetActive( true );
		}
		else
		{
			hudCanvas.SetActive( true );
			pauseMenu.SetActive( false );
		}

		OnPause?.Invoke( _isPaused );
	}
}

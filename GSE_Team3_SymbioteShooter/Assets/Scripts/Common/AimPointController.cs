using UnityEngine;

public class AimPointController : MonoBehaviour
{
	[SerializeField] private InputReader inputReader;
	[SerializeField] private AimPointPositionSO aimPointPosition;
	[SerializeField] private float distanceFromCamera = 14f;

	private Transform _transform;
	private Camera _mainCamera; 

	private Vector2 _mousePosition;
	private Vector3 _mousePositionInWorld; 
	
	private void Start()
	{
		_transform = GetComponent<Transform>();
		_mainCamera = Camera.main; 
	}

	private void OnEnable()
	{
		inputReader.AimEvent += OnAim;
	}

	private void OnDisable()
	{
		inputReader.AimEvent -= OnAim;
	}

	private void OnAim( Vector2 input, bool isMouse )
	{
		_mousePosition = input;
	}

	private void Update()
	{
		Vector3 pos = new( _mousePosition.x, _mousePosition.y, distanceFromCamera );
		_mousePositionInWorld = _mainCamera.ScreenToWorldPoint( pos );

		_transform.position = _mousePositionInWorld;

		aimPointPosition.WorldPosition = _mousePositionInWorld;
		aimPointPosition.WorldPosition.y = 0f;
	}
}

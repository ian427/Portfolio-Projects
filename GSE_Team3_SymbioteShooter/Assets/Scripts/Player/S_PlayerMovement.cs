using System;
using Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
	public class S_PlayerMovment : MonoBehaviour
	{
		[SerializeField] private InputReader _inputReader;
		private Rigidbody _rigidbody;

		[SerializeField] private float _moveSpeed = 5.0f;
		private Vector3 _velocity = Vector3.zero;
		private Vector3 _movement;
		private Vector3 _rollMovement;
		private Animator _anim;
		private RollControl _rollControl;

		[SerializeField] private AimPointPositionSO _aimPointPosition;

		[SerializeField] private bool shouldStopMovement;

		private Vector3 _aimPosition = Vector3.zero;
		private Transform _characterTransform;
		private Camera _mainCamera;

		private const float DirOffset = 2.0f;
		private const float MinDistance = 0.4f;

		private static readonly int Roll = Animator.StringToHash( "Roll" );
		private static readonly int VelocityX = Animator.StringToHash( "VelocityX" );
		private static readonly int VelocityZ = Animator.StringToHash( "VelocityZ" );

		private bool _isPaused;

		private void OnEnable() => EnableInput();
		private void OnDisable() => DisableInput();

		public void Start()
		{
			_anim = gameObject.GetComponentInChildren<Animator>();
			_mainCamera = Camera.main;
			_characterTransform = transform;
			_rigidbody = GetComponent<Rigidbody>();
			_rollControl = GetComponent<RollControl>();
		}

		public void PauseHandler( bool isPaused )
		{
			_isPaused = isPaused;
			_anim.enabled = !isPaused;
		}

		public void Update()
		{
			if ( _isPaused ) return;

			float distance = Vector3.Distance( transform.position, _aimPosition );
			if ( distance <= MinDistance )
			{
				if ( shouldStopMovement )
				{
					_velocity = Vector2.zero;
				}
				else
				{
					// this approach leads to some weird behaviour once you move "past" where the aim point is.
					_aimPosition = transform.position + ( transform.forward * 2.0f );
				}
			}

			_anim.SetFloat( VelocityX, _rigidbody.linearVelocity.x );
			_anim.SetFloat( VelocityZ, _rigidbody.linearVelocity.z );
			_characterTransform.LookAt( _aimPointPosition.WorldPosition );
		}

		private void FixedUpdate()
		{
			MovePlayer();
		}

		private void MovePlayer()
		{
			Vector3 targetVelocity; 

			if ( _rollControl.IsRolling )
			{
				targetVelocity = _rollMovement * _moveSpeed * 1.5f; 
			}
			else
			{
				targetVelocity = _moveSpeed * _movement;
			}

			_rigidbody.linearVelocity = Vector3.SmoothDamp( _rigidbody.linearVelocity, targetVelocity, ref _velocity, 0.015f );
		}

		#region Player Input
		private void EnableInput()
		{
			_inputReader.MoveEvent += OnMove;
			_inputReader.RollEventStart += OnRollStart;
		}

		private void DisableInput()
		{
			_inputReader.MoveEvent -= OnMove;
			_inputReader.RollEventStart -= OnRollStart;
		}

		private void OnMove( Vector2 input )
		{
			_movement = new( input.x, 0f, input.y );
		}

		private void OnRollStart()
		{
			if ( _rollControl.CanRoll )
			{
				_anim.SetTrigger( Roll );
				_rollMovement = _movement != Vector3.zero ? _movement : (_aimPointPosition.WorldPosition - _rigidbody.position).normalized; 
				_rollControl.OnRollStart();
			}
		}

		// Rotate uses GamePad stick or Keyboard
		/*public void OnRotate(InputAction.CallbackContext context)
        {
            if (_isPaused) return;
            
            Vector2 direction = context.ReadValue<Vector2>();

            // gives us a bit of a dead zone on the stick
            if (direction.magnitude <= 0.2f) return;
            
            Vector3 normalizedDir = new Vector3(direction.x, 0.0f, direction.y).normalized;
            
            Vector3 offsetPosition = _characterTransform.position + normalizedDir * DirOffset;
            Vector3 aimPosition = new Vector3(offsetPosition.x, _aimPointObject.position.y, offsetPosition.z);
            
            _aimPointObject.position = aimPosition;
            _aimPosition = offsetPosition;
        }*/
		#endregion
	}
}

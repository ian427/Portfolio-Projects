using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


namespace Player
{
    [RequireComponent(typeof(SphereCollider), typeof(CapsuleCollider))]
    public class RollControl : MonoBehaviour
    {
        [SerializeField] private float _coolDown = 5f;
        //float nextroll; 
		private bool _canRoll = true;
		private bool _isRolling = false;

        private CapsuleCollider _capsule;
        private SphereCollider _sphere;

		public bool CanRoll { get { return _canRoll; } }
		public bool IsRolling { get { return _isRolling; } }

		private void Start()
        {
            _canRoll = true;
            _capsule = GetComponent<CapsuleCollider>();
            _sphere = GetComponent<SphereCollider>();
        }

        public void OnRollStart()
        {
			if ( !_canRoll ) return; 
            
            _capsule.enabled = false;
            _sphere.enabled = true;
            _canRoll = false;
			_isRolling = true;
            //  nextroll = Time.time +_coolDown;
        }

        public void OnRollEnd()
        {
            _capsule.enabled = true;
            _sphere.enabled = false;
			_isRolling = false;
            StartCoroutine(CoolDown());
        }

        IEnumerator CoolDown()
        {
            yield return new WaitForSeconds(_coolDown); //adds delay
            _canRoll = true;
        }
    } 
}

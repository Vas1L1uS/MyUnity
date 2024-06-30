using System;
using UnityEngine;

namespace aaa_Project.Scripts
{
    public class CharacterMovement : MonoBehaviour
    {
        public event Action Died;
        
        [SerializeField]private Animator  _animator;
        [SerializeField]private float     _moveSpeed = 5f;

        private Vector3 _movement;
        private bool    _isWalk;
        private bool    _dead;

        public void Kill()
        {
            if (_dead) return;
            
            _animator.enabled = false;
            _dead = true;
            Died?.Invoke();
        }

        private void Update()
        {
            if (_dead) return;
            
            _movement.x = Input.GetAxisRaw("Horizontal");
            _movement.z = Input.GetAxisRaw("Vertical");
            _isWalk = Input.GetKey(KeyCode.LeftShift);
            
            float currentSpeed = _moveSpeed * _movement.magnitude;

            if (_isWalk)
            {
                currentSpeed /= 2;
            }
                
            transform.position = Vector3.MoveTowards(transform.position, transform.position + _movement.normalized, currentSpeed * Time.deltaTime);
            _animator.SetFloat("Speed", currentSpeed);

            if (_movement != Vector3.zero)
            {
                float angle = Mathf.Atan2(_movement.x, _movement.z) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0, angle, 0));
            }
        }
    }
}

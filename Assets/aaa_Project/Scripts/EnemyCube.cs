using DG.Tweening;
using UnityEngine;

namespace aaa_Project.Scripts
{
    public class EnemyCube : MonoBehaviour
    {
        [SerializeField]private CharacterMovement _player;
        [SerializeField]private float             _speed;
        [SerializeField]private float             _detectDistance;
        [SerializeField]private float             _attackDistance;

        private States _currentState = States.Idle;
        private bool   _playerDied;
        
        private void Start()
        {
            _player.Died += () => _playerDied = true;
        }

        private void Update()
        {
            if (_playerDied)
            {
                return;
            }

            switch (_currentState)
            {
                case States.Idle:
                    if (CheckDistance(_detectDistance)) _currentState = States.Moving;
                    break;
                case States.Moving:
                    MoveToPlayer();
                    if (CheckDistance(_attackDistance)) _currentState = States.Attack;
                    else if (!CheckDistance(_detectDistance)) _currentState = States.Idle;
                    break;
                case States.Attack:
                    Attack();
                    break;
                default:
                    if (CheckDistance(_detectDistance)) _currentState = States.Moving;
                    break;
            }
        }

        private bool CheckDistance(float distance)
        {
            if (Vector3.Distance(_player.transform.position, transform.position) < distance)
            {
                return true;
            }

            return false;
        }

        private void MoveToPlayer()
        {
            Vector3 direction = (_player.transform.position - transform.position).normalized;
            transform.Translate(direction * _speed * Time.deltaTime);
        }

        private void Attack()
        {
            _player.Kill();
            transform.DOScale(3, 0.2f).onComplete += () => {
                transform.DOScale(1, 0.2f);
            };
        }

        enum States
        {
            Idle,
            Moving,
            Attack
        }
    }
}

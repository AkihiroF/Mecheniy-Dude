using UnityEngine;
using UnityEngine.AI;

namespace _Source.Holy_Shit
{
    public class Enemy : MonoBehaviour
    {
        private GameObject _player;
        private NavMeshAgent _agent;

        public Transform[] moveSpots;
        private int _index = 0;


        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");

            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;

            _agent.SetDestination(moveSpots[_index].position);
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position, _player.transform.position) <= 10)
            {
                _agent.SetDestination(_player.transform.position);
            }
            else
            {
                if(!_agent.hasPath)
                {
                    _index++;
                    if (_index >= moveSpots.Length)
                    {
                        _index = 0;
                    }
                    _agent.SetDestination(moveSpots[_index].position);
                }   
            }
        }
    }
}

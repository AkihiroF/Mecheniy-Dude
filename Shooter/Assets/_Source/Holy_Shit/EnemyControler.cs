using UnityEngine;
using UnityEngine.AI;

namespace _Source.Holy_Shit
{
    public class EnemyControler : MonoBehaviour
    {
        private GameObject _player;
        private NavMeshAgent _agent;


        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player");

            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private void Update()
        {
            if (Vector2.Distance(transform.position , _player.transform.position) <= 10)
            {
                _agent.SetDestination(_player.transform.position);
            }
           
        }


    }
}

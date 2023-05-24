using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mother : MonoBehaviour
{
    private GameObject _player;
    private NavMeshAgent _agent;

    [SerializeField] GameObject _enemyPrefab;
    [SerializeField] private int amountEnemy;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }
    private void Update()
    {
        if (Vector2.Distance(transform.position, _player.transform.position) <= 10)
        {
            _agent.SetDestination(_player.transform.position);
        }
    }

    private void OnDestroy()
    {
        for (int i = 0; i < amountEnemy; i++)
        {
            Instantiate(_enemyPrefab, transform.position, new Quaternion(0,0,0,0));
        }
        
    }
}


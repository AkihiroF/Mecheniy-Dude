using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mother : MonoBehaviour
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
        if (Vector2.Distance(transform.position, _player.transform.position) <= 10)
        {
            var direction = _player.transform.position - transform.position;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
            _agent.SetDestination(_player.transform.position);
        }
    }
}


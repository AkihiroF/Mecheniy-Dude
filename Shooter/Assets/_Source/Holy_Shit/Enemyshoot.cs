using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemyshoot : MonoBehaviour
{

    private float timeBTwShots;
    public float startTimeBtwShots;

    public GameObject projecttile;

    private GameObject _player;
    private NavMeshAgent _agent;

    [SerializeField] private Transform Player;


    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");

        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;

        timeBTwShots = startTimeBtwShots;
    }

    private void Update()
    {
        var direction = Player.position - transform.position;
        var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);

        if (Vector2.Distance(transform.position, _player.transform.position) <= 10)
        {


            _agent.SetDestination(_player.transform.position);

                if (timeBTwShots <= 0)
                {
                    Instantiate(projecttile, transform.position, transform.rotation);
                    timeBTwShots = startTimeBtwShots;
                }
                else
                {
                    timeBTwShots -= Time.deltaTime;
                }
        }
    }
 
}

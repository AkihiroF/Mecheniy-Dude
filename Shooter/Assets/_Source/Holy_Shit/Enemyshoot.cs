using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyshoot : MonoBehaviour
{
    public float speed;
    public float stoopingDist;
    public float retreatDist;

    private float timeBTwShots;
    public float startTimeBtwShots;

    public GameObject projecttile;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeBTwShots = startTimeBtwShots;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stoopingDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) > stoopingDist && Vector2.Distance(transform.position, player.position) > retreatDist)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) > retreatDist)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }


        if(timeBTwShots <= 0)
        {
            Instantiate(projecttile, transform.position, Quaternion.identity);
            timeBTwShots = startTimeBtwShots;
        }
        else
        {
            timeBTwShots -= Time.deltaTime;
        }


    }
}

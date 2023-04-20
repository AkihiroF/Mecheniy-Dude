using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawnObjects : MonoBehaviour
{
    public GameObject[] bonuse;
    public Transform[] spawnPoints;

    private int rand;
    private int randPosition;


    void Start()
    {
        rand = Random.Range(0, bonuse.Length);
        randPosition = Random.Range(1, spawnPoints.Length);
        Instantiate(bonuse[rand], spawnPoints[randPosition].transform.position, Quaternion.identity);
    }

}


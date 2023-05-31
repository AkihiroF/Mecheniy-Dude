using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecongEnemySpawn : MonoBehaviour
{
    [SerializeField] GameObject gameObjectOne;
    [SerializeField] GameObject gameObjectTow;
    [SerializeField] GameObject gameObjectFree;

    [SerializeField] GameObject TakeObject;
    void Start()
    {


    }

    private void Update()
    {
        if (TakeObject == null)
        {
            gameObjectOne.SetActive(true);
            gameObjectTow.SetActive(true);
            gameObjectFree.SetActive(true);
            Destroy(gameObject);
        }
    }

}

using _Source.Services;
using _Source.SignalsEvents.CoreEvents;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFirstenemy : MonoBehaviour
{
    [SerializeField] GameObject gsameObject;
    void Start()
    {

        
    }

    private void Update()
    {
        if (Time.timeScale == 1)
        {
            StartCoroutine(enumerator());
        }
    }


    IEnumerator enumerator()
    {
        yield return new WaitForSeconds(2);
        gsameObject.SetActive(true);
        Destroy(gameObject);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdEnemySP : MonoBehaviour
{
    [SerializeField] List<GameObject> enemy;
    public LayerMask layerMaskEnemy;
    private int numberLayer;
    void Start()
    {
        foreach (GameObject obj in enemy)
        {
            obj.SetActive(false);
        }

        numberLayer = (int)Mathf.Log(layerMaskEnemy.value, 2);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer != numberLayer)
        {
            foreach (GameObject obj in enemy)
            {
                obj.SetActive(true);
            }
            Destroy(gameObject);
        }
    }
}

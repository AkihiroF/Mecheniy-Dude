using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Massenger : MonoBehaviour
{

    [SerializeField] private GameObject panelGameObject;
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "SMS" && UnityEngine.Input.GetKeyDown(KeyCode.F))
        {
            panelGameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void SMS()
    {
        panelGameObject.SetActive(false);
        //Time.timeScale = 1;
    }
}

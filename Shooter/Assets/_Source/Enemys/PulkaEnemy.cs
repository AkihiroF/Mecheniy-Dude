using _Source.HealthSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulkaEnemy : MonoBehaviour
{

    public float speed;
    [SerializeField] Rigidbody2D rb;
    public LayerMask layerMaskEnemy;
    private int numberLayer;

    void Start()
    {
        numberLayer = (int)Mathf.Log(layerMaskEnemy.value, 2);
    }

    void Update()
    {
        rb.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerHealth>().GetDamage(10);
        }
        if (collision.gameObject.layer != numberLayer)
        {
            Destroy(gameObject);
        } 
            
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulkaEnemy : MonoBehaviour
{

    public float speed;
    [SerializeField] Rigidbody2D rb;


    void Start()
    {

    }

    void Update()
    {
        rb.AddForce(transform.up * speed * Time.deltaTime, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}

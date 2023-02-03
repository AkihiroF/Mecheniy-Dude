using UnityEngine;

namespace _Source.Holy_Shit
{
    public class Enemy : MonoBehaviour
    {
        
        public float speed;
        private float _waitTime;
        public float startWaitTime;

        public bool cheker;


        private Transform _player;
        public Transform[] moveSpots;
        public int randomSpot;

        void Start()
        {
            _waitTime = startWaitTime;
            randomSpot = Random.Range(0, moveSpots.Length);
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        }

        void Update()
        {
            if(cheker == false)
            {
                transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

                if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
                {
                    if (_waitTime <= 0)
                    {
                        randomSpot = Random.Range(0, moveSpots.Length);
                        _waitTime = startWaitTime;
                    }
                    else
                    {
                        _waitTime -= Time.deltaTime;
                    }
                }
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position, speed * Time.deltaTime);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                cheker = true;
            }

        }
        private void OnTriggerExit2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                cheker = false;
            }

        }
    }
}

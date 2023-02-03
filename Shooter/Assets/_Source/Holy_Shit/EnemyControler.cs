using UnityEngine;

namespace _Source.Holy_Shit
{
    public class EnemyControler : MonoBehaviour
    {
        public float speed;
        private Transform _player;
   


        void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }



        private void OnTriggerStay2D(Collider2D collision)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position, speed * Time.deltaTime);
            }
        }


    }
}

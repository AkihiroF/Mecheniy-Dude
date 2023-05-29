using UnityEngine;

namespace _Source.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speedMoving;
        private Rigidbody2D _rb;
        private Vector2 _directionMoving;
        private Input _input;
        private Camera _camera;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
        }

        public void SetInput(Input input) => _input = input;

        private void FixedUpdate()
        {
            PlayerRotate();
            PlayerMove();
        }
        

        private void PlayerRotate()
        {
            Vector3 mousePosition = _input.Player.Rotate.ReadValue<Vector2>();
            var ss = _camera.WorldToScreenPoint(transform.position);
            var direction = mousePosition - ss;
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf. Rad2Deg;
            transform. rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        private void PlayerMove()
        {
            Vector3 dir = _input.Player.Moving.ReadValue<Vector2>();
            var thisTransform = transform;
            //var currentDirection = thisTransform.up * dir.y + thisTransform.right * dir.x;
            _rb.velocity = dir * speedMoving;
        }
    }
}
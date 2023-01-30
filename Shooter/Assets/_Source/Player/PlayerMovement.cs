using System;
using UnityEngine;

namespace _Source.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speedMoving;
        private Rigidbody2D _rb;
        private Vector2 _directionMoving;
        private Input _input;

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
        }

        public void SetInput(Input input) => _input = input;

        public void SetDirectionMoving(Vector2 direction)
        {
            _directionMoving = direction;
        }

        private void FixedUpdate()
        {
            Vector3 dir = _input.Player.Moving.ReadValue<Vector2>();
            var currentPos = transform.position;
            _rb.MovePosition(Vector3.Lerp(currentPos, currentPos +dir* speedMoving, Time.deltaTime) );
        }
    }
}
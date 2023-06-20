using System;
using _Source.Services;
using _Source.SignalsEvents.UpgradesEvents;
using UnityEngine;
using Zenject;

namespace _Source.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float speedMoving;
        [SerializeField] private Transform pointAim;
        private Rigidbody2D _rb;
        private Vector2 _directionMoving;
        private Input _input;
        private Camera _camera;

        private void Awake()
        {
            Signals.Get<OnUpgradeSpeedMoving>().AddListener(SetUpgrade);
        }

        private void Start()
        {
            _rb = GetComponent<Rigidbody2D>();
            _camera = Camera.main;
        }

        private void SetUpgrade(float percent)
        {
            speedMoving += speedMoving * percent /100;
        }

        [Inject]
        public void SetInput(Input input)
        {
            _input = input;
        }

        private void FixedUpdate()
        {
            AimPosition();
            PlayerRotate();
            PlayerMove();
        }

        private Vector3 AimPosition()
        {
            Vector3 mousePosition = _input.Player.Rotate.ReadValue<Vector2>();
            var worldPos = _camera.ScreenToWorldPoint(mousePosition);
            var currentPos = new Vector3(worldPos.x, worldPos.y, transform.position.z);
            pointAim.position = currentPos;
            return mousePosition;
        }


        private void PlayerRotate()
        {
            var ss = _camera.WorldToScreenPoint(transform.position);
            var aa = AimPosition();
            var direction = aa - ss;
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

        private void OnDestroy()
        {
            Signals.Get<OnUpgradeSpeedMoving>().RemoveListener(SetUpgrade);
        }
    }
}
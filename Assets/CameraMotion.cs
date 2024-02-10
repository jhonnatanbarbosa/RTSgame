using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS_Camera
{
    public class CameraMotion : MonoBehaviour
    {
        [SerializeField] private float _speed = 1f;
        [SerializeField] private float _smoothing = 5f;
        [SerializeField] private Vector2 _range = new Vector2(100, 100);

        private Vector3 _targetPosition;
        private Vector3 _input;
        
        private void Awake() {
            _targetPosition = transform.position;
        }

        private void HandleInput(){
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 right = transform.right * x;
            Vector3 forward = transform.forward * z;

            _input = (right + forward).normalized;
        }

        private void Move(){
            Vector3 nextTargetPosition = _targetPosition + _input * _speed;
            if(IsInBounds(nextTargetPosition)){
                _targetPosition = nextTargetPosition;
            }
            transform.position = Vector3.Lerp(transform.position, _targetPosition, _smoothing * Time.deltaTime);
        }

        private bool IsInBounds(Vector3 position){
            return position.x > -_range.x && 
                   position.x < _range.x &&
                   position.z > -_range.y &&
                   position.z < _range.y;
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_targetPosition, 5f);
            Gizmos.DrawWireCube(Vector3.zero, new Vector3(_range.x * 2f, 5f, _range.y * 2f));
        }

        private void Update() {
            HandleInput();
            Move();
        }
    }
}

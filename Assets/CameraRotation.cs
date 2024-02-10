using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RTS_Camera{
public class CameraRotation : MonoBehaviour
{
    
        [SerializeField] private float _speed = 15f; 
        [SerializeField] private float _smoothing = 5f;

        private float _targetAngle;
        private float _curretAngle;

        private void Awake() {
            _targetAngle = transform.eulerAngles.y;
            _curretAngle = _targetAngle;
        } 

        private void HandleInput(){
            if(!Input.GetMouseButton(1)) return;
            _targetAngle += Input.GetAxis("Mouse X") * _speed;
            
        }

        private void Rotate(){
            _curretAngle = Mathf.LerpAngle(_curretAngle, _targetAngle, _smoothing * Time.deltaTime);
            transform.rotation = Quaternion.AngleAxis(_curretAngle, Vector3.up);
        }

        private void Update() {
            HandleInput();
            Rotate();
        }

}
}
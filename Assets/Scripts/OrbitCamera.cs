using UnityEngine;
using Zenject;

public class OrbitCamera : MonoBehaviour {
	[SerializeField] private float rotSpeed = 1.5f;
	[SerializeField] private Transform _player;
	
	private float _rotY;
	private Vector3 _offset;
	
	private void Start() {
		_rotY = transform.eulerAngles.y;
		_offset = _player.position - transform.position;
	}
	
	void LateUpdate() {
		float horInput = Input.GetAxis("Horizontal");
		if (horInput != 0) {
			_rotY += horInput * rotSpeed;
		} else {
			_rotY += Input.GetAxis("Mouse X") * rotSpeed * 3;
		}

		Quaternion rotation = Quaternion.Euler(15, _rotY, 0);
		transform.position = _player.position - (rotation * _offset);
		transform.LookAt(_player);
	}
}
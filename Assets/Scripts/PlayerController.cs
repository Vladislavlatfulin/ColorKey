using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour {
	[SerializeField] private float moveSpeed = 6.0f;
	[SerializeField] private float rotSpeed = 15.0f;
	[SerializeField] float minFall = -1.5f;

	[SerializeField] private Transform _camera;
	[SerializeField] private Joystick _joystick;
	private float _vertSpeed;
	private CharacterController _charController;
	private Animator _animator;

	private void Start() {
		_vertSpeed = minFall;
		_charController = GetComponent<CharacterController>();
		_animator = GetComponent<Animator>();
	}
	
	private void Update() {
		Vector3 movement = Vector3.zero;

		float horInput = _joystick.Horizontal;
		float vertInput = _joystick.Vertical;
		
		if (horInput != 0 || vertInput != 0) {
			movement.x = horInput * moveSpeed;
			movement.z = vertInput * moveSpeed;
			movement = Vector3.ClampMagnitude(movement, moveSpeed);
	
			Quaternion tmp = _camera.rotation;
			_camera.eulerAngles = new Vector3(0, _camera.eulerAngles.y, 0);
			movement = _camera.TransformDirection(movement);
			_camera.rotation = tmp;
			
			Quaternion direction = Quaternion.LookRotation(movement);
			transform.rotation = Quaternion.Lerp(transform.rotation,
			                                     direction, rotSpeed * Time.deltaTime);
		}
		
		_animator.SetFloat("Speed", movement.sqrMagnitude);
		movement.y = _vertSpeed;
		movement *= Time.deltaTime;

		_charController.Move(movement);
	}
}
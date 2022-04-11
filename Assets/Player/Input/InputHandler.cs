using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 move;
    public Vector2 look;
    public UnityEvent jump;
	public UnityEvent attack;
	public void OnMove(InputValue value)
	{
		move = value.Get<Vector2>();
	}

	public void OnLook(InputValue value)
	{
		look = value.Get<Vector2>();
	}

	public void OnJump(InputValue value)
	{
		jump.Invoke();
	}

	public void OnAttack(InputValue value)
	{
		attack.Invoke();
	}

	private void OnEnable()
	{
		Cursor.lockState = CursorLockMode.Locked;	
	}

	private void OnDisable()
	{
		Cursor.lockState = CursorLockMode.None;
	}
}

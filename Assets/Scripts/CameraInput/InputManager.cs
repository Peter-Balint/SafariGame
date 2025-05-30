using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
	public Action<Vector3Int> OnMouseClick, OnMouseHold;
	public Action OnMouseUp;
	private Vector2 cameraMovementVector;

	public Vector2 CameraMovementVector
	{
		get { return cameraMovementVector; }
	}

	[SerializeField]
	Camera mainCamera;

	public LayerMask groundMask;

	private void Update()
	{
		CheckClickDownEvent();
		CheckArrowInput();
		CheckClickHoldEvent();
		CheckClickUpEvent();
	}

	private Vector3Int? RaycastGround()
	{
		RaycastHit hit;
		Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
		if(Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
		{
			Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
			return positionInt;
		}
		return null;
	}

	private void CheckArrowInput()
	{
		cameraMovementVector = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

	}
	private void CheckClickDownEvent()
	{
		if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
		{
			var position = RaycastGround();
			if (position != null)
			{
				OnMouseClick?.Invoke(position.Value);
			}
		}
	}


	private void CheckClickHoldEvent()
	{
		if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
		{
			var position = RaycastGround();
			if (position != null)
			{
				OnMouseHold?.Invoke(position.Value);
			}
		}
	}

	private void CheckClickUpEvent()
	{
		if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
		{
			OnMouseUp?.Invoke();
		}
	}
}

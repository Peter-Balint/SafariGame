using CamMovement;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public CameraMovement cameraMovement;
	public InputManager inputManager;

	private void Start()
	{
		inputManager.OnMouseClick += HandleMouseClick;
	}

	private void HandleMouseClick(Vector3Int position)
	{
		Debug.Log(position.ToString());
	}

	private void Update()
	{
		cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x,0,
			inputManager.CameraMovementVector.y)); 
	}
}

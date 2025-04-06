using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CamMovement
{

	public class CameraMovement : MonoBehaviour
	{
		public Camera gameCamera;
		public float cameraMovementSpeed = 5;

		private void Start()
		{
			gameCamera = GetComponent<Camera>();
		}
		public void MoveCamera(Vector3 inputVector)
		{
			inputVector.Normalize();
			gameCamera.transform.position += inputVector * Time.deltaTime * cameraMovementSpeed;
		}
	}
}
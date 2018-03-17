using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

	[SerializeField]
	private Camera m_camera;

	// Use this for initialization
	private void Start ()
	{
		m_camera.orthographicSize = Screen.height / 2;

		Vector3 newLocation = transform.position;
		newLocation.x = Screen.width / 2;
		newLocation.y = Screen.height / 2;

		transform.SetPositionAndRotation(newLocation, transform.rotation);
	}
	
	// Update is called once per frame
	private void Update () {
		
	}
}

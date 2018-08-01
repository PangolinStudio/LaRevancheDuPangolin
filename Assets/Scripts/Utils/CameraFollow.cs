using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	public Transform target;
	public float smoothspeed = 0.125f;
	public Vector3 offset;

	private Vector3 vel = Vector3.zero;
	private Vector3 final_offset;

	void FixedUpdate () {
		final_offset = offset;
		final_offset.x *= target.GetComponent<Entity>().forwardvector.x;

		// On récupère la position à atteindre
		Vector3 desiredPosition = target.position + final_offset;
		// On calcule la nouvelle position pour avoir cet effet "lissé"
		Vector3 calculatedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref vel, smoothspeed);
		// On déplace la caméra
		transform.position = calculatedPosition;
	}
}

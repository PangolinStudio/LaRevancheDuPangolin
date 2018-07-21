﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	public float health;
	public float speed;
	public LayerMask groundLayer;
	public Rect groundDetection;

	protected Rigidbody2D rb;
	protected bool isFacingRight = true;
	public bool isGrounded = false;

	private Vector2 top_left;
	private Vector2 bottom_right;

	// On fait les références
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		// On récupère les points de la zone de détection du sol
	}
	// Update is called once per frame
	void Update () {
		Flip();

		// On vérifie si l'entité touche le sol
		top_left = new Vector2(transform.position.x + groundDetection.x, transform.position.y + groundDetection.y);
		bottom_right = top_left + new Vector2(groundDetection.width, groundDetection.height);
		isGrounded = Physics2D.OverlapBox(top_left, bottom_right - top_left, 0, groundLayer);
	}

	// Change le sprite de sens en fonction de son ordre de marche
	protected void Flip(bool forceFlip = false) {
		if (forceFlip)
		{
			isFacingRight = !isFacingRight;
		}
		else if (rb.velocity.x > 0)
		{
			// On va a droite
			isFacingRight = true;
		} else if (rb.velocity.x < 0) {
			// On va a gauche
			isFacingRight = false;
		}

		if (isFacingRight)
			transform.localRotation = Quaternion.Euler(0, 0, 0);
		else
			transform.localRotation = Quaternion.Euler(0, 180, 0);
	}

	// Permet de dessiner la boite rouge pour voir la zone de détection
	protected void OnDrawGizmosSelected()
	{
		top_left = new Vector2(transform.position.x + groundDetection.x, transform.position.y + groundDetection.y);
		bottom_right = top_left + new Vector2(groundDetection.width, groundDetection.height);

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(top_left, bottom_right - top_left);
	}
}
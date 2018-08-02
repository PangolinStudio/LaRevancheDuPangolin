using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {

	public float maxHealth;
	public float health;
	public float speed;
	public LayerMask groundLayer;
	public Rect groundDetection;
	public bool isGrounded = false;
	public Vector2 forwardvector;
	public bool isAlive = true;
	public GameObject impactEffect;
	public GameObject deathEffect;

	protected Rigidbody2D rb;
	protected bool isFacingRight = true;

	private Vector2 top_left;
	private Vector2 bottom_right;

	// On fait les références
	void Awake () {
		rb = GetComponent<Rigidbody2D>();
		forwardvector = new Vector2(1, 0);
		
		health = maxHealth;
	}
	// Update is called once per frame
	protected void Update () {
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
		else if (rb.velocity.x > 0.1f)
		{
			// On va a droite
			isFacingRight = true;
		} else if (rb.velocity.x < -0.1f) {
			// On va a gauche
			isFacingRight = false;
		}

		if (isFacingRight)
		{
			transform.localRotation = Quaternion.Euler(0, 0, 0);
			forwardvector = new Vector2(1, 0);
		}
		else
		{
			transform.localRotation = Quaternion.Euler(0, 180, 0);
			forwardvector = new Vector2(-1, 0);
		}
	}

	public void TakeDamage(float damage) {
		health -= damage;
		if (health <= 0)
		{
			Instantiate(deathEffect, transform.position, transform.rotation);
			isAlive = false;
		}
	}

	// Permet de dessiner la boite rouge pour voir la zone de détection
	protected virtual void OnDrawGizmosSelected()
	{
		top_left = new Vector2(transform.position.x + groundDetection.x, transform.position.y + groundDetection.y);
		bottom_right = top_left + new Vector2(groundDetection.width, groundDetection.height);

		Gizmos.color = Color.red;
		Gizmos.DrawWireCube(top_left, bottom_right - top_left);
	}
}
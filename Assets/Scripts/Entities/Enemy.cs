using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	public Rect wallDectection;
	public bool isTouchingWall = false;
	public Transform target;
	public float detectionRadius = 2;
	public float jumpForce = 5f;

	private FSM brain;
	private Vector2 top_left_wall;
	private Vector2 bottom_right_wall;
	private float idleTime;
	private float init_health;
	private float fleeDirection;

	// Use this for initialization
	void Start () {
		brain = new FSM();
		brain.Add(Wander);

		idleTime = -1;
		init_health = health;
	}
	
	// Update is called once per frame
	void Update () {
		base.Update();

		if (isFacingRight)
		{
			top_left_wall = new Vector2(transform.position.x + wallDectection.x, transform.position.y + wallDectection.y);
			bottom_right_wall = top_left_wall + new Vector2(wallDectection.width, wallDectection.height);
		}
		else {
			top_left_wall = new Vector2(transform.position.x - wallDectection.x, transform.position.y + wallDectection.y);
			bottom_right_wall = top_left_wall + new Vector2(-wallDectection.width, wallDectection.height);
		}
		
		isTouchingWall = Physics2D.OverlapBox(top_left_wall, bottom_right_wall - top_left_wall, 0, groundLayer);

		if (isTouchingWall)
			Flip(true);

		brain.Update(Time.deltaTime);

		if (!isAlive)
			Destroy(this.gameObject);
	}

	public void Idle()
	{
		if (idleTime == -1)
			idleTime = Random.Range(5.0f, 25.0f);

		// Play Idle animation

		if (brain.timer > idleTime)
		{
			idleTime = -1;
			brain.Pop();
			brain.Add(Wander);
		}
		if (CanPlayerBeSeen())
		{
			idleTime = -1;
			brain.Add(Chase);
		}
	}

	public void Wander()
	{
		// Play walking animation

		if (isFacingRight)
		{
			rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
		}
		else
		{
			rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
		}

		if (Random.Range(0, 1.0f) > 0.995)
		{
			brain.Pop();
			brain.Add(Idle);	
		}
		if (CanPlayerBeSeen())
		{
			brain.Add(Chase);
		}
	}

	public void Chase()
	{
		float distance = Vector2.Distance(transform.position, target.position);

		if (distance > detectionRadius)
		{
			brain.Pop();
		}
		else if (health <= 20/100 * init_health)
		{
			brain.Pop();
			fleeDirection = Mathf.Sign(transform.position.x - target.position.x);
			brain.Add(Flee);
		}

		if (transform.position.x > target.position.x)
		{
			isFacingRight = false;
		}
		else {
			isFacingRight = true;
		}

		// Si le joueur est à la bonne hauteur
		if (Mathf.Abs(target.position.y - transform.position.y) < 2)
		{
			GetComponentInChildren<Gun>().Shoot();
		}
		// Sinon si le joueur est au dessus
		else if (target.position.y >= transform.position.y)
		{
			Jump();
		}
	}

	public void Jump()
	{
		if (isGrounded)
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpForce);
		}
	}

	public void Flee()
	{
		// Si l'ennemi est bloqué
		if (Mathf.Abs(rb.velocity.x) < 0.2f)
		{
			fleeDirection *= -1;
		}

		rb.velocity = new Vector2(fleeDirection * speed * Time.deltaTime, rb.velocity.y);
	}

	private bool CanPlayerBeSeen()
	{
		// Si le joueur est assez proche
		float distance = Vector2.Distance(transform.position, target.position);

		// Ici réglé à 10 cases
		if (distance < detectionRadius)
		{
			// Si le joueur est dans le champ de vision de l'ennemi
			Vector2 directionToPlayer = target.position - transform.position;
			Vector2 lineOfSight = forwardvector * distance - new Vector2(transform.position.x, transform.position.y);	
			float angle = Vector2.Angle(directionToPlayer, lineOfSight);
			if (angle < 65)
			{
				// Si le joueur n'est pas derrière un bloc solide
				RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, target.position - transform.position, distance);
				foreach (RaycastHit2D hit in hits)
				{           
					// ignore les enemis
					if (hit.transform.tag == "Enemy")
						continue;
					
					// si il y a autre chose que le joueur c'est qu'il y a un obstacle
					if (hit.transform.tag != "Player")
					{
						return false;
					}
				}
				return true;
			}
		}
		return false;
	}

	// Permet de dessiner la boite rouge pour voir la zone de détection
	protected override void OnDrawGizmosSelected()
	{
		base.OnDrawGizmosSelected();

		if (isFacingRight)
		{
			top_left_wall = new Vector2(transform.position.x + wallDectection.x, transform.position.y + wallDectection.y);
			bottom_right_wall = top_left_wall + new Vector2(wallDectection.width, wallDectection.height);
		}
		else {
			top_left_wall = new Vector2(transform.position.x - wallDectection.x, transform.position.y + wallDectection.y);
			bottom_right_wall = top_left_wall + new Vector2(-wallDectection.width, wallDectection.height);
		}

		Gizmos.color = Color.blue;
		Gizmos.DrawWireCube(top_left_wall, bottom_right_wall - top_left_wall);
	}
}

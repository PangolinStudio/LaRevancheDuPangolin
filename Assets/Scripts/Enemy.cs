using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

	public Rect wallDectection;
	public bool isTouchingWall = false;

	private Vector2 top_left_wall;
	private Vector2 bottom_right_wall;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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

		Movement();
	}

	void Movement()
	{
		if (isFacingRight)
		{
			rb.velocity = new Vector2(speed * Time.deltaTime, rb.velocity.y);
		}
		else
		{
			rb.velocity = new Vector2(-speed * Time.deltaTime, rb.velocity.y);
		}
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

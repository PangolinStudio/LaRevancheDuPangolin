using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	private bool isDoubleJumpAllowed = false;
	private bool canDoubleJump = false;
	private float jumpCoolDown = 0.5f;
	private float jumpTime = 0.2f;
	
	// Update is called once per frame
	void FixedUpdate () {
		Movement();

		jumpCoolDown += Time.deltaTime;
	}

	// Fonction pour gérer le mouvement du joueur
	void Movement() {
		float move = SimpleInput.GetAxis("Horizontal");
		float acc = Input.acceleration.x;

		// Si un déplacement au clavier à été détecté
		if (move != 0) {
			rb.velocity = new Vector2(move * speed * Time.deltaTime, rb.velocity.y);
		} else {
			// On prend le relai avec l'inclinaison du tel
			rb.velocity = new Vector2(2 * acc * speed * Time.deltaTime, rb.velocity.y);
		}

		
		// Si le joueur tente de sauter
		if (SimpleInput.GetKeyDown(KeyCode.Space))
		{
			// Si le joueur à assew attendu entre 2 sauts
			if (jumpCoolDown > jumpTime)
			{
				// Si le joueur est au sol
				if (isGrounded)
				{
					// On contrebalacne les effets de la gravité en annulant la vitesse sur y
					rb.velocity.Set(rb.velocity.x, 0);
					// On ajoute la force pour faire sauter le joueur
					rb.AddForce(new Vector2(0, jumpForce));
					// On lui autorise le double saut
					canDoubleJump = true;
					jumpCoolDown = 0;
				} 
				else if (isDoubleJumpAllowed && canDoubleJump) 
				{
					// On interdit le double saut
					canDoubleJump = false;
					// On contrebalacne les effets de la gravité en annulant la vitesse sur y
					rb.velocity.Set(rb.velocity.x, 0);
					// On ajoute la force pour faire sauter le joueur
					rb.AddForce(new Vector2(0, jumpForce));
					jumpCoolDown = 0;
				}
			}
		}
	}
}

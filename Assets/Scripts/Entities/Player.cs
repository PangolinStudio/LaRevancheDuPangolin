﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

	private float jumpCoolDown = 0.5f;
	private Animator anim;

	void Start(){
		anim = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate () {
		Movement();

		if (Mathf.Abs(rb.velocity.x) > 0.5f)
		{
			anim.SetBool("moving", true);
		} else {
			anim.SetBool("moving", false);
		}

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
	}
}

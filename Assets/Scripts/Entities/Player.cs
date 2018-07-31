using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity {

	private float jumpCoolDown = 0.5f;
	private Animator anim;

	public Text acc_t;
	public float acc_threshold;

	void Start(){
		anim = GetComponent<Animator>();
		acc_t.text = "acc : 0";
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
			if (acc > acc_threshold)
			{
				rb.velocity = new Vector2(2 * speed * Time.deltaTime, rb.velocity.y);
			}
			else if (acc < -acc_threshold)
			{
				rb.velocity = new Vector2(-2 * speed * Time.deltaTime, rb.velocity.y);
			}
			else {
				rb.velocity = new Vector2(2 * acc * speed * Time.deltaTime, rb.velocity.y);
			}
			acc_t.text = "acc : " + acc.ToString();
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    public float jumpForce = 5f;
    public float jumpTime = 0.12f;
    private float jumpTimeCounter;
    public float fallMultiplier = 2.0f;
    public float ySpeedLimit = 3.0f;
    private Entity ent; //Pour récupérer la methode isGrounded
    private Rigidbody2D rb;
    public bool stoppedJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ent = GetComponent<Entity>();
        jumpTimeCounter = jumpTime;
    }

    void Update()
    {

        //Si on est au sol
        if (ent.isGrounded)
        {
            //On initialise le compteur.
            jumpTimeCounter = jumpTime;
        }
    }

    void FixedUpdate() //Dans FixedUpdate pour jouer sur la physique
    {

        //Quand on appuie sur la barre espace ...
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Et qu'on est au sol ...
            if (ent.isGrounded)
            {
                //On saute !
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                stoppedJumping = false;
            }
        }

        //Si on reste appuyer...
        if ((Input.GetKey(KeyCode.Space)) && !stoppedJumping)
        {
            //et que le compteur n'a pas atteint 0...
            if (jumpTimeCounter > 0)
            {
                // On saute plus haut !
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
        }


        //Quand on lache le bouton...
        if (Input.GetKeyUp(KeyCode.Space))
        {
            //On arrete de sauter et on réinitialise le comptuer.
            jumpTimeCounter = 0;
            stoppedJumping = true;
        }

        //Ajoute une force rappel vers le sol lors de la chute (commence avant le sommet de la parabole, ajuster ySpeedLimit pour avoir le meilleur resultat)
        if (rb.velocity.y < ySpeedLimit)
        {
            rb.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier) * Time.deltaTime;
        }
    }
}

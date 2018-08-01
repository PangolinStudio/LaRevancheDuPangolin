using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public float damage;
	public LayerMask whatToHit;

	void OnCollisionEnter2D(Collision2D col)
	{
		// Si on touche
		if (Utils.IsInLayerMask(col.gameObject.layer, whatToHit))
		{
			Entity ent = col.gameObject.GetComponent<Entity>();

			if (ent == null)
			{
				Debug.LogError("Not entity component found on: "+gameObject.name+"! Is it (or the layer) setup correctly?");
				return;
			}
			ent.TakeDamage(damage);
			gameObject.SetActive(false);
		}
		else 
		{
			gameObject.SetActive(false);
		}
	}
}

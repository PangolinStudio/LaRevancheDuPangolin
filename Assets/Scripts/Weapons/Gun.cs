using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : BaseWeapon {
	public float bulletSpeed;
	public float spread;
	public LayerMask whatToHit;
	public bool isPlayer;

	private Transform firePoint;
	private Entity parent;
	private ObjectPool bullets;

	void Start()
	{
		bullets = GetComponent<ObjectPool>();

		firePoint = transform.Find("FirePoint");
		parent = transform.GetComponentInParent<Entity>();

		if (firePoint == null) {
			Debug.LogError("Not firepoint found ! Has it been setup?");
		}
		if (parent == null) {
			Debug.LogError("No parent found !");
		}
	}

	void Update() {
		if(isPlayer)
		{
			if ((SimpleInput.GetButton("Fire1") || SimpleInput.GetKey(KeyCode.A)))
			{
				Shoot();
			}
		}
	}

	public void Shoot() {
		if (Time.time > attack_timer)
		{
			attack_timer = Time.time + 1/fireRate;

			GameObject bullet = bullets.GetPooledObject();
			if (bullet == null) return;
			
			// Paramétrage du projectile
			bullet.GetComponent<BulletController>().damage = damage;
			bullet.GetComponent<BulletController>().whatToHit = whatToHit;

			// Rotation aléatoire du projectile
			Quaternion rot = firePoint.rotation;
			rot.z = Random.Range(-0.1f, 0.1f);
			bullet.transform.rotation = rot;
			bullet.transform.position = firePoint.position;

			// Rotation aléatoire en direction (précision)
			float angle = Random.Range(-spread, spread);
			Vector2 dir = new Vector2(parent.forwardvector.x * Mathf.Cos(angle * Mathf.PI / 180), Mathf.Sin(angle * Mathf.PI / 180));
			bullet.GetComponent<Rigidbody2D>().velocity = dir * bulletSpeed;

			if (isPlayer)
				CameraShake.Shake(0.05f, 0.1f);
		}
	}
}

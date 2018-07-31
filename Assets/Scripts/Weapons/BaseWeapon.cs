using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseWeapon : MonoBehaviour {

	public String title;
	public float damages;
	public float fireRate; // Bullet per second

	public bool isReloadable;
	public int mag_size;
	
	protected int ammo;
	protected float attack_timer;

	void Start() {
		if (isReloadable) {
			attack_timer = 0;
			ammo = mag_size;
		}
	}
}

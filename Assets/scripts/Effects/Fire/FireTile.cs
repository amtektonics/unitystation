﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sprites;

public class FireTile : MonoBehaviour {

	//Not networked, this is client side effects
	public SpriteRenderer spriteRend;
	public GameObject ambientTile;
	private Sprite[] sprites;
	private bool burning = false;
	private float fuel = 0f;
	private float animSpriteTime = 0f;
	private float changeRate = 0.1f;


	public void StartFire(float addFuel){
		if (sprites == null) {
			sprites = SpriteManager.FireSprites["fire"];
		}
		fuel += addFuel;
		animSpriteTime = 0f;
		spriteRend.sprite = sprites[Random.Range(0, 100)];
		burning = true;
	}

	public void AddMorefuel(float addFuel){
		fuel += addFuel;
	}
	// Update is called once per frame
	void Update () {
		if (burning) {
			animSpriteTime += Time.deltaTime;
			if (animSpriteTime > changeRate) {
				animSpriteTime = 0f;
				fuel -= changeRate;
				spriteRend.sprite = sprites[Random.Range(0, 135)];
			}

			if (fuel <= 0f) {
				burning = false;
				BurntOut();
			}
		}
	}

	//Ran out of fuel, return to pool
	private void BurntOut(){
		FloorTile fT = Matrix.Matrix.At((Vector2)transform.position).GetFloorTile();
		if (fT != null) {
			fT.AddFireScorch();
		}
		PoolManager.PoolClientDestroy(gameObject);
	}
}

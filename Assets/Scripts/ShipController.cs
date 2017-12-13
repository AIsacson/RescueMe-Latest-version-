using System;
using UnityEngine;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {
	
	GameObject rescShip;
	GameObject player;
	EndGameController endGame;

	public bool fly;

	void Start () {
		rescShip = GameObject.FindGameObjectWithTag ("Ship");
		player = GameObject.FindGameObjectWithTag ("Player");
		endGame = FindObjectOfType<EndGameController> ();
		fly = false;

	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			fly = true;
		}
	}

	void FixedUpdate(){
		if (fly) {
			endGame.Congrats ();
			player.transform.Translate (Vector3.up* 3 * Time.deltaTime);
			rescShip.transform.Translate (Vector3.forward * 3 * Time.deltaTime);
		}
	}
}

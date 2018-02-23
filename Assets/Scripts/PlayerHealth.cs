using System;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int startingHealth = 100;
	public int currentHealth;
	public Slider healthBar;
	public event Action<int> onHealthChanged;

	Animator anim;
	PlayerController player;
	bool isDead;
	bool damaged;
	GameOverMenuController gameOver;
	GameObject healthPack;
	GameObject cross;

	void Awake(){
		anim = GetComponent<Animator> ();
		player = GetComponent<PlayerController> ();
		currentHealth = startingHealth;
		gameOver = FindObjectOfType<GameOverMenuController> ();
		healthPack = GameObject.FindGameObjectWithTag ("HealthPack");
		cross = healthPack.transform.GetChild (0).gameObject;
	}

	void Update () {
		if (damaged) {
			anim.SetTrigger ("GetHit");
		}
		damaged = false;
		anim.SetTrigger ("Recover");
	}


	void OnCollisionEnter(Collision other) {
		if (other.collider.tag == "HealthPack") {
			currentHealth = 100;
			onHealthChanged (currentHealth);
			Destroy (healthPack);
			Destroy (cross);
		}
	}

	public void TakeDamage(int amount){
		damaged = true;

		currentHealth -= amount;
		player.gettingHit.Play ();

		if (currentHealth <= 0 && !isDead) {
			Death ();
		}
		onHealthChanged (currentHealth);
	}

	void Death(){
		isDead = true;
		anim.SetTrigger ("PlayerDeath");
		player.enabled = false;
		gameOver.GameOver ();
		player.running.Stop ();
		player.dying.Play ();
	}
}
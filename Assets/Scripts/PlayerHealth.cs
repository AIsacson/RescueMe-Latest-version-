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

	void Awake(){
		anim = GetComponent<Animator> ();
		player = GetComponent<PlayerController> ();
		currentHealth = startingHealth;
		gameOver = FindObjectOfType<GameOverMenuController> ();
	}

	void Update () {
		if (damaged) {
			anim.SetTrigger ("GetHit");
		}
		damaged = false;
		anim.SetTrigger ("Recover");
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
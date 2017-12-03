using System;
using UnityEngine.AI;
using UnityEngine;

public class BossController : MonoBehaviour {

	GameObject player;
	NavMeshAgent nav;
	Animator anim;
	PlayerHealth playerHP;
	AudioSource dying;
	EnemyActions enemyAction;
	bool alive = true;

	public int lives = 10;
	public event Action<int> onHealthChanged;

	void Awake(){
		enemyAction = FindObjectOfType<EnemyActions> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHP = player.GetComponent<PlayerHealth> ();
		anim = this.transform.GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
		AudioSource [] audio = GetComponents<AudioSource> ();
		dying = audio [1];
	}

	void Update () {
		if (alive) {

			if (Vector3.Distance (player.transform.position, transform.position) < 20f) {
				enemyAction.MoveTowardsTarget (player, anim, nav, 3f, 1f);
			}
			if (playerHP.currentHealth <= 0) {
				enemyAction.PlayerDies (anim, nav);
			}
			if (Vector3.Distance (player.transform.position, transform.position) < 5f) {
				nav.speed = 3f;
				nav.SetDestination (player.transform.position);
				anim.SetTrigger ("PlayerSpotted");
			}
			else {
				anim.SetTrigger ("PlayerGone");
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player")
			return;
		Destroy (other.gameObject);
		lives--;
		onHealthChanged (lives);
		if (lives > 0 && alive) {
			GetHit ();
		}
		if (lives <= 0) {
			nav.speed = 0f;
			Death (gameObject);
		}
	}

	void Death(GameObject gameObj){
		if (alive) {
			anim.SetTrigger ("EnemyDead");
			nav.enabled = false;
			Destroy (gameObj, 20f);
			dying.Play ();
		}
		alive = false;
	}

	void GetHit(){
		anim.SetTrigger ("EnemyHit");
	}
}
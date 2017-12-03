using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

	EnemyActions enemyAction;
	GameObject player;
	PlayerHealth playerHP;
	Animator anim;
	NavMeshAgent nav;
	bool playerInRange;
	int attackDamage = 10;
	bool alive = true;
	int lives = 4;
	float timeBetweenAttacks = 1.5f;

	public event Action<int> onHealthChanged;
	public float timer;

	void Start () {
		enemyAction = FindObjectOfType<EnemyActions> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		playerHP = player.GetComponent<PlayerHealth> ();
		anim = this.transform.GetComponent<Animator> ();
		nav = GetComponent<NavMeshAgent> ();
	}

	void Update () {
		if (alive) {
			timer += Time.deltaTime;

			if (Vector3.Distance (player.transform.position, transform.position) < 20f) {
				enemyAction.MoveTowardsTarget (player, anim, nav, 6.5f, 2.3f);
			}
			if(Vector3.Distance (player.transform.position, transform.position) > 20f){
				enemyAction.Move (anim, nav);
			}
			if (playerHP.currentHealth <= 0) {
				enemyAction.PlayerDies (anim, nav);
			}
			if (timer >= timeBetweenAttacks && playerInRange && lives > 0) {
				timer = enemyAction.Attack(timer,playerHP, attackDamage);
			}
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.tag == "Player") {
			playerInRange = true;
			return;
		}
		Destroy (other.gameObject);
		lives--;
		onHealthChanged (lives);

		if (lives > 0 && alive) {
			enemyAction.GetHit (anim);
		}

		if (lives <= 0) {
			alive = enemyAction.Death (gameObject, anim, nav);
		}
	
	}

	void OnTriggerExit(Collider other){
		if (other.tag == "Player") {
			playerInRange = false;
		}
	}
}

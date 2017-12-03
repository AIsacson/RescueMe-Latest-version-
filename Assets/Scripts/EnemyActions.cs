using UnityEngine;
using UnityEngine.AI;

public class EnemyActions : MonoBehaviour {

	AudioSource dying;
	AudioSource gettingHit;
	AudioSource attack;
	EnemyController enemyControl;
	bool alive = true;

	void Awake () {
		AudioSource [] audio = GetComponents<AudioSource> ();
		attack = audio [0];
		gettingHit = audio [1];
		dying = audio [2];
	}

	public void Move(Animator anim, NavMeshAgent nav){
		Vector3 randomPos = Random.insideUnitSphere * 30f;
		NavMeshHit navHit;
		NavMesh.SamplePosition (transform.position + randomPos, out navHit, 10f, NavMesh.AllAreas);
		nav.SetDestination (navHit.position);
		anim.SetTrigger ("PlayerGone");
	}

	public void MoveTowardsTarget(GameObject player, Animator anim, NavMeshAgent nav, float navSpeed, float animSpeed){
		nav.speed = navSpeed;
		anim.speed = animSpeed;
		nav.SetDestination (player.transform.position);
		anim.SetTrigger ("PlayerSpotted");
	}

	public bool Death(GameObject gameObj, Animator anim, NavMeshAgent nav){

		if (alive) {
			dying.Play ();
			nav.speed = 0f;
			anim.speed = 1f;
			anim.SetTrigger ("EnemyDead");
			Destroy (gameObj, 20f);
			nav.enabled = false;
		}
		alive = false;
		return alive;
	}

	public void GetHit(Animator anim){
		anim.SetTrigger ("EnemyHit");
		gettingHit.Play ();
	}

	public float Attack(float timer, PlayerHealth playerHP, int attackDamage){
		timer = 0;
		if (playerHP.currentHealth > 0) {
			playerHP.TakeDamage (attackDamage);
			attack.Play ();
		}
		return timer;
	}

	public void PlayerDies(Animator anim, NavMeshAgent nav){
		anim.SetTrigger ("PlayerDies");
		nav.speed = 0f;
	}
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float turnSpeed;
	public float fireRate;
	public Text ammo;
	public int bullets = 15;
	public AudioSource running;
	public AudioSource gettingHit;
	public AudioSource dying;
	public float delay = 3.5f;
	public bool shoot = true;
	Animator anim;
	float nextFire;
	AudioSource shooting;
	GameController control;
	PlayerActions player;

	void Start () 
	{
		anim = GetComponent<Animator> ();
		control = FindObjectOfType<GameController> ();
		AudioSource [] audio = GetComponents<AudioSource> ();
		shooting = audio [0];
		running = audio [1];
		dying = audio [2];
		gettingHit = audio [3];
		ammo.text = bullets.ToString() + "/15";
		player = FindObjectOfType<PlayerActions> ();
	}

	void FixedUpdate () 
	{
		var x = Input.GetAxis ("Horizontal");
		var y = Input.GetAxis ("Vertical");
		player.Walk (y, x, anim, speed, turnSpeed);
	}

	void Update(){
		bool fire = Input.GetButtonDown ("Fire1");
		bool reload = Input.GetButtonDown ("Reload");
		anim.SetBool ("Fire", fire);
		anim.SetBool ("Reload", reload);

		if ((bullets > 0) && fire && Time.time > nextFire && !reload && shoot) {
			bullets = player.Fire (bullets, control, ammo);
			nextFire = Time.time + fireRate;
			shooting.Play ();
		}
		if (reload) {
			StartCoroutine (player.Reload (ammo, bullets, anim, delay));
			StartCoroutine (Bullets (delay));
			shoot = false;
		}
		player.WalkSound (running);
	}

	IEnumerator Bullets(float delay){
		yield return new WaitForSeconds (delay);
		bullets = 15;
		shoot = true;
	}
}
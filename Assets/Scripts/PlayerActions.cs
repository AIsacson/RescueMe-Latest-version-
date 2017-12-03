using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActions : MonoBehaviour {

	public void Walk(float y, float x, Animator anim, float speed, float turnSpeed)
	{
		anim.speed = 0.9f;
		anim.SetFloat ("VelY", y);
		anim.SetFloat ("VelX", x);
		if (Input.GetKey(KeyCode.W)) {
			transform.Translate (Vector3.forward * speed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.S)) {
			transform.Translate (-Vector3.forward * speed * Time.deltaTime);
		} 
		if (Input.GetKey (KeyCode.A)) {
			transform.Rotate (Vector3.up, -turnSpeed * Time.deltaTime);
		}
		if (Input.GetKey (KeyCode.D)) {
			transform.Rotate (Vector3.up, turnSpeed * Time.deltaTime);
		}
	}

	public void WalkSound(AudioSource running){

		if (Input.GetButtonDown ("Horizontal") || Input.GetButtonDown ("Vertical")) {
			running.Play ();
		}
		else if(!(Input.GetButton ("Horizontal") || Input.GetButton ("Vertical")) && running.isPlaying){
			running.Pause ();
		}
	}

	public int Fire(int bullets, GameController control, Text ammo){
		control.BulletSpawn ();
		bullets--;
		ammo.text = bullets.ToString () + "/15";
		return bullets;
	}

	public IEnumerator Reload(Text ammo, int bullets, Animator anim, float delay){
		yield return new WaitForSeconds (delay);
			bullets = 15;
			anim.speed = 0.9f;
			ammo.text = bullets.ToString () + "/15";
	}

	public void PushButton (Animator anim){
		anim.SetBool ("PushButton", true);
	}

	public void Recover(Animator anim){
		anim.SetBool ("PushButton", false);
	}
}

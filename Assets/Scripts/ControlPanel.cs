using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class ControlPanel : MonoBehaviour {

	public Text message;
	public GameObject player;

	PlayerActions playerAction;
	GameController control;
	Animator anim;

	void Awake () {
		message.text = "";
		playerAction = player.GetComponent<PlayerActions>();
		control = FindObjectOfType<GameController> ();
		anim = player.GetComponent<Animator> ();
	}

	void Update () {
		if (Vector3.Distance (player.transform.position, control.cPanel.transform.position) < 2f) {
			message.text = "Send SOS message, press [E]";
			if (Input.GetButton ("Push")) {
				playerAction.PushButton (anim);
				message.text = "Rescue on the way, find the rescueship!";
				control.ShipSpawn ();
			}
		}
		else {
			playerAction.Recover (anim);
			message.text = "";
		}
	}
}
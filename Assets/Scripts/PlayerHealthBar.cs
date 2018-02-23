using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour {
	
	public Slider healthBar;
	public Image fill;
	Color fullHealth = new Color32 (79, 184, 70, 255);
	Color mediumHealth = Color.yellow;
	Color lowHealth = Color.red;

	public PlayerHealth playerHealth;

	void OnEnable(){
		playerHealth.onHealthChanged += HandleHealthChange;
	}

	void OnDisable(){
		playerHealth.onHealthChanged -= HandleHealthChange;
	}

	void HandleHealthChange(int currentHealth){
		healthBar.value = currentHealth;

		if (currentHealth == 100) {
			fill.color = fullHealth;
		}

		if (currentHealth == 50) {
			fill.color = mediumHealth;
		}
		if (healthBar.value <= 20) {
			fill.color = lowHealth;
		}
		if (currentHealth == 0) {
			fill.enabled = false;
		}
	}
}

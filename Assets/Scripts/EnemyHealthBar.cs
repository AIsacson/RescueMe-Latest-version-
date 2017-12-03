using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour {
	public EnemyController enemyHealth;
	public Slider healthBar;
	public Image fill;

	void OnEnable(){
		enemyHealth.onHealthChanged += HealthChanged;
	}

	void OnDisable(){
		enemyHealth.onHealthChanged -= HealthChanged;
	}

	void HealthChanged(int lives){
		healthBar.value = lives;
		if (lives == 0) {
			fill.enabled = false;
		}
	}
}

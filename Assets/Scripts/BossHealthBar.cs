using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour {
	public BossController bossHealth;
	public Slider healthBar;
	public Image fill;

	void OnEnable(){
		bossHealth.onHealthChanged += HealthChanged;
	}

	void OnDisable(){
		bossHealth.onHealthChanged -= HealthChanged;
	}

	void HealthChanged(int lives){
		healthBar.value = lives;
		if (lives == 0) {
			fill.enabled = false;
		}
	}
}

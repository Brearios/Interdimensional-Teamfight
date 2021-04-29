using System;
using System.Collections;
using System.Collections.Generic;
using RengeGames.HealthBars;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RengeGames.HealthBars.Demo {

	public class DemoHealthController : MonoBehaviour {

		public string parentName = "Player";
		public string healthBarName = "Primary";
		public bool updateFromScript = true;
		public float value = 0.5f;
		
		private void Update() {
			if (updateFromScript) {
				StatusBarsManager.SetPercent(parentName, healthBarName, value);
			}
		}

		public void SetHealthPercent(float value) {
			StatusBarsManager.SetPercent("Player", "Main", value);
		}

		public void ToggleNoise(bool toggle) {
			StatusBarsManager.SetShaderPropertyValue(parentName, healthBarName, RadialHealthBarProperties.EmptyNoiseEnabled, toggle);
			//if you want to manually store the healthbars in an array, this is how you can access properties
			//you can select specific healthbars by querying for parent names and health bar names
			//if you don't want to use the health bar manager at all, you can disable it completely by calling StatusBarsManager.Disable();
			//healthBars?.ForEach((healthBar) => { healthBar.EmptyNoiseEnabled.Value = toggle; });
		}

		public void Instantiate() {
			var parent = new GameObject("Daddy o' 100");
			parent.transform.position = new Vector3(0, 0, 0);
			for (int i = 0; i < 100; i++) {
				var go = new GameObject("HealthBar" + i, typeof(RadialSegmentedHealthBar));
				go.transform.parent = parent.transform;
				go.transform.position = new Vector3((Random.value * 2 - 1) * 6, (Random.value * 2 - 1) * 3, 0);
			}
		}
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RengeGames.HealthBars {
	public class StatusBarsManager {
		private static StatusBarsManager _instance;
		private bool enabled = true;
		static StatusBarsManager InternalInstance => _instance ?? (_instance = new StatusBarsManager());

		private Dictionary<string, Dictionary<string, List<ISegmentedHealthBar>>> _healthBars;

		private StatusBarsManager() {
			_healthBars = new Dictionary<string, Dictionary<string, List<ISegmentedHealthBar>>>();
		}

		public bool IsEnabled() {
			return InternalInstance.enabled;
		}
		public void Disable() {
			InternalInstance.enabled = false;
		}
		public void Enable() {
			InternalInstance.enabled = true;
		}
		public void Clear() {
			InternalInstance._healthBars.Clear();
		}

		/// <summary>
		/// This method is applied automatically and shouldn't be used otherwise
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="healthBarName"></param>
		/// <param name="healthBar"></param>
		public static void AddHealthBar(ISegmentedHealthBar healthBar) {
			if (!InternalInstance.enabled) return;
			if (string.IsNullOrEmpty(healthBar.GetParentName()) || string.IsNullOrEmpty(healthBar.GetName())) return;
			if (InternalInstance._healthBars.TryGetValue(healthBar.GetParentName(), out var parent)) {
				if (parent.TryGetValue(healthBar.GetName(), out var hbs)) {
					hbs.Add(healthBar);
				}
				else {
					var hbList = new List<ISegmentedHealthBar>();
					hbList.Add(healthBar);
					parent.Add(healthBar.GetName(), hbList);
				}
				//Debug.Log($"Health Bar {healthBar.GetParentName()}/{healthBar.GetName()} added");
			} else {
				var newParent = new Dictionary<string, List<ISegmentedHealthBar>>();
				InternalInstance._healthBars.Add(healthBar.GetParentName(), newParent);
				var hbList = new List<ISegmentedHealthBar>();
				hbList.Add(healthBar);
				newParent.Add(healthBar.GetName(), hbList);
				//Debug.Log($"Health Bar {healthBar.GetParentName()}/{healthBar.GetName()} added");
			}
			
		}

		public static void RemoveHealthBar(ISegmentedHealthBar healthBar, bool removeAll) {
			if (!InternalInstance.enabled) return;
			if (string.IsNullOrEmpty(healthBar.GetParentName()) || string.IsNullOrEmpty(healthBar.GetName())) return;
			if (InternalInstance._healthBars.TryGetValue(healthBar.GetParentName(), out var parent)) {
				bool removed = true;
				if (removeAll) {
					removed = parent.Remove(healthBar.GetName());
				}
				else if (parent.TryGetValue(healthBar.GetName(), out var hbs)) {
					removed = hbs.Remove(healthBar);
					if (hbs.Count == 0) 
						parent.Remove(healthBar.GetName());
				}
				else {
					removed = false;
				}

				if (removed && parent.Count == 0)
					InternalInstance._healthBars.Remove(healthBar.GetParentName());
			}
		}
		
		public static void RemoveHealthBar(ISegmentedHealthBar healthBar, string parentName, string name, bool removeAll) {
			if (!InternalInstance.enabled) return;
			if (string.IsNullOrEmpty(parentName) || string.IsNullOrEmpty(name)) return;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				bool removed = true;
				if (removeAll) {
					removed = parent.Remove(name);
				}
				else if (parent.TryGetValue(name, out var hbs)) {
					removed = hbs.Remove(healthBar);
					if (hbs.Count == 0) 
						parent.Remove(name);
				}
				else {
					removed = false;
				}
				if (removed && parent.Count == 0)
					InternalInstance._healthBars.Remove(parentName);
			}
		}

		public static void RemoveHealthBar(string parentName, string name, bool removeAll) {
			if (!InternalInstance.enabled) return;
			if (string.IsNullOrEmpty(parentName) || string.IsNullOrEmpty(name)) return;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				bool removed = true;
				if (removeAll) {
					removed = parent.Remove(name);
				}
				else if (parent.TryGetValue(name, out var hbs)) {
					int index = hbs.FindIndex(hb => hb.GetName() == name);
					removed = index != -1;
					if(removed) 
						hbs.RemoveAt(index);
					if (hbs.Count == 0) 
						parent.Remove(name);
				}
				else {
					removed = false;
				}
				if (removed && parent.Count == 0)
					InternalInstance._healthBars.Remove(parentName);
			}
		}

		public static void RemoveHealthBars(string parentName) {
			if (!InternalInstance.enabled) return;
			InternalInstance._healthBars.Remove(parentName);
		}

		/// <summary>
		/// Returns all health bars with the given name combo as a List<ISegmentedHealthBar>
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="childName"></param>
		/// <returns></returns>
		public List<ISegmentedHealthBar> GetHealthBar(string parentName, string childName) {
			if (!InternalInstance.enabled) return null;
			if (string.IsNullOrEmpty(parentName) || string.IsNullOrEmpty(childName)) return null;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(childName, out var hbs)) {
					return hbs;
				}
			}

			return null;
		}

		/// <summary>
		/// Get all health bars with the given parent as a Dictionary
		/// </summary>
		/// <param name="parentName"></param>
		/// <returns></returns>
		public Dictionary<string, List<ISegmentedHealthBar>> GetHealthBars(string parentName) {
			if (!InternalInstance.enabled) return null;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				return parent;
			}

			return null;
		}

		/// <summary>
		/// Set the segment count of all health bars with a given parent/name combo
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="healthBarName"></param>
		/// <param name="value"></param>
		public static void SetSegmentCount(string parentName, string healthBarName, float value) {
			if (!InternalInstance.enabled) return;
			if (string.IsNullOrEmpty(healthBarName) || string.IsNullOrEmpty(parentName)) return;

			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(healthBarName, out var hbs))
					hbs.ForEach((hb) => hb.SetSegmentCount(value));
			}
		}

		/// <summary>
		/// Set the segment count of all health bars with the given parent
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="value"></param>
		public static void SetSegmentCount(string parentName, float value) {
			if (!InternalInstance.enabled) return;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				foreach (var hbs in parent) {
					hbs.Value.ForEach(hb => hb.SetSegmentCount(value));
				}
			}
		}

		/// <summary>
		/// Set the removed segment count of all health bars with a given parent/name combo 
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="healthBarName"></param>
		/// <param name="value"></param>
		public static void SetRemovedSegments(string parentName, string healthBarName, float value) {
			if (!InternalInstance.enabled) return;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(healthBarName, out var hbs))
					hbs.ForEach(hb => hb.SetRemovedSegments(value));
			}
		}
		
		/// <summary>
		/// Set the removed segment count of all health bars with a given parent
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="value"></param>
		public static void SetRemovedSegments(string parentName, float value) {
			if (!InternalInstance.enabled) return;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				foreach (var hbs in parent) {
					hbs.Value.ForEach(hb => hb.SetRemovedSegments(value));
				}
			}
		}

		/// <summary>
		/// Set the health percent of all health bars with a given parent/name combo
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="healthBarName"></param>
		/// <param name="value"></param>
		public static void SetPercent(string parentName, string healthBarName, float value) {
			if (!InternalInstance.enabled) return;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(healthBarName, out var hbs))
					hbs.ForEach(hb => hb.SetPercent(value));
			}
		}

		/// <summary>
		/// Set the health percent of all health bars with a given parent
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="value"></param>
		public static void SetPercent(string parentName, float value) {
			if (!InternalInstance.enabled) return;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				foreach (var hbs in parent) {
					hbs.Value.ForEach(hb => hb.SetPercent(value));
				}
			}
		}

		/// <summary>
		/// Add or remove segments of all health bars with a given parent/name combo.
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="healthBarName"></param>
		/// <param name="value">can be positive or negative</param>
		public static void AddRemoveSegments(string parentName, string healthBarName, float value) {
			if (!InternalInstance.enabled) return;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(healthBarName, out var hbs))
					hbs.ForEach(hb => hb.AddRemoveSegments(value));
			}
		}

		/// <summary>
		/// Add or remove segments of all health bars with a given parent
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="healthBarName"></param>
		/// <param name="value">can be positive or negative</param>
		public static void AddRemoveSegments(string parentName, float value) {
			if (!InternalInstance.enabled) return;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				foreach (var hbs in parent) {
					hbs.Value.ForEach(hb => hb.AddRemoveSegments(value));
				}
			}
		}

		/// <summary>
		/// Add or remove percent of all health bars with a given parent/name combo
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="healthBarName"></param>
		/// <param name="value">can be positive or negative</param>
		public static void AddRemovePercent(string parentName, string healthBarName, float value) {
			if (!InternalInstance.enabled) return;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(healthBarName, out var hbs))
					hbs.ForEach(hb => hb.AddRemovePercent(value));
			}
		}

		/// <summary>
		/// Add or remove percent of all health bars with a given parent
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="healthBarName"></param>
		/// <param name="value">can be positive or negative</param>
		public static void AddRemovePercent(string parentName, float value) {
			if (!InternalInstance.enabled) return;
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				foreach (var hbs in parent) {
					hbs.Value.ForEach(hb => hb.AddRemovePercent(value));
				}
			}
		}

		/// <summary>
		/// Get a list of all shader properties of all health bars with a given parent/name combo
		/// </summary>
		/// <param name="parentName"></param>
		/// <param name="healthBarName"></param>
		/// <param name="propertyName"></param>
		/// <param name="shaderProperties"></param>
		/// <typeparam name="T">this can be bool, float, Color, Vector or Texture2D</typeparam>
		/// <returns>true if success</returns>
		public static bool GetShaderProperties<T>(string parentName, string healthBarName, string propertyName, out List<ShaderProperty<T>> shaderProperties) {
			if (!InternalInstance.enabled) {
				shaderProperties = null;
				return false;
			}
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(healthBarName, out var hbs)) {
					shaderProperties = hbs.Select(hb => {
						hb.GetShaderProperty(propertyName, out ShaderProperty<T> sp);
						return sp;
					}).ToList();
					return true;
				}
			}

			shaderProperties = null;
			return false;
		}

		public static bool GetShaderPropertyValue<T>(string parentName, string healthBarName, string propertyName, out T value) {
			if (!InternalInstance.enabled) {
				value = default;
				return false;
			}
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(healthBarName, out var hbs))
					return hbs[0].GetShaderPropertyValue(propertyName, out value);
			}

			value = default;
			return false;
		}

		public static bool SetShaderPropertyValue<T>(string parentName, string healthBarName, string propertyName, T value) {
			if (!InternalInstance.enabled) {
				return false;
			}
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(healthBarName, out var hbs)) {
					hbs.ForEach(hb => hb.SetShaderPropertyValue(propertyName, value));
					return true;
				}
			}
			return false;
		}

		public bool GetShaderKeywords(string parentName, string healthBarName, string keywordName, out List<ShaderKeyword> shaderKeywords) {
			if (!InternalInstance.enabled) {
				shaderKeywords = null;
				return false;
			}
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(healthBarName, out var hbs)){
					shaderKeywords = hbs.Select(hb => {
						hb.GetShaderKeyword(keywordName, out ShaderKeyword sk);
						return sk;
					}).ToList();
					return true;
				}
			}

			shaderKeywords = null;
			return false;
		}

		public bool GetShaderKeywordValue(string parentName, string healthBarName, string propertyName, out bool value) {
			if (!InternalInstance.enabled) {
				value = false;
				return false;
			}
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(healthBarName, out var hbs))
					return hbs[0].GetShaderKeywordValue(propertyName, out value);
			}

			value = default;
			return false;
		}

		public bool SetShaderKeywordValue(string parentName, string healthBarName, string keywordName, bool value) {
			if (!InternalInstance.enabled) {
				return false;
			}
			if (InternalInstance._healthBars.TryGetValue(parentName, out var parent)) {
				if (parent.TryGetValue(healthBarName, out var hbs)) {
					hbs.ForEach(hb => hb.SetShaderKeywordValue(keywordName, value));
				}
				return true;
			}

			return false;
		}
	}
}
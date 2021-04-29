using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RengeGames.HealthBars {

	public interface ISegmentedHealthBar {
		string GetParentName();
		string GetName();
		/// <summary>
		/// Set the number of segments in this health bar
		/// </summary>
		/// <param name="value">number of segments</param>
		void SetSegmentCount(float value);
		/// <summary>
		/// Sets the absolute count of removed segments
		/// </summary>
		/// <param name="value">segment count to be set as removed</param>
		void SetRemovedSegments(float value);
		/// <summary>
		/// Sets the absolute percentage of the health bar
		/// </summary>
		/// <param name="value">percentage from 0 (no health) to 1 (max health)</param>
		void SetPercent(float value);
		/// <summary>
		/// Add or remove a certain amount of segments from/to the health bar
		/// This does not alter the segment count
		/// </summary>
		/// <param name="value">Amount of segments to add(+)/remove(-)</param>
		void AddRemoveSegments(float value);
		/// <summary>
		/// Add or remove a certain percent of the health bar
		/// This does not alter the maximum value
		/// </summary>
		/// <param name="value">percentage to add(+)/remove(-) from/to the health bar</param>
		void AddRemovePercent(float value);
		/// <summary>
		/// Get the specified property
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertyName">The property name, use the RadialHealthBarProperties class for help</param>
		/// <param name="shaderProperty"></param>
		/// <returns>true if successful</returns>
		bool GetShaderProperty<T>(string propertyName, out ShaderProperty<T> shaderProperty);
		/// <summary>
		/// Get the specified keyword
		/// </summary>
		/// <param name="propertyName">The keyword name, use the RadialHealthBarKeywords class for help</param>
		/// <param name="shaderKeyword"></param>
		/// <returns>true if successful</returns>
		bool GetShaderKeyword(string propertyName, out ShaderKeyword shaderKeyword);
		/// <summary>
		/// Get the specified property value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertyName">The property name, use the RadialHealthBarProperties class for help</param>
		/// <param name="value"></param>
		/// <returns>true if success</returns>
		bool GetShaderPropertyValue<T>(string propertyName, out T value);
		/// <summary>
		/// Set the specified property value
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="propertyName">The property name, use the RadialHealthBarProperties class for help</param>
		/// <param name="value"></param>
		/// <returns>true if success</returns>
		bool SetShaderPropertyValue<T>(string propertyName, T value);
		/// <summary>
		/// Get the specified keyword value
		/// </summary>
		/// <param name="propertyName">The keyword name, use the RadialHealthBarKeywords class for help</param>
		/// <param name="value"></param>
		/// <returns>true if success</returns>
		bool GetShaderKeywordValue(string propertyName, out bool value);
		/// <summary>
		/// Set the specified keyword value
		/// </summary>
		/// <param name="propertyName">The keyword name, use the RadialHealthBarKeywords class for help</param>
		/// <param name="value"></param>
		/// <returns>true if success</returns>
		bool SetShaderKeywordValue(string propertyName, bool value);

	}
}
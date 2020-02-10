using UnityEngine;

namespace Assets.HeroEditor.Common.CharacterScripts
{
	/// <summary>
	/// Makes character to look at cursor side (flip by X scale).
	/// </summary>
	public class CharacterFlip : MonoBehaviour
	{
		public void Update()
		{
			var scale = transform.localScale;

			scale.x = Mathf.Abs(scale.x);

			// Flips if right or left of target
			if (GetComponent<Actor>().autoAtkTarget.transform.position.x < transform.position.x) scale.x *= -1;

			transform.localScale = scale;
		}
	}
}
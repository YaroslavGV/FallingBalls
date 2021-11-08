using UnityEngine;

namespace Balls
{
	public interface IBallSkin
	{
		void SetColor (Color color);
		IBallSkin Create (Transform parent);
		void Destroy ();
	}
}
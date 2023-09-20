using UnityEngine;

namespace Aid
{
	public abstract class IntervalMonoBehviour : MonoBehaviour
	{

		private float time;
		
		private void Update()
		{
			time += Time.deltaTime;

			if (time >= 1)
			{
				time = 0;
				EverySecoundUpdate();
			}
		}

		protected virtual void EverySecoundUpdate()
		{
		}
	}
}
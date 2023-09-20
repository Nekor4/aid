namespace Aid.Effects
{
	using UnityEngine;

	public class ParticleTest : MonoBehaviour
	{
		public Transform start,
		                 end;

		public int count;

		public float duration;

		public TargetParticle targetParticle;

		private void Awake()
		{
			enabled = false;
		}

		private void Update()
		{
			var config = new TargetParticle.Config
			{
				count = count, duration = duration, endPoint = end, startPoint = start
			};
			
			targetParticle.Play(config);
			enabled = false;
		}
	}
}

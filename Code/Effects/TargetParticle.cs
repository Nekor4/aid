namespace Aid.Effects
{
	using UnityEngine;
	[RequireComponent(typeof(ParticleSystem))]

	public class TargetParticle : MonoBehaviour
	{
		private ParticleSystem particleSystem;

		private ParticleSystem.MainModule main;
		
		private Config currentConfig;
		private void Awake()
		
		{
			particleSystem = GetComponent<ParticleSystem>();
			main = particleSystem.main;

			main.playOnAwake = false;
		}

		public void Play(Config config)
		{
			particleSystem.Clear();
			
			
			currentConfig = config;
			main.duration = config.duration;
			main.startLifetime = config.duration;
			transform.position = config.startPoint.position;

			particleSystem.Emit(currentConfig.count);
			enabled = true;
		}

		private void Update()
		{
			
			var particles = new ParticleSystem.Particle[particleSystem.particleCount];
			particleSystem.GetParticles(particles);
			var psProgress = 0f;

			var isCompleted = true;
			
			
			for (int i = 0; i < particles.Length; i += 1)
			{
				psProgress = 1 - (particles[i].remainingLifetime / particles[i].startLifetime);

				if (psProgress < 1)
				{
					isCompleted = false;
				}

				particles[i].position = Vector3.Lerp(
					particles[i].position,
					currentConfig.endPoint.position,
					psProgress);
			}

			particleSystem.SetParticles(particles);
			
			if(isCompleted)
				Complete();
		}

		private void Complete()
		{
			enabled = false;
		}
		
		public struct Config
		{
			public float duration;

			public int count;

			public Transform startPoint, endPoint;
		}
	}
}

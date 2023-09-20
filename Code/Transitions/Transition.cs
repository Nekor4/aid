using System;

namespace Aid
{
	public class Transition
	{
		public readonly float Duration;

		private float _elapsedTime;

		private float _progress;
		
		public Transition(float duration, OnUpdate onUpdated, Action completed)
		{
			Duration = duration;
			Updated = onUpdated;
			Completed = completed;
		}
		
		public delegate void OnUpdate(float progress);
		
		private event OnUpdate Updated;
		private event Action Completed;

		public float Progress
		{
			get
			{
				if (Duration < 0) return 0;
				return _progress;
			}
		}

		public void Update(float deltaTime)
		{
			_elapsedTime += deltaTime;
			_progress = _elapsedTime / Duration;
			Updated?.Invoke(Progress);
		}

		public void Complete()
		{
			Completed?.Invoke();
		}
	}
}
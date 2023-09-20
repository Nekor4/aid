using System;
using UnityEngine.Assertions;

namespace Aid
{
	[Serializable]
	public class TimeProcess
	{
		public DateTime startTime;
		public int durationInMinutes;
		public State state;
		public TimeSpan estimatedTime;
		public float percent;

		public void Start(DateTime newStartTime, int newDurationInMinutes)
		{
			startTime = newStartTime;
			durationInMinutes = newDurationInMinutes;
			state = State.Processing;
		}
		
		public void Update(DateTime currentTime)
		{
			estimatedTime = startTime.AddMinutes(durationInMinutes).Subtract(currentTime);
			percent = 1 - (float) estimatedTime.TotalSeconds / (durationInMinutes * 60f);

			if (estimatedTime.Ticks <= 0 && state == State.Processing)
				state = State.ReadyForCompletation;
		}

		public void Complete()
		{
			Assert.IsTrue(state == State.ReadyForCompletation);

			state = State.Idle;
		}

		public enum State
		{
			Idle,
			Processing,
			ReadyForCompletation
		}
	}
}
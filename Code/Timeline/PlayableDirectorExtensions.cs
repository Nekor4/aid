using UnityEngine.Playables;

namespace Aid.Timeline
{
	public static class PlayableDirectorExtensions
	{
		public static void RewindToEnd(this PlayableDirector director)
		{
			director.Play();
			director.time = 10000;
			director.Pause();
		}

		public static void Play(this PlayableDirector dierctor, double time)
		{
			dierctor.time = time;
			dierctor.Play();
		}
	}
}
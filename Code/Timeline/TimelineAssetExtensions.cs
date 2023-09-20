using System.Collections.Generic;
using System.Linq;
using UnityEngine.Timeline;

namespace Aid.Timeline
{
	public static class TimelineAssetExtensions
	{
		public static void RemoveAllTracks(this TimelineAsset timelineAsset)
		{
			var enumTracks = timelineAsset.GetRootTracks();
			var tracks = enumTracks.Where(track => !track.name.ToLower().Equals("other")).ToList();

			foreach (var track in tracks)
				timelineAsset.DeleteTrack(track);
		}
	}
}
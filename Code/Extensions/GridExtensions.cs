using UnityEngine;

namespace Aid.Extensions
{
	public static class GridExtensions
	{
		public static Vector3Int WorldToClosestCell(this Grid grid, Vector3 worldPosition)
		{
			var cellSizeAndGap = grid.cellGap + grid.cellSize;

			var pos = new Vector3(worldPosition.x / cellSizeAndGap.x, worldPosition.y / cellSizeAndGap.y,
				worldPosition.z / cellSizeAndGap.z);

			return pos.ToClosestVector3Int();
		}
	}
}
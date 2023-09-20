using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Aid;

[RequireComponent(typeof(LineRenderer))]
public class SmoothLine : MonoBehaviour
{
	public Vector3 start, control, end;

	public int numOfPoints = 30;

	public void Smooth()
	{
		var lr = GetComponent<LineRenderer>();

		Assert.IsNotNull(lr);

		var smoothed = new List<Vector3>();

		for (int i = 0; i <= numOfPoints; i++)
		{
			var point = MathExtension.CubicBezier(start, control, end, (float) i / numOfPoints);
			smoothed.Add(point);
		}

		lr.positionCount = smoothed.Count;
		lr.SetPositions(smoothed.ToArray());
	}
}
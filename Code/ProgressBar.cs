using System;
using UnityEngine;

namespace Aid
{
	public class ProgressBar : MonoBehaviour
	{
		public event Action AllFilledWithGreen;

		[SerializeField] private Color successColor = Color.green;
		[SerializeField] private Color failColor = Color.red;

		[SerializeField] private Transform progressParent = null;

		private Color defaultColor;
		private SpriteRenderer[] progressParts;

		private int partIndex, successCount, failCount;


		private void Awake()
		{
			progressParts = progressParent.GetComponentsInChildren<SpriteRenderer>();
			defaultColor = progressParts[0].color;
		}

		public void Clear()
		{
			foreach (var part in progressParts)
				part.color = defaultColor;

			partIndex = 0;
			successCount = 0;
			failCount = 0;
		}

		public void AddSuccess()
		{
			if (partIndex == progressParts.Length) return;

			progressParts[partIndex].color = successColor;
			partIndex++;
			successCount++;

			if (successCount == progressParts.Length && AllFilledWithGreen != null)
				AllFilledWithGreen.Invoke();
		}

		public void AddFail()
		{
			if (partIndex == progressParts.Length) return;

			progressParts[partIndex].color = failColor;
			partIndex++;
			failCount++;
		}
	}
}
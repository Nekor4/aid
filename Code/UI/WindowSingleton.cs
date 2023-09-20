using UnityEngine.Assertions;

namespace Aid.UI
{
	public class WindowSingleton <T> : Window where T : Window
	{
		private const string SingletonIsNull = "An instance of {0} is needed in the scene, but there is none.";

		protected static T instance;

		public static T Instance
		{
			get
			{
				if (instance == null)
				{
					instance = (T) FindObjectOfType(typeof(T));

					Assert.IsNotNull(instance, string.Format(SingletonIsNull, typeof(T)));
				}

				return instance;
			}
		}

		public static bool IsInstanceExists
		{
			get
			{
				if (instance == null)
					instance = (T) FindObjectOfType(typeof(T));

				return instance != null;
			}
		}
	}
}
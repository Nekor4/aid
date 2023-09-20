using UnityEngine;

namespace Aid.Data
{
    public class DataSaver : MonoBehaviour
    {
        private void OnApplicationQuit()
        {
            Save();
        }

        public void Save()
        {
            DataStorage.Save();
        }
    }
}
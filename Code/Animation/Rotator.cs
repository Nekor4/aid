using UnityEngine;

namespace Aid.Animation
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 speed;

        public void Update()
        {
            var rot = transform.rotation;
            rot *= Quaternion.Euler(speed * Time.deltaTime);
            transform.rotation = rot;
        }
    }
}
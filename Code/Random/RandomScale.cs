namespace Aid {
using UnityEngine;

public class RandomScale : MonoBehaviour
{
    [SerializeField] private Vector3 min = Vector3.zero, max = Vector3.one;

    private void Awake()
    {
        transform.localScale = new Vector3(UnityEngine.Random.Range(min.x, max.x),
            UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
    }
}
}
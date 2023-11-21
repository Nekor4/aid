using UnityEngine;
using UnityEngine.Pool;

namespace Aid.Health
{
    public class HealthBarsPool : Singleton<HealthBarsPool>, IObjectPool<HealthBar>
    {
        [SerializeField] private HealthBar healthBarViewPrefab;

        private ObjectPool<HealthBar> _barsPool;

        private void Awake()
        {
            _barsPool ??= new ObjectPool<HealthBar>(Create, null, OnRelease);
        }

        private HealthBar Create()
        {
            var bar = Instantiate(healthBarViewPrefab, transform);
            return bar;
        }

        private void OnRelease(HealthBar healthBarView)
        {
            healthBarView.Clear();
            healthBarView.gameObject.SetActive(false);
        }

        public HealthBar Get()
        {
            _barsPool ??= new ObjectPool<HealthBar>(Create, null, OnRelease);
            return _barsPool.Get();
        }

        public PooledObject<HealthBar> Get(out HealthBar bar) => _barsPool.Get(out bar);

        public void Release(HealthBar element) => _barsPool.Release(element);

        public void Clear() => _barsPool.Clear();

        public int CountInactive => _barsPool.CountInactive;
    }
}
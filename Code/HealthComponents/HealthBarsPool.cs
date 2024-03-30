using System.Collections.Generic;
using UnityEngine.Pool;

namespace Aid.HealthComponents
{
    public class HealthBarsPool : Singleton<HealthBarsPool>
    {
        private readonly Dictionary<HealthBarFactorySO, ObjectPool<HealthBar>> _barsPools = new();

        public HealthBar Get(HealthBarFactorySO healthBarFactory)
        {
            var pool = GetPoolByFactory(healthBarFactory);
            var bar = pool.Get();
            bar.gameObject.SetActive(true);
            return bar;
        }

        private ObjectPool<HealthBar> GetPoolByFactory(HealthBarFactorySO factory)
        {
            if (_barsPools.TryGetValue(factory, out var pool))
                return pool;

            pool = new ObjectPool<HealthBar>(factory.Create, OnGet, OnRelease);
            _barsPools.Add(factory, pool);
            return pool;
        }

        private void OnGet(HealthBar healthBarView)
        {
            healthBarView.gameObject.SetActive(true);
            healthBarView.transform.SetParent(transform, false);
        }

        private void OnRelease(HealthBar healthBarView)
        {
            healthBarView.Clear();
            healthBarView.gameObject.SetActive(false);
        }

        public void Release(HealthBar element, HealthBarFactorySO factory) => _barsPools[factory].Release(element);
    }
}
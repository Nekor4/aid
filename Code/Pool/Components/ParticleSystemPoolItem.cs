using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Aid.Pool.Components
{
    [RequireComponent(typeof(ParticleSystem))]
    public class ParticleSystemPoolItem : MonoBehaviour
    {
        private ParticleSystem _particleSystem;

        private ParticleSystemPoolSO _ownerPool;

        private void Awake()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }

        internal void SetOwnerPool(ParticleSystemPoolSO ownerPool)
        {
            _ownerPool = ownerPool;
        }

        public void Play()
        {
            _particleSystem.Play();
        }

        public void Stop()
        {
            _particleSystem.Stop();
        }

        public void Clear()
        {
            _particleSystem.Clear();
        }

        public void Pause()
        {
            _particleSystem.Pause();
        }

        public async void ReturnInTime(float time)
        {
            await UniTask.WaitForSeconds(time);
            _ownerPool.Return(this);
        }
    }
}
﻿using Aid.Factory;
using Aid.Pool;
using UnityEngine;

namespace Aid.Audio.SoundEmitters
{
    [CreateAssetMenu(fileName = "NewSoundEmitterPool", menuName = "Aid/Pool/SoundEmitter Pool")]
    public class SoundEmitterPoolSO : ComponentPoolSO<SoundEmitter>
    {
        [SerializeField] private SoundEmitterFactorySO _factory;

        public override IFactory<SoundEmitter> Factory
        {
            get { return _factory; }
        }
    }
}
using Aid.Factory;
using Aid.Factory.SO;
using UnityEngine;

namespace Aid.Audio.SoundEmitters
{
	[CreateAssetMenu(fileName = "NewSoundEmitterFactory", menuName = "Factory/SoundEmitter Factory")]
	public class SoundEmitterFactorySO : FactorySO<SoundEmitter>
	{
		public SoundEmitter prefab = default;

		public override SoundEmitter Create()
		{
			return Instantiate(prefab);
		}
	}
}
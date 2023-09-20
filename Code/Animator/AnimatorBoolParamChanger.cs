namespace TapNice.Scripts.GameCore.Animations
{
	using UnityEngine;

	public class AnimatorBoolParamChanger : StateMachineBehaviour
	{
		[SerializeField]
		private bool makeTrueOnEnter = true;

		[SerializeField]
		private string paramName;

		public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			animator.SetBool(paramName, makeTrueOnEnter);
		}

		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			animator.SetBool(paramName, makeTrueOnEnter == false);
		}
	}
}

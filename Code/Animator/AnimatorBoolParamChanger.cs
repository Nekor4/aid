using UnityEngine;

namespace Aid.Animator
{
	public class AnimatorBoolParamChanger : StateMachineBehaviour
	{
		[SerializeField]
		private bool makeTrueOnEnter = true;

		[SerializeField]
		private string paramName;

		public override void OnStateEnter(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			animator.SetBool(paramName, makeTrueOnEnter);
		}

		public override void OnStateExit(UnityEngine.Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			animator.SetBool(paramName, makeTrueOnEnter == false);
		}
	}
}

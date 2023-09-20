using Aid.StateMachine.Core;
using UnityEngine;

namespace Aid.UI
{    
    [CreateAssetMenu(menuName = "Aid/States/Actions/Show Windows")]
    public class ShowWindowsAction : StateAction
    {

        [SerializeField] private bool hideAllPreviousWindows = true;
        [SerializeField] private WindowConfig[] windowsToShow;

        public override void OnStateEnter()
        {
            if (hideAllPreviousWindows)
            {
                WindowsManager.Instance.HideAll();
            }

            for (int i = 0; i < windowsToShow.Length; i++)
            {
                windowsToShow[i].Show();
            }
        }

        public override void OnUpdate()
        {
            
        }
    }
}
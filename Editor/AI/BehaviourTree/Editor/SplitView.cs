namespace Aid.BehaviourTree.Editor
{
    using UnityEngine.UIElements;

    public class SplitView : TwoPaneSplitView
    {
        public new class UxmlFactory : UxmlFactory<SplitView, TwoPaneSplitView.UxmlTraits>
        {
        }
    }
}
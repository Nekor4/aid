namespace Aid.BehaviourTree
{
    using UnityEngine;

    public class BehaviourTreeRunner : MonoBehaviour
    {
        [SerializeField] private BehaviourTree behaviourTree;

        public BehaviourTree RunningTree => behaviourTree;
        
        private void Awake()
        {
            SetBehaviourTree(behaviourTree);
        }

        public void SetBehaviourTree(BehaviourTree newBehaviourTree)
        {
            if(newBehaviourTree == null) return;
            behaviourTree = newBehaviourTree.Clone(name);
            behaviourTree.Bind(gameObject);
        }

        private void Update()
        {
            if (behaviourTree != null) behaviourTree.Update();
        }
    }
}
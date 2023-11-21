namespace Aid.AI.BehaviourTree.Nodes.Composites {
    public class RandomSelector : CompositeNode {
        protected int current;

        protected override void OnStart() {
            current = UnityEngine.Random.Range(0, children.Count);
        }

        protected override void OnStop() {
        }

        protected override State OnUpdate() {
            var child = children[current];
            return child.Update();
        }
    }
}
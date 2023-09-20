using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Aid.BehaviourTree {
    public class InterruptSelector : Selector {
        protected override State OnUpdate() {
            int previous = current;
            base.OnStart();
            var status = base.OnUpdate();
            if (previous != current) {
                if (children[previous].CurrentState == State.Running) {
                    children[previous].Abort();
                }
            }

            return status;
        }
    }
}
﻿using UnityEngine;

namespace Aid.BehaviourTree
{
    public class WaitNode : ActionNode
    {

        public float duration = 1;
        private float startTime;
        protected override void OnStart()
        {
            startTime = Time.time;
        }

        protected override void OnStop()
        {
        }

        protected override State OnUpdate()
        {
            if (Time.time - startTime > duration)
            {
                return State.Success;
            }

            return State.Running;
        }
    }
}
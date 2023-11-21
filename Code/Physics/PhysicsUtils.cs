using UnityEngine;

namespace Aid.Physics
{
    public class PhysicsUtils
    {
        public static Vector3 JumpTo(Vector3 from, Vector3 to ,float jumpAngle)
        {
            float gravity = UnityEngine.Physics.gravity.magnitude;
            // Selected angle in radians
            float angle = jumpAngle * Mathf.Deg2Rad;
 
            // Positions of this object and the target on the same plane
            Vector3 planarTarget = new Vector3(to.x, 0, to.z);
            Vector3 planarPostion = new Vector3(from.x, 0, from.z);
 
            // Planar distance between objects
            float distance = Vector3.Distance(planarTarget, planarPostion);
            // Distance along the y axis between objects
            float yOffset = from.y - to.y;
 
            float initialVelocity = (1 / Mathf.Cos(angle)) * Mathf.Sqrt((0.5f * gravity * Mathf.Pow(distance, 2)) / (distance * Mathf.Tan(angle) + yOffset));
 
            Vector3 velocity = new Vector3(0, initialVelocity * Mathf.Sin(angle), initialVelocity * Mathf.Cos(angle));
 
            // Rotate our velocity to match the direction between the two objects
            float angleBetweenObjects = Vector3.Angle(Vector3.forward, planarTarget - planarPostion) * (to.x > from.x ? 1 : -1);
            Vector3 finalVelocity = Quaternion.AngleAxis(angleBetweenObjects, Vector3.up) * velocity;
 
            // Fire!
            return finalVelocity;
        }
        
        public static Vector3[] GetPrediction(float rigidbodyDrag, Vector3 pos, Vector3 velocity, int steps)
        {
            Vector3[] results = new Vector3[steps];
 
            float timestep = Time.fixedDeltaTime / UnityEngine.Physics.defaultSolverVelocityIterations;
            Vector3 gravityAccel = UnityEngine.Physics.gravity * timestep * timestep;
            float drag = 1f - timestep * rigidbodyDrag;
            Vector3 moveStep = velocity * timestep;
 
            for (int i = 0; i < steps; ++i)
            {
                moveStep += gravityAccel;
                moveStep *= drag;
                pos += moveStep;
                results[i] = pos;
            }
 
            return results;
        }
    }
}
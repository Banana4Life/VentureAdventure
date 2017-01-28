using UnityEngine;

namespace Util
{
    public class MoveTo : MonoBehaviour
    {
        private bool start = false;
        private Vector3 targetPosition;
        private Vector3 sourcePosition;
        public float speed = 1;

        private void Update()
        {
            if (start)
            {
                if (sourcePosition == Vector3.zero)
                {
                    sourcePosition = transform.position;
                }

                transform.position = Vector3.Lerp(sourcePosition, targetPosition, speed * Time.deltaTime);
                if (transform.position == targetPosition)
                {
                    Destroy(this);
                }
            }
        }

        public void Move(Vector3 target)
        {
            sourcePosition = transform.position;
            targetPosition = target;
            start = true;
        }

        public static void MoveObject(GameObject go, Vector3 target, float speed)
        {
            var mover = go.AddComponent<MoveTo>();
            mover.speed = speed;
            mover.Move(target);
        }
    }
}
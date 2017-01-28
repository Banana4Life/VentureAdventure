using UnityEngine;

namespace Util
{
    public class MoveTo : MonoBehaviour
    {
        private bool start = false;
        private Vector3 targetPosition;
        private Vector3 sourcePosition;
        public float speed = 1;
        private double _startTime;

        private void Update()
        {
            if (start)
            {
                if (this._startTime == 0)
                {
                    this._startTime = Time.time;
                }

                float diff = (float) (Time.time - _startTime);

                transform.position = Vector3.Lerp(sourcePosition, targetPosition, diff/speed);
                if (transform.position == targetPosition)
                {
                    Destroy(this);
                }
            }
        }

        public void Move(Vector3 target, float speed)
        {
            this.speed = speed;
            sourcePosition = transform.position;
            targetPosition = target;
            start = true;
        }

        public static void MoveObject(GameObject go, Vector3 target, float speed)
        {
            var mover = go.AddComponent<MoveTo>();
            mover.Move(target, speed);
        }
    }
}
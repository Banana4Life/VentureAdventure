using System;
using Model.Util;
using UnityEngine;

namespace Util
{
    public class MoveTo : MonoBehaviour
    {
        private bool _start;
        private Vector3 _targetPosition;
        private Vector3 _sourcePosition;
        private double _startTime = Double.NegativeInfinity;

        private float _moveDuration = GameData.MoveAnimationTime;
        private Action _onMoveComplete;


        private void Update()
        {
            if (_start)
            {
                if (_startTime < 0)
                {
                    _startTime = Time.time;
                }

                var diff = (float) (Time.time - _startTime);

                transform.position = Vector3.Lerp(_sourcePosition, _targetPosition, diff/_moveDuration);

                if (transform.position != _targetPosition) return;

                if (_onMoveComplete != null)
                {
                    _onMoveComplete();
                }

                Destroy(this);
            }
        }

        public void Move(Vector3 target, float speed, Action afterMoveCallback)
        {
            _onMoveComplete = afterMoveCallback;
            _moveDuration = speed;
            _sourcePosition = transform.position;
            _targetPosition = target;

            _start = true;
        }
    }
}
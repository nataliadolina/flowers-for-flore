using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.Characters.Enums;

namespace Game.Characters.States.Movements
{
    internal class MovingSystem : MonoBehaviour
    {
        private Transform[] _points = null;
        private int _pointsLimit = 0;
        private int _currentPoint = 0;
        private int _direction = 1;

        private Dictionary<TypeMove, Action> typesMovement = null;

        internal Transform GetNextPoint(TypeMove typeMove)
        {
            _currentPoint += _direction;

            if (_currentPoint >= _pointsLimit || _currentPoint < 0)
                typesMovement[typeMove]();

            return _points[_currentPoint];
        }

        internal Transform GetStartPoint()
        {
            return _points[0];
        }

        internal void CreateWay()
        {
            typesMovement = new Dictionary<TypeMove, Action>()
            {
                { TypeMove.Reverse, this.ReverseChangeCurrentPoint},
                { TypeMove.Circle, this.CircleChangeCurrentPoint}
            };

            var i = 0;
            _points = new Transform[transform.childCount];

            if (_points.Length != 0)
            {
                foreach (Transform child in transform)
                {
                    _points[i] = child.transform;
                    i += 1;
                }
                _pointsLimit = _points.Length;
            }

        }

        internal void CircleChangeCurrentPoint()
        {
            _currentPoint = 0;
        }

        private void ReverseChangeCurrentPoint()
        {
            _direction *= -1;
            _currentPoint += _direction;
        }
    }
}

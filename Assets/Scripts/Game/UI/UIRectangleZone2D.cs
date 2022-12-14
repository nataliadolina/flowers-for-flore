using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Game.UI.Abstract;

namespace Game.UI
{
    /// <summary>
    /// Rectangle zone used in UI
    /// </summary>
    internal class UIRectangleZone2D : ZoneBase2D
    {
        [SerializeField] private Vector2 zoneSize;

        /// <summary>
        /// point that doesn't change its position while changing zone size
        /// </summary>
        private Vector3 _zoneFixingPoint;

        private void Start()
        {
            _zoneFixingPoint = transform.position;
        }

        internal override bool IsPositionInsideZone(Vector2 position)
        {
            return position.x <= _zoneFixingPoint.x && position.x >= _zoneFixingPoint.x - zoneSize.x
                && position.y >= _zoneFixingPoint.y && position.y <= _zoneFixingPoint.y + zoneSize.y;
        }

        private Vector3 GetZoneCenter(Vector3 fixingPoint)
        {
            return new Vector3(fixingPoint.x - zoneSize.x / 2, fixingPoint.y + zoneSize.y / 2, fixingPoint.z);
        }

#if UNITY_EDITOR

        private protected override void DrawGizmos()
        {
            Gizmos.DrawWireCube(GetZoneCenter(transform.position), new Vector3(zoneSize.x, zoneSize.y, 1));
        }

#endif
    }
}

using System;
using UnityEngine;

namespace CenterBall.Data
{
    /// <summary>
    /// Represents the state and position of a single ball
    /// </summary>
    [Serializable]
    public class BallData
    {
        public string id;
        public Vector3 position;
        public bool active; // Whether this ball has been played
        public int ownerId; // 1 for Player 1, 2 for Player 2, 0 for center ball

        public BallData(string ballId, Vector3 initialPosition, int owner)
        {
            id = ballId;
            position = initialPosition;
            active = false;
            ownerId = owner;
        }

        public BallData(BallData other)
        {
            id = other.id;
            position = other.position;
            active = other.active;
            ownerId = other.ownerId;
        }

        /// <summary>
        /// Calculate distance to another ball (2D X-Z plane)
        /// </summary>
        public float DistanceTo(BallData other)
        {
            return Vector2.Distance(
                new Vector2(position.x, position.z),
                new Vector2(other.position.x, other.position.z)
            );
        }

        /// <summary>
        /// Calculate distance from center (0, 0)
        /// </summary>
        public float DistanceFromCenter()
        {
            return Mathf.Sqrt(position.x * position.x + position.z * position.z);
        }

        /// <summary>
        /// Check if this ball is touching another ball
        /// </summary>
        public bool IsTouching(BallData other)
        {
            return DistanceTo(other) <= GameConstants.TOUCHING_DISTANCE;
        }

        /// <summary>
        /// Check if this ball is inside the center ring
        /// </summary>
        public bool IsInCenterRing()
        {
            return DistanceFromCenter() <= GameConstants.CENTER_RING_RADIUS;
        }
    }
}

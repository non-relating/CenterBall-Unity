using UnityEngine;
using CenterBall.Data;

namespace CenterBall.Physics
{
    /// <summary>
    /// Simplified arcade physics engine for CenterBall
    /// Ported from src/utils/physics.ts
    ///
    /// IMPORTANT: This is a SIMPLIFIED ARCADE PHYSICS model, not a realistic simulation.
    /// The game uses simplified trajectory calculations for predictable, fun gameplay.
    ///
    /// Physics Model:
    /// - No ball-to-ball collisions (balls pass through each other)
    /// - No ball rotation or spin
    /// - Linear trajectory with simple multiplier (not realistic physics)
    /// - Boundary bouncing with energy loss
    /// - Friction simulation for gradual stopping
    /// </summary>
    public static class ArcadePhysics
    {
        /// <summary>
        /// Calculate the final position of a ball after a shot with boundary bouncing.
        ///
        /// SIMPLIFIED PHYSICS with BOUNCING: This uses a basic linear trajectory with bounce physics.
        /// - Ball bounces off table edges with energy loss
        /// - Multiple bounces handled until ball settles
        /// - Stays within table boundaries
        /// </summary>
        /// <param name="ballPosition">Current ball position</param>
        /// <param name="angle">Angle in degrees (0-360)</param>
        /// <param name="power">Power percentage (0-100)</param>
        /// <param name="config">Physics configuration (optional, uses default if null)</param>
        /// <returns>Final ball position after trajectory</returns>
        public static Vector3 CalculateBallTrajectory(
            Vector3 ballPosition,
            float angle,
            float power,
            PhysicsConfig config = null)
        {
            // Use default values if no config provided
            float bounceCoefficient = config != null ? config.arcadeBounceCoefficient : 0.7f;
            float forceMultiplier = config != null ? config.arcadeForceMultiplier : 0.15f;
            float movementMultiplier = config != null ? config.arcadeMovementMultiplier : 0.5f;
            int maxBounces = config != null ? config.arcadeMaxBounces : 5;
            bool debugLogs = config != null && config.enableDebugLogs;

            // Convert angle from degrees to radians
            float radians = angle * Mathf.Deg2Rad;

            // Calculate force from power (0-100 -> 0-MAX_FORCE)
            float force = power * forceMultiplier;

            // Calculate initial velocity components
            // In Unity's coordinate system:
            // - X axis: left (-) to right (+)
            // - Z axis: back (-) to front (+)
            float velocityX = Mathf.Sin(radians) * force * movementMultiplier;
            float velocityZ = Mathf.Cos(radians) * force * movementMultiplier;

            // Starting position
            float currentX = ballPosition.x;
            float currentZ = ballPosition.z;

            // Simulate ball movement with bouncing
            int bounceCount = 0;

            while (bounceCount < maxBounces && (Mathf.Abs(velocityX) > 0.5f || Mathf.Abs(velocityZ) > 0.5f))
            {
                // Calculate next position
                float nextX = currentX + velocityX;
                float nextZ = currentZ + velocityZ;

                // Check X boundaries and bounce
                if (nextX < GameConstants.BOUNDARY_X_MIN)
                {
                    nextX = GameConstants.BOUNDARY_X_MIN;
                    velocityX = -velocityX * bounceCoefficient; // Reverse and reduce velocity
                    bounceCount++;
                    if (debugLogs) Debug.Log($"Bounce off left wall. VelocityX: {velocityX}");
                }
                else if (nextX > GameConstants.BOUNDARY_X_MAX)
                {
                    nextX = GameConstants.BOUNDARY_X_MAX;
                    velocityX = -velocityX * bounceCoefficient;
                    bounceCount++;
                    if (debugLogs) Debug.Log($"Bounce off right wall. VelocityX: {velocityX}");
                }

                // Check Z boundaries and bounce
                if (nextZ < GameConstants.BOUNDARY_Z_MIN)
                {
                    nextZ = GameConstants.BOUNDARY_Z_MIN;
                    velocityZ = -velocityZ * bounceCoefficient; // Reverse and reduce velocity
                    bounceCount++;
                    if (debugLogs) Debug.Log($"Bounce off back wall. VelocityZ: {velocityZ}");
                }
                else if (nextZ > GameConstants.BOUNDARY_Z_MAX)
                {
                    nextZ = GameConstants.BOUNDARY_Z_MAX;
                    velocityZ = -velocityZ * bounceCoefficient;
                    bounceCount++;
                    if (debugLogs) Debug.Log($"Bounce off front wall. VelocityZ: {velocityZ}");
                }

                // Update current position
                currentX = nextX;
                currentZ = nextZ;

                // Apply friction/energy loss (0.95 from web version)
                velocityX *= 0.95f;
                velocityZ *= 0.95f;
            }

            // Final position clamped to ensure it's within bounds
            float finalX = Mathf.Clamp(currentX, GameConstants.BOUNDARY_X_MIN, GameConstants.BOUNDARY_X_MAX);
            float finalZ = Mathf.Clamp(currentZ, GameConstants.BOUNDARY_Z_MIN, GameConstants.BOUNDARY_Z_MAX);

            if (debugLogs)
            {
                Debug.Log($"Final position: ({finalX}, {ballPosition.y}, {finalZ}). Bounces: {bounceCount}");
            }

            return new Vector3(finalX, ballPosition.y, finalZ);
        }

        /// <summary>
        /// Calculate trajectory preview points for visualization
        /// </summary>
        /// <param name="ballPosition">Starting ball position</param>
        /// <param name="angle">Angle in degrees</param>
        /// <param name="power">Power percentage</param>
        /// <param name="config">Physics configuration</param>
        /// <param name="pointCount">Number of preview points</param>
        /// <returns>Array of positions along trajectory</returns>
        public static Vector3[] CalculateTrajectoryPreview(
            Vector3 ballPosition,
            float angle,
            float power,
            PhysicsConfig config,
            int pointCount = 50)
        {
            Vector3[] points = new Vector3[pointCount + 1];
            points[0] = ballPosition;

            // Use same logic as main trajectory calculation but step through it
            float bounceCoefficient = config.arcadeBounceCoefficient;
            float forceMultiplier = config.arcadeForceMultiplier;
            float movementMultiplier = config.arcadeMovementMultiplier;

            float radians = angle * Mathf.Deg2Rad;
            float force = power * forceMultiplier;

            float velocityX = Mathf.Sin(radians) * force * movementMultiplier;
            float velocityZ = Mathf.Cos(radians) * force * movementMultiplier;

            float currentX = ballPosition.x;
            float currentZ = ballPosition.z;

            for (int i = 1; i <= pointCount; i++)
            {
                float nextX = currentX + velocityX / pointCount;
                float nextZ = currentZ + velocityZ / pointCount;

                // Check boundaries
                if (nextX < GameConstants.BOUNDARY_X_MIN || nextX > GameConstants.BOUNDARY_X_MAX)
                {
                    nextX = Mathf.Clamp(nextX, GameConstants.BOUNDARY_X_MIN, GameConstants.BOUNDARY_X_MAX);
                    velocityX = -velocityX * bounceCoefficient;
                }

                if (nextZ < GameConstants.BOUNDARY_Z_MIN || nextZ > GameConstants.BOUNDARY_Z_MAX)
                {
                    nextZ = Mathf.Clamp(nextZ, GameConstants.BOUNDARY_Z_MIN, GameConstants.BOUNDARY_Z_MAX);
                    velocityZ = -velocityZ * bounceCoefficient;
                }

                currentX = nextX;
                currentZ = nextZ;

                velocityX *= 0.95f;
                velocityZ *= 0.95f;

                points[i] = new Vector3(currentX, ballPosition.y, currentZ);
            }

            return points;
        }

        /// <summary>
        /// Check if a position is within table boundaries
        /// </summary>
        public static bool IsWithinBounds(Vector3 position)
        {
            return position.x >= GameConstants.BOUNDARY_X_MIN &&
                   position.x <= GameConstants.BOUNDARY_X_MAX &&
                   position.z >= GameConstants.BOUNDARY_Z_MIN &&
                   position.z <= GameConstants.BOUNDARY_Z_MAX;
        }

        /// <summary>
        /// Clamp a position to stay within table boundaries
        /// </summary>
        public static Vector3 ClampToBoundary(Vector3 position)
        {
            return new Vector3(
                Mathf.Clamp(position.x, GameConstants.BOUNDARY_X_MIN, GameConstants.BOUNDARY_X_MAX),
                position.y,
                Mathf.Clamp(position.z, GameConstants.BOUNDARY_Z_MIN, GameConstants.BOUNDARY_Z_MAX)
            );
        }

        /// <summary>
        /// Calculate distance between two balls (2D distance in X-Z plane)
        /// </summary>
        public static float DistanceBetweenBalls(Vector3 ball1, Vector3 ball2)
        {
            float dx = ball2.x - ball1.x;
            float dz = ball2.z - ball1.z;
            return Mathf.Sqrt(dx * dx + dz * dz);
        }

        /// <summary>
        /// Calculate distance from a ball to the center (0, 0)
        /// </summary>
        public static float DistanceFromCenter(Vector3 ball)
        {
            return Mathf.Sqrt(ball.x * ball.x + ball.z * ball.z);
        }

        /// <summary>
        /// Check if a ball is in the center ring
        /// </summary>
        public static bool IsBallInCenterRing(Vector3 ball)
        {
            return DistanceFromCenter(ball) <= GameConstants.CENTER_RING_RADIUS;
        }

        /// <summary>
        /// Check if a ball is touching the center ball
        /// </summary>
        public static bool IsBallTouchingCenter(Vector3 ball, Vector3 centerBall)
        {
            return DistanceBetweenBalls(ball, centerBall) <= GameConstants.TOUCHING_DISTANCE;
        }

        /// <summary>
        /// Check if two balls are colliding (for future use)
        /// NOTE: Currently not used in simplified physics
        /// </summary>
        public static bool CheckBallCollision(Vector3 ball1, Vector3 ball2)
        {
            float distance = DistanceBetweenBalls(ball1, ball2);
            float collisionDistance = GameConstants.PLAYER_BALL_RADIUS * 2;
            return distance < collisionDistance;
        }
    }
}

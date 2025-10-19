using UnityEngine;

namespace CenterBall.Data
{
    /// <summary>
    /// Physics configuration settings - editable in Unity Inspector
    /// Supports both Arcade and Realistic physics modes
    /// </summary>
    [CreateAssetMenu(fileName = "PhysicsConfig", menuName = "CenterBall/Physics Configuration")]
    public class PhysicsConfig : ScriptableObject
    {
        [Header("Physics Mode")]
        [Tooltip("Select physics simulation mode")]
        public PhysicsMode mode = PhysicsMode.Arcade;

        [Header("Arcade Physics Settings")]
        [Tooltip("Energy retention when bouncing off walls (0-1)")]
        [Range(0f, 1f)]
        public float arcadeBounceCoefficient = 0.7f;

        [Tooltip("Force multiplier for ball shooting (affects distance)")]
        [Range(0.01f, 1f)]
        public float arcadeForceMultiplier = 0.15f;

        [Tooltip("Movement calculation multiplier")]
        [Range(0.1f, 2f)]
        public float arcadeMovementMultiplier = 0.5f;

        [Tooltip("Maximum number of bounces to simulate")]
        [Range(1, 10)]
        public int arcadeMaxBounces = 5;

        [Header("Realistic Physics Settings")]
        [Tooltip("Friction applied each timestep (0-1, closer to 1 = less friction)")]
        [Range(0.9f, 0.999f)]
        public float realisticFriction = 0.98f;

        [Tooltip("Energy retention on wall collision (0-1)")]
        [Range(0f, 1f)]
        public float realisticBounceCoefficient = 0.85f;

        [Tooltip("Force multiplier for ball shooting")]
        [Range(0.01f, 1f)]
        public float realisticForceMultiplier = 0.20f;

        [Tooltip("Number of timesteps to simulate per frame")]
        [Range(10, 500)]
        public int realisticMaxIterations = 300;

        [Tooltip("Delta time per simulation step (seconds)")]
        [Range(0.001f, 0.05f)]
        public float realisticDeltaTime = 0.0167f; // ~60 FPS

        [Tooltip("Enable ball-to-ball collision detection")]
        public bool realisticEnableBallCollisions = false;

        [Tooltip("Minimum velocity before stopping simulation (performance)")]
        [Range(0.001f, 0.1f)]
        public float realisticMinVelocity = 0.01f;

        [Header("Common Settings")]
        [Tooltip("Speed of ball movement animation (visual only)")]
        [Range(0.1f, 5f)]
        public float ballAnimationSpeed = 1f;

        [Tooltip("Smoothing time for ball movement")]
        [Range(0.01f, 0.5f)]
        public float ballSmoothTime = 0.1f;

        [Header("Debug")]
        [Tooltip("Show trajectory preview in editor")]
        public bool showTrajectoryPreview = true;

        [Tooltip("Number of points in trajectory preview")]
        [Range(10, 100)]
        public int trajectoryPreviewPoints = 50;

        [Tooltip("Enable physics debug logging")]
        public bool enableDebugLogs = false;

        /// <summary>
        /// Reset to default values
        /// </summary>
        public void ResetToDefaults()
        {
            mode = PhysicsMode.Arcade;

            // Arcade defaults
            arcadeBounceCoefficient = 0.7f;
            arcadeForceMultiplier = 0.15f;
            arcadeMovementMultiplier = 0.5f;
            arcadeMaxBounces = 5;

            // Realistic defaults
            realisticFriction = 0.98f;
            realisticBounceCoefficient = 0.85f;
            realisticForceMultiplier = 0.20f;
            realisticMaxIterations = 300;
            realisticDeltaTime = 0.0167f;
            realisticEnableBallCollisions = false;
            realisticMinVelocity = 0.01f;

            // Common
            ballAnimationSpeed = 1f;
            ballSmoothTime = 0.1f;

            // Debug
            showTrajectoryPreview = true;
            trajectoryPreviewPoints = 50;
            enableDebugLogs = false;
        }
    }

    public enum PhysicsMode
    {
        Arcade,
        Realistic
    }
}

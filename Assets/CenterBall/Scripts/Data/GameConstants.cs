using UnityEngine;

namespace CenterBall.Data
{
    /// <summary>
    /// Core game constants ported from src/constants/game.ts
    /// Defines rules, physics parameters, and gameplay configuration
    /// </summary>
    public static class GameConstants
    {
        // Game Rules
        public const int TARGET_SCORE_STANDARD = 21;
        public const int TARGET_SCORE_QUICK = 11;
        public const int BALLS_PER_PLAYER = 5;
        public const int MAX_ROUNDS = 50; // Safety limit

        // Table Dimensions (in Unity units)
        public const float TABLE_WIDTH = 400f;
        public const float TABLE_LENGTH = 600f;
        public const float TABLE_HEIGHT = 0.5f;

        // Playable Area Boundaries
        public const float BOUNDARY_X_MIN = -180f;
        public const float BOUNDARY_X_MAX = 180f;
        public const float BOUNDARY_Z_MIN = -280f;
        public const float BOUNDARY_Z_MAX = 280f;

        // Ball Properties
        public const float CENTER_BALL_RADIUS = 12f;
        public const float PLAYER_BALL_RADIUS = 18f;
        public const float BALL_Y_POSITION = 0.4f; // Height above table

        // Scoring Zones
        public const float CENTER_RING_RADIUS = 2.5f;
        public const float TOUCHING_DISTANCE = 0.8f; // 2 * ball radius approximation

        // Scoring Points
        public const int POINTS_TOUCHING_IN_RING = 3;
        public const int POINTS_IN_RING = 2;
        public const int POINTS_CLOSEST = 1;

        // Starting Positions
        public const float PLAYER1_START_Z = 150f;
        public const float PLAYER2_START_Z = -150f;
        public const float BARRIER_OFFSET = 50f;

        // Player Barrier Positions
        public static readonly Vector3 PLAYER1_BARRIER_POSITION = new Vector3(0, 0, PLAYER1_START_Z + BARRIER_OFFSET);
        public static readonly Vector3 PLAYER2_BARRIER_POSITION = new Vector3(0, 0, PLAYER2_START_Z - BARRIER_OFFSET);

        // Game State Constants
        public const string STATE_SETUP = "setup";
        public const string STATE_PLAYING = "playing";
        public const string STATE_FINISHED = "finished";

        // Turn System
        public const int PLAYER_1 = 1;
        public const int PLAYER_2 = 2;
    }
}

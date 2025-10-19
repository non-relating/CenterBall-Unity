using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using CenterBall.Data;
using CenterBall.Physics;

namespace CenterBall.Core
{
    /// <summary>
    /// Score calculation logic ported from src/utils/physics.ts
    ///
    /// Scoring Rules:
    /// - 3 points: Ball touching center ball AND both balls in center ring
    /// - 2 points: Ball in center ring but NOT touching center ball
    /// - 1 point: Closest ball to center ball (outside the ring)
    /// </summary>
    public static class ScoreCalculator
    {
        private class BallWithMetrics
        {
            public BallData ball;
            public float distanceToCenter;
            public float distanceFromCenter;
            public int player;
        }

        public struct ScoreResult
        {
            public int player1;
            public int player2;

            public ScoreResult(int p1, int p2)
            {
                player1 = p1;
                player2 = p2;
            }
        }

        /// <summary>
        /// Calculate scores for both players based on ball positions
        /// </summary>
        /// <param name="gameState">Current game state with all ball positions</param>
        /// <returns>Score result with player1 and player2 scores</returns>
        public static ScoreResult CalculateScore(GameState gameState)
        {
            if (gameState == null)
            {
                return new ScoreResult(0, 0);
            }

            int player1Score = 0;
            int player2Score = 0;

            var centerBall = gameState.centerBall;

            // Check if center ball is in the ring
            bool centerBallInRing = ArcadePhysics.DistanceFromCenter(centerBall.position) <= GameConstants.CENTER_RING_RADIUS;

            // Helper to get ball data with calculated distances
            var getBallData = new System.Func<List<BallData>, int, List<BallWithMetrics>>((balls, player) =>
            {
                return balls
                    .Where(b => b.active)
                    .Select(ball => new BallWithMetrics
                    {
                        ball = ball,
                        distanceToCenter = ArcadePhysics.DistanceBetweenBalls(ball.position, centerBall.position),
                        distanceFromCenter = ArcadePhysics.DistanceFromCenter(ball.position),
                        player = player
                    })
                    .ToList();
            });

            var player1Balls = getBallData(gameState.player1Balls, GameConstants.PLAYER_1);
            var player2Balls = getBallData(gameState.player2Balls, GameConstants.PLAYER_2);
            var allBalls = new List<BallWithMetrics>();
            allBalls.AddRange(player1Balls);
            allBalls.AddRange(player2Balls);

            // 3 points: touching center ball AND both balls in center ring
            foreach (var ball in allBalls)
            {
                bool touchingCenter = ball.distanceToCenter <= GameConstants.TOUCHING_DISTANCE;
                bool ballInRing = ball.distanceFromCenter <= GameConstants.CENTER_RING_RADIUS;

                if (touchingCenter && ballInRing && centerBallInRing)
                {
                    if (ball.player == GameConstants.PLAYER_1)
                    {
                        player1Score += GameConstants.POINTS_TOUCHING_IN_RING;
                    }
                    else
                    {
                        player2Score += GameConstants.POINTS_TOUCHING_IN_RING;
                    }
                }
            }

            // 2 points: ball in center ring NOT touching center ball
            foreach (var ball in allBalls)
            {
                bool touchingCenter = ball.distanceToCenter <= GameConstants.TOUCHING_DISTANCE;
                bool ballInRing = ball.distanceFromCenter <= GameConstants.CENTER_RING_RADIUS;

                if (!touchingCenter && ballInRing)
                {
                    if (ball.player == GameConstants.PLAYER_1)
                    {
                        player1Score += GameConstants.POINTS_IN_RING;
                    }
                    else
                    {
                        player2Score += GameConstants.POINTS_IN_RING;
                    }
                }
            }

            // 1 point: closest ball outside the ring
            var ballsOutsideRing = allBalls
                .Where(ball => ball.distanceFromCenter > GameConstants.CENTER_RING_RADIUS)
                .ToList();

            if (ballsOutsideRing.Count > 0)
            {
                var closest = ballsOutsideRing
                    .OrderBy(ball => ball.distanceToCenter)
                    .First();

                if (closest.player == GameConstants.PLAYER_1)
                {
                    player1Score += GameConstants.POINTS_CLOSEST;
                }
                else
                {
                    player2Score += GameConstants.POINTS_CLOSEST;
                }
            }

            return new ScoreResult(player1Score, player2Score);
        }

        /// <summary>
        /// Calculate detailed scoring breakdown for UI display
        /// </summary>
        public static string GetScoreBreakdown(GameState gameState)
        {
            if (gameState == null) return "No game state";

            var result = CalculateScore(gameState);
            var breakdown = $"Player 1: {result.player1} points\nPlayer 2: {result.player2} points\n\n";

            var centerBall = gameState.centerBall;
            bool centerBallInRing = ArcadePhysics.DistanceFromCenter(centerBall.position) <= GameConstants.CENTER_RING_RADIUS;

            breakdown += $"Center ball in ring: {centerBallInRing}\n\n";

            // Analyze each player's balls
            foreach (var ball in gameState.player1Balls.Where(b => b.active))
            {
                float distToCenter = ArcadePhysics.DistanceBetweenBalls(ball.position, centerBall.position);
                float distFromCenter = ArcadePhysics.DistanceFromCenter(ball.position);
                bool touching = distToCenter <= GameConstants.TOUCHING_DISTANCE;
                bool inRing = distFromCenter <= GameConstants.CENTER_RING_RADIUS;

                breakdown += $"P1 {ball.id}: Dist={distFromCenter:F2}, Touch={touching}, InRing={inRing}\n";
            }

            foreach (var ball in gameState.player2Balls.Where(b => b.active))
            {
                float distToCenter = ArcadePhysics.DistanceBetweenBalls(ball.position, centerBall.position);
                float distFromCenter = ArcadePhysics.DistanceFromCenter(ball.position);
                bool touching = distToCenter <= GameConstants.TOUCHING_DISTANCE;
                bool inRing = distFromCenter <= GameConstants.CENTER_RING_RADIUS;

                breakdown += $"P2 {ball.id}: Dist={distFromCenter:F2}, Touch={touching}, InRing={inRing}\n";
            }

            return breakdown;
        }
    }
}

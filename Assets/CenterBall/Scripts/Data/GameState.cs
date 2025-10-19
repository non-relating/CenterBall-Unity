using System;
using System.Collections.Generic;
using UnityEngine;

namespace CenterBall.Data
{
    /// <summary>
    /// Complete game state matching the web version structure
    /// Ported from src/entities/index.js and src/Pages/Game.jsx
    /// </summary>
    [Serializable]
    public class GameState
    {
        public string id;
        public string player1Name;
        public string player2Name;
        public int player1Score;
        public int player2Score;
        public int targetScore;
        public int currentTurn; // 1 or 2
        public string gameStatus; // "setup", "playing", "finished"
        public int roundNumber;
        public string winner;

        // Ball positions
        public BallData centerBall;
        public List<BallData> player1Balls;
        public List<BallData> player2Balls;

        public GameState()
        {
            id = GenerateGameId();
            player1Name = "Player 1";
            player2Name = "Player 2";
            player1Score = 0;
            player2Score = 0;
            targetScore = GameConstants.TARGET_SCORE_STANDARD;
            currentTurn = GameConstants.PLAYER_1;
            gameStatus = GameConstants.STATE_SETUP;
            roundNumber = 1;
            winner = null;

            InitializeBalls();
        }

        public GameState(string p1Name, string p2Name, int target)
        {
            id = GenerateGameId();
            player1Name = p1Name;
            player2Name = p2Name;
            player1Score = 0;
            player2Score = 0;
            targetScore = target;
            currentTurn = GameConstants.PLAYER_1;
            gameStatus = GameConstants.STATE_PLAYING;
            roundNumber = 1;
            winner = null;

            InitializeBalls();
        }

        private void InitializeBalls()
        {
            // Center ball at origin
            centerBall = new BallData(
                "center",
                new Vector3(0, GameConstants.BALL_Y_POSITION, 0),
                0
            );

            // Player 1 balls (red)
            player1Balls = new List<BallData>();
            for (int i = 0; i < GameConstants.BALLS_PER_PLAYER; i++)
            {
                float xOffset = (i - 2) * (GameConstants.PLAYER_BALL_RADIUS * 3);
                player1Balls.Add(new BallData(
                    $"p1_ball_{i}",
                    new Vector3(xOffset, GameConstants.BALL_Y_POSITION, GameConstants.PLAYER1_START_Z),
                    GameConstants.PLAYER_1
                ));
            }

            // Player 2 balls (blue)
            player2Balls = new List<BallData>();
            for (int i = 0; i < GameConstants.BALLS_PER_PLAYER; i++)
            {
                float xOffset = (i - 2) * (GameConstants.PLAYER_BALL_RADIUS * 3);
                player2Balls.Add(new BallData(
                    $"p2_ball_{i}",
                    new Vector3(xOffset, GameConstants.BALL_Y_POSITION, GameConstants.PLAYER2_START_Z),
                    GameConstants.PLAYER_2
                ));
            }
        }

        /// <summary>
        /// Get all balls for a specific player
        /// </summary>
        public List<BallData> GetPlayerBalls(int playerId)
        {
            return playerId == GameConstants.PLAYER_1 ? player1Balls : player2Balls;
        }

        /// <summary>
        /// Get the current player's balls
        /// </summary>
        public List<BallData> GetCurrentPlayerBalls()
        {
            return GetPlayerBalls(currentTurn);
        }

        /// <summary>
        /// Get the current player's name
        /// </summary>
        public string GetCurrentPlayerName()
        {
            return currentTurn == GameConstants.PLAYER_1 ? player1Name : player2Name;
        }

        /// <summary>
        /// Switch to the other player
        /// </summary>
        public void SwitchTurn()
        {
            currentTurn = currentTurn == GameConstants.PLAYER_1
                ? GameConstants.PLAYER_2
                : GameConstants.PLAYER_1;
        }

        /// <summary>
        /// Check if all balls have been played
        /// </summary>
        public bool AllBallsPlayed()
        {
            foreach (var ball in player1Balls)
            {
                if (!ball.active) return false;
            }
            foreach (var ball in player2Balls)
            {
                if (!ball.active) return false;
            }
            return true;
        }

        /// <summary>
        /// Reset all balls for a new round
        /// </summary>
        public void ResetBallsForNewRound()
        {
            // Reset center ball
            centerBall.position = new Vector3(0, GameConstants.BALL_Y_POSITION, 0);

            // Reset player 1 balls
            for (int i = 0; i < player1Balls.Count; i++)
            {
                float xOffset = (i - 2) * (GameConstants.PLAYER_BALL_RADIUS * 3);
                player1Balls[i].position = new Vector3(xOffset, GameConstants.BALL_Y_POSITION, GameConstants.PLAYER1_START_Z);
                player1Balls[i].active = false;
            }

            // Reset player 2 balls
            for (int i = 0; i < player2Balls.Count; i++)
            {
                float xOffset = (i - 2) * (GameConstants.PLAYER_BALL_RADIUS * 3);
                player2Balls[i].position = new Vector3(xOffset, GameConstants.BALL_Y_POSITION, GameConstants.PLAYER2_START_Z);
                player2Balls[i].active = false;
            }
        }

        /// <summary>
        /// Check if game is over
        /// </summary>
        public bool IsGameOver()
        {
            return player1Score >= targetScore || player2Score >= targetScore;
        }

        /// <summary>
        /// Determine the winner
        /// </summary>
        public void DetermineWinner()
        {
            if (player1Score >= targetScore)
            {
                winner = player1Name;
                gameStatus = GameConstants.STATE_FINISHED;
            }
            else if (player2Score >= targetScore)
            {
                winner = player2Name;
                gameStatus = GameConstants.STATE_FINISHED;
            }
        }

        /// <summary>
        /// Generate a unique game ID (simple implementation)
        /// </summary>
        private string GenerateGameId()
        {
            return $"game_{DateTime.Now.Ticks}_{UnityEngine.Random.Range(1000, 9999)}";
        }

        /// <summary>
        /// Create a deep copy of this game state
        /// </summary>
        public GameState Clone()
        {
            var clone = new GameState
            {
                id = this.id,
                player1Name = this.player1Name,
                player2Name = this.player2Name,
                player1Score = this.player1Score,
                player2Score = this.player2Score,
                targetScore = this.targetScore,
                currentTurn = this.currentTurn,
                gameStatus = this.gameStatus,
                roundNumber = this.roundNumber,
                winner = this.winner,
                centerBall = new BallData(this.centerBall),
                player1Balls = new List<BallData>(),
                player2Balls = new List<BallData>()
            };

            foreach (var ball in this.player1Balls)
            {
                clone.player1Balls.Add(new BallData(ball));
            }

            foreach (var ball in this.player2Balls)
            {
                clone.player2Balls.Add(new BallData(ball));
            }

            return clone;
        }
    }
}

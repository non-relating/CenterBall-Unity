using UnityEngine;
using CenterBall.Data;
using CenterBall.Core;

namespace CenterBall.Managers
{
    /// <summary>
    /// Main game manager - Singleton pattern
    /// Handles overall game flow, state management, and coordination between systems
    /// Ported from src/Pages/Game.jsx logic
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<GameManager>();
                    if (_instance == null)
                    {
                        GameObject go = new GameObject("GameManager");
                        _instance = go.AddComponent<GameManager>();
                    }
                }
                return _instance;
            }
        }

        [Header("Game Configuration")]
        [SerializeField] private PhysicsConfig physicsConfig;

        [Header("Current Game State")]
        [SerializeField] private GameState currentGame;

        // Events
        public System.Action<GameState> OnGameStateChanged;
        public System.Action<int, int> OnScoreUpdated;
        public System.Action<int> OnTurnChanged;
        public System.Action<int> OnRoundCompleted;
        public System.Action<string> OnGameFinished;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);

            // Create default physics config if none assigned
            if (physicsConfig == null)
            {
                physicsConfig = ScriptableObject.CreateInstance<PhysicsConfig>();
                physicsConfig.ResetToDefaults();
            }
        }

        /// <summary>
        /// Start a new game
        /// </summary>
        public void StartNewGame(string player1Name, string player2Name, int targetScore)
        {
            currentGame = new GameState(player1Name, player2Name, targetScore);
            OnGameStateChanged?.Invoke(currentGame);
            Debug.Log($"New game started: {player1Name} vs {player2Name}, target: {targetScore}");
        }

        /// <summary>
        /// Shoot a ball
        /// </summary>
        public void ShootBall(BallData ball, float angle, float power)
        {
            if (currentGame == null || currentGame.gameStatus != GameConstants.STATE_PLAYING)
            {
                Debug.LogWarning("Cannot shoot ball - game not in playing state");
                return;
            }

            // Calculate trajectory using physics system
            Vector3 finalPosition = CenterBall.Physics.ArcadePhysics.CalculateBallTrajectory(
                ball.position,
                angle,
                power,
                physicsConfig
            );

            // Update ball position and mark as active
            ball.position = finalPosition;
            ball.active = true;

            Debug.Log($"Ball {ball.id} shot to position {finalPosition}");

            // Check if all balls played - if so, calculate round score
            if (currentGame.AllBallsPlayed())
            {
                CompleteRound();
            }
            else
            {
                // Switch turn
                currentGame.SwitchTurn();
                OnTurnChanged?.Invoke(currentGame.currentTurn);
            }

            OnGameStateChanged?.Invoke(currentGame);
        }

        /// <summary>
        /// Complete the current round and calculate scores
        /// </summary>
        private void CompleteRound()
        {
            var scores = ScoreCalculator.CalculateScore(currentGame);

            currentGame.player1Score += scores.player1;
            currentGame.player2Score += scores.player2;

            Debug.Log($"Round {currentGame.roundNumber} complete. Scores: P1={scores.player1}, P2={scores.player2}");
            Debug.Log($"Total scores: P1={currentGame.player1Score}, P2={currentGame.player2Score}");

            OnScoreUpdated?.Invoke(currentGame.player1Score, currentGame.player2Score);
            OnRoundCompleted?.Invoke(currentGame.roundNumber);

            // Check for winner
            if (currentGame.IsGameOver())
            {
                currentGame.DetermineWinner();
                OnGameFinished?.Invoke(currentGame.winner);
                Debug.Log($"Game finished! Winner: {currentGame.winner}");
            }
            else
            {
                // Start next round
                currentGame.roundNumber++;
                currentGame.ResetBallsForNewRound();
                currentGame.currentTurn = GameConstants.PLAYER_1;
                OnGameStateChanged?.Invoke(currentGame);
            }
        }

        /// <summary>
        /// Get current game state
        /// </summary>
        public GameState GetCurrentGame()
        {
            return currentGame;
        }

        /// <summary>
        /// Get physics configuration
        /// </summary>
        public PhysicsConfig GetPhysicsConfig()
        {
            return physicsConfig;
        }

        /// <summary>
        /// Reset current game
        /// </summary>
        public void ResetGame()
        {
            if (currentGame != null)
            {
                currentGame.roundNumber = 1;
                currentGame.player1Score = 0;
                currentGame.player2Score = 0;
                currentGame.currentTurn = GameConstants.PLAYER_1;
                currentGame.gameStatus = GameConstants.STATE_PLAYING;
                currentGame.winner = null;
                currentGame.ResetBallsForNewRound();
                OnGameStateChanged?.Invoke(currentGame);
            }
        }

        /// <summary>
        /// End current game
        /// </summary>
        public void EndGame()
        {
            if (currentGame != null)
            {
                currentGame.gameStatus = GameConstants.STATE_FINISHED;
                OnGameStateChanged?.Invoke(currentGame);
            }
        }
    }
}

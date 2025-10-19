using UnityEngine;

namespace CenterBall.Data
{
    /// <summary>
    /// Visual and rendering constants ported from src/constants/visual.ts
    /// Defines camera, lighting, and material properties
    /// </summary>
    public static class VisualConstants
    {
        // Camera Settings - Desktop
        public const float CAMERA_FOV_DESKTOP = 75f;
        public const float CAMERA_DISTANCE_DESKTOP = 450f;
        public const float CAMERA_HEIGHT = 300f;

        // Camera Settings - Mobile
        public const float CAMERA_FOV_MOBILE = 85f;
        public const float CAMERA_DISTANCE_MOBILE = 500f;
        public const int MOBILE_BREAKPOINT = 640;

        // Lighting
        public const float AMBIENT_LIGHT_INTENSITY = 0.5f;
        public const float SPOTLIGHT_INTENSITY = 1.5f;
        public const float SPOTLIGHT_ANGLE = 45f; // degrees
        public const int SHADOW_MAP_SIZE = 1024;

        // Rendering
        public const float TONE_MAPPING_EXPOSURE = 1.2f;
        public const int MAX_PIXEL_RATIO = 2;

        // Ball Materials
        public const float BALL_METALNESS = 0.3f;
        public const float BALL_SMOOTHNESS = 0.85f; // Unity uses smoothness instead of roughness
        public const float BALL_CLEARCOAT = 0.9f;
        public const float BALL_OPACITY = 0.95f;

        // Center Ball Properties
        public const float CENTER_BALL_METALNESS = 0.4f;
        public const float CENTER_BALL_SMOOTHNESS = 0.8f;
        public const float CENTER_BALL_CLEARCOAT = 1.0f;

        // Center Ring Properties
        public const float CENTER_RING_INNER_RADIUS = 48f;
        public const float CENTER_RING_OUTER_RADIUS = 52f;
        public const float CENTER_RING_HEIGHT = 0.1f;
        public static readonly Color CENTER_RING_COLOR = new Color(0f, 1f, 1f, 0.8f); // Cyan with alpha

        // Player Colors
        public static readonly Color PLAYER1_COLOR = new Color(1f, 0.2f, 0.2f); // Red
        public static readonly Color PLAYER2_COLOR = new Color(0.2f, 0.4f, 1f); // Blue
        public static readonly Color CENTER_BALL_COLOR = Color.white;

        // Barrier Properties
        public const float BARRIER_WIDTH = 300f;
        public const float BARRIER_HEIGHT = 50f;
        public const float BARRIER_THICKNESS = 5f;
        public static readonly Color PLAYER1_BARRIER_COLOR = new Color(1f, 0.3f, 0.3f, 0.6f); // Semi-transparent red
        public static readonly Color PLAYER2_BARRIER_COLOR = new Color(0.3f, 0.5f, 1f, 0.6f); // Semi-transparent blue

        // Table Properties
        public static readonly Color TABLE_BORDER_COLOR = new Color(0.4f, 0.25f, 0.15f); // Brown

        // UI Colors (Glass-morphic theme)
        public static readonly Color UI_GLASS_BACKGROUND = new Color(0.1f, 0.1f, 0.15f, 0.7f);
        public static readonly Color UI_NEON_CYAN = new Color(0f, 1f, 1f);
        public static readonly Color UI_NEON_MAGENTA = new Color(1f, 0f, 1f);
        public static readonly Color UI_NEON_YELLOW = new Color(1f, 1f, 0f);

        // Animation
        public const float UI_TRANSITION_SPEED = 0.3f;
        public const float BALL_MOVEMENT_SMOOTH_TIME = 0.1f;

        // Effects
        public const float BLOOM_INTENSITY = 0.5f;
        public const float GLOW_INTENSITY = 1.5f;
    }
}

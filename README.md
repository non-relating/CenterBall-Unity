# CenterBall Unity

Unity 6.2 port of the CenterBall 3D precision ball game with full feature parity and multi-platform support.

## About

This is a comprehensive Unity implementation of CenterBall, originally built with React + Three.js for the web. The Unity version maintains complete feature parity while adding native platform support for mobile and desktop.

**Features:**
- 🎮 Full 3D physics-based gameplay
- 📱 Multi-platform support (WebGL, iOS, Android, Windows, Mac, Linux)
- 🎨 Modern UI with glass-morphic design (UI Toolkit)
- 🎵 Complete audio system with sound effects and music
- 📊 Player statistics, achievements, and leaderboards
- ⚙️ Dual physics modes: Arcade (simplified) and Realistic (simulation)
- 🎓 Interactive 8-step tutorial system
- 🌈 6 preset color themes
- ♿ Accessibility features (reduced motion, high contrast, large text)

## Web Version

- **Original web version**: https://centerball.netlify.app
- **Web repository**: https://github.com/non-relating/CenterBall
- **Unity repository**: https://github.com/non-relating/CenterBall-Unity

## Project Structure

```
Assets/CenterBall/
├── Scripts/
│   ├── Core/              # Game logic (ScoreCalculator, etc.)
│   ├── Data/              # Data models and constants
│   ├── Managers/          # Game managers (GameManager, AudioManager, etc.)
│   ├── Physics/           # Physics systems (Arcade, Realistic)
│   ├── UI/                # UI controllers
│   └── Utilities/         # Helper functions
├── Prefabs/               # Ball prefabs, UI prefabs
├── Materials/             # PBR materials for balls and table
├── Textures/              # Ball textures and table texture (1.6MB total)
├── Scenes/                # Game scenes (Menu, Game, Settings, Leaderboard)
├── Audio/                 # Sound effects and music
├── UI/
│   ├── UXML/              # UI Toolkit structure files
│   ├── USS/               # UI Toolkit stylesheets
│   └── Runtime/           # Runtime UI components
└── Resources/             # Runtime loadable assets

Packages/
└── manifest.json          # Unity package dependencies

ProjectSettings/           # Unity project configuration
```

## Development Status

**Current State**: Foundation Complete (Phase 1-3 Partial)

- ✅ Unity 6.2 project structure
- ✅ Multi-platform build configuration (WebGL, iOS, Android, PC)
- ✅ Texture assets imported (1.6MB - balls + table)
- ✅ Core data models (GameConstants, GameState, BallData, PhysicsConfig)
- ✅ Complete arcade physics system (exact port from web version)
- ✅ Scoring calculator (3-tier scoring system)
- ✅ GameManager skeleton with event system
- 📋 ~72-92 C# scripts remaining (see DEVELOPMENT_ROADMAP.md)
- 📋 UI implementation (UI Toolkit UXML/USS)
- 📋 Audio system
- 📋 Input handling
- 📋 Statistics & achievements
- 📋 Visual effects and polish

**See [DEVELOPMENT_ROADMAP.md](DEVELOPMENT_ROADMAP.md) for complete implementation plan.**

## Technical Details

### Unity Version
**Unity 6.2 (6000.0.24f1)** - Latest Unity 6 release

### Dependencies
- Input System 1.11.2
- TextMeshPro 3.2.0-pre.8
- UI Toolkit (uGUI) 2.0.0
- Newtonsoft JSON 3.2.1
- Universal Render Pipeline 17.0.3

### Platform Targets
- **WebGL**: Browser-based, PWA-ready
- **iOS**: Native iOS app (Xcode 14+)
- **Android**: Native Android app (API 24+)
- **PC Standalone**: Windows, Mac, Linux

### Physics System
The game includes two physics modes, both fully editable in the Unity Inspector via `PhysicsConfig` ScriptableObject:

1. **Arcade Physics** (Default):
   - Simplified trajectory calculation
   - Boundary bouncing with energy loss
   - Predictable, fun gameplay
   - Configurable parameters: bounce coefficient, force multiplier, movement multiplier

2. **Realistic Physics** (Optional):
   - Full timestep simulation
   - Friction-based deceleration
   - Ball-to-ball collision detection (optional)
   - Energy loss on impacts

### Architecture
- **Singleton Pattern**: Core managers (GameManager, AudioManager, etc.)
- **Event-Driven**: C# events for loose coupling between systems
- **ScriptableObjects**: Configuration data (PhysicsConfig, AudioConfig, etc.)
- **UI Toolkit**: Modern, performance-focused UI with UXML/USS

## Development

### Requirements
- Unity 6.2 (6000.0.24f1) or later
- Unity Hub (recommended)
- For iOS builds: macOS with Xcode 14+
- For Android builds: Android SDK and NDK

### Opening the Project

1. Clone this repository:
   ```bash
   git clone https://github.com/non-relating/CenterBall-Unity.git
   cd CenterBall-Unity
   ```

2. Open Unity Hub

3. Click "Open" and select the `CenterBall-Unity` folder

4. Unity will import all packages and assets (may take a few minutes on first open)

### Building for Platforms

#### WebGL
1. File → Build Settings → WebGL
2. Click "Switch Platform"
3. Click "Build" and select output folder

#### iOS
1. File → Build Settings → iOS
2. Click "Switch Platform"
3. Click "Build" to export Xcode project
4. Open exported project in Xcode
5. Configure signing and build

#### Android
1. File → Build Settings → Android
2. Click "Switch Platform"
3. Click "Build" to create APK or AAB

#### PC Standalone
1. File → Build Settings → Windows/Mac/Linux
2. Click "Switch Platform"
3. Click "Build" to create executable

## Game Rules

### Objective
Two players compete to position their balls closest to a center ball to score points.

### Scoring System
- **3 points**: Ball touching center ball AND both balls in center ring (radius 2.5)
- **2 points**: Ball in center ring but NOT touching center ball
- **1 point**: Closest ball to center ball (outside the ring)

### Gameplay
- Each player has 5 balls
- Players alternate turns shooting one ball at a time
- Control angle (0-360°) and power (0-100%)
- After all balls are played, round scores are calculated
- First player to reach target score (11 or 21) wins

### Game Modes
- **Standard**: First to 21 points
- **Quick**: First to 11 points

## Key Files

### Core Systems (Completed)
- `Scripts/Data/GameConstants.cs` - Game rules and physics constants
- `Scripts/Data/VisualConstants.cs` - Camera, lighting, and rendering settings
- `Scripts/Data/GameState.cs` - Complete game state management
- `Scripts/Data/BallData.cs` - Ball position and state data
- `Scripts/Data/PhysicsConfig.cs` - Editable physics configuration (ScriptableObject)
- `Scripts/Physics/ArcadePhysics.cs` - Arcade physics engine
- `Scripts/Core/ScoreCalculator.cs` - Scoring logic
- `Scripts/Managers/GameManager.cs` - Main game manager

### Configuration Files
- `Packages/manifest.json` - Unity package dependencies
- `ProjectSettings/ProjectSettings.asset` - Build and platform settings

### Documentation
- `DEVELOPMENT_ROADMAP.md` - Complete development plan and progress tracking
- `README.md` - This file

## Contributing

This is a port of an existing web game. For consistency, new features should:
1. Match the web version behavior exactly (unless improving)
2. Use the established architecture patterns
3. Be fully cross-platform compatible
4. Include editable parameters in ScriptableObjects or Inspector

## Performance Targets

- **Desktop**: 60 FPS
- **Mobile**: 30-60 FPS (adaptive)
- **WebGL Build**: <50 MB compressed
- **Mobile Builds**: <100 MB

## License

MIT License - See LICENSE file for details

## Credits

- **Original Web Version**: CenterBall team (https://github.com/non-relating/CenterBall)
- **Unity Port**: In active development
- **Physics Engine**: Ported from web version with Unity adaptations
- **Textures**: Original assets from web version (optimized for Unity)

## Links

- **Web Version**: https://centerball.netlify.app
- **Web Repository**: https://github.com/non-relating/CenterBall
- **Unity Repository**: https://github.com/non-relating/CenterBall-Unity
- **Development Roadmap**: [DEVELOPMENT_ROADMAP.md](DEVELOPMENT_ROADMAP.md)

---

**Last Updated**: October 19, 2025
**Unity Version**: 6.2 (6000.0.24f1)
**Status**: Foundation Complete - Active Development

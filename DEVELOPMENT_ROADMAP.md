# CenterBall Unity - Development Roadmap

## Project Status: Foundation Complete (Phase 1-3 Partial)

This document outlines the complete development plan for converting the CenterBall web game to Unity 6.2.

---

## ✅ Completed (Current State)

### Phase 1: Unity Project Setup ✅
- [x] Created complete Unity 6.2 project structure
- [x] Set up folder hierarchy (Scripts, Prefabs, Materials, Textures, Scenes, Audio, UI, Resources)
- [x] Created Packages/manifest.json with all required dependencies:
  - Input System 1.11.2
  - TextMeshPro 3.2.0
  - UI Toolkit (uGUI) 2.0.0
  - Newtonsoft JSON 3.2.1
  - URP 17.0.3
- [x] Created ProjectSettings configuration files
- [x] Configured multi-platform build targets (WebGL, iOS, Android, PC)

### Phase 2: Asset Conversion ✅
- [x] Copied all texture assets from web version:
  - `blue-ball.png` (164KB) - Player 2 ball texture
  - `red-ball.png` (328KB) - Player 1 ball texture
  - `center-ball.png` (170KB) - Center ball texture
  - `table-texture.png` (646KB) - Table surface texture
  - App icons (192x192, 512x512)
- [x] Assets ready for Unity import and material creation

### Phase 3: Core Systems (Partial) ✅
- [x] **Data Models**:
  - `GameConstants.cs` - All game rules and physics constants
  - `VisualConstants.cs` - Camera, lighting, and rendering settings
  - `BallData.cs` - Ball state and position data
  - `GameState.cs` - Complete game state management
  - `PhysicsConfig.cs` - ScriptableObject for editable physics parameters

- [x] **Physics Engine**:
  - `ArcadePhysics.cs` - Complete arcade physics implementation ported from web version
  - Trajectory calculation with boundary bouncing
  - Energy loss and friction simulation
  - Trajectory preview calculation for UI
  - Distance and collision detection utilities

- [x] **Core Logic**:
  - `ScoreCalculator.cs` - Exact scoring logic from web version
  - 3-tier scoring system (3pts touching in ring, 2pts in ring, 1pt closest)
  - Score breakdown for debugging/UI

- [x] **Game Management**:
  - `GameManager.cs` - Singleton game manager skeleton
  - Event system for game state changes
  - Ball shooting logic
  - Round completion and score tallying
  - Winner determination

---

## 📋 Remaining Work

### Phase 3: Complete Core Systems (IN PROGRESS)

#### Managers to Create:
- [ ] `TurnManager.cs` - Turn-based system
- [ ] `RoundManager.cs` - Round progression
- [ ] `BallSpawner.cs` - Ball instantiation
- [ ] `SaveSystem.cs` - PlayerPrefs/JSON persistence

#### Physics to Create:
- [ ] `RealisticPhysics.cs` - Advanced physics simulation (optional)
- [ ] `BallController.cs` - Individual ball movement and physics
- [ ] `CollisionDetector.cs` - Ball-to-ball collision (optional)

---

### Phase 4: UI Implementation (UI Toolkit)

#### UXML Files (UI Structure):
- [ ] `HomeScreen.uxml` - Main menu
- [ ] `GameHUD.uxml` - In-game overlay
- [ ] `GameControls.uxml` - Shooting controls (angle/power sliders)
- [ ] `SettingsScreen.uxml` - Settings menu
- [ ] `LeaderboardScreen.uxml` - Rankings
- [ ] `Tutorial.uxml` - 8-step tutorial overlay
- [ ] `ScorePanel.uxml` - Score display component
- [ ] `PlayerCard.uxml` - Leaderboard player entry

#### USS Files (UI Styling):
- [ ] `Global.uss` - Global styles and variables
- [ ] `GlassMorphic.uss` - Glass-panel effects
- [ ] `NeonGlow.uss` - Glowing effects
- [ ] `HomeScreen.uss`
- [ ] `GameHUD.uss`
- [ ] `Settings.uss`
- [ ] `Leaderboard.uss`

#### UI Controllers (C#):
- [ ] `HomeScreenController.cs`
- [ ] `GameHUDController.cs`
- [ ] `GameControlsController.cs`
- [ ] `SettingsController.cs`
- [ ] `LeaderboardController.cs`
- [ ] `TutorialController.cs`
- [ ] `ScorePanelController.cs`

---

### Phase 5: Scene Setup

#### Scenes to Create:
- [ ] **MenuScene** - Main menu with background
- [ ] **GameScene** - 3D game table and gameplay
- [ ] **SettingsScene** - Settings management
- [ ] **LeaderboardScene** - Player rankings

#### GameScene 3D Setup:
- [ ] Table GameObject with PBR material
- [ ] Lighting (Ambient + Spotlight with shadows)
- [ ] Center ring with emissive glow
- [ ] Player barriers (red/blue)
- [ ] Camera setup (perspective, adaptive FOV)
- [ ] Ball prefabs (center, player 1, player 2)
- [ ] Ball materials with textures

---

### Phase 6: Audio System

#### AudioManager:
- [ ] `AudioManager.cs` - Singleton audio controller
- [ ] Audio source pooling
- [ ] Volume mixing (Master/SFX/Music)
- [ ] Mute functionality
- [ ] Settings persistence

#### Sound Effects (8 sounds):
- [ ] `ballShoot.wav` - Ball launch
- [ ] `ballHit.wav` - Ball collision
- [ ] `ballRoll.wav` - Ball rolling
- [ ] `score.wav` - Scoring sound
- [ ] `win.wav` - Victory fanfare
- [ ] `buttonClick.wav` - UI interaction
- [ ] `turnChange.wav` - Turn transition
- [ ] `roundComplete.wav` - Round end

#### Music:
- [ ] Menu background music (looping)
- [ ] Game background music (looping)

---

### Phase 7: Input System

#### InputManager:
- [ ] `InputManager.cs` - Cross-platform input handling
- [ ] Touch input for mobile (drag for aim)
- [ ] Mouse input for PC (click/drag)
- [ ] Keyboard shortcuts (arrow keys for fine tuning)
- [ ] Raycasting for ball selection
- [ ] Visual feedback on selection

#### Input Actions Asset:
- [ ] Create Input Actions asset with mappings
- [ ] Configure touch, mouse, and keyboard bindings

---

### Phase 8: Game Mechanics Implementation

#### Ball System:
- [ ] `BallController.cs` - Ball behavior
- [ ] Ball selection system
- [ ] Ball movement animation
- [ ] Trajectory visualization (LineRenderer)
- [ ] Ball highlighting/selection feedback

#### Shooting System:
- [ ] Angle and power UI controls
- [ ] Drag-to-aim on mobile
- [ ] Trajectory preview line
- [ ] Shot execution
- [ ] Ball physics integration

#### Turn System:
- [ ] Turn indicator UI
- [ ] Auto-switching between players
- [ ] Ball activation state management
- [ ] Round completion detection

---

### Phase 9: Statistics & Achievements

#### Statistics System:
- [ ] `StatisticsManager.cs` - Player stats tracking
- [ ] Games played, wins, losses
- [ ] Win rates and streaks
- [ ] Highest scores
- [ ] Perfect games
- [ ] JSON serialization

#### Achievement System:
- [ ] `AchievementManager.cs` - 14 achievements
- [ ] Achievement unlocking logic
- [ ] Badge display in UI
- [ ] Milestone tracking

#### Leaderboard:
- [ ] Player ranking system
- [ ] Rank badges (gold/silver/bronze)
- [ ] Statistics display
- [ ] Recent battles history

---

### Phase 10: Visual Polish

#### Effects:
- [ ] Ball trail effects (Trail Renderer)
- [ ] Particle effects on scoring
- [ ] Glow effects on center ring (Emissive materials)
- [ ] UI animations and transitions
- [ ] Victory celebration effects

#### Rendering:
- [ ] Post-processing (Bloom, Vignette)
- [ ] Real-time shadows from spotlight
- [ ] Ambient occlusion
- [ ] HDR rendering
- [ ] Mobile optimization (LOD, quality settings)

---

### Phase 11: Settings System

#### Settings Categories:
- [ ] Audio settings (Master, SFX, Music volumes)
- [ ] Display settings (Theme selection, Graphics quality)
- [ ] Gameplay settings (Physics mode toggle, Default game mode)
- [ ] Accessibility (Reduced motion, High contrast, Large text)
- [ ] Import/Export functionality

#### Theme System:
- [ ] 6 preset color schemes
- [ ] Dynamic CSS-like theming
- [ ] Theme persistence

---

### Phase 12: Build Optimization

#### Performance:
- [ ] Texture atlasing for UI
- [ ] Audio compression
- [ ] Code stripping (IL2CPP)
- [ ] Asset bundle optimization
- [ ] Draw call batching

#### Platform-Specific:
- [ ] WebGL: Compression, PWA manifest
- [ ] iOS: Bitcode, App icons, Xcode settings
- [ ] Android: AAB format, Adaptive icons
- [ ] PC: Standalone executables

---

### Phase 13: Testing & QA

#### Functionality Testing:
- [ ] All game rules work correctly
- [ ] Physics behaves as expected (both modes)
- [ ] Scoring calculations accurate
- [ ] UI responsive across screen sizes
- [ ] Settings persist correctly

#### Platform Testing:
- [ ] WebGL browser compatibility
- [ ] iOS device testing
- [ ] Android device testing
- [ ] PC testing (Windows/Mac/Linux)

---

### Phase 14: Deployment

#### WebGL Build:
- [ ] Custom template matching web version style
- [ ] PWA manifest and service worker
- [ ] Deploy to itch.io or Unity Play

#### Mobile Builds:
- [ ] iOS App Store submission
- [ ] Android Play Store submission
- [ ] App icons and screenshots

#### PC Builds:
- [ ] Standalone executables
- [ ] Installer creation (optional)

---

## File Count Summary

### Completed:
- ✅ 8 C# Scripts (Data models, Physics, Core logic, Managers)
- ✅ 4 Texture assets (balls + table)
- ✅ 2 Config files (manifest.json, ProjectSettings.asset)

### Remaining:
- 📋 ~72-92 C# Scripts
- 📋 ~15-20 UXML files
- 📋 ~10-15 USS files
- 📋 ~20-30 Prefabs
- 📋 ~10-15 Materials
- 📋 ~8-10 Audio clips
- 📋 5 Scenes

### Total Estimate:
- **~80-100 C# Scripts** (8 done, ~72-92 remaining)
- **Full feature parity with web version**

---

## Development Time Estimate

- **Phase 1-3 (Completed)**: ~3-4 hours ✅
- **Phase 3 (Complete)**: ~2-3 hours
- **Phase 4-5 (UI + Scenes)**: ~8-10 hours
- **Phase 6-7 (Audio + Input)**: ~4-6 hours
- **Phase 8 (Game Mechanics)**: ~6-8 hours
- **Phase 9 (Stats + Achievements)**: ~4-6 hours
- **Phase 10-11 (Polish + Settings)**: ~4-6 hours
- **Phase 12-14 (Optimization + Deployment)**: ~4-6 hours

**Total Remaining**: ~32-45 hours
**Total Project**: ~35-49 hours

---

## Key Technical Notes

1. **Physics System**: Both arcade and realistic physics fully editable in Unity Inspector via `PhysicsConfig` ScriptableObject
2. **Scoring Logic**: Exact port from web version ensures identical gameplay
3. **Multi-Platform**: Configured for WebGL, iOS, Android, and PC from the start
4. **UI Toolkit**: Modern, performance-focused UI system with CSS-like styling
5. **Event-Driven Architecture**: Manager classes use C# events for loose coupling
6. **Singleton Pattern**: Core managers (GameManager, AudioManager, etc.) use singleton pattern

---

## Next Steps

1. Complete remaining manager classes (TurnManager, RoundManager, BallSpawner)
2. Create basic UI structure with UI Toolkit
3. Set up GameScene with 3D table and balls
4. Implement ball selection and shooting mechanics
5. Add audio system
6. Implement statistics tracking
7. Add visual polish and effects
8. Optimize and deploy

---

**Last Updated**: October 19, 2025
**Status**: Foundation Complete - Ready for Full Implementation

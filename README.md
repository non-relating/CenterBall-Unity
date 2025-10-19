# CenterBall Unity

Unity port of the CenterBall 3D precision ball game.

## About

This is a Unity implementation of CenterBall, originally built with React + Three.js. The game features two players competing to position balls closest to a center ball with strategic barriers and scoring zones.

## Web Version

The original web version is available at: https://centerball.netlify.app

Web version repository: https://github.com/non-relating/CenterBall

## Project Structure

```
Assets/
├── Materials/    # Game materials and textures
├── Prefabs/      # Reusable game objects
├── Scenes/       # Game scenes
├── Scripts/      # C# game logic
└── UI/           # UI elements
```

## Development

This project is built with Unity 2022.3+ LTS.

### Opening the Project

1. Clone this repository
2. Open Unity Hub
3. Click "Open" and select the `CenterBall-Unity` folder
4. Wait for Unity to import all assets

## Game Rules

- Two players compete to position balls closest to a center ball
- **3 points**: Ball touching center ball AND both balls in center ring
- **2 points**: Ball in center ring but NOT touching center ball
- **1 point**: Closest ball to center ball outside the ring
- First player to reach target score (default 21) wins

## License

MIT License - See LICENSE file for details

## Credits

Original web version by the CenterBall team.
Unity port in progress.

# 🦾 Prosthetic Training Game Prototype (Unity)

An interactive Unity-based game designed to support rehabilitation by improving motor control and coordination for individuals with upper limb differences. The game blends fun mechanics with functional training exercises to help users build grip strength and dexterity in an engaging way.

---

## 🕹️ Features

- Interactive grip-based mini-games (Gripping and Slicing)
- Training modes to improve pinch and grasp strength
- Modular system for EMG input integration (future feature)
- Customizable difficulty levels and gameplay modes
- Designed with accessibility and rehabilitation in mind

---

## 🚀 Getting Started

### Prerequisites

- Unity 2022.3.x (LTS recommended)
- Git (optional, for version control)
- Windows or macOS

### Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/prosthetic-training-game.git
Open the project with Unity Hub:

Click “Add Project” and select the cloned folder

Open the MainMenu.unity or TrainingScene.unity scene to get started

## 🧪 How to Play
Use keyboard/mouse or connected hardware (future: EMG wristband)

Choose a training mode from the Main Menu

Follow in-game instructions for grip-based challenges

Game progress is saved locally (central database coming soon)

## Controls (EMG for future)

Cursor Movement: 
- Mouse Movement 

Buttons Interaction: 
- Left Click for all interactions 

Gripping Mode Controls: 
- 1 Key (on keyboard) – Clenching a full fist

- 2 Key (on keyboard) – Pinching with the thumb and index finger

- 3 Key (on keyboard) – Pinching with the thumb and middle finger

- 4 Key (on keyboard) – Pinching with the thumb and ring finger

- 5 Key (on keyboard) – Pinching with the thumb and pinky finger

Slicing Mode Controls: 
- Right Click - Slice Fruits

## 📁 Project Structure

```bash
ProjectRoot/
├── .vscode/                     # VS Code workspace settings and editor preferences
│
├── Assets/        <-------      # ALL OF MY CODE IN HERE
│   ├── 3D/                      # All assets used during game play
│   │   ├── Materials/           # Textures and materials for 3D models.
│   │   ├── Model/               # All 3D models used
│   │   ├── Prefabs/             # Reusable 3D game objects (fruits & bombs)
│   │   ├── Scripts/             # All C# scripts for game logic and physics (fruits spawning, game flow, etc.)
│   │   └── .meta files/         # Unity asset metadata, configuration, and settings files
│   │   
│   ├── Audios/                  # All audio assets
│   ├── BackgroundArt/           # All background styles
│   ├── Boxes/                   # All container boxes styles
│   ├── Icons/                   # All buttons, icons, cursor styles
│   ├── Prefabs/                 # Reusable game objects (friend item box in the social page)
│   ├── Scenes/                  # All unity scene files
│   ├── Scripts/                 # C# scripts for game management, input (Other than the 3D ones used in game)
│   ├── TextMesh Pro/            # Assets for TextMesh Pro text rendering
│   └── .meta files              # same as the ones in 3D
│
├── Packages/                    # Unity package dependencies
├── ProjectSettings/             # Unity configuration files
├── UserSettings/                # Local Unity editor settings, not project-specific
├── .gitignore                   # Git ignore rules
├── Assembly-CSharp.csproj       # Unity C# project file for IDEs
├── Prosthetic Training Game.sln # Unity solution file for managing projects
└── README.md                    # This file
```
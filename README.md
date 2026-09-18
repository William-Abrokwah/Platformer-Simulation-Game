# Platformer Simulation Game

A 3D first-person mini-game created in Unity3D featuring resource management, basic obstacle destruction, and competing AI bot dynamics across floating platforms. Made as an assignment for COMP 521.

## 📌 Introduction / Overview

This project is a single-level 3D game prototype in which the player navigates across four floating platforms to reach a final goal area. To advance, the player must collect food pellets to gain ammo, shoot down tree barriers blocking platform gaps, and manage resources efficiently before a competing AI bot consumes the available pellets.

Video Preview: https://youtu.be/enfoUCSJSBw

## 🚀 Features (MVP)

* **Platforms & Navigation:** 
Created 4 floating platforms separated by 3 tree barriers, with a victory goal on platform 4. Players can jump across gaps and return to previous platforms.


* **Tree Barrier Mechanics:** 
Passage requires destroying 2 adjacent trees. Includes custom edge collision barriers that disable only when a valid 2-tree gap is created.


* **Food Pellets & Resource System:** 
8 non-overlapping pellets spawn per platform, granting 1 ammo per pellet. Ammo resets to 0 and remaining pellets disappear upon entering a new platform.


* **Competing Bot AI:** 
Spawns on platform activation, moves in direct straight lines to randomly select and consume pellets without colliding with the player, and stops when pellets are depleted.


* **Projectiles & Shooting:** 
First-person left-click shooting firing straight-line projectiles (max 1 active projectile in flight). Destroys trees on hit and despawns on non-tree assets or platform boundaries.


* **Player Motion & Camera:** 
Standard WASD movement and mouse-look built using Unity Starter Assets.


* **Game UI & Conditions:** 
Includes HUD ammo counter, win screen on goal reach, and game over screens triggered by falling into the KillZone or having insufficient ammo/pellets left to clear a barrier.


## 🛠️ Tech Stack / Tools

* Unity3D Game Engine
* C# (Unity Engine Scripts)
* Unity Starter Assets (First-Person Controller)


## ⚙️ Installation / Running the Project

1. **Clone the Repository:**
```bash
git clone https://github.com/your-username/your-repo-name.git
```

2. **Open in Unity:**
* Launch **Unity Hub**.
* Click **Add** -> **Add project from disk**.
* Select the cloned repository folder.
* Open using Unity 3D.

3. **Run the Game:**
* Open `Assets/` in the Project tab and double-click the main Scene file.
* Press the **Play** button at the top of the Unity Editor.


## 📖 Usage

* **WASD:** Move player


* **Mouse:** Look around / Camera view


* **Spacebar:** Jump across platform gaps


* **Left Mouse Click:** Fire projectile (requires ammo)
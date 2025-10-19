<table>
  <tr>
    <td align="left" width="50%">
      <img width="100%" alt="gif1" src="https://github.com/user-attachments/assets/045e5f30-9d7b-44d8-9354-912cf4b713e3">
    </td>
    <td align="right" width="50%">
      <img width="100%" alt="gif2" src="https://github.com/user-attachments/assets/2e84d561-293f-4fd5-8767-ed20f82ff81d">
    </td>
  </tr>
</table>

##  📜Scripts and Features

You can move around, pick up objects, and extinguish fires through some of the given scripts below.

|  Script       | Description                                                  |
| ------------------- | ------------------------------------------------------------ |
| `FireClass.cs` | Manages fire objects' health, knockback effects on players, and store original prefabs when extinguished |
| `FireManager.cs` | Defines and checks which extinguisher types can put out specific fire classes using a dictionary mapping system |
| `GameManager.cs`  | Watches over the game's overall state (menu, playing, paused, dead), and handles scene transitions and pause functionality |
| `SprayHandler.cs`  | Applies damage to fires when sprayed with correct extinguishers and triggers player knockback for incorrect types |
| `PressPlate.cs`  | Opens/closes gates by tracking objects on a pressure plate and toggling sprites based on player presence detection |
| `etc`  | |

<br>


## 🔴About
FireFire is a short educational sidescrolling game where we need to figure out which extinguisher could extinguish certain types of fire. Our objective is to escape the fire safely. I handled the player movement, fire, extinguishing fire, pause menu, and settings menu. Here's some details about FireFire's development.
<br>

## 🕹️Play Game
Currently in demo game mode in itchio.
<br>

## 👤Developer & Roles
- Nolan Filbert Tandun (Game Designer)
- Rizki Raul Ferdinan (Game Designer)
- Revaldo Christian Yoga (Game Programmer)
- Kayla Cynthia Lukman (Game Programmer)
- Fayren William (Game Artist)
- Dave Sebastian Kurniawan (Game Artist)
- Muhammad Ryan Arrafi (Game Artist)
- Emilio Faustanka Kemal (Sound Engineer)
- Kennedy (Publication)
<br>

## 📂Files description

```
├── BGDC-PKM2024 (FireFire)           # Contains everything needed for FireFire game.
   ├── Assets                         # Contains every assets needed to make the game work, like scripts, sprites, etc.
      ├── Animation                   # Contains every animator and animation clips needed for the game.
      ├── Audio                       # Contains audio files needed for the background music and sound effects.
      ├── Prefabs                     # Contains every reusable game object that is used to be instantiated in the game scene.
      ├── Scenes                      # Contains scenes that are used to connect the game between main menu and gameplay.
      ├── Scripts                     # Contains the scripts needed to make the game work.
      ├── Sprites                     # Contains the art assets needed to display the game's visuals.
```
      

<br>

## 🕹️Game controls

The following controls are for the main gameplay, not applicable in main menu.

| Key Binding       | Function          |
| ----------------- | ----------------- |
| W           | Teleport to next waypoint |
| A/D             | Move left/right              |
| Space             | Jump           |
| S             | Drop through platform             |
| Left Mouse Click             | Start shooting extinguisher              |
| Right Mouse Click             | Pick up, drop, or swap extinguisher              |
| Escape           | Pause/Resume game |

<br>

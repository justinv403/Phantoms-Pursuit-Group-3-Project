# Phantoms-Pursuit-Group-3-Project

Controls
Objective: Navigate around the house with the following controls. Avoid getting caught by the ghost, or you lose the game.


"W" key moves the player forward.
"A" key moves the player to the left.
"S" key moves the player backwards.
"D" key moves the player to the right.
Holding the shift key while pressing any of the control keys will make the player run in that direction (Running consumes stamina, which is depicted as the white bar as seen in the image above. If the player runs out of stamina, they can’t run until their bar refills).



## Introduction

The game starts the player off outside their house. With no weapons and only a flashlight, the player’s objective is to navigate through a haunted house and avoid the poltergeist while finding the needed items to perform the exorcism and win the game. 


## Doors

Our game uses a unique system for our doors that allows the player to drag doors to any angle. In practical terms, this means that for the player to open or close a door, then must be within range of the door, and hold left click while looking at the door itself. Once the player has grabbed the door, they can drag their mouse in order to open and close the door. This position the door opens to is also positionally based on the location of the player, so if the player walks towards the door or away from the door, it will affect the angle the door opens or closes to.

Another way to image the way the door works is that a distance is set in front of the player, then attached to the player camera, and the door will try to rotate to the position that points towards that point. This means that when the player walks towards the door, that position will move forward relative to the player, which will change the position the door tries to point to.


## Exorcism & Beating the Game

To perform the exorcism to win the game, the player needs to collect all the cursed dolls scattered across the house. The player will press “E” to pick up each doll when within range. (Tip: Try looking in different rooms and places where the dolls could be found. This includes garages, bathrooms, kitchens, etc.)


Once the player has collected all of the cursed dolls, they must take the dolls into the closet (located in the bedroom) and press “E” to perform the exorcism.

(Win screen appears after you have successfully exorcized the ghost).

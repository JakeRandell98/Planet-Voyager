# Planet Voyager
Repository storing all files associated with the Planet Voyager game.
NOTE: See the file "Unity Collab Log.txt" for the version history.


Executable Download:
Download the contents of the "Projects" folder located in the "Planet Voyager" folder, then open the 
"Planet Voyager.exe" application to start the game.
NOTE: It is recommended that the game be played on a 1920x1080 resolution display since other resolutions are not 
currently supported.


Objective:

In each level, the goal is to collect all 3 stars with the rocket by using gravity assists from planets and the 
available fuel the rocket has.


Instructions:

Menu	- When the game is opened, a Level Select menu is shown with 3 possible levels to choose from.
	- Click on the play button to start the chosen level (Level 1 = Easy; Level 2 = Medium; Level 3 = Hard).

Planets - Drag planets to change their position along their orbital path.
	- Pinch the planets in/out (touchscreen only) to resize the planets and their gravitational fields (this 
	  alters the planets gravitational pull on the rocket).
	- The glowing area surrounding each planet is the planets gravitational influence area - if the rocket is 
	  within this region, a force is exerted on the rocket towards the planet.
	- The planets cannot be moved or resized once the rocket has been launched - use the reset button to be 
	  able to.
	- Currently, each planet exerts the same force on the rocket as long as the size is not changed.

Rocket 	- Drag back on the rocket to slingshot it in the direction of the dotted line (currently any size drag 
	  back will give the same initial speed to the rocket).
	- The rocket will explode on impact with a planet.
	- The rocket has a pre-determined amount of fuel depending on the level.

UI 	- Home Button (Top Left): Returns to the Level Select menu.
	- Reset Button (Top Right): Restarts the level.
	- Forward Thrust Button (Bottom Right - Up Arrow): Accelerates the rocket in the direction the rocket is 
	  facing and consumes fuel.
	- Reverse Thrust Button (Bottom Right - Down Arrow): Accelerates the rocket in the opposite direction to 
	  where the rocket is facing and consumes fuel.
# Vehicle-Generator-Game
Scripts from my vehicle generator game in unity
This is a collection of the important scripts from my game. It includes car generation scripts, car creator scripts, UI slider scripts and camera controller scripts.

### About
Vehicle generator mode is a borderlands-esque generational system for vehicle parts, it picks one main cab component, then attaches other components via origin points, resulting in a generated car from different parts. Vehicle creator mode uses the same origin point attaching components while the user can cycle through parts using UI buttons, creator mode has a day/night cycle selector slider and light colour slider, while the generator mode just has a light slider.

### known issues
- The camera rotation is very jumpy for both modes and is also unclamped as I couldn't get it to work properly (will bug out if you zoom too close or go up too high or low).
- Vehicle meshes are also out sometimes slightly out of alinment (origin issues) and some have other vertice issues.
- - You have to click elsewhee in the window after setting the sliders as they will move if you then move the camera, will sometimes happen anyway even if you do click elsewhere.

### Technology
Created with Unity Game Engine and the help of a lot of youtube tutorials.

### Game versions
There are two versions of the game, version 1 is just a workshop setting and car generator, the camera tracks smoothly. Unfortunately I didn't have Github at the time nor did I save an original version as a seperate file so I can't check on my previous scripts.

Version 1:
https://adrift-dev.itch.io/vehicle-generator

Version 2 is this current version:
https://adrift-dev.itch.io/random-vehicle-generator-2

## CUSTOM AUDIO MANAGER CLASS

This AudioManager is a Singleton class that works with the custom class AudioItem.

There is a AudioItem array in the AM Game Object that you can use to play music or anything in the background, or if your game is jsut going to have one source, you can play all sounds from here. The script will create one AudioSource per AudioItem.

When you create an AudioItem elsewhere it will not be part of this array, but it will be played from the Game Object where it is created, which allows you to play with 3D settings and different sound sources.

### Functions

1. Play
2. Stop
3. Pause
4. Unpause
5. StopAll
6. PauseAll
7. UnpauseAll
8. PlaySfx

### What can it do?

- Play: select the AudioItem that you want to play. It will add an AudioSource to the GameObject and set the desired output.
- Choose to play from one of the following output using `AudioManager.sfx` (reference to the Audio Mixer Group) →
    - sxf
    - music
    - ambience
    - dialogue

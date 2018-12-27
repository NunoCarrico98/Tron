# 2o Projeto de LP2 (2018)

## Our solution

### Division of Tasks

We both structured the program before we began.

Rui: 
* Took care of `Tron` class and kept improving the `Gameloop` along with the `Update` method and several others.
* Created `KeyReader` class (later `InputSystem`) events to receive both player inputs.
* Added a 2D Double Buffer (later on deleted because for something so simple we could use a simple buffer.)
* Created a `Renderer` class with several methods for world rendering, menus, etc.

Nuno:
* Restructured `InputSystem` and took care of the multi threading to receive user inputs.
* Added enumeration `PlayerDirections` and took care of the player movement
* Added `Player` class along with methods to Move, Detect Collisions, etc.
* Added `AI` class with a simple AI.

Improvements:

Rui:
* Fixed bugs with threading, game reset, navigation between menus, wrong array verifications, etc.
* Improved AI.

Nuno:

* Fixed bugs.
* Improved AI.

Both worked on all the classes as the project kept improving.

### Architecture

When starting the program, a `Renderer` class is created for the visual part of the game. Then, `InputSystem` is also instantiated
and its method `GetUserInput()` is called on another thread. That way, we can have the game running while having a parallel cycle to
get the user inputs.

`Tron` class is instantiated, creating our buffer `gameWorld` which is a bidimensional array. 
`MainMenu()` is called to start our game, showing the main menu first.

The user can now choose to play against another player, play against AI, check credits, controls or simply quit the program.

If a 1v1 match is chosen, two `Player` objects are instantiated, each having their own `ChangeDirection` method which serves
as a listener to different events. One for the first player (WASD keys), other for the second player (Arrow keys).

If AI is chosen, a `Player` with a WASD keys listener is instantiated and a `AI` object is also created.
`AI` extends `Player` and has its own methods for looking for neighbours in a long distance, checking if enemies are in the nearby tiles, 
and also has overriden methods from `Player`.

The other menu options only show other pages or exit the program.

### Flowchart

![Flowchart](https://gitlab.com/rui-martins/lp2p1/uploads/39b8a56be11c03c6e57e18a694bb8402/LP2_P1_Flowchart.png)

### UML Diagram

![Diagram(UML)](https://gitlab.com/rui-martins/lp2p1/uploads/b1122a7706e2e3a4b1b1266b507dead5/LP2_P1_UML.png)

## Conclusions

This was a simple, but tricky project. We had some problems with the `Gameloop` thanks to the timer at the end (`Thread.Sleep`), 
because we wanted to show messages to the user after every win / draw and we simply couldn`t do it inside the `Gameloop`.

A whole restructure had to be done along with all the inputs (menus and such) all being received from the second `Thread`, otherwise
we would have to press keys two times each time we wanted a simple `Console.ReadKey()`.

We tried to do a very simple AI as an extra (it it dumb sometimes but it also surprises you).
We also tried made some music but later discovered that audio is not supported in `NetCore App`.

Despite being simple, it expanded our knowledge to how games really work and it was very fun to do.
We both worked equally hard which was a key factor to have everything ready in time.

## References

* <a name="ref1">\[1\]</a> Whitaker, R. B. (2016). The C# Player`s Guide (3rd Edition). Starbound Software

## Metadata

* Authors: Nuno Carriço and Rui Martins
# Super Mono Pong

A (super :) )simple pong clone made in C# with MonoGame.

Uses (rudamentary) OOP principles.

Game Features:
- Rectangle showing where the screen's borders are
- Paddle acceleration and friction, enabling smoother feeling controls
- Ball bounce angles dependent on where it hits the paddle
- Sounds for scoring, ball collisions, etc
- 1p, 2p, or 1p VS CPU modes (done by seperating input logic into different "controller" classes that I could then pass to a paddle)
- Ability to set the score needed to win (max of 25)
- Scenes / Game states dependent on if a player / cpu wins, or if the game is paused

Controls:
- Left/Right (change Score To Win on title)
- M (set score to maximum)
- D (set score to default)

- Up (P1 paddle up)
- Down (P1 paddle down)
- W (P2 paddle up)
- S (P2 paddle down)
- P (pause)

History:
- Project was built as a remake to my older, much more basic clone of pong, named "Mono Pong"
- Old project was written pretty badly, made it very hard to implement new features
- New project was made as an attempt to better organize code / add features I couldn't add in the original

To Do:
- Fix up places where code becomes messy again (I'm terrible at this)
- Add the ability to change the game speed (Super Mono Pong Turbo: Hyper Fighting)
- Improve CPU so that playing against it is more interesting
- Fix the stalemate issue (Technically fixed, but current solution isn't so good / fun to play with)

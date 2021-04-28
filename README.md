# Gaming-Framework
A framework that can be easily adapted to different games

Currently it is having a Connect Four aka four in a row. Two players take turns dropping pieces on a 7x6 
board. The player forms an unbroken chain of four pieces horizontally, vertically, or 
diagonally, wins the game.

For an extension this design allows diffrenet modes of play like 

● Human vs Human

● Computer vs Human

With human players, the system checks the validity of moves as they are entered.

With computer play, the user should be able to select amongst a range of possible strategies 
and/or levels of difficulty. Implemented with the following two strategies:

● Random selection of a legal move

● Simple rules without thinking ahead

The only thing the computer is concerned with is the present game state, without thinking any 
number of moves ahead. Thus, the computer will go for the currently best move (e.g. capturing 
most pieces, or avoid being captured, etc.) without thinking about the consequences in future 
moves. This algorithm is game-specific and having appropriate board scoring functions.
![alt text](https://th.bing.com/th/id/R7e74fa99eefb89044fa796ed0770df9f?rik=gZp4laYZSYPAYQ&riu=http%3a%2f%2fecx.images-amazon.com%2fimages%2fI%2f61QZQl1GGAL.jpg&ehk=5ZXWhzWRf8v4KG3B9V%2fWxilk1Iof1pywz5G7vPnUEdg%3d&risl=&pid=ImgRaw)

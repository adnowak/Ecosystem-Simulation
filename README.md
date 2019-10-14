# Ecosystem-Simulation
A desktop app, meant to simulate a simplified enviroment

The program simulates an enviroment based on the following rules:
-the time is divided into turns
-the program displays a map, made out of square pieces of land
  -in the beginning there are only:
    -sand
    -stone
    -water
  -sand can get humid when neighbouring water or slightly moist when it's neighbour neighbours water
  -in random places, rain will occur, increasing the humidity of the sand
  -in random places, grass will arive, humidity of the sand increases it's probability of surviving or densening
  -existance and density of grass and humidity of sand increase the chance of the sand turning into soil
  -neighbourhood of soil, stone and water increases the chance of any piece of land turning into soil
  -neighbourhood of grass increases the chance of grass arriving at any piece of land
-through clicking the "Add" button, a sheep is created in a random place on the map
  -a sheep makes one move per turn, where possible moves are:
    -heading north
    -heading west
    -heading south
    -heading east
    -interacting with the block it's on
  -a sheep has it's hunger and thirst levels, once reached, on individual dies
  -interaction with a piece with dense grass takes away the density and reduces the sheep's hunger
  -interaction with a piece with a grass which is not dense destroys the grass totally and reduces the sheep's hunger
  -interaction with a piece of water sets the thirst level to 0

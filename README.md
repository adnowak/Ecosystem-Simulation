# Ecosystem-Simulation
A desktop app, meant to simulate a simplified enviroment

The program simulates an enviroment based on the following rules:

1.The time is divided into turns

2.the program displays a map, made out of square pieces of land

  2.1 in the beginning there are only:
  
    2.1.3 sand
    
    2.1.4 stone
    
    2.1.5 water
    
  2.2 sand can get humid when neighbouring water or slightly moist when it's neighbour neighbours water
  
  2.3 in random places, rain will occur, increasing the humidity of the sand
  
  2.4 in random places, grass will arive, humidity of the sand increases it's probability of surviving or densening
  
  2.5 existance and density of grass and humidity of sand increase the chance of the sand turning into soil
  
  2.6 neighbourhood of soil, stone and water increases the chance of any piece of land turning into soil
  
  2.7 neighbourhood of grass increases the chance of grass arriving at any piece of land
  
3 through clicking the "Add" button, a sheep is created in a random place on the map

  3.1 a sheep makes one move per turn, where possible moves are:
  
    3.1.1 heading north
    
    3.1.2 heading west
    
    3.1.3 heading south
    
    3.1.4 heading east
    
    3.1.5 interacting with the block it's on
    
  3.2 a sheep has it's hunger and thirst levels, once reached, on individual dies
  
  3.3 interaction with a piece with dense grass takes away the density and reduces the sheep's hunger
  
  3.4 interaction with a piece with a grass which is not dense destroys the grass totally and reduces the sheep's hunger
  
  3.5 interaction with a piece of water sets the thirst level to 0
  

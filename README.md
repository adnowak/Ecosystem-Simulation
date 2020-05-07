# Ecosystem-Simulation
A desktop app, meant to simulate a simplified enviroment

The program simulates an enviroment based on the following rules:

1.The time is divided into turns

2.The program displays a map, made out of square pieces of land

  2.1 In the beginning there are only:
  
    2.1.3 Sand
    
    2.1.4 Stone
    
    2.1.5 Water
    
  2.2 Sand can get humid when neighbouring water or slightly humid when it's neighbour neighbours water
  
  2.3 In random places, rain will occur, increasing the humidity of the sand
  
  2.4 In random places, grass will arive, humidity of the sand increases it's probability of surviving or densening
  
  2.5 Existance and density of grass and humidity of sand increase the chance of the sand turning into soil
  
  2.6 Neighbourhood of soil, stone and water increases the chance of any piece of land turning into soil
  
  2.7 Neighbourhood of grass increases the chance of grass arriving at any piece of land
  
  2.8 Humidity of sand increases the chance of grass survival on it
  
  2.9 Grass survives on dry soil
  
3 By clicking the "Add" button, a sheep is created in a random place on the map

  3.1 A sheep makes one move per turn, where possible moves are:
  
    3.1.1 Heading north
    
    3.1.2 Heading west
    
    3.1.3 Heading south
    
    3.1.4 Heading east
    
    3.1.5 Interacting with the block it's on
    
  3.2 A sheep has it's hunger and thirst levels, once reached the individual dies
  
  3.3 Interaction with a piece with dense grass takes away the density and reduces the sheep's hunger
  
  3.4 Interaction with a piece with a grass which is not dense destroys the grass totally and reduces the sheep's hunger
  
  3.5 Interaction with a piece of water sets the thirst level to 0
  

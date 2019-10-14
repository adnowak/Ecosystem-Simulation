using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grass_simulation
{
    public partial class Form1 : Form
    {
        private PictureBox[,] display = new PictureBox[Constants.MAP_SIZE, Constants.MAP_SIZE];
        private Field[,] map = new Field[Constants.MAP_SIZE, Constants.MAP_SIZE];
        private int[,] averageHeight = new int[Constants.MAP_SIZE, Constants.MAP_SIZE];
        private ArrayList animals = new ArrayList();
        private ArrayList animalsToKill = new ArrayList();
        private ArrayList animalsPictureBoxes = new ArrayList();

        Image soilWetDenseGrass = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/soil/wet-grass-dense.bmp");
        Image soilWetGrass = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/soil/wet-grass.bmp");
        Image soilWet = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/soil/wet.bmp");
        Image soilRegularGrass = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/soil/regular-grass.bmp");
        Image soilRegular = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/soil/regular.bmp");

        Image sandWetDenseGrass = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/sand/wet-grass-dense.bmp");
        Image sandWetGrass = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/sand/wet-grass.bmp");
        Image sandWet = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/sand/wet.bmp");
        Image sandRegularGrass = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/sand/regular-grass.bmp");
        Image sandRegular = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/sand/regular.bmp");

        Image stoneRegularGrass = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/stone/regular-grass.bmp");
        Image stoneRegular = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/stone/regular.bmp");

        Image waterRegularGrass = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/water/regular-grass.bmp");
        Image waterRegular = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/water/regular.bmp");

        Image sheepRegular = Image.FromFile(System.IO.Directory.GetCurrentDirectory() + @"/img/sheep/regular.bmp");

        public Form1()
        {
            InitializeComponent();

            for (int i = 0; i < Constants.MAP_SIZE; i++)
            {
                for (int j = 0; j < Constants.MAP_SIZE; j++)
                {
                    int rand = RandomNumber.Between(0, Constants.FIELD_TYPES_AMOUNT * 100);
                    if (rand < 20)
                    {
                        map[i, j] = new Stone(this, i, j);
                    }
                    else
                    {
                        map[i, j] = new Sand(this, i, j);
                    }

                    display[i, j] = new PictureBox();

                    display[i, j].Size = new System.Drawing.Size(16, 16);
                    display[i, j].Location = new System.Drawing.Point(i * 16, j * 16);

                    display[i, j].Click += new EventHandler(this.click);//
                }
            }

            for (int xPos = 0; xPos < Constants.MAP_SIZE; xPos++)
            {
                for (int yPos = 0; yPos < Constants.MAP_SIZE; yPos++)
                {
                    int sum = 0;

                    if (xPos > 0 && xPos < Constants.MAP_SIZE - 1)
                    {
                        if (yPos > 0 && yPos < Constants.MAP_SIZE - 1)
                        {
                            sum = sumFour(map[xPos - 1, yPos].getZPos(), map[xPos, yPos - 1].getZPos(), map[xPos + 1, yPos].getZPos(), map[xPos, yPos + 1].getZPos());
                        }
                        else if (yPos == 0)
                        {
                            sum = sumFour(map[xPos - 1, yPos].getZPos(), map[xPos, Constants.MAP_SIZE - 1].getZPos(), map[xPos + 1, yPos].getZPos(), map[xPos, yPos + 1].getZPos());
                        }
                        else
                        {
                            sum = sumFour(map[xPos - 1, yPos].getZPos(), map[xPos, yPos - 1].getZPos(), map[xPos + 1, yPos].getZPos(), map[xPos, 0].getZPos());
                        }
                    }
                    else if (xPos == 0)
                    {
                        if (yPos > 0 && yPos < Constants.MAP_SIZE - 1)
                        {
                            sum = sumFour(map[Constants.MAP_SIZE - 1, yPos].getZPos(), map[xPos, yPos - 1].getZPos(), map[xPos + 1, yPos].getZPos(), map[xPos, yPos + 1].getZPos());
                        }
                        else if (yPos == 0)
                        {
                            sum = sumFour(map[Constants.MAP_SIZE - 1, yPos].getZPos(), map[xPos, Constants.MAP_SIZE - 1].getZPos(), map[xPos + 1, yPos].getZPos(), map[xPos, yPos + 1].getZPos());
                        }
                        else
                        {
                            sum = sumFour(map[Constants.MAP_SIZE - 1, yPos].getZPos(), map[xPos, yPos - 1].getZPos(), map[xPos + 1, yPos].getZPos(), map[xPos, 0].getZPos());
                        }
                    }
                    else
                    {
                        if (yPos > 0 && yPos < Constants.MAP_SIZE - 1)
                        {
                            sum = sumFour(map[xPos - 1, yPos].getZPos(), map[xPos, yPos - 1].getZPos(), map[0, yPos].getZPos(), map[xPos, yPos + 1].getZPos());
                        }
                        else if (yPos == 0)
                        {
                            sum = sumFour(map[xPos - 1, yPos].getZPos(), map[xPos, Constants.MAP_SIZE - 1].getZPos(), map[0, yPos].getZPos(), map[xPos, yPos + 1].getZPos());
                        }
                        else
                        {
                            sum = sumFour(map[xPos - 1, yPos].getZPos(), map[xPos, yPos - 1].getZPos(), map[0, yPos].getZPos(), map[xPos, 0].getZPos());
                        }
                    }

                    sum += map[xPos, yPos].getZPos();

                    averageHeight[xPos, yPos] = sum / 5;
                }
            }

            for (int i = 0; i < Constants.MAP_SIZE; i++)
            {
                for (int j = 0; j < Constants.MAP_SIZE; j++)
                {
                    if (averageHeight[i, j] < 39)
                    {
                        map[i, j] = new Water(this, i, j);
                    }
                    map[i, j].setZPos(averageHeight[i, j]);
                }
            }

            runDisplay();

            for (int i = 0; i < Constants.MAP_SIZE; i++)
            {
                for (int j = 0; j < Constants.MAP_SIZE; j++)
                {
                    this.Controls.Add(display[i, j]);
                }
            }

            assignNeighboursToAll();

            for (int i = 0; i < Constants.MAP_SIZE; i++)
            {
                for (int j = 0; j < Constants.MAP_SIZE; j++)
                {
                    display[i, j].Name = map[i, j].toString();
                }
            }
        }

        private int sumFour(int first, int second, int third, int fourth)
        {
            return first + second + third + fourth;
        }

        //
        void click(object sender, EventArgs e)
        {
            PictureBox pic = sender as PictureBox;
            MessageBox.Show(pic.Name);
        }

        void assignNeighboursToAll()
        {
            for (int i = 0; i < Constants.MAP_SIZE; i++)
            {
                for (int j = 0; j < Constants.MAP_SIZE; j++)
                {
                    assignNeighbours(i, j);
                }
            }
        }

        public void runDisplay()
        {
            foreach(PictureBox animalPictureBox in animalsPictureBoxes)
            {
                this.Controls.Remove(animalPictureBox);
            }

            animalsPictureBoxes.Clear();
            
            for (int i = 0; i < Constants.MAP_SIZE; i++)
            {
                for (int j = 0; j < Constants.MAP_SIZE; j++)
                {
                    display[i, j].SuspendLayout();
                    if(map[i,j].getType() == 0)
                    {
                        if (map[i, j].getHumidity() >= Constants.MIN_HUMIDITY_FOR_MOIST)
                        {
                            if (map[i, j].hasGrass())
                            {
                                if (map[i, j].hasDenseGrass())
                                {
                                    display[i, j].Image = soilWetDenseGrass;
                                }
                                else
                                {
                                    display[i, j].Image = soilWetGrass;
                                }
                            }
                            else
                            {
                                display[i, j].Image = soilWet;
                            }
                        }
                        else
                        {
                            if (map[i, j].hasGrass())
                            {
                                display[i, j].Image = soilRegularGrass;
                            }
                            else
                            {
                                display[i, j].Image = soilRegular;
                            }
                        }
                    }
                    else if(map[i, j].getType() == 1)
                    {
                        if(map[i, j].getHumidity()>= Constants.MIN_HUMIDITY_FOR_MOIST)
                        {
                            if (map[i, j].hasGrass())
                            {
                                if(map[i, j].hasDenseGrass())
                                {
                                    display[i, j].Image = sandWetDenseGrass;
                                }
                                else
                                {
                                    display[i, j].Image = sandWetGrass;
                                }
                            }
                            else
                            {
                                display[i, j].Image = sandWet;
                            }
                        }
                        else
                        {
                            if(map[i, j].hasGrass())
                            {
                                display[i, j].Image = sandRegularGrass;
                            }
                            else
                            {
                                display[i, j].Image = sandRegular;
                            }
                        }
                    }
                    else if (map[i, j].getType() == 2)
                    {
                        if (map[i, j].hasGrass())
                        {
                            display[i, j].Image = stoneRegularGrass;
                        }
                        else
                        {
                            display[i, j].Image = stoneRegular;
                        }
                    }
                    else if (map[i, j].getType() == 3)
                    {
                        if (map[i, j].hasGrass())
                        {
                            display[i, j].Image = waterRegularGrass;
                        }
                        else
                        {
                            display[i, j].Image = waterRegular;
                        }
                    }

                    if(map[i,j].hasAnimal())
                    {
                        PictureBox sheepPictureBox = new PictureBox();

                        sheepPictureBox.Size = new System.Drawing.Size(8, 8);
                        sheepPictureBox.Location = new System.Drawing.Point(i * 16+4, j * 16+4);

                        sheepPictureBox.Image = sheepRegular;

                        this.Controls.Add(sheepPictureBox);
                        map[i, j].addAnimalPictureBox(sheepPictureBox);

                        int zIndex = this.Controls.GetChildIndex(sheepPictureBox);
                        sheepPictureBox.BringToFront();

                        animalsPictureBoxes.Add(sheepPictureBox);
                    }
                }
            }
        }

        private void assignNeighbours(int xPos, int yPos)
        {
            if (xPos > 0 && xPos < Constants.MAP_SIZE-1)
            {
                if(yPos >0 && yPos < Constants.MAP_SIZE-1)
                {
                    map[xPos, yPos].updateNeighbours(map[xPos-1,yPos], map[xPos, yPos-1], map[xPos + 1, yPos], map[xPos, yPos+1]);
                }
                else if(yPos == 0)
                {
                    map[xPos, yPos].updateNeighbours(map[xPos - 1, yPos], map[xPos, Constants.MAP_SIZE - 1], map[xPos + 1, yPos], map[xPos, yPos + 1]);
                }
                else
                {
                    map[xPos, yPos].updateNeighbours(map[xPos - 1, yPos], map[xPos, yPos - 1], map[xPos + 1, yPos], map[xPos, 0]);
                }
            }
            else if(xPos == 0)
            {
                if (yPos > 0 && yPos < Constants.MAP_SIZE - 1)
                {
                    map[xPos, yPos].updateNeighbours(map[Constants.MAP_SIZE - 1, yPos], map[xPos, yPos - 1], map[xPos + 1, yPos], map[xPos, yPos + 1]);
                }
                else if (yPos == 0)
                {
                    map[xPos, yPos].updateNeighbours(map[Constants.MAP_SIZE - 1, yPos], map[xPos, Constants.MAP_SIZE - 1], map[xPos + 1, yPos], map[xPos, yPos + 1]);
                }
                else
                {
                    map[xPos, yPos].updateNeighbours(map[Constants.MAP_SIZE - 1, yPos], map[xPos, yPos - 1], map[xPos + 1, yPos], map[xPos, 0]);
                }
            }
            else
            {
                if(yPos >0 && yPos < Constants.MAP_SIZE-1)
                {
                    map[xPos, yPos].updateNeighbours(map[xPos-1,yPos], map[xPos, yPos-1], map[0, yPos], map[xPos, yPos+1]);
                }
                else if(yPos == 0)
                {
                    map[xPos, yPos].updateNeighbours(map[xPos - 1, yPos], map[xPos, Constants.MAP_SIZE - 1], map[0, yPos], map[xPos, yPos + 1]);
                }
                else
                {
                    map[xPos, yPos].updateNeighbours(map[xPos - 1, yPos], map[xPos, yPos - 1], map[0, yPos], map[xPos, 0]);
                }
            }
        }

        private void NextRoundButton_Click(object sender, EventArgs e)
        {
            nextRound();
        }

        private void nextRound()
        {
            foreach(Animal animalToKill in animalsToKill)
            {
                animals.Remove(animalToKill);
            }
            animalsToKill.Clear();

            foreach (Animal animal in animals)
            {
                animal.nextRound();
            }

            for (int i = 0; i < Constants.MAP_SIZE; i++)
            {
                for (int j = 0; j < Constants.MAP_SIZE; j++)
                {
                    map[i, j].nextRound();
                    display[i, j].Name = map[i, j].toString();//
                }
            }

            assignNeighboursToAll();
            runDisplay();
        }

        public void makeSoil(int xPos, int yPos)
        {
            map[xPos, yPos] = new Soil(this, xPos, yPos);
            assignNeighbours(xPos, yPos);
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            Timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            nextRound();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            Timer.Stop();
        }

        private void SheepButon_Click(object sender, EventArgs e)
        {
            int fieldNum = RandomNumber.Between(0, Constants.MAP_SIZE * Constants.MAP_SIZE-1);

            int xPos = fieldNum / Constants.MAP_SIZE;
            int yPos = fieldNum % Constants.MAP_SIZE;

            animals.Add(new Sheep(map[xPos, yPos]));
            display[xPos, yPos].Name = map[xPos, yPos].toString();

            runDisplay();
        }

        public ArrayList getAnimalsToKill()
        {
            return animalsToKill;
        }
    }

    static class Constants
    {
        public const int NEIGHBOURS_AMOUNT = 4;
        public const int MAP_SIZE = 30;
        public const int FIELD_TYPES_AMOUNT = 4;
        public const int SAND_GETS_HUMID_PROB = 500;
        public const int SOIL_GETS_HUMID_PROB = 350;
        public const int GRASS_GROWS_PROB = 2;
        public const int NEIGHBOURING_GRASS_BONUS = 2;
        public const int NEIGHBOURING_DENSE_GRASS_BONUS = 5;
        public const int GRASS_DENSES_ON_SAND_PROB = 100;
        public const int GRASS_DENSES_ON_SOIL_PROB = 300;
        public const int HUMIDITY_DECREASE = 200;
        public const int RAIN_PROB = 100;
        public const int TURN_TO_SOIL_PROB = 5;
        public const int RAIN_HUMIDITY_INCREASE = 500;
        public const int MIN_HUMIDITY_FOR_MOIST = 500;
        public const int MAX_HUMIDITY = 1000;
        public const int MAX_HEIGHT = 100;
    }

    class RandomNumber
    {
        private static readonly RNGCryptoServiceProvider _generator = new RNGCryptoServiceProvider();

        public static int Between(int minimumValue, int maximumValue)
        {
            byte[] randomNumber = new byte[1];

            _generator.GetBytes(randomNumber);

            double asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);

            // We are using Math.Max, and substracting 0.00000000001, 
            // to ensure "multiplier" will always be between 0.0 and .99999999999
            // Otherwise, it's possible for it to be "1", which causes problems in our rounding.
            double multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);

            // We need to add one to the range, to allow for the rounding done with Math.Floor
            int range = maximumValue - minimumValue + 1;

            double randomValueInRange = Math.Floor(multiplier * range);

            return (int)(minimumValue + randomValueInRange);
        }
    }

    abstract class Entity
    {
        public Entity()
        {

        }

        public abstract void nextRound();
    }

    abstract class Field : Entity
    {
        protected Form1 parent;

        protected int xPos;
        protected int yPos;
        protected int zPos;
        protected FieldCopy[] neighbours;
        protected int type;
        protected bool grass;
        protected bool denseGrass;
        protected int humidity;
        protected Animal animal;
        protected PictureBox animalPictureBox;

        public Field(Form1 parent, int xPos, int yPos) : base()
        {
            animal = null;
            this.parent = parent;
            this.xPos = xPos;
            this.yPos = yPos;
            this.zPos = RandomNumber.Between(0, Constants.MAX_HEIGHT);
            grass = false;
            denseGrass = false;
            humidity = 0;
            neighbours = new FieldCopy[Constants.NEIGHBOURS_AMOUNT];
        }

        public void updateNeighbours(Field neighbour1, Field neighbour2, Field neighbour3, Field neighbour4)
        {
            neighbours[0] = new FieldCopy(neighbour1);
            neighbours[1] = new FieldCopy(neighbour2);
            neighbours[2] = new FieldCopy(neighbour3);
            neighbours[3] = new FieldCopy(neighbour4);
        }

        public Field getNorthernNeighbour()
        {
            return neighbours[1].getBaseField();
        }

        public Field getSouthernNeighbour()
        {
            return neighbours[3].getBaseField();
        }

        public Field getEasternNeighbour()
        {
            return neighbours[2].getBaseField();
        }

        public Field getWesternNeighbour()
        {
            return neighbours[0].getBaseField();
        }

        public int getZPos()
        {
            return zPos;
        }

        public void setZPos(int newZPos)
        {
            this.zPos = newZPos;
        }

        public int getType()
        {
            return type;
        }

        public bool hasGrass()
        {
            return grass;
        }

        public void removeGrass()
        {
            this.grass = false;
        }

        public bool hasDenseGrass()
        {
            return denseGrass;
        }

        public void removeDenseGrass()
        {
            this.denseGrass = false;
        }

        public int getHumidity()
        {
            return humidity;
        }

        public string toString()
        {
            String neighboursString = "";
            for (int i = 0; i < Constants.NEIGHBOURS_AMOUNT; i++)
            {
                neighboursString += neighbours[i].xPos + "," + neighbours[i].yPos + "\n";
            }

            return "Type:" + type + " Grass: " + grass + " Humidity:" + humidity + " denseGrass:" + denseGrass + "[" + xPos + ";" + yPos + "]" + "\nHeight:" + zPos + "\n" + neighboursString + "\nAnimal:" + hasAnimal() + "\nNeighbours water:" + neighboursWater();
        }

        public void turnToSoil()
        {
            parent.makeSoil(xPos, yPos);
        }

        public int getXPos()
        {
            return xPos;
        }

        public int getYPos()
        {
            return yPos;
        }

        public Form1 getParent()
        {
            return parent;
        }

        public FieldCopy[] getNeighbours()
        {
            return neighbours;
        }

        public void setAnimal(Animal animal)
        {
            this.animal = animal;
        }

        public bool hasAnimal()
        {
            return animal != null;
        }

        public Animal getAnimal()
        {
            return animal;
        }

        public void addAnimalPictureBox(PictureBox animalPictureBox)
        {
            this.animalPictureBox = animalPictureBox;
        }

        public PictureBox getAnimalPictureBox()
        {
            return animalPictureBox;
        }

        public bool neighboursWater()
        {
            for (int i = 0; i < Constants.NEIGHBOURS_AMOUNT; i++)
            {
                if (neighbours[i].getType() == 3)
                {
                    return true;
                }
            }
            return false;
        }

        public int neighbouringAnimalsAmount()
        {
            int amount = 0;
            for (int i = 0; i < Constants.NEIGHBOURS_AMOUNT; i++)
            {
                if (neighbours[i].hasAnimal())
                {
                    amount++;
                }
            }
            return amount;
        }

        public ArrayList getReproductionCompetitors()
        {
            ArrayList reproductionCompetitors = new ArrayList();
            for (int i = 0; i < Constants.NEIGHBOURS_AMOUNT; i++)
            {
                if (neighbours[i].hasAnimal())
                {
                    reproductionCompetitors.Add(neighbours[i].getAnimal());
                }
            }
            return reproductionCompetitors;
        }
    }

    class FieldCopy : Field
    {
        private Field baseField;

        public FieldCopy(Field fieldToCopy) : base(fieldToCopy.getParent(), fieldToCopy.getXPos(), fieldToCopy.getYPos())
        {
            baseField = fieldToCopy;
            grass = fieldToCopy.hasGrass();
            denseGrass = fieldToCopy.hasDenseGrass();
            type = fieldToCopy.getType();
            zPos = fieldToCopy.getZPos();
            this.neighbours = fieldToCopy.getNeighbours();
            this.type = fieldToCopy.getType();
        }

        public override void nextRound()
        {
            
        }

        public Field getBaseField()
        {
            return baseField;
        }
    }

    class Soil : Field
    {
        public Soil(Form1 parent, int xPos, int yPos) : base(parent, xPos, yPos)
        {
            type = 0;
        }

        public override void nextRound()
        {
            if (humidity >= Constants.HUMIDITY_DECREASE)
            {
                humidity -= Constants.HUMIDITY_DECREASE;
            }

            if (RandomNumber.Between(0, 1000) <= Constants.RAIN_PROB)
            {
                humidity += Constants.RAIN_HUMIDITY_INCREASE;
            }

            bool neighbourNeighboursWater = false;
            bool neighboursGrass = false;
            bool neighboursDenseGrass = false;

            for (int i = 0; i < Constants.NEIGHBOURS_AMOUNT; i++)
            {
                for (int j = 0; j < Constants.NEIGHBOURS_AMOUNT; j++)
                {
                    if (neighbours[i].getNeighbours()[j].getType() == 3)
                    {
                        neighbourNeighboursWater = true;
                    }
                }

                if (neighbours[i].hasGrass() == true)
                {
                    neighboursGrass = true;
                }

                if (neighbours[i].hasDenseGrass() == true)
                {
                    neighboursDenseGrass = true;
                }
            }

            if (neighbourNeighboursWater && RandomNumber.Between(0, 1000) <= Constants.SAND_GETS_HUMID_PROB)
            {  
                if(humidity<500)
                {
                    humidity += 500;
                }
                else
                {
                    humidity = 1000;
                }
            }

            if (neighboursWater() && RandomNumber.Between(0, 1000) <= Constants.SOIL_GETS_HUMID_PROB)
            {
                humidity = 1000;
            }

            if (RandomNumber.Between(0, 1000) <= Constants.GRASS_GROWS_PROB)
            {
                grass = true;
            }
            else if (neighboursGrass && RandomNumber.Between(0, 1000) <= Constants.GRASS_GROWS_PROB * Constants.NEIGHBOURING_GRASS_BONUS)
            {
                grass = true;
            }
            else if (neighboursDenseGrass && RandomNumber.Between(0, 1000) <= Constants.GRASS_GROWS_PROB * Constants.NEIGHBOURING_DENSE_GRASS_BONUS)
            {
                grass = true;
            }
            else if (grass && !denseGrass && RandomNumber.Between(0, 1000) <= Constants.GRASS_DENSES_ON_SOIL_PROB)
            {
                denseGrass = true;
            }
        }
    }

    class Sand : Field
    {
        public Sand(Form1 parent, int xPos, int yPos) : base(parent, xPos, yPos)
        {
            type = 1;
        }

        public override void nextRound()
        {
            if(humidity>= Constants.HUMIDITY_DECREASE)
            {
                humidity -= Constants.HUMIDITY_DECREASE;
            }

            if(RandomNumber.Between(0, 1000) <= Constants.RAIN_PROB)
            {
                humidity += Constants.RAIN_HUMIDITY_INCREASE;
            }

            bool neighbourNeighboursWater = false;
            bool neighboursGrass = false;
            bool neighboursDenseGrass = false;
            bool neighboursStone= false;
            int neighbouringSoils = 0;

            for (int i=0; i<Constants.NEIGHBOURS_AMOUNT; i++)
            {
                for(int j = 0; j<Constants.NEIGHBOURS_AMOUNT; j++)
                {
                    if(neighbours[i].getNeighbours()[j].getType() == 3)
                    {
                        neighbourNeighboursWater = true;
                    }
                }

                if (neighbours[i].getType() == 2)
                {
                    neighboursStone = true;
                }

                if (neighbours[i].hasGrass() == true)
                {
                    neighboursGrass = true;
                }

                if (neighbours[i].hasDenseGrass() == true)
                {
                    neighboursDenseGrass = true;
                }

                if (neighbours[i].getType() == 0)
                {
                    neighbouringSoils ++;
                }
            }

            if(neighbourNeighboursWater && RandomNumber.Between(0, 1000) <= Constants.SAND_GETS_HUMID_PROB)
            {
                if (humidity < 500)
                {
                    humidity += 500;
                }
                else
                {
                    humidity = 1000;
                }
            }

            if(neighboursWater() && RandomNumber.Between(0, 1000) <= Constants.SAND_GETS_HUMID_PROB)
            {
                humidity = 1000;
            }

            if(RandomNumber.Between(0, 1000) <= Constants.GRASS_GROWS_PROB)
            {
                grass = true;
            }
            else if(neighboursGrass && RandomNumber.Between(0,1000) <= Constants.GRASS_GROWS_PROB*Constants.NEIGHBOURING_GRASS_BONUS)
            {
                grass = true;
            }
            else if(neighboursDenseGrass && RandomNumber.Between(0, 1000) <= Constants.GRASS_GROWS_PROB * Constants.NEIGHBOURING_DENSE_GRASS_BONUS)
            {
                grass = true;
            }
            else
            {
                if (humidity < Constants.MIN_HUMIDITY_FOR_MOIST)
                {
                    grass = false;
                    denseGrass = false;
                }
                else
                {
                    if(grass && !denseGrass && RandomNumber.Between(0, 1000) <= Constants.GRASS_DENSES_ON_SAND_PROB)
                    {
                        denseGrass = true;
                    }
                    else if(denseGrass && (neighboursStone || neighbouringSoils>=1)&& neighboursWater() && RandomNumber.Between(0, 1000) <= Constants.TURN_TO_SOIL_PROB)
                    {
                        turnToSoil();
                    }
                    else if(grass && neighbouringSoils >=2 && humidity >=500)
                    {
                        turnToSoil();
                    }
                    else if(grass && neighbouringSoils >= 1 && humidity >= 500 && RandomNumber.Between(0, 1000) <= Constants.TURN_TO_SOIL_PROB)
                    {
                        turnToSoil();
                    }
                }
            }
        }
    }

    class Stone : Field
    {
        public Stone(Form1 parent, int xPos, int yPos) : base(parent, xPos, yPos)
        {
            type = 2;
        }

        public override void nextRound()
        {
            if (RandomNumber.Between(0, 1000) <= Constants.GRASS_GROWS_PROB)
            {
                grass = true;
            }
            else
            {
                grass = false;
            }

            int neighbouringSoils = 0;
            for (int i = 0; i < Constants.NEIGHBOURS_AMOUNT; i++)
            {
                if (neighbours[i].getType() == 0)
                {
                    neighbouringSoils++;
                }
            }

            if (neighbouringSoils == 4)
            {
                turnToSoil();
            }
        }
    }

    class Water : Field
    {
        public Water(Form1 parent, int xPos, int yPos) : base(parent, xPos, yPos)
        {
            type = 3;
            humidity = 1000;
        }

        public override void nextRound()
        {
            if (RandomNumber.Between(0, 1000) <= Constants.GRASS_GROWS_PROB)
            {
                grass = true;
            }
            else
            {
                grass = false;
            }
        }
    }

    abstract class Animal : Entity
    {
        protected Field locationField;
        protected int thirst;
        protected int hunger;

        public Animal(Field locationField) : base()
        {
            this.locationField = locationField;
            thirst = 0;
            hunger = 0;
        }

        public int getXPos()
        {
            return locationField.getXPos();
        }

        public int getYPos()
        {
            return locationField.getYPos();
        }

        public Field getLocationField()
        {
            return locationField;
        }

        protected abstract void moveNorth();
        protected abstract void moveSouth();
        protected abstract void moveEast();
        protected abstract void moveWest();
        protected abstract void stay();

        protected abstract void die();
        protected abstract void reproduce();

        protected static ArrayList selectForReproduction(ArrayList competitors)
        {
            if(competitors.Count<=1)
            {
                throw new NotEnoughAnimalsException();
            }
            else
            {
                ArrayList selectedAnimals = new ArrayList();
                int firstIndex=0, secondIndex=0;
                
                for(int i = 0; i< competitors.Count; i++)
                {
                    if(((Animal)competitors[i]).getFitness()> ((Animal)competitors[firstIndex]).getFitness())
                    {
                        secondIndex = firstIndex;
                        firstIndex = i;
                    }
                    else if(((Animal)competitors[i]).getFitness() > ((Animal)competitors[secondIndex]).getFitness())
                    {
                        secondIndex = i;
                    }
                }

                selectedAnimals.Add((Animal)competitors[firstIndex]);
                selectedAnimals.Add((Animal)competitors[secondIndex]);

                return selectedAnimals;
            }
        }

        public int getFitness()
        {
            return (1000 - hunger) * (1000 - thirst);
        }
    }

    class Sheep: Animal
    {
        public Sheep(Field locationField) : base(locationField)
        {
            locationField.setAnimal(this);
        }

        protected override void moveNorth()
        {
            locationField.setAnimal(null);

            locationField = locationField.getNorthernNeighbour();
            locationField.setAnimal(this);
        }

        protected override void moveSouth()
        {
            locationField.setAnimal(null);

            locationField = locationField.getSouthernNeighbour();
            locationField.setAnimal(this);
        }

        protected override void moveEast()
        {
            locationField.setAnimal(null);

            locationField = locationField.getEasternNeighbour();
            locationField.setAnimal(this);
        }

        protected override void moveWest()
        {
            locationField.setAnimal(null);

            locationField = locationField.getWesternNeighbour();
            locationField.setAnimal(this);
        }

        protected override void stay()
        {
            if(locationField.neighboursWater())
            {
                thirst = 0;
            }
            else if(locationField.getType()==0)
            {
                if(locationField.hasGrass())
                {
                    if (locationField.hasDenseGrass())
                    {
                        locationField.removeDenseGrass();
                        if(hunger<=300)
                        {
                            hunger = 0;
                        }
                        else
                        {
                            hunger -= 300;
                        }
                    }
                    else
                    {
                        locationField.removeGrass();
                        if (hunger <= 200)
                        {
                            hunger = 0;
                        }
                        else
                        {
                            hunger -= 200;
                        }
                    }
                }
            }
            else if(locationField.getType()==1)
            {
                if (locationField.hasGrass())
                {
                    if (locationField.hasDenseGrass())
                    {
                        locationField.removeDenseGrass();
                        if (hunger <= 200)
                        {
                            hunger = 0;
                        }
                        else
                        {
                            hunger -= 200;
                        }
                    }
                    else
                    {
                        locationField.removeGrass();
                        if (hunger <= 100)
                        {
                            hunger = 0;
                        }
                        else
                        {
                            hunger -= 100;
                        }
                    }
                }
            }
        }

        public override void nextRound()
        {
            thirst += ((100 *(Constants.MAX_HUMIDITY-locationField.getHumidity())) /Constants.MAX_HUMIDITY);
            hunger += 50;

            /*
             * What should be taken into account:
             * thirst
             * hunger
             * locationField type
             * locationField hasGrass
             * locationField hasDenseGrass
             * locationField humidity
             * locationField areaHumidity (average humidity of locationField and it's neighbours)
             * neighbour0 type
             * neighbour0 hasGrass
             * neighbour0 hasDenseGrass
             * neighbour0 humidity
             * neighbour0 areaHumidity
             * neighbour0 hasAnimal
             * neighbour1 type
             * neighbour1 hasGrass
             * neighbour1 hasDenseGrass
             * neighbour1 humidity
             * neighbour1 areaHumidity
             * neighbour1 hasAnimal
             * neighbour2 type
             * neighbour2 hasGrass
             * neighbour2 hasDenseGrass
             * neighbour2 humidity
             * neighbour2 areaHumidity
             * neighbour2 hasAnimal
             * neighbour3 type
             * neighbour3 hasGrass
             * neighbour3 hasDenseGrass
             * neighbour3 humidity
             * neighbour3 areaHumidity
             * neighbour3 hasAnimal
             */


            int direction = RandomNumber.Between(0, 5);

            switch(direction)
            {
                default:
                    stay();
                    break;
                case 1:
                    if(!locationField.getNorthernNeighbour().hasAnimal()&&locationField.getNorthernNeighbour().getType() != 3)
                    {
                        moveNorth();
                    }
                    else
                    {
                        stay();
                    }
                    break;
                case 2:
                    if (!locationField.getSouthernNeighbour().hasAnimal() && locationField.getSouthernNeighbour().getType() != 3)
                    {
                        moveSouth();
                    }
                    else
                    {
                        stay();
                    }
                    break;
                case 3:
                    if (!locationField.getEasternNeighbour().hasAnimal() && locationField.getEasternNeighbour().getType() != 3)
                    {
                        moveEast();
                    }
                    else
                    {
                        stay();
                    }
                    break;
                case 4:
                    if (!locationField.getWesternNeighbour().hasAnimal() && locationField.getWesternNeighbour().getType() != 3)
                    {
                        moveWest();
                    }
                    else
                    {
                        stay();
                    }
                    break;
            }
            
            if(thirst >=1000)
            {
                die();
            }
            else if(hunger >=1000)
            {
                die();
            }
            else
            {

            }
        }

        protected override void die()
        {
            locationField.setAnimal(null);
            locationField.getParent().getAnimalsToKill().Add(this);   
        }

        protected override void reproduce()
        {
            throw new NotImplementedException();
        }
    }

    class NotEnoughAnimalsException : Exception
    {

    }
}
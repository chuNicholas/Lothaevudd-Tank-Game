//Ernest, Gabriel, Nicholas
//January 27, 2017
//Main Class
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Drawing;

using System.IO;

namespace LothaevuddTankGameForm
{
    class BattleField
    {
        //The pixels size of the projectile
        public const int PROJECTILE_SIZE = 5;
        //The pixels size of the tank
        public const int TANKS_SIZE = 20;
        //The pixel size of the mine
        public const int MINE_SIZE = 5;
        //The pixel size of the tile
        public const int TILE_SIZE = 25;
        //The pixel size of the mine blast
        public const int MINE_BLAST_RADIUS = 50;
        //Important for loading stage map
        //The amount of tile types
        private const int NUMBER_OF_TILES_TYPES = 9;
        //Create the battlefield interface
        private MapTile[,] _battlefield = new MapTile[32, 20];
        private int _level;
        private Size _tileSize = new Size(TILE_SIZE, TILE_SIZE);
        private List<Projectile> _projectile = new List<Projectile>();
        private Size _projectileSize = new Size(PROJECTILE_SIZE, PROJECTILE_SIZE);
        private List<Mine> _mine = new List<Mine>();


        // variabel to hold the high scores
        private int _score = 0;

        //The tank controlled by player
        private Tank _playerTank;
        private List<AITanks> _enemyTank = new List<AITanks>();

        


        /// <summary>
        /// Return the Tile Size
        /// </summary>
        public Size TileSize
        {
            get
            {
                return _tileSize;
            }
        }
        /// <summary>
        /// Create Battlefield Object
        /// </summary>
        public BattleField(int level)
        {
            _level = level;

            LoadMapString((Directory.GetParent((Directory.GetParent(Environment.CurrentDirectory)).ToString()) + "/level" + _level + ".txt").ToString());
        }

        /// <summary>
        /// Reads and loads the file into the battlefield interface
        /// </summary>
        /// <param name="mapString">The file name that contains the map stage</param>
        private void LoadMapString(string mapString)
        {

            //Store the lines in the text file
            string[] lines;
            //Count the amount of line in the text file
            int count = 0;




            //This is to get the amount of lines in the file
            //Read the file that contains the stage
            using (StreamReader file = new StreamReader(mapString))
            {
                //Check if there is a line and loop
                while (file.Peek() != -1)
                {
                    //Goes to next line
                    file.ReadLine();
                    ////Increase count of lines
                    count++;
                }
                //State the size of the int array
                lines = new string[count];

            }
            //Read the file to store information of the file's lines
            using (StreamReader file = new StreamReader(mapString))
            {
                //Loop to save the file lines to the array
                for (int i = 0; i < count; i++)
                {
                    //convert file line to int and store it in the array
                    lines[i] = file.ReadLine();
                }

            }
            //Nested loop for creating the map tiles in the battlefield interface
            //Checking each row of battlefield
            int col = _battlefield.GetLength(0);
            int row = _battlefield.GetLength(1);
            int tankCounter = 0;
            for (int y = 0; y < row; y++)
            {
                //Checking each column of battlefield
                for (int x = 0; x < col; x++)
                {
                    //Create char values for each number in the file line
                    char[] line = lines[y].ToCharArray();
                    lines[y] = lines[y].Trim();
                    //Convert each char value into int
                    int value = (int)char.GetNumericValue(line[x]);
                    //Check if it is a valid tile type value
                    if (value <= NUMBER_OF_TILES_TYPES)
                    {
                        //Create battlefield interface
                        _battlefield[x, y] = (MapTile)value;
                        PointF location = new PointF(x * TILE_SIZE, y * TILE_SIZE);
                        //Check if battlefield tile is the starting location
                        if (_battlefield[x, y] == MapTile.StartingLocation)
                        {
                            //Create player on starting location battlefield tile
                            _playerTank = new Tank(x * TILE_SIZE, y * TILE_SIZE, Brushes.Black);
                        }
                        if(_battlefield[x,y] == MapTile.NormalTank)
                        {
                            tankCounter++;
                            _enemyTank.Add(new AITanks(x * TILE_SIZE, y * TILE_SIZE, 2, location, Brushes.Brown));
                        }
                        if(_battlefield[x,y] == MapTile.RocketTankStartLocation)
                        {
                            tankCounter++;
                            //_enemyTank.Add(new )                                               
                        }
                        if (_battlefield[x,y] == MapTile.DisabledTankStartTank)
                        {
                            tankCounter++;
                            _enemyTank.Add(new AserranDisabledTank(x * TILE_SIZE, y * TILE_SIZE, 2, location, Brushes.Brown));
                        }
                    }
                }
            }


       

        }
        /// <summary>
        /// Create the player
        /// </summary>
        /// <param name="col">The column of the tile where the player starts</param>
        /// <param name="row">The row of the tile where the player starts</param>
        /// <param name="colour">The starting colour of the tank</param>
        public void MakePlayer(float col, float row, Brush colour)
        {
            _playerTank = new Tank(col, row, colour);
        }

        /// <summary>
        /// Get the player tank
        /// </summary>
        public Tank Player
        {
            get
            {
                return _playerTank;
            }
        }
        /// <summary>
        /// To move the projectile
        /// </summary>
        private void MoveProjectile()
        {
            //Loop through each projectile in the battlefield
            for (int i = 0; i < _projectile.Count; i++)
            {





                //Move the projectile
                PointF projectilePoint = _projectile[i].Move();
                //Convert float to integer
                //Column
                int projectileCol = (int)(projectilePoint.X / TileSize.Height);
                //Row
                int projectileRow = (int)(projectilePoint.Y / TileSize.Width);



                //loop through all enemies in the list of enemies 
                for (int k = 0; k < AiTank.Count; k++)
                {

                    // check when the projectile hits the enemy 
                    if (_projectile[i].Hitbox.IntersectsWith(AiTank[k].Hitbox))
                    {

                        //kill the enemy by making isDead true 
                        AiTank[k].IsDead = true;

                        //Remove the projectile if it hits an enemy 
                        _projectile.Remove(_projectile[i]);
                    }
                }


                //Case 1: projectile is out of the window screen
                if (projectileCol >= _battlefield.GetLength(0) || projectileRow >= _battlefield.GetLength(1) || projectileRow < 0 || projectileCol < 0)
                {
                    //Remove the projectile
                    _projectile.Remove(_projectile[i]);

                }
                //Case 2: Projectile hits a wall, or destructable wall, or border
                else if (_battlefield[projectileCol, projectileRow] == MapTile.DestructableWall || _battlefield[projectileCol, projectileRow] == MapTile.Wall || _battlefield[projectileCol, projectileRow] == MapTile.Border)
                {
                    //Check if the projectile hits wall
                    if (_projectile[i].BounceCounter < _projectile[i].MaxBounce)
                    {
                        //Projectile gets bounced
                        _projectile[i].SpeedX = -_projectile[i].SpeedX;
                        _projectile[i].SpeedY = -_projectile[i].SpeedY;
                        //Projectile bounce counter increases by one
                        _projectile[i].BounceCounter++;
                    }
                    else
                    {
                        //Remove projectile from battlefield
                        _projectile.Remove(_projectile[i]);
                    }
                }

                //check if the projectile intersects with the enemy 

                //else if ((_playerTank.Location.X + 10) == Projectile[i].Location.X &&)
                //check collisions
                /*
                if (_projectile[i].Hitbox.IntersectsWith(_playerTank.Hitbox))
                {
                    if (_playerTank.PlayerLives <= 0)
                    {
                        _playerTank.IsDead = true;
                    }
                    else
                    {
                        _playerTank.PlayerLives--;
                        _projectile.Clear();
                        _enemyTank.Clear();
                        _mine.Clear();
                        LoadMapString((Directory.GetParent((Directory.GetParent(Environment.CurrentDirectory)).ToString()) + "/level" + _level + ".txt").ToString());
                    }
                }
                 */


         
                
                
            }
        }
        /// <summary>
        /// Get the battlefield interface
        /// </summary>
        public MapTile[,] BattlefieldMap
        {
            get
            {
                return _battlefield;
            }
            set
            {

            }
        }
        /// <summary>
        /// Adds a projectile to the battlefield
        /// </summary>
        /// <param name="projectile">The projectile of the tank</param>
        public void AddProjectile(Projectile projectile)
        {
            _projectile.Add(projectile);
        }
        /// <summary>
        /// To update the battlefield changes
        /// </summary>
        public void Update()
        {

            NextLevel();
            MoveProjectile();
            ExplodeMine();


        }
        /// <summary>
        /// Get size of the projectile
        /// </summary>
        public Size ProjectileSize
        {
            get
            {
                return _projectileSize;
            }
        }
        /// <summary>
        /// Get the list of projectiles
        /// </summary>
        public List<Projectile> Projectile
        {
            get
            {
                return _projectile;

            }

        }
        /// <summary>
        /// allows other classes to get the Mine List
        /// </summary>
        public List<Mine> Mine
        {
            get
            {
                return _mine;
            }
        }
        /// <summary>
        /// Add a mine object
        /// </summary>
        /// <param name="mine"></param>
        public void AddMine(Mine mine)
        {
            _mine.Add(mine);
        }
        /// <summary>
        /// Use a timer to blow up mine, check for 4000 milleseconds (4 seconds)
        /// if it exceeds or is equal to 4000 blow the mine up and remove 
        /// </summary>
        public void ExplodeMine()
        {
            int currentTime = Environment.TickCount;

            for (int i = 0; i < Mine.Count; i++)
            {
                if (currentTime - Mine[i].Time >= 4000)
                {
                    Mine[i].Explode(this);
                    Mine.Remove(Mine[i]);
                }
            }
        }

        public List<AITanks> AiTank
        {
            get
            {
                return _enemyTank;
            }
        }
        /// <summary>
        /// Loop through the list of enemies if applicable and if all are dead return true
        /// </summary>
        public bool AIAllDead
        {
            get
            {
                //Check if all enemies are dead
                bool isDead = false;

                for (int i = 0; i < AiTank.Count(); i++)
                {
                    if (AiTank[i].IsDead == false)
                    {
                        break;
                    }
                    else
                    {
                        isDead = true;
                    }
                }


                return isDead;
            }
        }
        /// <summary>
        /// Get / Set Current Level
        /// </summary>
        public int Level
        {
            get
            {
                return _level;
            }

            set
            {
                _level = value;
            }
        }



        /// <summary>
        /// Load the new map when player finishes level
        /// </summary>
        public void NextLevel()
        {

            //check if all the enemies on the current level are dead
            if (AIAllDead == true)
            {
                //increase the level by 1
 
                _level++;
                
                //delete all the projectiles from the list, which deletes all from the screen 
                _projectile.Clear();
                //remove all previous enemies from the list 
                AiTank.Clear();
                //remove all mines in the level (list)
                Mine.Clear();


                //send the current level the player is on, to the save file
                using (StreamWriter input = new StreamWriter((Directory.GetParent((Directory.GetParent(Environment.CurrentDirectory)).ToString()) + "/Save.txt")))
                {

                    input.Write(Level);

                }
            
                //draw the new map 
                LoadMapString((Directory.GetParent((Directory.GetParent(Environment.CurrentDirectory)).ToString()) + "/level" + _level + ".txt").ToString());

            }



        }

   

        /// <summary>
        /// Save the Highscore of the player || W.I.P.
        /// </summary>
        /// <param name="playerScore"></param>
        public void SaveHighscore(int playerScore)
        {
            //Store high scores 
            Queue<string> highScores = new Queue<string>();
            //make sure it doesn't crash
            try
            {
                using (StreamReader input = new StreamReader((Directory.GetParent((Directory.GetParent(Environment.CurrentDirectory)).ToString()) + "/Highscores").ToString()))
                {
                    while (input.Peek() != -1)
                    {
                        highScores.Enqueue(input.ReadLine());
                    }
                    //allow overwrite
                    using (StreamWriter output = new StreamWriter((Directory.GetParent((Directory.GetParent(Environment.CurrentDirectory)).ToString()) + "/Highscores").ToString(), true))
                    {
                        //check queue from top
                        while (highScores.Count != -1)
                        {
                            string temp = highScores.Dequeue();
                            int score;
                            //Initials (space) score
                            //012/
                            //Score starts at index 3
                            int.TryParse(temp.Substring(3, temp.Length - 3), out score);
                            //check if user scored higher than score
                            if (playerScore > score)
                            {
                                //If so input the player initials and score and stop loop
                                output.WriteLine(playerScore);
                                break;
                            }
                        }
                    }
                }
            }
            //if file can't be read or error occurs
            catch (Exception e)
            {
                throw new Exception("File could not be read/ found");
            }
        }



    }
}

/* Ernest Gabriel Nicholas
 * January 25, 2017
 * Emulate the Wii Play Tanks Game
 * **Note: Incomplete Game; on the bright side, this is the first project I haven't finished in
 *  the 3 years I have taken this course - nick :(**
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace LothaevuddTankGameForm
{
    public partial class LothauvuddTankGame : Form
    {

        private bool moveUp = false;
        private bool moveDown = false;
        private bool moveRight = false;
        private bool moveLeft = false;


        //the level to start of at
        private int _startLevel;

        //The point location of the mouse
        private static PointF mouseLocation;

        //create a battlefield 
        BattleField battlefield;

        // tank object;
        Tank player;

        //array of enemy tanks
        AITanks[] enemies;

        

        //Constructor of the battle interface
        public LothauvuddTankGame()
        {
            InitializeComponent();
        }



        private void Setup()
        {
            //Create battlefield map and the player and enemies 
            //loop through all possible enemies and create them 
            battlefield = new BattleField(_startLevel);
            player = battlefield.Player;
            enemies = new AITanks[battlefield.AiTank.Count];
            for (int i = 0; i < battlefield.AiTank.Count; i++)
            {
                enemies[i] = battlefield.AiTank[i];
            }


            //enable the timer to start game 
            tmrGameTimer.Enabled = true;

            //Brush colour = new Brush(cboColour.ToString());

            //cboColour


        }
        
        //create on paint to draw grpahics 
        protected override void OnPaint(PaintEventArgs e)
        {   
            base.OnPaint(e);
            
        }

        // function to draw the map ( each tile ) 
        private void DrawMap()
        {
           
            BufferedGraphics e = BufferedGraphicsManager.Current.Allocate(this.CreateGraphics(), this.DisplayRectangle);


            // set the row and col variable to the length of the battlefield
            int numRows = battlefield.BattlefieldMap.GetLength(0);
            int numCols = battlefield.BattlefieldMap.GetLength(1);
            ClientSize = new Size(numRows * battlefield.TileSize.Width, numCols * battlefield.TileSize.Height);


            //loop through the col and rows for the battlfield 
            for (int y = 0; y < numCols; y++)
            {
                for (int x = 0; x < numRows; x++)
                {
                    //set the size of a rectangle tile 
                    Rectangle tile = new Rectangle(battlefield.TileSize.Width* x, battlefield.TileSize.Height * y, battlefield.TileSize.Width, battlefield.TileSize.Height);

                    //if the tile at x and y is a border make the tile salmoned colour 
                    if (battlefield.BattlefieldMap[x, y] == MapTile.Border)
                    {
                        e.Graphics.FillRectangle(Brushes.Salmon, tile);
                    }
                    //if the tile is a destructable wall make it a nice sea green colour 
                    else if (battlefield.BattlefieldMap[x, y] == MapTile.DestructableWall)
                    {
                        e.Graphics.FillRectangle(Brushes.SeaGreen, tile);
                    }
                    //if the tile is a wall make it a nice yellow colour 
                    else if (battlefield.BattlefieldMap[x, y] == MapTile.Wall)
                    {
                        e.Graphics.FillRectangle(Brushes.Yellow, tile);
                    }
                    //if the tile is a normal enemy tank starting location 
                    else if (battlefield.BattlefieldMap[x,y] == MapTile.NormalTank)
                    {
                        //colour the tile blue (not the tank)
                        e.Graphics.FillRectangle(Brushes.Blue, tile);
                        //loop throught the aitank list and call the create enemy graphic function 
                        for (int i = 0; i < battlefield.AiTank.Count; i++)
                        {
                            if (battlefield.AiTank[i] is AITanks)
                            {
                                CreateEnemiesGraphic(battlefield.AiTank[i]);
                            }
                        }
                    }
                    //if the tile is a rocket tank enemy starting location 
                    else if (battlefield.BattlefieldMap[x,y] == MapTile.RocketTankStartLocation)
                    {
                        //colour the tile blue (not the tank)
                        e.Graphics.FillRectangle(Brushes.Blue, tile);
                        //loop through the aitank list and call the createenemygraphic function
                        for (int i = 0; i < battlefield.AiTank.Count; i++)
                        {
                            if (battlefield.AiTank[i] is AITanks)
                            {
                                CreateEnemiesGraphic(battlefield.AiTank[i]);
                            }
                        }
                    }
                    //if the tile is a disabled enemy tank starting location 
                    else if (battlefield.BattlefieldMap[x,y] == MapTile.DisabledTankStartTank)
                    {
                        //colour the tile blue (not the tank)
                        e.Graphics.FillRectangle(Brushes.Blue, tile);
                        //loop through the aitank list and call the createenemygraphic function
                        for (int i = 0; i < battlefield.AiTank.Count; i++)
                        {
                            // if the tile is a disabled tank draw the enemy tank out and the barrel
                            if (battlefield.AiTank[i] is AserranDisabledTank)
                            {
                                //draw the disabled tank with barrel 
                                PointF[] points = CreateEnemiesGraphic(battlefield.AiTank[i]);
                                PointF[] enemyBarrelPoints = CreateEnemyBarrelGraphic(battlefield.AiTank[i]);
                                e.Graphics.FillPolygon(Brushes.Brown, points);
                                e.Graphics.FillPolygon(Brushes.Brown, enemyBarrelPoints);
                                

                            }
                        }
                    }
                    else if (battlefield.BattlefieldMap[x,y] == MapTile.BossTankStartLocation)
                    {

                        //colour the tile blue (not the tank)
                        e.Graphics.FillRectangle(Brushes.Blue, tile);
                        //loop through the aitank list and call the createenemygraphic function
                        for (int i = 0; i < battlefield.AiTank.Count; i++)
                        {
                            //if the tile is a boss tank starting location 
                            if (battlefield.AiTank[i] is USSAdaevielTank)
                            {
                                PointF[] points = CreateEnemiesGraphic(battlefield.AiTank[i]);
                                e.Graphics.FillPolygon(Brushes.Brown, points);
                            }
                        }
                    }
                    //if the tiel is a bogo tank starting tile 
                    else if (battlefield.BattlefieldMap[x,y] == MapTile.BogoTankStartLocation)
                    {
                        //for (int i = 0; i < battlefield.AiTank.Count; i++)
                        //{
                        //    if (battlefield.AiTank[i] is AserranDisabledTank)
                        //    {
                        //        CreateEnemiesGraphic(battlefield.AiTank[i]);
                        //    }
                        //}
                    }
                    //if it is not any of the other tiles it is an emmty tile
                    else
                    {
                        //fill it in with a nice blue 
                        e.Graphics.FillRectangle(Brushes.Blue, tile);
                    }

                }
            }
            //loop through all the projectiles 
            for (int i = 0; i < battlefield.Projectile.Count; i++)
            {
                // fill in the projectile with an old beige colour
                e.Graphics.FillEllipse(Brushes.Beige, battlefield.Projectile[i].Location.X, battlefield.Projectile[i].Location.Y, battlefield.ProjectileSize.Width, battlefield.ProjectileSize.Height);
            }

            //get the array of tank points from the createtankgraphic function and pass in the player 
            PointF[] tankPoints = CreateTankGraphic(player);

            //get the array of the barrel points from create barrelgraphic and pass in the player
            PointF[] barrelPoints = CreateTankBarrelGraphic(player);

            //loop throuh the amount of mines on the battlefield
            for (int i = 0; i < battlefield.Mine.Count; i++)
            {
                //get the array of mine points and pass in the mines on the battlefield
                //fill in the mine with black 
                PointF[] minePoints = CreateMineGraphic(battlefield.Mine[i]);
                e.Graphics.FillPolygon(Brushes.Black, minePoints);

                //create a variable for the current time in the game 
                int currentTime = Environment.TickCount;

                // get the array of the explosion graphic and mass in the mine array 
                PointF[] mineExplodePoints = CreateMineExplosionGraphic(battlefield.Mine[i]);

                //if the current time after a mine is placed down is right before 4 seconds
                // show the explosion graphic 
                if (currentTime - battlefield.Mine[i].Time > 3950)
                { 
                    // fill in the explosion with a deep orangered
                    e.Graphics.FillPolygon(Brushes.OrangeRed, mineExplodePoints);
                }

            }




            //draw out the player and the barrel of the player 
            e.Graphics.FillPolygon(player.Colour, barrelPoints);
            e.Graphics.FillPolygon(player.Colour, tankPoints);

            //if the plaer is dead then stop the timer and display the player dead message 
            if (player.IsDead)
            {
                tmrGameTimer.Enabled = false; 
                MessageBox.Show("You have died!");
            }
            
            battlefield.Update();
            e.Render();
            e.Dispose();

        }
        /// <summary>
        /// creates the enemy graphic by fetching the 4 points of the enemy 
        /// </summary>
        /// <param name="tank">pass in the position of the enemy tank</param>
        /// <returns>return the array of enemytank points</returns>
        private PointF[] CreateEnemiesGraphic(AITanks tank)
        {
            //create an array of enemy tank points
            PointF[] tankPoints = new PointF[4];
            //se the points of the enemy tank 
            tankPoints[0] = new PointF(tank.Location.X, tank.Location.Y);
            tankPoints[1] = new PointF(tank.Location.X + tank.Size.Width, tank.Location.Y);
            tankPoints[2] = new PointF(tank.Location.X + tank.Size.Width, tank.Location.Y + tank.Size.Height);
            tankPoints[3] = new PointF(tank.Location.X, tank.Location.Y + tank.Size.Height);
            return tankPoints;
        }

        /// <summary>
        /// create the player tank graphic by fetching the 4 points of it 
        /// </summary>
        /// <param name="tank">pass in the location of the player tank </param>
        /// <returns> return the array of player tank points </returns>
        private PointF[] CreateTankGraphic(Tank tank)
        {
            // create an array of points for the player tank 
            PointF[] tankPoints = new PointF[4];
            //set the location of the 4 tank points 
            tankPoints[0] = new PointF(tank.Location.X, tank.Location.Y);
            tankPoints[1] = new PointF(tank.Location.X + tank.Size.Width, tank.Location.Y);
            tankPoints[2] = new PointF(tank.Location.X + tank.Size.Width, tank.Location.Y + tank.Size.Height);
            tankPoints[3] = new PointF(tank.Location.X, tank.Location.Y + tank.Size.Height);           

            return tankPoints;
        }

        /// <summary>
        /// create the mine graphic by fatching the 4 points of the mine
        /// </summary>
        /// <param name="mine"> pass in the mine location</param>
        /// <returns> return the array of mine points </returns>
        private PointF[] CreateMineGraphic(Mine mine)
        {

            // make a pointF array to store the values for the mine 
            PointF[] minePoints = new PointF[4];

            // top left mine point
            minePoints[0] = new PointF(mine.Location.X, mine.Location.Y);


            // top right point
            minePoints[1] = new PointF(mine.Location.X + 10, mine.Location.Y);

            //bottom right point
            minePoints[2] = new PointF(mine.Location.X + 10, mine.Location.Y + 10);

            // bottom left point
            minePoints[3] = new PointF(mine.Location.X , mine.Location.Y + 10);


            return minePoints;

        }

        /// <summary>
        /// create the explosion by fetching the 4 points of it 
        /// </summary>
        /// <param name="mine"> pass in the mine for location </param>
        /// <returns> return the array of explosion points </returns>
        private PointF[] CreateMineExplosionGraphic(Mine mine)
        {
            // make a pointF array to store the values for the mine 
            PointF[] mineExplodePoints = new PointF[4];

            // top left mine explosion point
            mineExplodePoints[0] = new PointF(mine.Location.X - 25, mine.Location.Y - 25);

            // top right explosion point
            mineExplodePoints[1] = new PointF(mine.Location.X + 35, mine.Location.Y - 25);

            //bottom right explosion point
            mineExplodePoints[2] = new PointF(mine.Location.X + 35, mine.Location.Y + 25);

            // bottom left explosion point
            mineExplodePoints[3] = new PointF(mine.Location.X - 25, mine.Location.Y + 25);

            return mineExplodePoints;
        }

        /// <summary>
        /// create the graphic for the enemy barrel by fetching the 4 points 
        /// </summary>
        /// <param name="tank"> pass in the location of the enemy tank </param>
        /// <returns></returns>
        private PointF[] CreateEnemyBarrelGraphic(AITanks tank)
        {
            //make array for the barrel points 
            Point[] basePoints = new Point[4];

            // if the player is on the right side of the enemy 
            // add one to see if it fixes graphic glitch
            if (player.Location.X > tank.Location.X + 1)
            {

                // top left corner 
                basePoints[0] = new Point(-5, -2);

                //top right corner 
                basePoints[1] = new Point(25, -2);

                //bottom right corner
                basePoints[2] = new Point(25, 2);

                //bottom left corner 
                basePoints[3] = new Point(-5, 2);
            }

            //if the player is on the right side of the enemy 
            //add one to see if it fixes graphics glitch 
            else if (player.Location.X < tank.Location.X + 1)
            {

                // top left corner 
                basePoints[0] = new Point(-25, -2);

                //top right corner 
                basePoints[1] = new Point(5, -2);

                //bottom right corner
                basePoints[2] = new Point(5, 2);

                //bottom left corner 
                basePoints[3] = new Point(-25, 2);
            }

            // when the player is directly above or below the enemy
            else if (player.Location.X == tank.Location.X)
            {

                //when the player is below the enemy
                if (player.Location.Y > tank.Location.Y)
                {
                    // top left corner 
                    basePoints[0] = new Point(-25, -2);

                    //top right corner 
                    basePoints[1] = new Point(5, -2);

                    //bottom right corner
                    basePoints[2] = new Point(5, 2);

                    //bottom left corner 
                    basePoints[3] = new Point(-25, 2);

                }

                // when the player is above the eenemy 
                else
                {
                    // top left corner 
                    basePoints[0] = new Point(-5, -2);

                    //top right corner 
                    basePoints[1] = new Point(25, -2);

                    //bottom right corner
                    basePoints[2] = new Point(25, 2);

                    //bottom left corner 
                    basePoints[3] = new Point(-5, 2);

                }

            }

            //calculate the theta angle for the barrel calcualtions 
            float angle = -(float)Math.Atan((tank.Location.Y - player.Location.Y + (tank.Size.Height / 2)) / (player.Location.X - tank.Location.X - (tank.Size.Width / 2)));
            //Barrel Points Array for the modified points 
            PointF[] barrelPoints = new PointF[4];

            // put the cos and sin theta calculations in their own variables to make equation less messy 
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            // put the base points into the barrel point
            for (int i = 0; i < 4; i++)
            {

                //calculate where the points need to go and move the barrel to where the enemy is
                barrelPoints[i] = new PointF(basePoints[i].X * cos + basePoints[i].Y * sin + tank.Location.X + 10, basePoints[i].X * sin - basePoints[i].Y * cos + tank.Location.Y + 10);
            }

            return barrelPoints;

        }

        /// <summary>
        /// create the graphic of the player barrel by fetching the 4 points of it 
        /// </summary>
        /// <param name="tank"> pass in the player tank location </param>
        /// <returns> return the array of the barrel points for the player </returns>
        private PointF[] CreateTankBarrelGraphic(Tank tank)
        {

            //make array for the barrel ppints 
            Point[] basePoints = new Point[4];

            // right side 
            if (mouseLocation.X  > player.Location.X )
            {

                // top left corner 
                basePoints[0] = new Point(-5, -2);

                //top right corner 
                basePoints[1] = new Point(25, -2);

                //bottom right corner
                basePoints[2] = new Point(25, 2);

                //bottom left corner 
                basePoints[3] = new Point(-5, 2);
            }

            //left side 
            else if ( mouseLocation.X < player.Location.X )
            {

                // top left corner 
                basePoints[0] = new Point(-25, -2);

                //top right corner 
                basePoints[1] = new Point(5, -2);

                //bottom right corner
                basePoints[2] = new Point(5, 2);

                //bottom left corner 
                basePoints[3] = new Point(-25, 2);
            }
            else if ( mouseLocation.X == player.Location.X)
            {
                if( mouseLocation.Y > player.Location.Y )
                {
       

                    // top left corner 
                    basePoints[0] = new Point(-5, -2);

                    //top right corner 
                    basePoints[1] = new Point(25, -2);

                    //bottom right corner
                    basePoints[2] = new Point(25, 2);

                    //bottom left corner 
                    basePoints[3] = new Point(-5, 2);

                }

                else
                {
                    // top left corner 
                    basePoints[0] = new Point(-25, -2);

                    //top right corner 
                    basePoints[1] = new Point(5, -2);

                    //bottom right corner
                    basePoints[2] = new Point(5, 2);

                    //bottom left corner 
                    basePoints[3] = new Point(-25, 2);

                }

            }


            
            float angle = -(float)Math.Atan((player.Location.Y - mouseLocation.Y + (player.Size.Height / 2)) / (mouseLocation.X - player.Location.X - (player.Size.Width / 2)));
            //float angle = (float)Math.Atan((mouseLocation.Y + player.Location.Y + (player.Size.Height / 2)) / (mouseLocation.X - player.Location.X + (player.Size.Width / 2)));
            //Barrel Points Array for the modified points 
            PointF[] barrelPoints = new PointF[4];
            //Barrel X Values are 8 pixels from tanks x value and 8 pixels from tanks x value plus width
            //Barrel Y values are 20 pixels from tank y value plus height divided by 2
          
            float cos = (float)Math.Cos(angle);
            float sin = (float)Math.Sin(angle);

            // put the base points into the barrel point
            for ( int i = 0; i < 4; i++)
            {
                barrelPoints[i] = new PointF(basePoints[i].X * cos + basePoints[i].Y * sin + player.Location.X + 10, basePoints[i].X * sin - basePoints[i].Y * cos + player.Location.Y + 10);


            }

            return barrelPoints;
        }

        // keyup command for when a key is released 
        private void LothaevuddTankGame_KeyUp(object sender, KeyEventArgs e)
        {
            //Stop moving for player tank, make the move bool false 
            //when the user lets go of the respective WASD keys 
            if (e.KeyCode == Keys.W)
            {
                moveUp = false; 
            }
            else if (e.KeyCode == Keys.A)
            {
                moveLeft = false; 
            }
            else if (e.KeyCode == Keys.S)
            {
                moveDown = false; 
            }
            else if (e.KeyCode == Keys.D)
            {
                moveRight = false; 
            }
        }

        //keydown commadn for when a user presses down a key 
        private void LothaevuddTankGame_KeyDown(object sender, KeyEventArgs e)
        {

            //Movement for the player tank makes the bool
            // true for when the user pressed the repective
            //WASD keys
            if (e.KeyCode == Keys.W)
            {
                moveUp = true;

            }
            else if (e.KeyCode == Keys.A)
            {
                moveLeft = true;

            }
            else if (e.KeyCode == Keys.S)
            {
                moveDown = true;

            }
            else if (e.KeyCode == Keys.D)
            {
                moveRight = true; 

            }
            //if they press spacebar call the mine functions
            if (e.KeyCode == Keys.Space)
            {
                player.Mine(player.Location, battlefield);          
            }

        }

        private void LothaevuddTankGame_MouseMove(object sender, MouseEventArgs e)
        {
            //e.Location is a point object
            //set the variable mouselocation the x and y cord of the curser 
            mouseLocation = e.Location;
            
        }

        private void LothauvuddTankGame_MouseClick(object sender, MouseEventArgs e)
        {
            //when the left mouse button is clicked call the shoot function 
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                 
                player.Shoot(mouseLocation, battlefield);
            }
        }

        // the player movement and collusion detection 
        private void PlayerMovement()
        {
            //when the player is moving up 
            if (moveUp == true)
            {

                //create variables to store the left and right 
                int tileXLeft, tileXRight, tileY;

                //take the players location divide iy by the tilesize and subtract 5 
                //to account for the fact that the tank is 5 units smaller than a tile
                // subtract 6 from the XRight tile to prevent setting stuck on a border 
                tileXLeft = (int)((player.Location.X ) / battlefield.TileSize.Width);
                tileXRight = (int)((player.Location.X + battlefield.TileSize.Width - 6) / battlefield.TileSize.Width);
                tileY = (int)((player.Location.Y - 5) / battlefield.TileSize.Height);
                //check the tile in front of the player if it is a non movable tile.
                if (battlefield.BattlefieldMap[tileXLeft, tileY] == MapTile.Border ||
                    battlefield.BattlefieldMap[tileXLeft, tileY] == MapTile.Wall ||
                    battlefield.BattlefieldMap[tileXLeft, tileY] == MapTile.DestructableWall ||
                    battlefield.BattlefieldMap[tileXRight, tileY] == MapTile.Border ||
                    battlefield.BattlefieldMap[tileXRight, tileY] == MapTile.Wall ||
                    battlefield.BattlefieldMap[tileXRight, tileY] == MapTile.DestructableWall)
                {
                    //do not move the player 
                    player.Location = new PointF(player.Location.X, player.Location.Y);
                }
                // move the player up 
                else
                {
                    player.Location = new PointF(player.Location.X, player.Location.Y - player.Speed);
                }

            }

            //when the player is moving down 
            if (moveDown == true)
            {
                // playerLocation.Y += TANK_SPEED;
                int tileXLeft, tileXRight, tileY;

                //take the players location divide iy by the tilesize and subtract 5 
                //to account for the fact that the tank is 5 units smaller than a tile
                // subtract 6 from the XRight tile to prevent setting stuck on a border 
                tileXLeft = (int)((player.Location.X ) / battlefield.TileSize.Width);
                tileXRight = (int)((player.Location.X + battlefield.TileSize.Width - 6) / battlefield.TileSize.Width);
                tileY = (int)((player.Location.Y + battlefield.TileSize.Height - 5) / battlefield.TileSize.Height);
                //check the tile in front of the player if it is a non movable tile.
                if (battlefield.BattlefieldMap[tileXLeft, tileY] == MapTile.Border ||
                    battlefield.BattlefieldMap[tileXLeft, tileY] == MapTile.Wall ||
                    battlefield.BattlefieldMap[tileXLeft, tileY] == MapTile.DestructableWall ||
                    battlefield.BattlefieldMap[tileXRight, tileY] == MapTile.Border ||
                    battlefield.BattlefieldMap[tileXRight, tileY] == MapTile.Wall ||
                    battlefield.BattlefieldMap[tileXRight, tileY] == MapTile.DestructableWall)
                {
                    //do not move the player 
                    player.Location = new PointF(player.Location.X, player.Location.Y);
                }
                //if the tile is not blocked then move 
                else
                {
                    player.Location = new PointF(player.Location.X, player.Location.Y + player.Speed);
                }

            }

            //if the player is moving right 
            if (moveRight == true)
            {
                // PlayerLocation.X -= TANK_SPEED;
                int tileX, tileYTop, tileYBottom;

                //take the players location divide iy by the tilesize and subtract 10 
                //to account for the fact that the tank is 5 units smaller than a tile
                // and to check the right tile and not the top tile 
                // subtract 6 from the YBottom tile to prevent setting stuck on a border 
                tileX = (int)((player.Location.X + battlefield.TileSize.Width - 5) / battlefield.TileSize.Width);
                tileYTop = (int)((player.Location.Y) / battlefield.TileSize.Width);
                tileYBottom = (int)((player.Location.Y + battlefield.TileSize.Height - 6) / battlefield.TileSize.Height);
                //check the tile in front of the player if it is a non movable tile.
                if (battlefield.BattlefieldMap[tileX, tileYTop] == MapTile.Border ||
                    battlefield.BattlefieldMap[tileX, tileYTop] == MapTile.Wall ||
                    battlefield.BattlefieldMap[tileX, tileYTop] == MapTile.DestructableWall ||
                    battlefield.BattlefieldMap[tileX, tileYBottom] == MapTile.Border ||
                    battlefield.BattlefieldMap[tileX, tileYBottom] == MapTile.Wall ||
                    battlefield.BattlefieldMap[tileX, tileYBottom] == MapTile.DestructableWall)
                {
                    // do not move the player 
                    player.Location = new PointF(player.Location.X, player.Location.Y);
                }
                //if the tile is not blocked then move 
                else
                {
                    player.Location = new PointF(player.Location.X + player.Speed, player.Location.Y);
                }
            }

            //if the player is moving left 
            if (moveLeft == true)
            {

                //.Location.X += TANK_SPEED;
                // PlayerLocation.X -= TANK_SPEED;
                int tileX, tileYTop, tileYBottom;

                //take the players location divide iy by the tilesize and subtract 10 
                //to account for the fact that the tank is 5 units smaller than a tile
                // and to check the right tile and not the top tile 
                // subtract 6 from the YBottom tile to prevent setting stuck on a border 
                tileX = (int)((player.Location.X - 5) / battlefield.TileSize.Width);
                tileYTop = (int)((player.Location.Y) / battlefield.TileSize.Width);
                tileYBottom = (int)((player.Location.Y + battlefield.TileSize.Height - 6) / battlefield.TileSize.Height);
                //check the tile in front of the player if it is a non movable tile.
                if (battlefield.BattlefieldMap[tileX, tileYTop] == MapTile.Border ||
                    battlefield.BattlefieldMap[tileX, tileYTop] == MapTile.Wall ||
                    battlefield.BattlefieldMap[tileX, tileYTop] == MapTile.DestructableWall ||
                    battlefield.BattlefieldMap[tileX, tileYBottom] == MapTile.Border ||
                    battlefield.BattlefieldMap[tileX, tileYBottom] == MapTile.Wall ||
                    battlefield.BattlefieldMap[tileX, tileYBottom] == MapTile.DestructableWall)
                {
                    //do not move the player 
                    player.Location = new PointF(player.Location.X, player.Location.Y);
                }
                //if the tile is not blocked then move 
                else
                {
                    player.Location = new PointF(player.Location.X - player.Speed, player.Location.Y);
                }
            }
            //make the player hitbox 
            player.Hitbox = new RectangleF(player.Location, player.Size);
        }
        //timer for the game 
        private void tmrGameTimer_Tick(object sender, EventArgs e)
        {

            PlayerMovement();
            DrawMap();

            //when the player has breat the last level stop the timer and say they have won
            if(battlefield.Level == 10)
            {
                //stop timer
                tmrGameTimer.Enabled = false;
                //victor message 
                MessageBox.Show("You completed the game!");
            }

        }

        private void btnInstructions_Click(object sender, EventArgs e)
        {
            //show the message box for instructions 
            MessageBox.Show("Objective:\r\nDefeat all tanks in the stage to proceed to the next stage. As you defeat each level, the AI difficuluty increases and begins to gain new abilities." +
                "\r\nYou start with 3 lives, and gain one life every 'x' levels. Everytime you die, a life is subtracted from your total until it reaches zero, which will end the game." +
                "\r\nMovement:\r\nW - Move Forward\r\nA - Move Left\r\nS - Move Down\r\nD - Move Right\r\n\r\nAttack(s):\r\nLeft Click - Shoot Projectile\r\nSpacebar - Place mine \r\n " +
                "The game is autosaved", "Instructions", MessageBoxButtons.OK);
        }
        //NOT DONE NEED TO ADD HIGH SCORES FILE
        private void btnHighScores_Click(object sender, EventArgs e)
        {
            //Game Form Directory
            using (StreamReader scores = new StreamReader((Directory.GetParent((Directory.GetParent(Environment.CurrentDirectory)).ToString()) + "/Highscores.txt")))
            {
                MessageBox.Show(scores.ReadToEnd(), "High Scores");
            }
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            pnlScreen.Hide();
            Focus();
            _startLevel = 1;    
            Setup();


        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            // battlefield.LoadGame = true; 


            pnlScreen.Hide();


            //send the current level the player is on, to the 
            using (StreamReader output = new StreamReader((Directory.GetParent((Directory.GetParent(Environment.CurrentDirectory)).ToString()) + "/Save.txt")))
            {
                int level;
                int.TryParse(output.ReadLine(), out level);
                //set the starting level to the saved number 
                _startLevel = level;

            }

            Setup();
            Focus();

        }        
    
    }
    
}

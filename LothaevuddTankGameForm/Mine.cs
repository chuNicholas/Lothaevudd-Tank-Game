//Nicholas Chu
//January 27, 2017
//Mine Object, allows tanks to place mines
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace LothaevuddTankGameForm
{
    class Mine
    {
        //store the mine location
        private PointF _location;
        //set the size for the mine
        private SizeF _size = new SizeF(5, 5);
        //Set a rectangle for the mine
        private RectangleF _mineHitbox;
        //set the inital time for mine in order to count down for detonation
        private int _mineTime;
        /// <summary>
        /// Construct the mine object
        /// </summary>
        /// <param name="Location">get the current location of the player, which will also be the location of the mine</param>
        public Mine(PointF Location)
        {
            _location = Location;
            _mineHitbox = new RectangleF(_location, _size);
            _mineTime = Environment.TickCount;
        }
        
        public int Time
        {
            get
            {
                return _mineTime;
            }
        }
        //Allows other classes to access the location of the mine
        public PointF Location
        {
            get
            {
                return _location;
            }
        }
        public void Explode(BattleField world)
        {
            //check tiles in a nine block radius
            //TTT   T - Tile, M - Mine
            //TMT
            //TTT
            int col = (int)Math.Round(Location.X / world.TileSize.Height);
            int row = (int)Math.Round(Location.Y / world.TileSize.Width);
            col -= 1;
            row -= 1;
            //loop through maptile radius
            for (int j = row; j < row + 3; j++)
            {
                for (int i = col; i < col + 3; i++)
                {
                    //check if theres a tile that can be destroyed
                    if (world.BattlefieldMap[i,j] == MapTile.DestructableWall)
                    {
                        world.BattlefieldMap[i, j] = MapTile.Blank;
                    }
                    //Round player and enemy x and y to match maptile
                    int pCol = (int)Math.Round(world.Player.Location.X / world.TileSize.Width);
                    int pRow = (int)Math.Round(world.Player.Location.Y / world.TileSize.Height);
                    //Loop through enemies, check if they're in the vicinity of the mine
                    //if so blow them up and set their isDead variable to true
                    for (int k = 0; k < world.AiTank.Count(); k++)
                    {
                        //the Col and Row of the grid
                        int eCol = (int)(world.AiTank[k].Location.X / world.TileSize.Width);
                        int eRow = (int)(world.AiTank[k].Location.Y / world.TileSize.Height);

                        //check if enemy is radius
                        if (eCol == i && eRow == j)
                        {
                            world.AiTank[k].IsDead = true;
                        }
                    }
                    
                    //check for enemy or player in radius
                    if (pCol == i && pRow == j)
                    {
                        world.Player.IsDead = true;
                    }
                }
            }
        }
    }
}

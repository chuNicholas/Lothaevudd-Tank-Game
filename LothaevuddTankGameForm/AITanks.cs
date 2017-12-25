//Nicholas Chu
//January 27, 2017
//AI Tank Class for enemies
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Allows tank to be drawn
using System.Drawing;

namespace LothaevuddTankGameForm
{
    class AITanks
    {
        //default projectile speed is 2
        protected int _projectileSpeed;
        protected const int AI_SPEED = 5;
        //check if dead
        protected bool _isDead;
        //location variables
        protected PointF _botLocation;
        protected PointF _facingDirection;
        //Size and colour
        protected Brush _botColour = Brushes.Black;
        protected SizeF _aiSize = new SizeF(20, 20);
        //Hitbox
        protected RectangleF _botHitbox;
        /// <summary>
        /// Constructor for the AI Tank
        /// </summary>
        /// <param name="col">Column location of the ai</param>
        /// <param name="row">Row location of the ai</param>
        /// <param name="projectileSpeed">set projectile speed</param>
        /// <param name="direction">facing direction</param>
        /// <param name="colour">colour of the tank depending on type</param>
        public AITanks(float col, float row, int projectileSpeed, PointF direction, Brush colour)
        {
            _botLocation = new PointF(col, row);
            _botHitbox = new RectangleF(_botLocation, _aiSize);
            _projectileSpeed = projectileSpeed;
            _facingDirection = direction;
            _botColour = colour;
        }
        public void AIMovement(BattleField form)
        {
            //Rotate Tank to face player tank, Call Move Function to move AI

            //Enemy Movement
            Move(form.Player.Location, form);
            //Enemy Fire
            Shoot(Location, form);
        }
        //rotate barrel
        private float Rotate(PointF playerLocation)
        {
            float angle;
            return angle = (float)(Math.Atan2(playerLocation.Y - this.Location.Y, playerLocation.X - this.Location.X) * 180 / Math.PI);
        }
        /// <summary>
        /// Get/Set AI Hitbox
        /// </summary>
        public RectangleF Hitbox
        {
            get
            {
                return _botHitbox;
            }
            set
            {
                _botHitbox = value;
            }
        }
        /// <summary>
        /// Move Enemy
        /// </summary>
        /// <param name="playerLocation"></param>
        /// <param name="world"></param>
        /// <returns></returns>
        public virtual bool Move(PointF playerLocation, BattleField world)
        {
            //Random variable, if tank cannot move because of obstacle, use a random direction to get the tank moving
            Random rngDirection = new Random();
            //Use Rotate Subprogram before Move, since it is assumed that enemy
            //faces the player before moving in that direction
            //Check if enemy is dead
            //If so enemy cannot move
            if (_isDead == false)
            {
                int tileX, tileY = 0;
                //tile size is tilesize + 5 because tank is 5 units smaller than tile
                int tileSize = world.TileSize.Height + 5;
                //If playerLocation.X  is less than enemy's X Location (player is left of enemy)
                if (playerLocation.X < this.Location.X)
                {
                    //get the x and y tile to check battlefield maptile array
                    //subtract tilesize to check tile left of ai
                    tileX = (int)((this.Location.X - tileSize) / world.TileSize.Width);
                    tileY = (int)(this.Location.Y / world.TileSize.Height);
                    //check tile to see if ai can move
                    if (world.BattlefieldMap[tileX, tileY] == MapTile.Border ||
                        world.BattlefieldMap[tileX, tileY] == MapTile.Wall ||
                        world.BattlefieldMap[tileX,tileY] == MapTile.DestructableWall)
                    {
                        PointF direction = new PointF(rngDirection.Next(0, Form.ActiveForm.Size.Width), rngDirection.Next(0, Form.ActiveForm.Size.Height));
                        return Move(direction, world);
                    }
                    //if ai can move safely, move to next tile
                    else
                    {
                        Location = new PointF(Location.X - AI_SPEED, Location.Y);
                        //Set hitbox
                        _botHitbox = new RectangleF(_botLocation, _aiSize);
                        return true;
                    }
                }
                //If playerLocation.X is more than enemy's X Location (player is right of enemy)
                else if (playerLocation.X > this.Location.X)
                {
                    //get the x and y tile to check battlefield maptile array
                    //add tilesize to check tile right of ai
                    tileX = (int)((this.Location.X + tileSize) / world.TileSize.Height);
                    tileY = (int)(this.Location.Y / world.TileSize.Height);
                    //check tile to see if ai can move
                    if (world.BattlefieldMap[tileX, tileY] == MapTile.Border ||
                        world.BattlefieldMap[tileX, tileY] == MapTile.Wall ||
                        world.BattlefieldMap[tileX, tileY] == MapTile.DestructableWall)
                    {
                        PointF direction = new PointF(rngDirection.Next(0, Form.ActiveForm.Size.Width), rngDirection.Next(0, Form.ActiveForm.Size.Height));
                        return Move(direction, world);
                    }
                    //if ai can move safely, move to next tile
                    else
                    {
                        Location = new PointF(Location.X + AI_SPEED, Location.Y);
                        //Set hitbox
                        _botHitbox = new RectangleF(_botLocation, _aiSize);
                        return true;
                    }
                }
                //If playerLocation.Y is less than enemy's Y Location (player is above enemy)
                else if (playerLocation.Y < this.Location.Y)
                {
                    //get the x and y tile to check battlefield maptile array
                    //subtract tilesize to check tile on top of ai
                    tileX = (int)(this.Location.X / world.TileSize.Width);
                    tileY = (int)((this.Location.Y - tileSize) / world.TileSize.Height);
                    //check tile to see if ai can move
                    if (world.BattlefieldMap[tileX, tileY] == MapTile.Border ||
                        world.BattlefieldMap[tileX, tileY] == MapTile.Wall ||
                        world.BattlefieldMap[tileX, tileY] == MapTile.DestructableWall)
                    {
                        PointF direction = new PointF(rngDirection.Next(0, Form.ActiveForm.Size.Width), rngDirection.Next(0, Form.ActiveForm.Size.Height));
                        return Move(direction, world);
                    }
                    //if ai can move safely, move to next tile
                    else
                    {
                        Location = new PointF(Location.X, Location.Y - AI_SPEED);
                        //Set hitbox
                        _botHitbox = new RectangleF(_botLocation, _aiSize);
                        return true;
                    }
                }
                //If playerLocation.Y is more than enemy's Y Location (player is below enemy)
                else if (playerLocation.Y > this.Location.Y)
                {
                    //get the x and y tile to check battlefield maptile array
                    //add tilesize to check tile below of ai
                    tileX = (int)(this.Location.X / world.TileSize.Width);
                    tileY = (int)((this.Location.Y + tileSize) / world.TileSize.Height);
                    //check tile to see if ai can move
                    if (world.BattlefieldMap[tileX, tileY] == MapTile.Border ||
                        world.BattlefieldMap[tileX, tileY] == MapTile.Wall ||
                        world.BattlefieldMap[tileX, tileY] == MapTile.DestructableWall)
                    {
                        PointF direction = new PointF(rngDirection.Next(0, Form.ActiveForm.Size.Width), rngDirection.Next(0, Form.ActiveForm.Size.Height));
                        return Move(direction, world);
                    }
                    // if ai can move safely, move to next tile
                    else
                    {
                        Location = new PointF(Location.X, Location.Y + AI_SPEED);
                        //Set hitbox
                        _botHitbox = new RectangleF(_botLocation, _aiSize);
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public virtual bool Shoot(PointF playerLocation, BattleField form)
        {
            if (IsDead == false)
            {
                form.AddProjectile(new Projectile(Location, playerLocation, _projectileSpeed));
                //return true after creating
                return true;
            }
            //ai lost, cannot shoot projectile
            else
            {
                //return false
                return false;
            }
        }
        //Get the location of the AITank
        public PointF Location
        {
            get
            {
                return _botLocation;
            }
            set
            {
                _botLocation = value;
            }
        }
        //Get is dead
        public bool IsDead
        {
            get
            {
                return _isDead;
            }
            set
            {
                _isDead = value;
            }
        }

        // get the ai speed
        public int AiSpeed
        {
            get
            {
                return AI_SPEED;
            }

        }
        //get the size of the ai size 
        public SizeF Size
        {
            get
            {
                return _aiSize;
            }
        }

    }
}

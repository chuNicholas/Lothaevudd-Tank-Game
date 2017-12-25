//Gabriel Tu
//Jan 9, 2017
//Player tank class
//
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Allows User to be drawn
using System.Drawing;
namespace LothaevuddTankGameForm
{
    class Tank
    {

        // tank is 20 x 20

        // tile is 25x25
        //variable to store if user is dead
        private bool _isDead = false;
        //constant to store the tank's projectile speed
        private const int PROJECTILE_SPEED = 10;
        //point variable to store the location of the player
        private PointF _playerLocation;
        //Store the colour of the tank
        private Brush _colour;
        private SizeF _tankSize = new SizeF(20, 20);
        //constant to store the movement speed of the tank
        private const int TANK_SPEED = 5;
        //store important variables such as amount of mines and number of player lives
        private int _mineAmount;
        private int _playerLives;
        //Hitbox
        private RectangleF _playerHitbox;

        /// <summary>
        /// Allows user to create tank object
        /// </summary>
        /// <param name="col">set the column location of tank</param>
        /// <param name="row">set the row location of tank</param>
        /// <param name="colour">change the colour of the tank</param>
        public Tank(float col, float row, Brush colour)
        {
            _playerLocation = new PointF(col, row);
            _playerHitbox = new RectangleF(_playerLocation, _tankSize);
            _colour = colour;
            _playerLives = 3;
            _mineAmount = 5;
        }

        //get and set the colour of the tank 
        public Brush Colour
        {
            get
            {
                return _colour;
            }
            set
            {
                _colour = value; 
            }


        }

        public RectangleF Hitbox
        {
            get
            {
                return _playerHitbox;
            }
            set
            {
                _playerHitbox = value;
            }
        }
        public int Speed
        {
            get
            {
                return TANK_SPEED;
            }
        }
        /// <summary>
        /// Get/Set the number of Mines that the player has
        /// </summary>
        public int MineAmount
        {
            get
            {
                return _mineAmount;
            }
            set
            {
                _mineAmount = value;

            }
        }
        /// <summary>
        /// Get/Set the amount of lives the player has
        /// </summary>
        public int PlayerLives
        {
            get
            {
                return _playerLives;
            }
            set
            {
                _playerLives = value;

            }

        }
        /// <summary>
        /// Allow player to shoot projectile
        /// </summary>
        /// <param name="MouseLocation">Pass in the Mouselocation point to aim projectile in that direction</param>
        /// <param name="battlefield">pass in battlefield in order to create the object in the map</param>
        public void Shoot(PointF MouseLocation, BattleField battlefield)
        {
            if (IsDead == false)
            {
                battlefield.AddProjectile(new Projectile(this.Location, MouseLocation, PROJECTILE_SPEED));

            }
        }
        /// <summary>
        /// Allow player to place mines
        /// </summary>
        /// <param name="Location">pass in current location (location that the mine will be placed on top of)</param>
        /// <param name="battlefield">allows the program to create the mine object on the map</param>
        public void Mine(PointF Location, BattleField battlefield)
        {
            if (IsDead == false)
            {
                battlefield.AddMine(new LothaevuddTankGameForm.Mine(Location));
            }
        }
        /// <summary>
        /// Get the true or false value of is the user dead?
        /// Set the player death status
        /// </summary>
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
        /// <summary>
        /// Get the x,y location of the tank
        /// </summary>
        public PointF Location
        {
            get
            {
                return _playerLocation;
            }
            set
            {
                _playerLocation = value; 
            }
        }
        /// <summary>
        /// get the dimensions of the tank
        /// </summary>
        public SizeF Size
        {
            get
            {
                return _tankSize;
            }
        }


    }
}

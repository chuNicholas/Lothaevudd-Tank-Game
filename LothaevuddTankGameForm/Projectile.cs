//Nicholas Chu
//January 27, 2017
//Allows user to shoot projectile
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

namespace LothaevuddTankGameForm
{
    class Projectile
    {
        //location and direction variables
        private PointF _projectileLocation;
        private PointF _projectileDirection;
        //counter for bounces
        private int _counter;
        //speed of the projectile
        private float _projectileXSpeed;
        private float _projectileYSpeed;
        //Multiplier for how fast the projectile goes
        private int _projectileMultiplier;
        //the maximum amount of times the projectile can bounce
        private const int MAX_BOUNCE = 2;
        //constant for tile value, allows projectile to be displayed properly
        private const int TILE_SIZE = 25;
        //variables to calculate speed
        private float rise, run, hyp;
        //constant for size
        private SizeF _projectileSize = new SizeF(5,5);
        //Variable to calculate intersection
        private RectangleF _projectileHitbox;
        /// <summary>
        /// Create Projectile Object
        /// </summary>
        /// <param name="Location">Current Player Direction</param>
        /// <param name="Direction"></param>
        public Projectile(PointF Location, PointF Direction, int projectileSpeed)
        {
            //Offset to shoot from middle
            _projectileLocation.X = Location.X + 10;
            _projectileLocation.Y = Location.Y + 10;
            //Hitbox of projectile
            _projectileHitbox = new RectangleF(_projectileLocation, _projectileSize);
            //Direction of Projectile
            _projectileDirection = Direction;
            _projectileMultiplier = projectileSpeed;
            _counter = 0;
            //Calculations for Speed of projectile
            rise = (Direction.Y - Location.Y);
            run = (Direction.X - Location.X);
            hyp = (float)Math.Sqrt((rise * rise) + (run * run));
            _projectileXSpeed = run / hyp * _projectileMultiplier;
            _projectileYSpeed = rise / hyp * _projectileMultiplier;
        }
        /// <summary>
        /// Get/Set hitbox of projectile
        /// </summary>
        public RectangleF Hitbox
        {
            get
            {
                return _projectileHitbox;
            }
            set
            {
                _projectileHitbox = value;
            }
        }
        /// <summary>
        /// Get/Set the location of the projectile
        /// </summary>
        public PointF Location
        {
            get
            {
                return _projectileLocation;
            }
            set
            {
                _projectileLocation = value;
            }
        }
        /// <summary>
        /// Get/Set the XSpeed of the Projectile
        /// </summary>
        public float SpeedX
        {
            get
            {
                return _projectileXSpeed;
            }
            set
            {
                _projectileXSpeed = value;
            }
        }
        /// <summary>
        /// Get/Set the YSpeed of the Projectile
        /// </summary>
        public float SpeedY
        {
            get
            {
                return _projectileYSpeed;
            }
            set
            {
                _projectileYSpeed = value;
            }
        }
        /// <summary>
        /// move the projectile
        /// </summary>
        /// <returns>the location of the projectile</returns>
        public PointF Move()
        {
            _projectileLocation.X += _projectileXSpeed;
            _projectileLocation.Y += _projectileYSpeed;
            _projectileHitbox = new RectangleF(_projectileLocation, _projectileSize);
            return _projectileLocation;
        }
        /// <summary>
        /// get the max bounce amount
        /// </summary>
        public int MaxBounce
        {
            get
            {
                return MAX_BOUNCE;
            }
        }
        /// <summary>
        /// Get the amount of bounces the projectile has done
        /// </summary>
        public int BounceCounter
        {
            get
            {
                return _counter;
            }
            set
            {
                _counter = value;
            }
        }
    }
}

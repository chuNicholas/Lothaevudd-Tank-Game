//Nicholas Chu
//January 27, 2017
//Harder Tank Class, never implemented
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace LothaevuddTankGameForm
{
    class USSAdaevielTank : AITanks
    {

        //The Adaeveil is the boss enemy tank, it can shoot rocket projectiles quickly and takes two hits to kill 
        public USSAdaevielTank(float col, float row, int projectileSpeed, PointF direction, Brush colour) : base(col, row, 5, direction, Brushes.Orange)
        {

        }
        public override bool Shoot(PointF playerLocation, BattleField form)
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
    }
}

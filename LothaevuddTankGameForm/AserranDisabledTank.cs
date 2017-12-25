//Gabriel Tu
//January 27, 2017
//Default Tank
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
namespace LothaevuddTankGameForm
{
    class AserranDisabledTank : AITanks
    {
        public AserranDisabledTank(float col, float row, int projectileSpeed, PointF direction, Brush colour)
            : base(col, row, 5, direction, colour)
        {

        }
        public override bool Move(PointF playerLocation, BattleField world)
        {
            return false;
        }
    }
}

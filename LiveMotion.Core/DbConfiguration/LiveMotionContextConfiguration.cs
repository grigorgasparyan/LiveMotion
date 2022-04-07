using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveMotion.Core.DbConfiguration
{
    public class LiveMotionContextConfiguration : DbMigrationsConfiguration<LiveMotionContext>
    {
        public LiveMotionContextConfiguration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
        }
    }
}

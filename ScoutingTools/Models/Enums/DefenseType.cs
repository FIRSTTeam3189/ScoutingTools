using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScoutingTools.Models.Enums
{
    public enum DefenseType
    {
        /// <summary>
        /// Category A Defense
        /// </summary>
        Portcullis,

        /// <summary>
        /// Category A Defense
        /// </summary>
        ChevalDeFrise,

        /// <summary>
        /// Category B Defense
        /// </summary>
        Moat,

        /// <summary>
        /// Category B Defense
        /// </summary>
        Drawbridge,

        /// <summary>
        /// Category C Defense
        /// </summary>
        Ramparts,

        /// <summary>
        /// Category C Defense
        /// </summary>
        SallyPort,

        /// <summary>
        /// Category D Defense
        /// </summary>
        RockWall,

        /// <summary>
        /// Category D Defense
        /// </summary>
        RoughTerrain,

        /// <summary>
        /// Always on field defense
        /// </summary>
        LowBar,

        /// <summary>
        /// Hasn't been assigned yet
        /// </summary>
        Unassigned
    }
}

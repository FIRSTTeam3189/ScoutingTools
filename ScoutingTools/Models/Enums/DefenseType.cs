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

    public class DefenseTypeUI
    {
        public static readonly IReadOnlyDictionary<DefenseType, string>  UIString=new Dictionary<DefenseType, string>()
        {
            {DefenseType.ChevalDeFrise, "Cheval De Frise" },
            { DefenseType.Portcullis, "Portcullise" },
            {DefenseType.Moat, "Moat" },
            {DefenseType.Drawbridge, "Drawbridge" },
            {DefenseType.Ramparts,"Ramparts" },
            {DefenseType.SallyPort, "Sally Port" },
            {DefenseType.RockWall, "Rock Wall" },
            {DefenseType.RoughTerrain, "Rough Terrain" },
            {DefenseType.LowBar,"Low Bar" },
            {DefenseType.Unassigned,"Unassigned" }
        };
    }
}

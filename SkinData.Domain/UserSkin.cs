using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkinData.Domain
{
    public class UserSkin
    {
        public long UserId { get; set; }

        // Pores Left Cheek
        public decimal PoresLeftCheekConfidence { get; set; }
        public int PoresLeftCheekValue { get; set; }

        // Nasolabial Fold
        public decimal NasolabialFoldConfidence { get; set; }
        public int NasolabialFoldValue { get; set; }

        // Eye Pouch
        public decimal EyePouchConfidence { get; set; }
        public int EyePouchValue { get; set; }

        // Forehead Wrinkle
        public decimal ForeheadWrinkleConfidence { get; set; }
        public int ForeheadWrinkleValue { get; set; }

        // Skin Spot
        public decimal SkinSpotConfidence { get; set; }
        public int SkinSpotValue { get; set; }

        // Acne
        public decimal AcneConfidence { get; set; }
        public int AcneValue { get; set; }

        // Pores Forehead
        public decimal PoresForeheadConfidence { get; set; }
        public int PoresForeheadValue { get; set; }

        // Pores Jaw
        public decimal PoresJawConfidence { get; set; }
        public int PoresJawValue { get; set; }

        // Left Eyelids
        public decimal LeftEyelidsConfidence { get; set; }
        public int LeftEyelidsValue { get; set; }

        // Eye Finelines
        public decimal EyeFinelinesConfidence { get; set; }
        public int EyeFinelinesValue { get; set; }

        // Dark Circle
        public decimal DarkCircleConfidence { get; set; }
        public int DarkCircleValue { get; set; }

        // Crow's Feet
        public decimal CrowsFeetConfidence { get; set; }
        public int CrowsFeetValue { get; set; }

        // Pores Right Cheek
        public decimal PoresRightCheekConfidence { get; set; }
        public int PoresRightCheekValue { get; set; }

        // Blackhead
        public decimal BlackheadConfidence { get; set; }
        public int BlackheadValue { get; set; }

        // Glabella Wrinkle
        public decimal GlabellaWrinkleConfidence { get; set; }
        public int GlabellaWrinkleValue { get; set; }

        // Mole
        public decimal MoleConfidence { get; set; }
        public int MoleValue { get; set; }

        // Right Eyelids
        public decimal RightEyelidsConfidence { get; set; }
        public int RightEyelidsValue { get; set; }

        // Skin Type
        public int SkinType { get; set; }

        // Skin Type Detail 0
        public decimal SkinTypeDetail0Confidence { get; set; }
        public int SkinTypeDetail0Value { get; set; }

        // Skin Type Detail 1
        public decimal SkinTypeDetail1Confidence { get; set; }
        public int SkinTypeDetail1Value { get; set; }

        // Skin Type Detail 2
        public decimal SkinTypeDetail2Confidence { get; set; }
        public int SkinTypeDetail2Value { get; set; }

        // Skin Type Detail 3
        public decimal SkinTypeDetail3Confidence { get; set; }
        public int SkinTypeDetail3Value { get; set; }
    }

}

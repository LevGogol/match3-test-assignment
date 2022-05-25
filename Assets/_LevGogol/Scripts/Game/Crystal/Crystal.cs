using UnityEngine;

namespace LevGogol.Match3.Game.Model
{
    public class Crystal
    {
        private CrystalColor _crystalCrystalColor;

        public CrystalColor CrystalColor => _crystalCrystalColor;

        public Crystal(CrystalColor crystalColor)
        {
            _crystalCrystalColor = crystalColor;
        }
    }
}

public enum CrystalColor
{
    Red,
    Green,
    Blue,
    Orange,
    Purple,
}
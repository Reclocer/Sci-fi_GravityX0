using System;
using System.Collections.Generic;
using UnityEngine;

namespace Corebin.GravityX0
{
    public class WinCellCount
    {
        public int XTiles;
        public int OTiles;
        public bool WinCombination;

        private int _countingLimit;

        public event Action<Fill> OnWin;

        public WinCellCount(int countingLimit)
        {
            _countingLimit = countingLimit;
        }

        public void IncrementXTiles()
        {
            OTiles = 0;
            XTiles++;

            if (XTiles == _countingLimit)
            {
                WinCombination = true;
                OnWin(Fill.Iks);
            }
        }

        public void IncrementOTiles()
        {
            XTiles = 0;
            OTiles++;

            if (OTiles == _countingLimit)
            {
                WinCombination = true;
                OnWin(Fill.Zero);
            }
        }

        public void Revert()
        {
            XTiles = 0;
            OTiles = 0;
        }
    }
}

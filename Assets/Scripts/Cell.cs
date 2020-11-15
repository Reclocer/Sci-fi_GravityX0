using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Corebin.GravityX0
{
    public class Cell : MonoBehaviour
    {
        public Fill Fill { get; set; } = Fill.Empty;
        public Vector3 CellPosition { get; set; }
        public Tile Tile;
    }
}

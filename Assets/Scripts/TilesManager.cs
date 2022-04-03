using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;
using Corebin.GravityX0.AI;

namespace Corebin.GravityX0
{
    public class TilesManager : MonoBehaviour
    {
        [SerializeField] private Board _board;
        [SerializeField] private AIBase _AI;

        private Tile _selectedTile = null;
        private int _selectedColumn = 0;

        /// <summary>
        /// Чья очередь ходить
        /// </summary>
        [HideInInspector] public Fill SideNow = Fill.Iks;

        [Space]
        public Rigidbody[] TilesRigidbodies;
        public Tile[] Tiles;
        public List<Tile> IksTiles;
        public List<Tile> ZeroTiles;        

#if UNITY_EDITOR
        private void OnValidate()
        {
            TilesRigidbodies = null;
            TilesRigidbodies = FindObjectsOfType<Rigidbody>();

            Tiles = null;
            Tiles = FindObjectsOfType<Tile>();

            IksTiles = new List<Tile>(19);
            ZeroTiles = new List<Tile>(19);

            foreach (Tile tile in Tiles)
            {
                if(tile.Fill == Fill.Iks)
                {
                    IksTiles.Add(tile);
                }
                else if(tile.Fill == Fill.Zero)
                {
                    ZeroTiles.Add(tile);
                }
                else
                {
                    Debug.Log("Fill is not assigned");
                }
            }            
        }
#endif

        #region Click on column  
        public void ClickOnColumn(int columnNumber)
        {
            if (_selectedTile == null)
            {
                _selectedTile = SelectTile(SideNow);
                //UpTileToStartPosition(_selectedTile, columnNumber);
                _selectedTile.MoveTo(_board.TilesStartPositions[columnNumber]);
                _selectedColumn = columnNumber;
            }
            else if (_selectedColumn != columnNumber)
            {
                //UpTileToStartPosition(_selectedTile, columnNumber);
                _selectedTile.MoveTo(_board.TilesStartPositions[columnNumber]);
                _selectedColumn = columnNumber;
            }
            else if (!_board.CheckColumnIsFull(columnNumber))
            {
                _selectedTile.MoveTo(_board.SelectEmptyCell(columnNumber, _selectedTile));
                RemoveTileFromSideTiles(_selectedTile);
                _selectedColumn = 0;
                
                if(SideNow == Fill.Iks)
                {
                    SideNow = Fill.Zero;                    
                }
                else
                {
                    SideNow = Fill.Iks;
                }

                _selectedTile = null;
            }

            //AI
            if (SideNow == Fill.Zero)
            {
                if (_AI.enabled)
                {
                    _AI.Do();
                }
            }
        }

        private void UpTileToStartPosition(Tile tile, int columnNumber)
        {
            if (tile != null)
            {
                Rigidbody rigidbody = tile.GetComponent<Rigidbody>();
                rigidbody.useGravity = false;
                tile.MoveTo(_board.TilesStartPositions[columnNumber]);
                rigidbody.angularVelocity = Vector3.zero;
                rigidbody.velocity = Vector3.zero;
                tile.transform.rotation = Quaternion.identity;
            }
        }

        private Tile SelectTile(Fill fill)
        {
            if (fill == Fill.Iks)
            {
                if (IksTiles.Count > 0)
                {
                    int tileNumber = Random.Range(0, IksTiles.Count);
                    Tile tile = IksTiles[tileNumber];
                    return tile;
                }
            }
            else if(fill == Fill.Zero)
            {
                if (ZeroTiles.Count > 0)
                {
                    int tileNumber = Random.Range(0, ZeroTiles.Count);
                    Tile tile = ZeroTiles[tileNumber];
                    return tile;
                }
            }
            return null;
        }

        private void RemoveTileFromSideTiles(Tile tile)
        {
            if(tile.Fill == Fill.Iks)
            {
                if(IksTiles.Count > 0)                
                   IksTiles.Remove(tile);
            }
            else if(tile.Fill == Fill.Zero)
            {
                if(ZeroTiles.Count > 0)
                   ZeroTiles.Remove(tile);
            }
            else
            {
                Debug.Log("Условия не выполнились!");
            }
        }
        #endregion Click on column
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Corebin.GravityX0
{
    public enum Fill
    {
        Empty,
        Iks,
        Zero
    }    

    public class GridColumn: MonoBehaviour
    {
        private int _nextEmptyCell = 0;
        public int NextEmptyCell => _nextEmptyCell;
        public bool IsFull = false;

        public void SetNextEmptyCell()
        {
            _nextEmptyCell++;

            if (_nextEmptyCell > 5)
            {
                _nextEmptyCell = -1;
                IsFull = true;
            }
        }
    }

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

    public class Board : MonoBehaviour
    {    
        [HideInInspector] public Vector3[] TilesStartPositions = new Vector3[6];
        public Cell[,] Cells = new Cell[6, 6];
        [HideInInspector] public GridColumn[] GridColumns = new GridColumn[6];
        
        [HideInInspector] public int PlayerSteps = 0;
        private Cell[] winCells = new Cell[4];
        public WinCellCount WinCellCount = new WinCellCount(4);

        private void Start()
        {
            Initialize();                       
        }    

        private void Initialize()
        {
            for (int i = 0; i < GridColumns.Length; i++)
            {
                GridColumns[i] = new GridColumn();
            }

            for (int i = 0; i < Cells.Length / 6; i++)
            {
                for (int j = 0; j < Cells.Length / 6; j++)
                {
                    Cells[i, j] = new Cell();
                }
            }

            FillIn_tilesPositions();
        }
        
        public bool CheckColumnIsFull(int columnNumber)
        {
            if(GridColumns[columnNumber].IsFull)
            {
                return true;
            }
            return false;
        }

        public Vector3 SelectEmptyCell(int columnNumber, Tile tile)
        {
            int nextEmptyCell = GridColumns[columnNumber].NextEmptyCell;

            if(nextEmptyCell != -1)
            {
                Cell cell = Cells[columnNumber, nextEmptyCell];
                cell.Tile = tile;
                cell.Fill = tile.Fill;
                GridColumns[columnNumber].SetNextEmptyCell();
                CalculatingWinner();
                return cell.CellPosition;
            }
            return Vector3.zero;
        }

        #region Calculating the winner
        private void CalculatingWinner()
        {
            PlayerSteps++;

            if (PlayerSteps < 7)
                return;

            // horizontal
            for(int y = 0; y < 6; y++)
            {
                WinCellCount.Revert();

                for (int x = 0; x < 6; x++)
                {
                    Cell cell = Cells[x, y];

                    //если ячейка не пуста
                    if (cell.Fill != Fill.Empty)
                    {
                        //если ячейка равна икс
                        if (cell.Tile.Fill == Fill.Iks)
                        {
                            WinCellCount.IncrementXTiles();
                        }
                        else
                        {
                            WinCellCount.IncrementOTiles();
                        }                                
                    }
                    else
                    {
                        WinCellCount.Revert();
                    }
                }
            }

            //vertical
            for (int x = 0; x < 6; x++)
            {
                WinCellCount.Revert();

                for (int y = 0; y < 6; y++)
                {
                    Cell cell = Cells[x, y];

                    //если ячейка не пуста
                    if (cell.Fill != Fill.Empty)
                    {
                        //если ячейка равна икс
                        if (cell.Tile.Fill == Fill.Iks)
                        {
                            WinCellCount.IncrementXTiles();
                        }
                        else
                        {
                            WinCellCount.IncrementOTiles();
                        }
                    }
                    else
                    {
                        WinCellCount.Revert();
                    }
                }
            }
        }
        #endregion Calculating the winner

        private void FillIn_tilesPositions()
        {            
            Cells[0, 0].CellPosition = new Vector3(-0.1362f, 0.2429f, -2.659f);
            Cells[0, 1].CellPosition = new Vector3(-0.1362f, 0.2979f, -2.659f);
            Cells[0, 2].CellPosition = new Vector3(-0.1362f, 0.3529f, -2.659f);
            Cells[0, 3].CellPosition = new Vector3(-0.1362f, 0.4059f, -2.659f);
            Cells[0, 4].CellPosition = new Vector3(-0.1362f, 0.4599f, -2.659f);
            Cells[0, 5].CellPosition = new Vector3(-0.1362f, 0.5149f, -2.659f);

            Cells[1, 0].CellPosition = new Vector3(-0.08172f, 0.2429f, -2.659f);
            Cells[1, 1].CellPosition = new Vector3(-0.08172f, 0.2979f, -2.659f);
            Cells[1, 2].CellPosition = new Vector3(-0.08172f, 0.3529f, -2.659f);
            Cells[1, 3].CellPosition = new Vector3(-0.08172f, 0.4059f, -2.659f);
            Cells[1, 4].CellPosition = new Vector3(-0.08172f, 0.4599f, -2.659f);
            Cells[1, 5].CellPosition = new Vector3(-0.08172f, 0.5149f, -2.659f);

            Cells[2, 0].CellPosition = new Vector3(-0.02722f, 0.2429f, -2.659f);
            Cells[2, 1].CellPosition = new Vector3(-0.02722f, 0.2979f, -2.659f);
            Cells[2, 2].CellPosition = new Vector3(-0.02722f, 0.3529f, -2.659f);
            Cells[2, 3].CellPosition = new Vector3(-0.02722f, 0.4059f, -2.659f);
            Cells[2, 4].CellPosition = new Vector3(-0.02722f, 0.4599f, -2.659f);
            Cells[2, 5].CellPosition = new Vector3(-0.02722f, 0.5149f, -2.659f);

            Cells[3, 0].CellPosition = new Vector3(0.02678f, 0.2429f, -2.659f);
            Cells[3, 1].CellPosition = new Vector3(0.02678f, 0.2979f, -2.659f);
            Cells[3, 2].CellPosition = new Vector3(0.02678f, 0.3529f, -2.659f);
            Cells[3, 3].CellPosition = new Vector3(0.02678f, 0.4059f, -2.659f);
            Cells[3, 4].CellPosition = new Vector3(0.02678f, 0.4599f, -2.659f);
            Cells[3, 5].CellPosition = new Vector3(0.02678f, 0.5149f, -2.659f);

            Cells[4, 0].CellPosition = new Vector3(0.08178f, 0.2429f, -2.659f);
            Cells[4, 1].CellPosition = new Vector3(0.08178f, 0.2979f, -2.659f);
            Cells[4, 2].CellPosition = new Vector3(0.08178f, 0.3529f, -2.659f);
            Cells[4, 3].CellPosition = new Vector3(0.08178f, 0.4059f, -2.659f);
            Cells[4, 4].CellPosition = new Vector3(0.08178f, 0.4599f, -2.659f);
            Cells[4, 5].CellPosition = new Vector3(0.08178f, 0.5149f, -2.659f);

            Cells[5, 0].CellPosition = new Vector3(0.1368f, 0.2429f, -2.659f);
            Cells[5, 1].CellPosition = new Vector3(0.1368f, 0.2979f, -2.659f);
            Cells[5, 2].CellPosition = new Vector3(0.1368f, 0.3529f, -2.659f);
            Cells[5, 3].CellPosition = new Vector3(0.1368f, 0.4059f, -2.659f);
            Cells[5, 4].CellPosition = new Vector3(0.1368f, 0.4599f, -2.659f);
            Cells[5, 5].CellPosition = new Vector3(0.1368f, 0.5149f, -2.659f);


            TilesStartPositions[0] = new Vector3(-0.1362f, 0.5688f, -2.659f);
            TilesStartPositions[1] = new Vector3(-0.08172f, 0.5688f, -2.659f);
            TilesStartPositions[2] = new Vector3(-0.02722f, 0.5688f, -2.659f);
            TilesStartPositions[3] = new Vector3(0.02678f, 0.5688f, -2.659f);
            TilesStartPositions[4] = new Vector3(0.08178f, 0.5688f, -2.659f);
            TilesStartPositions[5] = new Vector3(0.1368f, 0.5688f, -2.659f);                       
        }
    }
}

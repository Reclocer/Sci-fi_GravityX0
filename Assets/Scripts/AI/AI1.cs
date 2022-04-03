using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

namespace Corebin.GravityX0.AI
{
    public class AI1 : AIBase
    {   
        private List<int> _blockedColumns = new List<int>(6);

        public override void Do()
        {        
            //test
            //if (_board.PlayerSteps < 6)
            //    return;

            #region Проверка на 3 занятые подряд ячейки                      
            if (_board.PlayerSteps > 4)
            {
                //vertical
                for (int x = 0; x < 6; x++)
                {
                    int iks = 0;
                    int zero = 0;

                    for (int y = 0; y < 6; y++)
                    {
                        Cell cell = _cells[x, y];

                        //если ячейка не пуста
                        if (cell.Fill != Fill.Empty)
                        {
                            //если ячейка равна Fill
                            if (cell.Tile.Fill == Fill)
                            {
                                iks = 0;
                                zero++;

                                if (!_blockedColumns.Contains(x))
                                {
                                    if (zero == 3 && !_board.CheckColumnIsFull(x))
                                    {
                                        //если над тремя одинаковыми ячейками, следует свободная ячейка
                                        if (_cells[x, y + 1].Fill == Fill.Empty)
                                        {
                                            NeedCloseColumn(x);
                                            _blockedColumns.Add(x);
                                            return;
                                        }
                                    }
                                }
                            }//если ячейка не равна Fill
                            else if (cell.Tile.Fill != Fill)
                            {
                                iks++;
                                zero = 0;

                                if (!_blockedColumns.Contains(x))
                                {
                                    if (iks == 3 && !_board.CheckColumnIsFull(x))
                                    {
                                        //если над тремя одинаковыми ячейками, следует свободная ячейка
                                        if (_cells[x, y + 1].Fill == Fill.Empty)
                                        {
                                            NeedCloseColumn(x);
                                            _blockedColumns.Add(x);
                                            return;
                                        }
                                    }
                                }
                            }                            
                        }
                        else
                        {
                            iks = 0;
                            zero = 0;
                        }
                    }
                }

                // horizontal
                for (int y = 0; y < 6; y++)
                {
                    int iks = 0;
                    int zero = 0;

                    for (int x = 0; x < 6; x++)
                    {
                        //если первый ряд
                        if (y == 0)
                        {
                            if (CheckHorizontal(x, y, ref iks, ref zero))
                            {
                                return;
                            }
                        }
                        else if (x + 3 < 6)
                        {
                            if (_cells[x + 3, y - 1].Fill != Fill.Empty
                                 && CheckHorizontal(x, y, ref iks, ref zero))
                            {
                                return;
                            }
                        }                       
                    }
                }
            }
            #endregion Проверка на 3 занятые подряд ячейки 
             
            bool done = false;

            while (!done)
            {
                int column = Random.Range(0, 6);

                if (!_board.CheckColumnIsFull(column))
                {
                    ClickOnSelectedColumn(column);
                    done = true;
                    return;
                }
            }   
        }

        private bool CheckHorizontal(int x, int y, ref int zero, ref int iks)
        {
            Cell cell = _cells[x, y];

            //если ячейка не пуста 
            if (cell.Fill != Fill.Empty)
            {
                //если ячейка равна Fill
                if (cell.Tile.Fill == Fill)
                {
                    iks = 0;
                    zero++;

                    //если 3 подряд одинаковые Fill и после них есть ячейки
                    if (zero == 3 && x + 1 < 6)
                    {
                        //если после трех одинаковых ячеек, следует свободная ячейка 
                        if (_cells[x + 1, y].Fill == Fill.Empty
                         && !_board.CheckColumnIsFull(x + 1))
                        {
                            NeedCloseColumn(x + 1);
                            return true;
                        }
                    }
                }//если ячейка не равна Fill
                else if (cell.Tile.Fill != Fill)
                {
                    zero = 0;
                    iks++;

                    Debug.Log(x + 3);

                    //если 1 ячейка занята х далее идет свободная затем 2 х (x xx)
                    if (iks == 1
                     && _cells[x + 1, y].Fill == Fill.Empty
                     && _cells[x + 2, y].Fill == Fill.Iks) 
                     //&& _cells[x + 3, y].Fill == Fill.Iks)
                     //&& _cells[x + 1, y - 1].Fill != Fill.Empty)
                    {
                        NeedCloseColumn(x + 1);
                        return true;
                    }//если 2 ячейка занята х далее идет свободная затем еще 1 х (хх х)
                    else if  (iks == 1
                           && _cells[x + 1, y].Fill == Fill.Iks
                           && _cells[x + 2, y].Fill == Fill.Empty
                           && _cells[x + 3, y].Fill == Fill.Iks)
                           //&& _cells[x + 2, y - 1].Fill != Fill.Empty)
                    {
                        NeedCloseColumn(x + 2);
                        return true;
                    }//если 3 подряд одинаковые не Fill и после них есть ячейки (xxx )
                    else if (iks == 3 && x + 1 < 6)
                    {
                        //если после трех одинаковых ячеек, следует свободная ячейка 
                        if (_cells[x + 1, y].Fill == Fill.Empty
                         && !_board.CheckColumnIsFull(x + 1))
                        {
                            NeedCloseColumn(x + 1);
                            return true;
                        }
                    }
                }                
            } //если пустая ячейка и после нее 3 одинаковых
            else if (x + 3 < 6)
            {
                zero = 0;
                iks = 0;
                int iksg = 0;
                int zerog = 0;

                for (int g = 0; g < 3; g++)
                {
                    //если 3 подряд zero ( 000)
                    if (_cells[x + 1 + g, y].Fill == Fill.Zero)
                    {
                        zerog++;

                        if (zerog == 3)
                        {
                            NeedCloseColumn(x);
                            return true;
                        }

                    }//если 3 подряд икс ( xxx)
                    else if (_cells[x + 1 + g, y].Fill == Fill.Iks)
                    {
                        iksg++;

                        if (iksg == 3)
                        {
                            NeedCloseColumn(x);
                            return true;
                        }
                    }
                }
            }
            else
            {
                zero = 0;
                iks = 0;
            }
            return false;
        }

        private void NeedCloseColumn(int column)
        {
            ClickOnSelectedColumn(column);
        }
    }
}

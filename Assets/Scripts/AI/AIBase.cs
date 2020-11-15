using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Corebin.GravityX0.AI
{
    public abstract class AIBase : MonoBehaviour
    {
        [SerializeField] protected ColumnsManager _columnsManager;
        [SerializeField] protected EventSystem _eventSystem;
        [SerializeField] protected Board _board;
        public Fill Fill = Fill.Zero;

        protected Cell[,] _cells;
        //protected WinCellCount _winCellCount = new WinCellCount(3);

        protected virtual void Start()
        {
            _cells = _board.Cells;
        }

        public abstract void Do();        

        protected virtual void ClickOnSelectedColumn(int selectedColumnNumber)
        {
            _columnsManager.Columns[selectedColumnNumber].OnPointerClick(new PointerEventData(_eventSystem));
        }
    }
}

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
        [SerializeField] protected Board _board;
        public Fill Fill = Fill.Zero;

        [SerializeField] private float _clickDelay = 1;
        private bool _canClick = true;

        protected Cell[,] _cells;
        //protected WinCellCount _winCellCount = new WinCellCount(3);

        protected virtual void Start()
        {
            _cells = _board.Cells;
        }

        public abstract void Do();

        protected virtual void ClickOnSelectedColumn(int selectedColumnNumber)
        {
            if (_canClick)
            {
                _columnsManager.Columns[selectedColumnNumber].OnPointerClick(new PointerEventData(EventSystem.current));

                _canClick = false;
                StartCoroutine(this.DoAfterSeconds(() => { _canClick = true; }, _clickDelay));
            }
        }
    }
}

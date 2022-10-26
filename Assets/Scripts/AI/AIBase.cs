using UnityEngine;
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
            PointerButton button = new PointerButton();

            switch (selectedColumnNumber)
            {
                case 0:
                    button.Init(PointerKey.column0);
                    break;

                case 1:
                    button.Init(PointerKey.column1);
                    break;

                case 2:
                    button.Init(PointerKey.column2);
                    break;

                case 3:
                    button.Init(PointerKey.column3);
                    break;

                case 4:
                    button.Init(PointerKey.column4);
                    break;

                case 5:
                    button.Init(PointerKey.column5);
                    break;
            }

            StartCoroutine(this.DoAfterSeconds(button.OnPointerClick, 1f));

            //button.OnPointerClick();

            //if (_canClick)
            //{
            //    _columnsManager.Columns[selectedColumnNumber].OnPointerClick(new PointerEventData(EventSystem.current));

            //    _canClick = false;
            //}
            //else
            //{
            //    StartCoroutine(this.DoAfterSeconds(() => 
            //    { 
            //        _canClick = true;
            //        ClickOnSelectedColumn(selectedColumnNumber);
            //    }, _clickDelay));
            //}
        }
    }
}

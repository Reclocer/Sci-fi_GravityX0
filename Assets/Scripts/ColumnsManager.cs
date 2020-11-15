using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Corebin.GravityX0
{
    public class ColumnsManager : MonoBehaviour
    {
        [SerializeField] private Board _board;
        public Button[] Columns = new Button[6];

#if UNITY_EDITOR
        private void OnValidate()
        {
            Columns = GetComponentsInChildren<Button>();
        }
#endif
        private void Start()
        {
            _board.WinCellCount.OnWin += (fill) => { DisableColumnsButtons(); };
        }

        //TODO: заменить button на свои IPointHendler

        private void DisableColumnsButtons()
        {
            foreach(Button button  in Columns)
            {
                button.interactable = false;
            }
        }
    }
}
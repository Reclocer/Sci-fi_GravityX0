using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace Corebin.GravityX0
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private Board _board;
        [SerializeField] private Text _winnerText;
        [SerializeField] private GameObject _restartBtn;

        private void Start()
        {
            _board.WinCellCount.OnWin += EnableFinish;
        }

        private void EnableFinish(Fill fill)
        {            
            _winnerText.enabled = true;

            if(fill == Fill.Iks)
            {
                _winnerText.text = "X win";
            }
            else
            {
                _winnerText.text = "0 win";
            }
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
    }
}

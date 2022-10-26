using System;
using System.Collections.Generic;
using UnityEngine;
using Corebin.GravityX0;

public enum PointerKey
{
    Tap = 0,
    goHome = 1,
    back = 2,

    settingOnOff = 3,
    present = 4,

    soundOnOff = 5,
    musicOnOff = 6,

    column0 = 7,
    column1 = 8,
    column2 = 9,
    column3 = 10,
    column4 = 11,
    column5 = 12,
}

public class InputHandler : MonoBehaviour
{
    //[SerializeField] protected ParticleSystem _particleSystem;
    [SerializeField] private TilesManager _tilesManager;
    public Action OnTap = () => { };
    protected bool _isClicked = false;

    public static InputHandler Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void ClickHandler(PointerButton button, Action callback)
    {
        switch (button.PointerKey)
        {
            case PointerKey.soundOnOff:
                callback();
                //_particleSystem.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
                //_particleSystem.Stop();
                //_particleSystem.Play();
                //AudioController.InstanceBase.ClickButton();
                //_setting.ClickOnSound();
                break;

            case PointerKey.musicOnOff:
                callback();
                //_particleSystem.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
                //_particleSystem.Stop();
                //_particleSystem.Play();
                //AudioController.InstanceBase.ClickButton();
                //_setting.ClickOnMusic();
                break;
        }

        if (_isClicked)
            return;

        _isClicked = true;
        StartCoroutine(this.DoAfterSeconds(() => { _isClicked = false; }, 0.4f));
        //Debug.Log(button.PointerKey);

        switch (button.PointerKey)
        {
            case PointerKey.Tap:
                callback();
                OnTap();
                break;

            case PointerKey.goHome:
                callback();
                //CarSelectionController.Instance.ReturnToSelection();
                button.gameObject.SetActive(false);
                break;

            case PointerKey.column0:
                callback();
                _tilesManager.ClickOnColumn(0);
                break;

            case PointerKey.column1:
                callback();
                _tilesManager.ClickOnColumn(1);
                break;

            case PointerKey.column2:
                callback();
                _tilesManager.ClickOnColumn(2);
                break;

            case PointerKey.column3:
                callback();
                _tilesManager.ClickOnColumn(3);
                break;

            case PointerKey.column4:
                callback();
                _tilesManager.ClickOnColumn(4);
                break;

            case PointerKey.column5:
                callback();
                _tilesManager.ClickOnColumn(5);
                break;

            default:
                Debug.Log($"{button.PointerKey} not handled!");
                break;
        }
    }

    public void SetDelayForClick(float seconds)
    {
        _isClicked = true;
        StartCoroutine(this.DoAfterSeconds(() => { _isClicked = false; }, seconds));
    }
}
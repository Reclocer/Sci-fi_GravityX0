using System;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class ButtonsHandlerBase : MonoBehaviour
{
    //[SerializeField] protected ParticleSystem _particleSystem;
    //public EventSystem EventSystem;
    //public Action OnTap = () => { };
    //protected bool _isClicked = false;

    //public static ButtonsHandlerBase InstanceBase;

    //protected virtual void Awake()
    //{
    //    if (InstanceBase == null)
    //    {
    //        InstanceBase = this;
    //        //DontDestroyOnLoad(this);
    //    }
    //    else
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    ///// <summary>
    ///// Handler the all buttons clicks
    ///// </summary>
    ///// <param name="button"></param>
    //public abstract void ClickHandler<PB>(PB button, Action callback);

    //public virtual void SetDelayForClick(float seconds)
    //{
    //    _isClicked = true;
    //    StartCoroutine(this.DoAfterSeconds(() => { _isClicked = false; }, seconds));
    //}
}
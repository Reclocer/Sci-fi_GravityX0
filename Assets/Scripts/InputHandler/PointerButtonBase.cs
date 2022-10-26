using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class PointerButtonBase : MonoBehaviour, IPointerClickHandler
{
    [Header("InfiniteAnimation")]
    [SerializeField] protected ButtonInfiniteAnimationState _infiniteAnimationState = ButtonInfiniteAnimationState.disable;

    [Tooltip("Where 1 is default scale")]
    [SerializeField] protected Vector2 _infiniteScaleRange = new Vector2(0.95f, 1.05f);
    [SerializeField] protected float _infiniteAnimationSpeed = 10f;

    [Tooltip("Cant start if animation state is Disable")]
    [SerializeField] protected bool _playOnStart = true;
    protected bool _playOnEnable = false;

    [Header("OnClickAnimation")]
    [SerializeField] protected ButtonAnimationState _animationState = ButtonAnimationState.disable;
    [Tooltip("Where 1 is default scale")]
    [SerializeField] protected Vector2 _scaleRange = new Vector2(0.95f, 1.05f);
    [SerializeField] protected float _animationSpeed = 10f;
    protected Vector3 _startScale = new Vector3(1.2f, 1.2f, 1);
    protected bool _isScaled = false;

    protected virtual void OnEnable()
    {
        if (_playOnEnable)
        {
            //PlayInfiniteAnimation();
            StartCoroutine(ReScaling());
        }

        _playOnEnable = false;
    }

    protected virtual void Start()
    {
        _animationSpeed /= 100;
        _infiniteAnimationSpeed /= 100;

        _startScale = transform.localScale;

        if (_playOnStart && _infiniteAnimationState != ButtonInfiniteAnimationState.disable)
            PlayInfiniteAnimation(_infiniteAnimationState);
    }

    public virtual void PlayInfiniteAnimation()
    {
        PlayInfiniteAnimation(_infiniteAnimationState);
    }

    public virtual void PlayInfiniteAnimation(ButtonInfiniteAnimationState state)
    {
        StopAllCoroutines();

        switch (state)
        {
            case ButtonInfiniteAnimationState.rescaling:
                if (gameObject.activeSelf)
                {
                    StartCoroutine(ReScaling());
                }
                else
                {
                    _playOnEnable = true;
                }
                break;
        }
    }

    public virtual void PlayInfiniteAnimationOnEnable()
    {
        _playOnEnable = true;
    }

    public abstract void OnPointerClick();

    public abstract void OnPointerClick(PointerEventData eventData);

    #region Animation
    protected virtual void PlaySelectedAnimation()
    {
        //StopAllCoroutines();

        switch (_animationState)
        {
            case ButtonAnimationState.rescalingDownUp:
                if (gameObject.activeSelf)
                    StartCoroutine(ReScalingDownUp());
                break;

            case ButtonAnimationState.rescalingUpDown:
                if (gameObject.activeSelf)
                    StartCoroutine(ReScalingUpDown());
                break;

            case ButtonAnimationState.scalingUp:
                if (gameObject.activeSelf && !_isScaled)
                {
                    _isScaled = true;
                    StartCoroutine(ScalingUp());
                }
                break;

            case ButtonAnimationState.scalingDown:
                if (gameObject.activeSelf && !_isScaled)
                {
                    _isScaled = true;
                    StartCoroutine(ScalingDown());
                }
                break;
        }
    }

    public virtual void SetDefaultScale()
    {
        StopAllCoroutines();
        _isScaled = false;
        transform.localScale = _startScale;
    }

    #region Infinite animations
    protected virtual IEnumerator ReScaling()
    {
        float deltaScale = _startScale.x * _infiniteAnimationSpeed * Time.deltaTime;
        ButtonAnimationState state = ButtonAnimationState.scalingDown;
        //bool final = false;

        while (true)
        {
            switch (state)
            {
                case ButtonAnimationState.scalingDown:
                    transform.localScale -= new Vector3(deltaScale, deltaScale, 0);

                    if (transform.localScale.x <= _startScale.x * _infiniteScaleRange.x)
                        state = ButtonAnimationState.scalingUp;
                    break;

                case ButtonAnimationState.scalingUp:
                    transform.localScale += new Vector3(deltaScale, deltaScale, 0);

                    if (transform.localScale.x >= _startScale.x * _infiniteScaleRange.y)
                        state = ButtonAnimationState.scalingDown;
                    break;
            }

            yield return null;
        }
    }
    #endregion Infinite animations

    protected virtual IEnumerator ReScalingDownUp()
    {
        float deltaScale = _startScale.x * _animationSpeed * Time.deltaTime;
        ButtonAnimationState state = ButtonAnimationState.scalingDown;
        bool final = false;

        while (!final)
        {
            switch (state)
            {
                case ButtonAnimationState.scalingDown:
                    transform.localScale -= new Vector3(deltaScale, deltaScale, 0);

                    if (transform.localScale.x <= _startScale.x * _scaleRange.x)
                        state = ButtonAnimationState.scalingUp;
                    break;

                case ButtonAnimationState.scalingUp:
                    transform.localScale += new Vector3(deltaScale, deltaScale, 0);

                    if (transform.localScale.x >= _startScale.x)
                    {
                        state = ButtonAnimationState.disable;
                        final = true;
                        StopCoroutine(ReScalingDownUp());
                    }
                    break;
            }

            yield return null;
        }
    }

    protected virtual IEnumerator ReScalingUpDown()
    {
        float deltaScale = _startScale.x * _animationSpeed * Time.deltaTime;
        ButtonAnimationState state = ButtonAnimationState.scalingUp;
        bool final = false;

        while (!final)
        {
            switch (state)
            {
                case ButtonAnimationState.scalingUp:
                    transform.localScale += new Vector3(deltaScale, deltaScale, 0);

                    if (transform.localScale.x >= _startScale.x * _scaleRange.y)
                        state = ButtonAnimationState.scalingDown;

                    break;

                case ButtonAnimationState.scalingDown:
                    transform.localScale -= new Vector3(deltaScale, deltaScale, 0);

                    if (transform.localScale.x <= _startScale.x)
                    {
                        state = ButtonAnimationState.disable;
                        final = true;
                        StopCoroutine(ReScalingUpDown());
                    }

                    break;
            }

            yield return null;
        }
    }

    public virtual void PlayAnimationScalingUp()
    {
        if (gameObject.activeSelf)
        {
            StartCoroutine(ScalingUp());
        }
        else
        {
            _playOnEnable = true;
        }
    }

    protected virtual IEnumerator ScalingUp()
    {
        float deltaScale = _startScale.x * _animationSpeed * Time.deltaTime;
        bool final = false;

        while (!final)
        {
            transform.localScale += new Vector3(deltaScale, deltaScale, 0);

            if (transform.localScale.x >= _startScale.x * _scaleRange.y)
            {
                final = true;
                StopCoroutine(ScalingUp());
            }

            yield return null;
        }
    }

    protected virtual IEnumerator ScalingDown()
    {
        float deltaScale = _startScale.x * _animationSpeed * Time.deltaTime;
        bool final = false;

        while (!final)
        {
            transform.localScale -= new Vector3(deltaScale, deltaScale, 0);

            if (transform.localScale.x <= _startScale.x * _scaleRange.x)
            {
                final = true;
                StopCoroutine(ScalingDown());
            }

            yield return null;
        }
    }
    #endregion Animation
}

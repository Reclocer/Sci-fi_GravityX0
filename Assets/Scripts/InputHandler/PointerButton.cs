using UnityEngine;
using UnityEngine.EventSystems;

public class PointerButton : PointerButtonBase
{
    [Space]
    [SerializeField] private PointerKey _pointerKey;
    public PointerKey PointerKey => _pointerKey;

    public void Init(PointerKey pointerKey)
    {
        _pointerKey = pointerKey;
    }

    public override void OnPointerClick()
    {
        OnPointerClick(new PointerEventData(EventSystem.current));
    }

    public override void OnPointerClick(PointerEventData eventData)
    {
        if (InputHandler.Instance == null)
        {
            Debug.Log($"{nameof(InputHandler)}.Instance = null");
            return;
        }

        if (eventData.button == PointerEventData.InputButton.Left)
            InputHandler.Instance.ClickHandler(this, PlaySelectedAnimation);
    }
}

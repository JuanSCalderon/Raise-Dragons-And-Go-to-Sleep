using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickButton : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _img;
    [SerializeField] private Sprite _default, _pressed;
    [SerializeField] private AudioClip _compressClip, _uncompressClip;
    [SerializeField] private AudioSource _source;

    private bool _isPressed = false;

    public void OnPointerClick(PointerEventData eventData)
    {
        _isPressed = !_isPressed;

        if (_isPressed)
        {
            _img.sprite = _pressed;
            _source.PlayOneShot(_compressClip);
        }
        else
        {
            _img.sprite = _default;
            _source.PlayOneShot(_uncompressClip);
        }

        Clicked();
    }

    public void Clicked()
    {
        Debug.Log("Perform Action clicked");
    }
}

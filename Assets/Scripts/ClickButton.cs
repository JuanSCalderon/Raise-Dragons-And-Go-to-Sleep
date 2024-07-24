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
    [SerializeField] private GameObject _panelToActivate;  // Panel a activar
    [SerializeField] private List<GameObject> _panelsToDeactivate;  // Paneles a desactivar
    [SerializeField] private GameObject _mainMenuPanel;  // Panel principal

    private bool _isPressed = false;
    private static GameObject _previousPanel;  // Panel anteriormente activo

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

        if (_panelToActivate.activeSelf)
        {
            DeactivatePanel();
        }
        else
        {
            ActivatePanel();
        }

        Clicked();
    }

    public void Clicked()
    {
        Debug.Log("Perform Action clicked");
    }

    private void ActivatePanel()
    {
        // Guardar el panel previamente activo
        _previousPanel = null;
        foreach (GameObject panel in _panelsToDeactivate)
        {
            if (panel.activeSelf)
            {
                _previousPanel = panel;
                panel.SetActive(false);
            }
        }

        _panelToActivate.SetActive(true);
        _img.sprite = _pressed;
        _source.PlayOneShot(_compressClip);

        // Reset other buttons' state
        foreach (ClickButton button in FindObjectsOfType<ClickButton>())
        {
            if (button != this && button._panelToActivate.activeSelf)
            {
                button.DeactivatePanel();
            }
        }
    }

    private void DeactivatePanel()
    {
        _panelToActivate.SetActive(false);
        _img.sprite = _default;
        _source.PlayOneShot(_uncompressClip);

        // Activar el panel previamente activo
        if (_previousPanel != null)
        {
            _previousPanel.SetActive(true);
            _previousPanel = null;  // Resetear el panel previo después de activarlo
        }
        else
        {
            // Si no hay panel previamente activo, activar el menú principal
            _mainMenuPanel.SetActive(true);
        }
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] private List<ToggleWithIntValue> _toggles;
    [SerializeField] private Button _buttonStart;
    
    private void Start()
    {
        foreach (var toggle in _toggles)
        {
            toggle.Subscribe(HandleToggleOnValueChange);
        }

        _buttonStart.onClick.AddListener(HandleButtonStartOnClick);
    }

    private void OnDestroy()
    {
        foreach (var toggle in _toggles)
        {
            toggle.Unsubscribe(HandleToggleOnValueChange);
        }

        _buttonStart.onClick.RemoveListener(HandleButtonStartOnClick);
    }

    private void HandleButtonStartOnClick()
    {
        SceneManager.LoadSceneAsync(Scene.GameScene.ToString(), LoadSceneMode.Single);
    }

    private void HandleToggleOnValueChange(int number)
    {
        BuildingsInitData.ResourceBuildingsNumber = number;
    }
}

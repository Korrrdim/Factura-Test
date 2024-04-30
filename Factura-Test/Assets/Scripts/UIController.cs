using System.Collections;
using System.Collections.Generic;
using Game.Scripts.Events;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    [SerializeField] private EventBool OnDeath;
    [SerializeField] private EventBool OnFinish;
    [Space]
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    private bool _wasEnd;

    private void OnEnable()
    {
        OnFinish.OnEvent += Win;
        OnDeath.OnEvent += Lose;
    }

    private void Win(bool _)
    {
        if (_wasEnd)
            return;
        _wasEnd = true;
        _winPanel.SetActive(true);
        _losePanel.SetActive(false);
    }

    private void Lose(bool _)
    {
        if (_wasEnd)
            return;
        _wasEnd = true;
        _winPanel.SetActive(false);
        _losePanel.SetActive(true);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void OnDisable()
    {
        OnFinish.OnEvent += Win;
        OnDeath.OnEvent += Lose;
    }
}

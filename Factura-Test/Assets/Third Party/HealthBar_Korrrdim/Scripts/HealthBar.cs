using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private delegate void updateHealthBar(float newHealth, float updateTime);
    private event updateHealthBar UpdateHealthBar;
    
    private Coroutine _updateHealthBarCor;

    private float StartHealth = 0;
    [SerializeField] private Image _healthBackGround;
    [SerializeField] private Image _healthVisual;
    [Space] 
    [SerializeField] private Gradient _healthVisualGradient;
    private void OnEnable()
    {
        UpdateHealthBar += OnUpdateHealthBar;
    }
    

    public virtual void Init(float startHealth)
    {
        StartHealth = startHealth;
        _healthVisual.color = _healthVisualGradient.Evaluate(_healthVisual.fillAmount);
    }

    protected void BaseActive(bool state)
    {
        _healthBackGround.enabled = state;
        _healthVisual.enabled = state;
    }
    
    private void OnUpdateHealthBar(float newHealth, float updateTime)
    {
        if (StartHealth == 0)
        {
            Debug.LogError($"Call Init method for Health Bar of {gameObject.name}");
            return;
        }

        if (_updateHealthBarCor != null)
            StopCoroutine(_updateHealthBarCor);
        
        StartCoroutine(UpdateHealthBarCor(newHealth, updateTime));
    }

    IEnumerator UpdateHealthBarCor(float newHealth, float updateTime)
    {
        UpdatingStart();

        var newFill = (newHealth / StartHealth);
        var oldFill = _healthVisual.fillAmount;
        
        for (float t = 0; t < 1; t += Time.unscaledDeltaTime / updateTime)
        {
            _healthVisual.fillAmount = Mathf.Lerp(oldFill, newFill, t);
            _healthVisual.color = _healthVisualGradient.Evaluate(_healthVisual.fillAmount);
            yield return null;
        }

        _healthVisual.fillAmount = newFill;
        _healthVisual.color = _healthVisualGradient.Evaluate(newFill);
        
        yield return new WaitForSecondsRealtime(updateTime);
        UpdatingEnd();
        _updateHealthBarCor = null;
    }

    protected virtual void UpdatingStart()
    {
        Debug.Log("Updating Start");
    }
    
    protected virtual void UpdatingEnd()
    {
        Debug.Log("Updating End");
    }
    

    private void OnDisable()
    {
        UpdateHealthBar -= OnUpdateHealthBar;
        if (_updateHealthBarCor != null)
            StopCoroutine(_updateHealthBarCor);
    }

    public void Invoke(float newHealth, float updateTime)
    {
        UpdateHealthBar?.Invoke(newHealth, updateTime);
    }
}

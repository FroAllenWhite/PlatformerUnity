using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class CharacterEvents : MonoBehaviour
{
    private CharacterInfo _info;
    private CharacterMovement _movement;
    private CharacterAnimations _animations;

    [SerializeField] private float _invulnerabilityTime;
    private float _currentInvulnerabilityTime;

    [SerializeField] private int _health = 20;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private Vector2 _startPosition;

    private void Start()
    {
        _info = FindObjectOfType<CharacterInfo>();
        _movement = GetComponent<CharacterMovement>();
        _animations = GetComponent<CharacterAnimations>();
        UpdateHealthUI();
        _startPosition = transform.position; // сохраняем начальную позицию персонажа
    }

    private void Update()
    {
        if (_currentInvulnerabilityTime > 0)
        {
            _currentInvulnerabilityTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Fruit fruit = collision.GetComponent<Fruit>();
        if (fruit != null)
        {
            fruit.Destroy();
            _info.AddScore(fruit.Points);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Trap trap = collision.GetComponent<Trap>();
        if (trap != null)
        {
            if (_currentInvulnerabilityTime <= 0)
            {
                Vector2 difference = transform.position - trap.transform.position;
                _movement.Knockback(difference.normalized);
                _animations.Hit();
                _currentInvulnerabilityTime = _invulnerabilityTime;
                _health -= 3;
                UpdateHealthUI();
                if (_health <= 0)
                {
                    Die();
                }
            }
        }
    }

    private void UpdateHealthUI()
    {
        if (_healthSlider != null)
        {
            _healthSlider.value = _health;
        }
    }

    private void Die()
    {
        _animations.Die();
        StartCoroutine(ReloadScene());
    }

    private IEnumerator ReloadScene()
    {
        yield return new WaitForSeconds(_animations.DeathAnimationDuration); // ждем завершения анимации смерти
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // перезагружаем текущую сцену
    }
}
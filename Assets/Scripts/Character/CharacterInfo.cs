using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterInfo : MonoBehaviour
{
    // Приватные переменные для хранения значения очков и здоровья
    private int _score;
    private float _health;

    // Ссылка на элемент текста в пользовательском интерфейсе для отображения очков
    [SerializeField] private TextMeshProUGUI _scoreText;

    // Ссылка на компонент аудиоисточника для проигрывания звуков
    [SerializeField] private AudioSource _audioSource;

    // Звуковой клип, который будет воспроизводиться при добавлении очков
    [SerializeField] private AudioClip _scoreAddedSound;

    private void Start()
    {
        // Получаем компонент аудиоисточника из объекта
        _audioSource = GetComponent<AudioSource>();
    }

    public void AddScore(int value)
    {
        // Увеличиваем значение очков на заданное значение
        _score += value;
        UpdateUI(); // Обновляем пользовательский интерфейс
        PlayScoreAddedSound(); // Проигрываем звук добавления очков
    }

    public void GetDamage(float damage)
    {
        // Уменьшаем значение здоровья на заданное значение
        _health -= damage;
    }

    private void UpdateUI()
    {
        // Обновляем текст очков в пользовательском интерфейсе
        _scoreText.text = _score.ToString();
    }

    private void PlayScoreAddedSound()
    {
        // Проигрываем звуковой клип добавления очков
        _audioSource.PlayOneShot(_scoreAddedSound);
    }
}

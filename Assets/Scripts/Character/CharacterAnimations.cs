using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    private Animator _animator; // Ссылка на компонент Animator, прикрепленный к персонажу
    private AudioSource _audioSource; // Ссылка на компонент AudioSource, прикрепленный к персонажу

    [SerializeField] private AudioClip _damageSound; // Звук при получении урона
    [SerializeField] private AudioClip _jumpSound; // Звук при прыжке
    [SerializeField] private AudioClip _walkSound; // Звук при движении

    [SerializeField] private float _deathAnimationDuration; // Длительность анимации смерти
    public float DeathAnimationDuration => _deathAnimationDuration; // Свойство для получения длительности анимации смерти

    public bool IsMoving { private get; set; } // Флаг, указывающий на движение персонажа
    public bool IsFlying { private get; set; } // Флаг, указывающий на полет персонажа
    public bool IsGrounded { private get; set; } // Флаг, указывающий на нахождение персонажа на земле

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>(); // Получаем компонент аниматора из дочерних объектов
        _audioSource = GetComponent<AudioSource>(); // Получаем компонент источника звука
    }

    private void Update()
    {
        _animator.SetBool("IsMoving", IsMoving); // Устанавливаем значение параметра "IsMoving" в аниматоре
        _animator.SetBool("IsGrounded", IsGrounded); // Устанавливаем значение параметра "IsGrounded" в аниматоре
        _animator.SetBool("IsFlying", IsFlying); // Устанавливаем значение параметра "IsFlying" в аниматоре
    }

    public void Hit()
    {
        _animator.SetTrigger("Hit"); // Устанавливаем триггер "Hit" в аниматоре для проигрывания анимации удара
        _audioSource.PlayOneShot(_damageSound); // Проигрываем звук получения урона
    }

    public void Jump()
    {
        _animator.SetTrigger("Jump"); // Устанавливаем триггер "Jump" в аниматоре для проигрывания анимации прыжка
        _audioSource.PlayOneShot(_jumpSound); // Проигрываем звук прыжка
    }

    public void Die()
    {
        _animator.SetTrigger("Die"); // Устанавливаем триггер "Die" в аниматоре для проигрывания анимации смерти
        StartCoroutine(Respawn()); // Запускаем корутину для реализации возрождения персонажа
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSecondsRealtime(1f); // Ждем 1 секунду
        Time.timeScale = 1; // Восстанавливаем нормальную скорость времени
    }
}

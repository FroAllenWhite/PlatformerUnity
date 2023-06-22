using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float _speed; // Скорость перемещения персонажа
    [SerializeField] private float _jumpForce; // Сила прыжка персонажа
    [SerializeField] private Vector3 _groundCheckOffset; // Смещение точки проверки нахождения на земле
    [SerializeField] private LayerMask groundMask; // Маска слоя "Земля"

    private Vector3 _input; // Вектор ввода от игрока
    private bool _isMoving; // Флаг движения персонажа
    private bool _isGrounded; // Флаг нахождения на земле
    private bool _isFlying; // Флаг нахождения в прыжке/полете

    private Rigidbody2D _rigidbody; // Ссылка на компонент Rigidbody2D персонажа
    private CharacterAnimations _animations; // Ссылка на компонент CharacterAnimations персонажа
    [SerializeField] private SpriteRenderer _characterSprite; // Ссылка на компонент SpriteRenderer персонажа

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animations = GetComponent<CharacterAnimations>();
    }

    private void Update()
    {
        Move();
        CheckGround();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        _animations.IsMoving = _isMoving;
        _animations.IsGrounded = _isGrounded;
        _animations.IsFlying = IsFlying();
    }

    private bool IsFlying()
    {
        if (_rigidbody.velocity.y < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CheckGround()
    {
        float rayLength = 0.4f; // Длина луча проверки нахождения на земле
        Vector3 rayStartPosition = transform.position + _groundCheckOffset; // Положение начала луча проверки
        RaycastHit2D hit = Physics2D.Raycast(rayStartPosition, Vector3.down, rayLength, groundMask); // Выполняем лучевой луч вниз и получаем информацию о столкновении

        _isGrounded = hit.collider != null && hit.collider.CompareTag("Ground"); // Проверяем, находится ли коллайдер земли под персонажем
    }

    private void Move()
    {
        _input = new Vector2(Input.GetAxis("Horizontal"), 0); // Получаем ввод от игрока по горизонтали

        if(_input.x > 0)
        {
            transform.position += _input * _speed * Time.deltaTime; // Перемещаем персонажа вправо
        }
        else if (_input.x < 0) // Исправлено условие для движения влево
        {
            transform.position -= -_input * _speed * Time.deltaTime; // Перемещаем персонажа влево
        }

        _isMoving = _input.x != 0 ? true : false; // Проверяем, движется ли персонаж

        if (_isMoving)
        {
            _characterSprite.flipX = _input.x > 0 ? false : true; // Отражаем спрайт персонажа по горизонтали в зависимости от направления движения
        }
    }

    private void Jump()
    {
        if (_isGrounded) // Проверяем, находится ли персонаж на земле перед прыжком
        {
            _rigidbody.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse); // Применяем силу прыжка к Rigidbody2D персонажа
            _animations.Jump(); // Запускаем анимацию прыжка
        }
    }

    public void Knockback(Vector2 vector)
    {
        _rigidbody.velocity = Vector2.zero; // Обнуляем текущую скорость персонажа
        float knockbackForce = 3f; // Сила отталкивания
        _rigidbody.AddForce(vector * knockbackForce, ForceMode2D.Impulse); // Применяем силу отталкивания к Rigidbody2D персонажа
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; // Устанавливаем цвет для гизмо
        Vector3 rayStartPosition = transform.position + _groundCheckOffset; // Положение начала луча проверки
        Gizmos.DrawLine(rayStartPosition, rayStartPosition + Vector3.down * 0.3f); // Рисуем луч проверки нахождения на земле
    }
}

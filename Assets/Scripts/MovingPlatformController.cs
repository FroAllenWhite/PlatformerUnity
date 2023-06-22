using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    public float speed = 2f; // Скорость движения платформы
    public float distance = 5f; // Расстояние, на которое будет перемещаться платформа

   private Rigidbody2D rb;
    private Vector2 startPosition;
    private Vector2 targetPosition;
    private bool movingRight = true; // Флаг для определения направления движения платформы

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        startPosition = rb.position;
        targetPosition = startPosition + new Vector2(distance, 0f); // Вычисляем конечную позицию платформы
    }

    private void FixedUpdate()
    {
        // Определение направления движения платформы
        if (movingRight)
        {
            MoveToTarget(targetPosition); // Движение платформы к целевой позиции
        }
        else
        {
            MoveToTarget(startPosition); // Движение платформы к начальной позиции
        }
    }

    private void MoveToTarget(Vector2 target)
    {
        // Перемещение платформы к целевой позиции
        Vector2 newPosition = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPosition);

        // Проверка достижения целевой позиции
        if (Vector2.Distance(rb.position, target) < 0.1f)
        {
            // Изменение направления движения платформы
            movingRight = !movingRight;
        }
    }
}

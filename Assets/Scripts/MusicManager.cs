using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance; // Статическая переменная для хранения единственного экземпляра класса MusicManager

    void Awake()
    {
        if (instance == null) // Проверяем, существует ли уже экземпляр класса MusicManager
        {
            instance = this; // Если нет, то сохраняем текущий экземпляр как единственный экземпляр
            DontDestroyOnLoad(gameObject); // Отмечаем этот объект (в данном случае игровой объект, к которому привязан этот скрипт) для сохранения при переходе между сценами
        }
        else
        {
            Destroy(gameObject); // Если уже существует другой экземпляр класса MusicManager, то уничтожаем этот экземпляр
        }
    }
}

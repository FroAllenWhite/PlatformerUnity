using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class pause_menu : MonoBehaviour
{
    public static bool GameIsPaused = false; // Переменная для отслеживания состояния паузы
    public AudioSource audioSource; // Аудио-источник для воспроизведения звука
    public GameObject pauseMenuUI; // Ссылка на игровой объект, представляющий интерфейс паузы
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // Если нажата клавиша Escape
        {
            if (GameIsPaused) // Если игра уже находится на паузе, то возобновить игру
            {
                Resume();
            }
            else // Если игра не на паузе, то поставить игру на паузу
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        StartCoroutine(PlaySoundAfterDelay()); // Запустить корутину для воспроизведения звука после небольшой задержки
        pauseMenuUI.SetActive(false); // Отключить интерфейс паузы
        Time.timeScale = 1f; // Возобновить время в игре
        GameIsPaused = false; // Обновить состояние паузы
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true); // Включить интерфейс паузы
        Time.timeScale = 0f; // Остановить время в игре
        GameIsPaused = true; // Обновить состояние паузы
    }
    public void Quit()
    {
        Time.timeScale = 1f; // Возобновить время в игре перед переходом на другую сцену
        SceneManager.LoadScene("Menu"); // Загрузить сцену с главным меню (замените "Menu" на имя вашей сцены главного меню)
    }
    private IEnumerator PlaySoundAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // Небольшая задержка перед воспроизведением звука
        audioSource.Play(); // Воспроизвести звук
    }
}


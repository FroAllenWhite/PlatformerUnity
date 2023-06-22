using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeadZone : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource; // Аудиоисточник для воспроизведения звуковых эффектов
    [SerializeField] private AudioClip _impactSound; // Звуковой эффект при столкновении

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(PlayImpactSoundAndLoadScene()); // Запустить корутину для воспроизведения звука и загрузки сцены
    }

    private IEnumerator PlayImpactSoundAndLoadScene()
    {
        _audioSource.PlayOneShot(_impactSound); // Воспроизвести звуковой эффект один раз

        // Ждем, пока звук полностью воспроизводится
        yield return new WaitForSeconds(_impactSound.length);

        // Загружаем текущую сцену
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
 
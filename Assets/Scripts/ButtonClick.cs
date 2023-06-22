using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ButtonClick : MonoBehaviour
{
    public Image buttonImage;
    public Sprite normalSprite;
    public Sprite pressedSprite;
    public float delayTime = 0.1f;

    public AudioClip clickSound;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = clickSound;
    }

    IEnumerator DelayedChangeSprite()
    {
        yield return new WaitForSeconds(delayTime);
        buttonImage.sprite = pressedSprite;
    }

    public void OnExit()
    {
        audioSource.Play();
        Debug.Log("Button Clicked");
        Application.Quit(); // Выход из игры

    }
}

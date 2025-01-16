using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DrumButton : MonoBehaviour
{
    public AudioClip drumSound; // Перетаскиваем звук сюда через инспектор
    public KeyCode key; // Привязываем клавишу через инспектор
    private AudioSource audioSource;
    private Button button;
    private Color originalColor; // Исходный цвет кнопки
    public Color pressedColor = Color.gray; // Цвет при нажатии

    void Start()
    {
        // Добавляем или получаем компонент AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.clip = drumSound;

        // Получаем компонент Button и сохраняем исходный цвет
        button = GetComponent<Button>();
        if (button != null)
        {
            originalColor = button.image.color;
        }
    }

    void Update()
    {
        // Проверяем, нажата ли назначенная клавиша
        if (Input.GetKeyDown(key))
        {
            if (button != null)
            {
                PlaySound();
                ShowPressedEffect();
            }
        }
    }

    public void PlaySound()
    {
        if (audioSource != null && drumSound != null)
        {
            audioSource.Play();
        }
    }

    public void ShowPressedEffect()
    {
        // Меняем цвет кнопки на pressedColor
        button.image.color = pressedColor;

        // Возвращаем цвет кнопки обратно через 0.2 секунды
        Invoke(nameof(ResetColor), 0.2f);
    }

    public void ResetColor()
    {
        button.image.color = originalColor;
    }

    // Вызывается при наведении мыши
    public void OnMouseEnter()
    {
        PlaySound();
    }
}

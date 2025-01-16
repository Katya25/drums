using System.Collections.Generic;
using UnityEngine;

public class DrumRecorder : MonoBehaviour
{
    public List<DrumEvent> recordedEvents = new List<DrumEvent>(); // Храним события
    private bool isRecording = false; // Флаг записи
    private float startTime; // Время начала записи

    [System.Serializable]
    public class DrumEvent
    {
        public string drumName; // Название барабана
        public float time; // Время нажатия
    }

    public void StartRecording()
    {
        recordedEvents.Clear(); // Очищаем старую запись
        isRecording = true;
        startTime = Time.time; // Запоминаем время начала записи
    }

    public void StopRecording()
    {
        isRecording = false;
    }

    public void PlayRecording()
    {
        StartCoroutine(PlayEvents());
    }

    private IEnumerator<WaitForSeconds> PlayEvents()
    {
        foreach (DrumEvent drumEvent in recordedEvents)
        {
            yield return new WaitForSeconds(drumEvent.time - (recordedEvents.Count > 0 ? recordedEvents[0].time : 0));
            GameObject drum = GameObject.Find(drumEvent.drumName); // Находим барабан по имени
            if (drum != null)
            {
                drum.GetComponent<DrumButton>().PlaySound(); // Проигрываем звук
            }
        }
    }

    public void RecordDrum(string drumName)
    {
        if (isRecording)
        {
            recordedEvents.Add(new DrumEvent
            {
                drumName = drumName,
                time = Time.time - startTime
            });
        }
    }
}

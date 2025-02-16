using System.Linq;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundContainer : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private Sound[] _sounds;

    private void OnValidate() => _source ??= GetComponent<AudioSource>();

    public void Play(AnswerStatus answerStatus)
    {
        AudioClip clip = _sounds.FirstOrDefault(sound => sound.AnswerStatus == answerStatus).Clip;
        _source.clip = clip;
        _source.Play();
    }

    public void Stop() => _source.Stop();
}
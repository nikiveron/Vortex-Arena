using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BackgroundMusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private List<AudioClip> _audioClips;

    private void Awake()
    {
        _audioClips = _audioClips.OrderBy(x => Random.value).ToList();
    }

    private void Start()
    {
        StartCoroutine(PlayAudioClips());
    }

    public IEnumerator PlayAudioClips()
    {
        while (true)
        {
            foreach (var clip in _audioClips)
            {
                _audioSource.clip = clip;
                _audioSource.Play();

                yield return new WaitForSeconds(10);
            }
        }
    }
}

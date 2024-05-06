
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingleMono<AudioManager>
{
    private class AudioInfo
    {
        public long Id;

        public AudioSource AudioSource;
    }
    
    private long _idCounter;

    private List<AudioInfo> _audioList;

    private Dictionary<long, AudioInfo> _audioDic;

    private List<AudioSource> _pool;

    private AudioSource GetAudioSourceFromPool()
    {
        if (_pool.Count > 0)
        {
            var audioSource = _pool[0];
            audioSource.gameObject.SetActive(true);
            _pool.RemoveAt(0);
            return audioSource;
        }
        else
        {
            var audioSourceGO = new GameObject("Audio Source", typeof(AudioSource));
            audioSourceGO.transform.SetParent(transform);
            return audioSourceGO.GetComponent<AudioSource>();
        }
    }

    private void ReturnAudioSourceToPool(AudioSource audioSource)
    {
        audioSource.clip = null;
        audioSource.gameObject.SetActive(false);
        _pool.Add(audioSource);
    }

    public override void Init()
    {
        _idCounter = 0;

        _audioList = new List<AudioInfo>();
        _audioDic = new Dictionary<long, AudioInfo>();
        _pool = new List<AudioSource>();
    }

    public override void Tick(int delta)
    {
        for (int i = 0; i < _audioList.Count; i++)
        {
            var audioInfo = _audioList[i];
            if (!audioInfo.AudioSource.isPlaying)
            {
                ReturnAudioSourceToPool(audioInfo.AudioSource);
                _audioDic.Remove(audioInfo.Id);
                _audioList.RemoveAt(i);
                break;
            }
        }
    }

    public long PlayAudio(string audioName, bool loop)
    {
        var audioClip = Resources.Load<AudioClip>($"Audios/{audioName}");

        var audioSource = GetAudioSourceFromPool();

        audioSource.clip = audioClip;
        audioSource.loop = loop;
        audioSource.Play();

        var audioInfo = new AudioInfo() { AudioSource = audioSource, Id = _idCounter };
        
        _audioList.Add(audioInfo);
        _audioDic.Add(_idCounter, audioInfo);
        return _idCounter++;
    }

    public void StopAudio(long id)
    {
        if (_audioDic.TryGetValue(id, out var audioInfo))
        {
            audioInfo.AudioSource.Stop();
            ReturnAudioSourceToPool( audioInfo.AudioSource);
            _audioList.Remove(audioInfo);
            _audioDic.Remove(id);
        }
    }

    public bool IsPlaying(long id)
    {
        return _audioDic.TryGetValue(id, out var _);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager
{
    AudioSource[] _audioSources = new AudioSource[(int)Define.Sound.MaxCount];
    Dictionary<string, AudioClip> _audioClips = new Dictionary<string, AudioClip>();

    public void Init()
    {
        GameObject root = GameObject.Find("@Sound");
        if(root == null)
        {
            root = new GameObject { name = "@Sound" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sound));
            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject go = new GameObject(soundNames[i]);
               _audioSources[i] = go.AddComponent<AudioSource>();
                go.transform.parent = root.transform;
            }

            _audioSources[(int)Define.Sound.Bgm].loop = true;
        }

    }

    public void Clear()
    {
        foreach (AudioSource audioSource in _audioSources)
        {
            audioSource.clip= null;
            audioSource.Stop();
        }
        _audioClips.Clear();
    }

    public void Play(string name, Define.Sound type = Define.Sound.Effect)
    {
        AudioClip audioClip = GetOrAddAudioClip(name, type);
        Play(audioClip, type);
    }

    public void Play(AudioClip audioClip, Define.Sound type = Define.Sound.Effect)
    {
        if (audioClip == null)
            return;
        
        if (type == Define.Sound.Bgm)
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Bgm];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else
        {
            AudioSource audioSource = _audioSources[(int)Define.Sound.Effect];
            audioSource.PlayOneShot(audioClip);
        }
    }

    AudioClip GetOrAddAudioClip(string name, Define.Sound type = Define.Sound.Effect)
    {
        AudioClip audioClip = null;

        if (type == Define.Sound.Bgm)
        {
            audioClip = Managers.Resource.Load<AudioClip>(name); ;
        }
        else
        {
            if (_audioClips.TryGetValue(name, out audioClip) == false)
            {
                audioClip = Managers.Resource.Load<AudioClip>(name);
                _audioClips.Add(name, audioClip);
            }
        }

        if (audioClip == null)
            Debug.Log($"AudioClip Missing ! {name}");

        return audioClip;
    }

}

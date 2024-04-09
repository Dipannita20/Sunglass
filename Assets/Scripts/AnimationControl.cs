using System.Collections;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    public static AnimationControl instance;
    public Animator CharacterAnimator;
    public AnimationClip idle1, idle2, idle3, lip1, lip2, lip3, lip4, lip5, lip6, lip7, lip8;
    public AudioSource audSource;
    public AudioClip lipaudio1, lipaudio2, lipaudio3, lipaudio4, lipaudio5, lipaudio6, lipaudio7, lipaudio8;

    private void Awake()
    {
        instance = this;
    }

    public void PlayAudioDialog(AudioClip clipName, float volume)
    {
        audSource.clip = clipName;
        audSource.volume = volume;
        audSource.Play();
    }

    public IEnumerator PlayAnimationClip(AnimationClip clipInfo, AnimationClip idleClipInfo, AudioClip clipName, float volume)
    {
        Debug.Log("playanimation");
        CharacterAnimator.Play(clipInfo.name);
        PlayAudioDialog(clipName, volume);
        yield return new WaitForSeconds(clipInfo.length);
        CharacterAnimator.CrossFade(idleClipInfo.name, 0.1f);

    }
}

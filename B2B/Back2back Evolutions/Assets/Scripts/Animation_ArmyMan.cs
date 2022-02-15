using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_ArmyMan : MonoBehaviour
{
    [SerializeField] List<AnimationClip> AnimationsClips = new List<AnimationClip>();

    //private Animator _animator;

    private Animation _animation;

    //List<AnimationState> states = new List<AnimationState>(animation.Cast<AnimationState>());

    void Start()
    {
        //_animator = gameObject.GetComponent<Animator>();
        _animation = gameObject.GetComponent<Animation>();
    }


    string ClipIndexToName(int index)
    {
        AnimationClip clip = GetClipByIndex(index);
        if (clip == null)
            return null;
        return clip.name;
    }
    AnimationClip GetClipByIndex(int index)
    {
        int i = 0;
        foreach (Animation animationState in _animation)
        {
            if (i == index)
                return animationState.clip;
            i++;
        }
        return null;
    }

    public void DonePlaying()
    {
        Debug.Log("Done Playing Called");
        SelectClip();
    }

    private void SelectClip()
    {
        int next = Random.Range(0, 2);
        GetClipByIndex(next);
        _animation.Play(ClipIndexToName(next));

        Debug.Log("SelectClip Called");
        Debug.Log("index: " + next);
    }


    public void GetReaction(string reaction)
    {
        if (reaction == "Annoyed")
        {

        }


        else
        {

        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_ArmyMan : MonoBehaviour
{
    [SerializeField] List<AnimationClip> AnimationsClips = new List<AnimationClip>();

    private Animator _animator;

    void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
    }


    public void DonePlaying()
    {
        //Debug.Log("Done Playing Called");
        SelectClip();
    }

    private void SelectClip()
    {
        int next = Random.Range(0, 2);
        AnimationsClips[next].ToString();
        _animator.Play(AnimationsClips[next].name);
        //Debug.Log("SelectClip Called");
        //Debug.Log("index: " + next);
        //Debug.Log("name: " + AnimationsClips[next].name);
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

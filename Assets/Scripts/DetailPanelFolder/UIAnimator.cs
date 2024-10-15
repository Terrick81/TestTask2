using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimator : MonoBehaviour
{
    private Animator _animator;
    private bool _hide = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Swith()
    {
        _hide = !_hide;
        _animator.SetBool("hide", _hide);
        _animator.SetTrigger("swith");
    }

}

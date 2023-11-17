using UnityEngine;
using Random = UnityEngine.Random;

namespace Misc
{
    public class RandomIdleAnimation : MonoBehaviour
    {
        private Animator myAnimator;

        private void Awake()
        {
            myAnimator = GetComponent<Animator>();
        }

        private void Start()
        {
            AnimatorStateInfo stateInfo = myAnimator.GetCurrentAnimatorStateInfo(0);
            myAnimator.Play(stateInfo.fullPathHash, -1, Random.Range(0, 1f));
        }
    }
}

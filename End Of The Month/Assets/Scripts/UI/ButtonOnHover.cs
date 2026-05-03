using UnityEngine;
using UnityEngine.EventSystems;

public class UIHoverAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Animator animator;
    public bool card = false;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetTrigger("Hover");
            
        }

        if(AudioManager.I != null && AudioManager.I.buttonHover != null && !card)
        {
            AudioManager.I.PlayOneShot(AudioManager.I.buttonHover);
        }
        else if (card)
        {
            AudioManager.I.PlayOneShot(AudioManager.I.paperHover);
        }
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (animator != null)
        {
            animator.SetTrigger("Normal");
        }
    }
}
using UnityEngine;
using UnityEngine.U2D.Animation;

public class PlayerVisual : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private SpriteLibrary spriteLibrary;

    private readonly int isMoveHash = Animator.StringToHash("IsMove");

    
    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        spriteLibrary = GetComponent<SpriteLibrary>();
    }
    
    
    public void FlipSpriteRenderer(bool isFlip)
    {
        spriteRenderer.flipX = isFlip;
    }
    
    
    public void SetAnimation(bool isMoving)
    {
        animator.SetBool(isMoveHash, isMoving);
    }
    
    
    public void SetSpriteLibrary(SpriteLibraryAsset spriteLibraryAsset)
    {
        this.spriteLibrary.spriteLibraryAsset = spriteLibraryAsset;
    }
}

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
    public void SetDirection(Vector2 dir)
    {
        if (dir.x != 0)
            spriteRenderer.flipX = dir.x < 0;
    }
    public void SetMoving(bool isMoving)
    {
        animator.SetBool(isMoveHash, isMoving);
    }
    public void SetSpriteLibrary(SpriteLibraryAsset spriteLibraryAsset)
    {
        this.spriteLibrary.spriteLibraryAsset = spriteLibraryAsset;
    }
}

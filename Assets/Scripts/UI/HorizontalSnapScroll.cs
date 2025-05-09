using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorizontalSnapScroll : SnapScroll
{

    protected override void SetLayoutPadding()
    {
        var viewPortHalfSize = (int)viewPort.rect.size.x / 2;

        layoutGroup.padding.left = viewPortHalfSize;
        layoutGroup.padding.right = viewPortHalfSize;
    }

    protected override Vector2 GetMoveContentPos() 
        =>   new (-childItems[centerIndex].anchoredPosition.x, content.anchoredPosition.y);
    protected override float GetMaxDisToViewPort(Vector2 viewportCenter) 
        => viewportCenter.x / 2;

    protected override float GetItemDisToViewPort(Vector2 itemCenter, Vector2 viewPortCenter)
        => itemCenter.x - viewPortCenter.x;

}

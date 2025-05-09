using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalSnapScroll : SnapScroll
{
    protected override void SetLayoutPadding()
    {
        var viewPortHalfSize = (int)viewPort.rect.size.y / 2;

        layoutGroup.padding.top = viewPortHalfSize;
        layoutGroup.padding.bottom = viewPortHalfSize;
    }

    protected override Vector2 GetMoveContentPos() 
        =>   new (content.anchoredPosition.x, -childItems[centerIndex].anchoredPosition.y);
    protected override float GetMaxDisToViewPort(Vector2 viewportCenter) 
        => viewportCenter.y / 2;

    protected override float GetItemDisToViewPort(Vector2 itemCenter, Vector2 viewPortCenter)
        => itemCenter.y - viewPortCenter.y;
}

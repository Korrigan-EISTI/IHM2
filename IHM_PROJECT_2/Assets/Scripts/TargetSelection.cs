using UnityEngine;
using UnityEngine.UI;

public class TargetSelection : MonoBehaviour
{
    private float hoverStartTime;
    private bool isHovering = false;

    public Text feedbackText;

    private void OnMouseEnter()
    {
        hoverStartTime = Time.time;
        isHovering = true;
    }

    private void OnMouseExit()
    {
        isHovering = false;
    }

    private void OnMouseDown()
    {
        if (isHovering)
        {
            float reactionTime = Time.time - hoverStartTime;
            feedbackText.text = $"Temps de réaction : {reactionTime:F2} secondes";
        }
    }
}

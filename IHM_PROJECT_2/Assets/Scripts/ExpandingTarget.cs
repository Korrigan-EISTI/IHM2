using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExpandingButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public float centralExpandedSize = 200f;   // Taille du bouton survolé
    public float adjacentExpandedSize = 150f; // Taille des boutons adjacents
    public float normalSize = 100f;           // Taille normale des boutons
    public float expansionSpeed = 10f;        // Vitesse d'animation

    private RectTransform rectTransform;
    private RectTransform leftNeighbor;
    private RectTransform rightNeighbor;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Transform parent = transform.parent;

        // Trouver les voisins directs (gauche et droite)
        int index = transform.GetSiblingIndex();
        if (index > 0)
        {
            leftNeighbor = parent.GetChild(index - 1).GetComponent<RectTransform>();
        }
        if (index < parent.childCount - 1)
        {
            rightNeighbor = parent.GetChild(index + 1).GetComponent<RectTransform>();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        StopAllCoroutines(); // Arrêter toute animation en cours
        StartCoroutine(ExpandButton());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopAllCoroutines(); // Arrêter toute animation en cours
        StartCoroutine(CollapseButtons());
    }

    private IEnumerator ExpandButton()
    {
        // Agrandir le bouton survolé
        StartCoroutine(AnimateSize(rectTransform, centralExpandedSize));

        // Agrandir les voisins directs
        if (leftNeighbor != null)
        {
            StartCoroutine(AnimateSize(leftNeighbor, adjacentExpandedSize));
        }
        if (rightNeighbor != null)
        {
            StartCoroutine(AnimateSize(rightNeighbor, adjacentExpandedSize));
        }

        yield return null;
    }

    private IEnumerator CollapseButtons()
    {
        // Réduire tous les boutons à leur taille normale
        StartCoroutine(AnimateSize(rectTransform, normalSize));
        if (leftNeighbor != null)
        {
            StartCoroutine(AnimateSize(leftNeighbor, normalSize));
        }
        if (rightNeighbor != null)
        {
            StartCoroutine(AnimateSize(rightNeighbor, normalSize));
        }

        yield return null;
    }

    private IEnumerator AnimateSize(RectTransform button, float targetSize)
    {
        Vector2 currentSize = button.sizeDelta;
        Vector2 target = new Vector2(targetSize, targetSize);

        while (Vector2.Distance(currentSize, target) > 0.1f)
        {
            currentSize = Vector2.Lerp(currentSize, target, Time.deltaTime * expansionSpeed);
            button.sizeDelta = currentSize;
            yield return null;
        }

        button.sizeDelta = target;
    }
}

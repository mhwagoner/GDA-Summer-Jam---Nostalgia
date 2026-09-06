using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class GlowingSprite : MonoBehaviour
{
    public Sprite glowSprite;
    public Sprite redGlowSprite;
    private Sprite _normalSprite;
    private SpriteRenderer _spriteRenderer;

    public ScoreCollider scoreEntryCollider;
    public ScoreCollider scoreExitCollider;

    private Color _originalColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _normalSprite = _spriteRenderer.sprite;
        _originalColor = _spriteRenderer.color;

        if (scoreExitCollider != null) scoreExitCollider.OnScoreEarned += DoGlow;
        if (scoreEntryCollider != null) scoreEntryCollider.OnScoreEarned += DoGlow;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (scoreEntryCollider != null) scoreEntryCollider.OnScoreEarned -= DoGlow;
        if (scoreExitCollider != null) scoreExitCollider.OnScoreEarned -= DoGlow;
    }

    private void DoGlow(int pointsEarned)
    {
        if (pointsEarned > 0)
        {
            StartCoroutine(YellowGlow()); 
        }
        else if (pointsEarned < 0)
        {
            StartCoroutine(RedGlow());
        }
    }

    private IEnumerator YellowGlow()
    {
        _spriteRenderer.sprite = glowSprite;
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.65f);
        _spriteRenderer.sprite = _normalSprite;
        _spriteRenderer.color = _originalColor;
    }

    private IEnumerator RedGlow()
    {

        _spriteRenderer.sprite = redGlowSprite;
        _spriteRenderer.color = Color.white;
        yield return new WaitForSeconds(0.65f);
        _spriteRenderer.sprite = _normalSprite;
        _spriteRenderer.color = _originalColor;
    }
}

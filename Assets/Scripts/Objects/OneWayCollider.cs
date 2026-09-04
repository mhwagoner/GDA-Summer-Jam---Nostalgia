using UnityEngine;

public class OneWayCollider : MonoBehaviour
{
    [SerializeField] private Collider2D colliderToBlock;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.TryGetComponent(out Object obj)) return;

        if (colliderToBlock == null)
        {
            Debug.Log("OneWayCollider not given a collider to block");
            return;
        }

        // Layer number represents the bit position in the mask, not the mask itself
        LayerMask layerToClear = 1 << colliderToBlock.gameObject.layer;
        obj.GetComponent<CircleCollider2D>().excludeLayers &= ~layerToClear;
    }
}

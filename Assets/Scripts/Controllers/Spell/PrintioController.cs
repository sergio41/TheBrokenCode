using UnityEngine;

public class PrintioController : SpellController
{

    // Start is called before the first frame update
    void Start()
    {
        base.StartBase();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        base.FixedUpdateBase();
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag.Equals(GameConstants.ENEMY))
        {
            var controller = collider.gameObject.GetComponent<EnemySpellEffectController>();
            controller.ShowHealth();
            Destroy(this.gameObject);
        }
    }
}

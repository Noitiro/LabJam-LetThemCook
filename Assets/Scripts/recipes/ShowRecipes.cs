using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShowRecipes : MonoBehaviour {
    [SerializeField] GameObject button;
    private Rigidbody2D rb;

    private void Start () {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update() {
       // showRecipes();
    }
    public void showRecipes() {
        if (transform.position.x < 0) {
            rb.linearVelocity = new Vector2(1 * 30, rb.linearVelocity.y);

        } 

    }

    public void hideRecipes() {
        if (transform.position.x > -3) {
            transform.Translate(Vector2.right * -1 * 50 * Time.deltaTime);
        }
    }
}

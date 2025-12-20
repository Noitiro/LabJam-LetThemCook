using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class ShowRecipes : MonoBehaviour {
    [SerializeField] GameObject button;
    bool show = false;
    private Rigidbody2D rb;

    void Update() {
        showRecipes();
    }
    public void showRecipes() {
        if (transform.position.x < -1) {
            transform.Translate(Vector2.right * 1 * 100 * Time.deltaTime);

        } else if (transform.position.x < -1 && transform.position.x > 0) {
            transform.Translate(0, 0, 0);

        }

    }

    public void hideRecipes() {
        if (transform.position.x > -3) {
            transform.Translate(Vector2.right * -1 * 50 * Time.deltaTime);
        }
    }
}

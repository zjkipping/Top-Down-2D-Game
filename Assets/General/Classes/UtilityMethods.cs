using UnityEngine;

public static class UtilityMethods {
    public static Vector2 GetRandomDirection() {
        float random = Random.Range(0f, 2 * Mathf.PI);
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }
}

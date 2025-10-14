using UnityEngine;

/// <summary>
/// Score management singleton
/// </summary>
public class Score : MonoBehaviour {

    /// <summary>
    /// Instance of the score manager
    /// </summary>
    public static Score Instance { get; private set; }

    /// <summary>
    /// Current score value
    /// </summary>
    public int Value { get; private set; }

    /// <summary>
    /// Initializes the score manager
    /// </summary>
    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Adds the specified value to the score
    /// </summary>
    /// <param name="value">Value to add</param>
    public void AddScore(int value) {
        Value += value;
        Debug.Log($"[{nameof(Score).ToUpperInvariant()}] add: {value}, current: {Value}");
    }

    /// <summary>
    /// Adds the specified value to the score
    /// Another method to add score, e.g. short Score.Add(10) 
    /// </summary>
    /// <param name="value">Value to add</param>
    public static void Add(int value) {
        if (Instance != null) {
            Instance.AddScore(value);
        }
    }
}

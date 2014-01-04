using UnityEngine;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {
	private static T instance = null;
	public static T Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType(typeof(T)) as T;

				// Object not found - create one
				if (instance == null) {
					Debug.LogWarning(typeof(T) + " instance was requested but not found. Creating one.");
					instance = new GameObject("Instance of " + typeof(T), typeof(T)).GetComponent<T>();
				}
				if (instance != null) {
					instance.Init();
				}
				else {
					Debug.LogError("Could not get or create an instance of " + typeof(T));
				}
			}
			return instance;
		}
	}

	// Assign if this is the first instance, else just show a warning that we have more than one
	void Awake() {
		if (instance == null) {
			instance = this as T;
			if (instance != null) {
				instance.Init();
			}
			else {
				Debug.LogError("Could not assign the instance of this class (" + typeof(T) + ")");
			}
		}
		else {
			Debug.LogWarning("Multiple instances of the MonoSingleton class " + typeof(T) + " created.");
		}
	}

	// Substitute for Awake() in implementing classes
	public virtual void Init() {}
}
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

namespace Framework {
  public class GameManager : MonoBehaviour {
    [Inject] public MenuMode MenuMode;
    [Inject] public StoryMode StoryMode;

    private GameMode _currentMode;
    private bool _isSwitching;

    public void RequestMode(GameMode mode) {
      StartCoroutine(SwitchMode(mode));
    }

    public void Quit() {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#endif
      Application.Quit();
    }

    private void Awake() {
      Time.timeScale = 0;
      MenuMode.Manager = this;
      StoryMode.Manager = this;

#if UNITY_EDITOR
      switch (SceneManager.GetActiveScene().buildIndex) {
        case 0:
          RequestMode(MenuMode);
          break;
        case 1:
          _currentMode = MenuMode;
          _currentMode.OnEditorStart();
          break;
        default:
          _currentMode = StoryMode;
          _currentMode.OnEditorStart();
          break;
      }
#else
      RequestMode(MenuMode);
#endif
    }

    private IEnumerator SwitchMode(GameMode mode) {
      yield return new WaitUntil(() => !_isSwitching);

      if (_currentMode == mode) {
        yield break;
      }

      _isSwitching = true;
      if (!ReferenceEquals(_currentMode, null)) {
        yield return _currentMode.OnEnd();
      }

      _currentMode = mode;
      yield return _currentMode.OnStart();

      _isSwitching = false;
    }
  }
}

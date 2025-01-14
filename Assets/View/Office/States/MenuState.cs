﻿using Cinemachine;
using Player.States;
using UnityEngine;
using Utils;

namespace View.Office.States {
  [DefaultExecutionOrder(200)]
  public class MenuState : BaseState {
    [SerializeField] protected CinemachineVirtualCamera Camera;
    protected MenuManager Manager;
    private CinemachineBrain _brain;
    private Cached<float> _progress;

    protected virtual void Awake() {
      Manager = GetComponent<MenuManager>();
      _brain = FindObjectOfType<CinemachineBrain>();
    }

    private void Start() {
      OnProgress(_progress);
    }

    protected virtual void LateUpdate() {
      float progress = IsActive ? 1 : 0;

      if (_brain.ActiveBlend != null) {
        if (_brain.ActiveBlend.CamA == Camera) {
          progress = 1 - _brain.ActiveBlend.BlendWeight;
        } else if (_brain.ActiveBlend.CamB == Camera) {
          progress = _brain.ActiveBlend.BlendWeight;
        }
      }

      if (_progress.HasChanged(progress)) {
        OnProgress(_progress);
      }
    }

    protected virtual void OnProgress(float value) { }

    public override void OnEnter() {
      base.OnEnter();
      Camera.Priority = 100;
    }

    public override void OnExit() {
      base.OnExit();
      Camera.Priority = 0;
    }
  }
}

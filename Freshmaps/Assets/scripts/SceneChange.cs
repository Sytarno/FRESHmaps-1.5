﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour {
    public void changeScene(string nextScene) {
        SceneManager.LoadScene(nextScene);
    }
}

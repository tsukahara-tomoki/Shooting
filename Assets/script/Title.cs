﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("start"))
        {
            SceneManager.LoadScene("BattleScene");
        }
        //if (Input.GetKey("return"))
        //{
        //    SceneManager.LoadScene("BattleScene");
        //}
    }
    public void BattleTransition ()
    {
        SceneManager.LoadScene("BattleScene");
    }
    public void TutorialTransition()
    {

    }
}

﻿using UnityEngine;

public class DestroyShell : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        Destroy(gameObject, 3);
    }
}
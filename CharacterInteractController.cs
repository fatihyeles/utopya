using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInteractController : MonoBehaviour
{
    CharacterController2d characterController;
    Rigidbody2D rgbd2d;
    [SerializeField] float offsetDistance = 1f; //karakterden öteleme mesafesiyle çarpmak
    [SerializeField] float sizeOfInteractableArea = 1.2f; //Karakterin etkileşim uzaklığı
    Character character;
    [SerializeReference] HighlightController highlightController; //Vurgulanan noktayı gösterme
    private void Awake()
    {
        characterController = GetComponent<CharacterController2d>();
        rgbd2d = GetComponent<Rigidbody2D>();
        character = GetComponent<Character>();
    }

    private void Update()
    {
        Check();
        if (Input.GetMouseButtonDown(1))
        {
            Interact(); // Farenin konumuna göre etkileşim
        }
    }

    private void Check()
    {
        Vector2 position = rgbd2d.position + characterController.lastMotionVector * offsetDistance; 

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);

        

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                highlightController.Highlight(hit.gameObject);
                return;
            }
        }

            highlightController.Hide();
    }

    private void Interact()
    {
        Vector2 position = rgbd2d.position + characterController.lastMotionVector * offsetDistance;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, sizeOfInteractableArea);//çemperin içinde çarpıştırıcıları önümüze getirmek için

        foreach (Collider2D c in colliders)
        {
            Interactable hit = c.GetComponent<Interactable>();
            if (hit != null)
            {
                hit.Interact(character);
                break;
            }
        }
    }

}

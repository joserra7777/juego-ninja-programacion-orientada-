using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaMove : MonoBehaviour
{
    // toda esta seccion sirve para organizar y declarar todas mis variables publicas, booleanas, privadas, etc 
    Animator anim;

    SpriteRenderer spriteNinja;
    public float Velocidad;
    public float fuerzaSalto;
    Rigidbody2D rb2D;
    bool estaEnPlataforma;
    bool desenfundar;

    // Start is called before the first frame update
    void Start()
    {
        // aqui declaro variables que voy a estar utilizando a lo largo de todo mi script 
        desenfundar = false;
        estaEnPlataforma = false;
        anim = GetComponent<Animator>();
        spriteNinja = GetComponent<SpriteRenderer>();
        rb2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
		#region
		
      
 
       
        #endregion
        // en esta region tengo organizados todos mis controles para cada una de las acciones que va a hacer el ninja 
        ControlMovimiento();
        ControlSalto();
        ControlAtaque();
        ControlCrouch();

          
        
    }
    // en el control ataque tengo unicamente los codigos para pasar del idle espada al ataque espada, y a la vez pasar del sato al ataque salto 
    void ControlAtaque()
    {
        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(1);
        bool presionarF = Input.GetKeyDown(KeyCode.F);
        if (estaEnPlataforma && stateInfo.IsName("idle_espada"))
        {
            Debug.Log("AtaqueMedio");

            if (presionarF)
            {
                anim.SetTrigger("ataque");
            }
            anim.SetBool("Empuñar", desenfundar); //tambien se puede usar para hacer una 
        }
        if (!estaEnPlataforma&&stateInfo.IsName("Jump_Espada"))
        {
            Debug.Log("ataqueSalto");
            if (presionarF)
            {
                anim.SetLayerWeight(2, 1);
                anim.SetTrigger("Salto_Ataque");
            }
        }
    }
    // control de movimiento, aqui contengo todos los codigos para mover al ninja con las teclas que yo quiera a la posicion que yo decida.
    void ControlMovimiento()
    {

        bool presionarA = Input.GetKey(KeyCode.A);
        bool presionarD = Input.GetKey(KeyCode.D);

        if (presionarA)
        {
            anim.SetBool("EstaCaminando", presionarA); 
            spriteNinja.flipX = true;
            transform.Translate(Vector3.left * Velocidad);

        }
        else if (presionarD)
        {
            anim.SetBool("EstaCaminando", presionarD);
            spriteNinja.flipX = false;
            transform.Translate(Vector3.right * Velocidad);
        }
        else
        {
            anim.SetBool("EstaCaminando", false);

        }

    }

    // control salto al igual que los demas controles sirve para decidir con que tecla voy a hacer saltar al ninja, ayudandome con una variable publica fuerza de salto 
    void ControlSalto()
    {

        bool presionarEspacio = Input.GetKeyDown(KeyCode.Space);

        if (estaEnPlataforma == true)
        {

            if (presionarEspacio)
            {
                anim.SetBool("Salto", true);
                rb2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
                estaEnPlataforma = false;

            }
        }

    }
    // control crouch sirve para pasar de mi estado idle espada, a crouch espada y poderme desplazar 
    void ControlCrouch()
    {
        bool PresionarZ = Input.GetKey(KeyCode.Z);
        if (true)
        {
            if (PresionarZ)
            {
                anim.SetBool("Crouch_Espada", true);

                estaEnPlataforma = true;

            }
            else
            {
                anim.SetBool("Crouch_Espada", false);
            }
        }
    }
    // todo este void sirve para reconocer cuando el collider de mi ninja colisiona con el collider del suelo etiquetado como plataforma para hacer que funcionen mi salto y mi crouch 
    private void OnCollisionEnter2D(Collision2D collision)
    {
		if (collision.gameObject.tag=="Plataforma")
		{
            anim.SetBool("Salto", false);
            estaEnPlataforma = true;
            if (estaEnPlataforma)
            {
                anim.SetLayerWeight(2, 0);
            }

		}
        
    }
}

using Unified.UniversalBlur.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class CupulaManager : MonoBehaviour
{
    [SerializeField] public CanvasToggle canvasComponents;
    [SerializeField] public CanvasToggle butaoEsconder;
    [SerializeField] private float posicaoAberta;
    [SerializeField] private float tolerancia;
    [SerializeField] GameObject sol;
    [SerializeField] GameObject raiosManager;
    [SerializeField] CanvasToggle butoesCupula;
    [SerializeField] CanvasToggle notificacao;
    [SerializeField] CanvasToggle butaoEdificio;
    public AudioData audioData;
    //bool playSound;
    bool isCupulaOpen = false;
    [SerializeField] Alertas alerta;

    public static bool IsAnyDragging { get; private set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sol.SetActive(false);
        raiosManager.SetActive(false);
    }

    void FixedUpdate()
    {
        float currentX = transform.position.x;
        float diff = Mathf.Abs(currentX - posicaoAberta);

        if (diff < tolerancia && !isCupulaOpen)
        {
            // Cupula just opened
            isCupulaOpen = true;


            //Debug.Log("Cupula Abriu");
            canvasComponents.Show();
            butaoEsconder.Show();
            sol.SetActive(true);
            raiosManager.SetActive(true);
            SoundFXManager.instance.PlaySoundFXClip(audioData.CorrectSound, transform, 1f);
            //playSound = false; // prevent re-triggering while still open
            if (butoesCupula != null)
            {
                butoesCupula.Hide();
            }

            if (notificacao != null)
            {
                notificacao.Show();
            }
            
            if (butaoEdificio != null)
            {
                butaoEdificio.Show();
            }



        }
        else if (diff >= tolerancia && isCupulaOpen)
        {
            // Cupula just closed
            isCupulaOpen = false;
            sol.SetActive(false);
            raiosManager.SetActive(false);
        }
        else if (!isCupulaOpen && MoveCelostato.IsAnyDragging)
        {
            string text = "Deve Abrir a cúpula antes de interagir com o instrumento";
            alerta.ClearText();
            alerta.SetText(text);
        }
    }


}

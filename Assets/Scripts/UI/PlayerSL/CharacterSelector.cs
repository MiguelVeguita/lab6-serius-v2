using UnityEngine;
using UnityEngine.UI;
using TMPro; 

[System.Serializable] 
public struct CharacterStats
{
    public string characterName; // Nombre opcional
    public Sprite characterSprite;
    public int life;
    public string race;
    public float speed;
    
}

public class CharacterSelector : MonoBehaviour
{
    // Referencias a los componentes de la UI para mostrar los datos
    [SerializeField] private Image characterDisplay;
    [SerializeField] private TextMeshProUGUI nameText; // Campo para el nombre
    [SerializeField] private TextMeshProUGUI lifeText; // Campo para la vida
    [SerializeField] private TextMeshProUGUI raceText; // Campo para la raza
    [SerializeField] private TextMeshProUGUI speedText; // Campo para la velocidad
    // A�ade m�s referencias TextMeshProUGUI si tienes m�s stats

    // 2. Usa un array de la estructura que definimos antes
    [SerializeField] private CharacterStats[] characters; // Array que contiene todos los datos de los personajes

    // Referencias a los botones
    [SerializeField] private Button nextButton;
    [SerializeField] private Button prevButton;

    private int currentIndex = 0;

    private void Awake()
    {
        // Validar si hay personajes asignados
        if (characters == null || characters.Length == 0)
        {
            Debug.LogError("CharacterSelector: No hay datos de personajes asignados en el Inspector.");
            // Opcionalmente, desactivar los botones si no hay personajes
            if (nextButton != null) nextButton.interactable = false;
            if (prevButton != null) prevButton.interactable = false;
            return; // Salir de Awake si no hay datos
        }

        // Asignar listeners a los botones
        if (nextButton != null) nextButton.onClick.AddListener(NextCharacter);
        if (prevButton != null) prevButton.onClick.AddListener(PreviousCharacter);

        // Mostrar el primer personaje al iniciar
        UpdateCharacterDisplay();
    }

    private void NextCharacter()
    {
        // Asegurarse de que el array no est� vac�o para evitar divisi�n por cero
        if (characters.Length == 0) return;

        currentIndex = (currentIndex + 1) % characters.Length;
        UpdateCharacterDisplay();
    }

    private void PreviousCharacter()
    {
        // Asegurarse de que el array no est� vac�o
        if (characters.Length == 0) return;

        // El c�lculo con "+ characters.Length" asegura que el resultado siempre sea positivo antes del m�dulo
        currentIndex = (currentIndex - 1 + characters.Length) % characters.Length;
        UpdateCharacterDisplay();
    }

    // 3. Modifica esta funci�n para actualizar TODO (imagen y texto)
    private void UpdateCharacterDisplay()
    {
        // Validar si el array est� vac�o o el �ndice es inv�lido (aunque el m�dulo deber�a prevenirlo)
        if (characters == null || characters.Length == 0 || currentIndex < 0 || currentIndex >= characters.Length)
        {
            Debug.LogWarning("CharacterSelector: No se puede actualizar la UI. Array vac�o o �ndice inv�lido.");
            // Podr�as limpiar la UI aqu� si lo deseas
            if (characterDisplay != null) characterDisplay.sprite = null;
            if (nameText != null) nameText.text = "Nombre: N/A";
            if (lifeText != null) lifeText.text = "Vida: N/A";
            if (raceText != null) raceText.text = "Raza: N/A";
            if (speedText != null) speedText.text = "Velocidad: N/A";
            return;
        }

        // Obtener los datos del personaje actual
        CharacterStats currentCharacter = characters[currentIndex];

        // Actualizar la imagen
        if (characterDisplay != null)
        {
            characterDisplay.sprite = currentCharacter.characterSprite;
        }
        else { Debug.LogWarning("CharacterSelector: Referencia a characterDisplay no asignada."); }


        // Actualizar los textos (con comprobaciones por si no asignaste alguno en el Inspector)
        if (nameText != null)
        {
            nameText.text = "Nombre: " + currentCharacter.characterName; // O simplemente currentCharacter.characterName si no quieres la etiqueta "Nombre: "
        }
        else { Debug.LogWarning("CharacterSelector: Referencia a nameText no asignada."); }

        if (lifeText != null)
        {
            lifeText.text = "Vida: " + currentCharacter.life.ToString();
        }
        else { Debug.LogWarning("CharacterSelector: Referencia a lifeText no asignada."); }

        if (raceText != null)
        {
            raceText.text = "Raza: " + currentCharacter.race;
        }
        else { Debug.LogWarning("CharacterSelector: Referencia a raceText no asignada."); }

        if (speedText != null)
        {
            // Usamos "F1" para mostrar el float con 1 decimal, ajusta seg�n necesites
            speedText.text = "Velocidad: " + currentCharacter.speed.ToString("F1");
        }
        else { Debug.LogWarning("CharacterSelector: Referencia a speedText no asignada."); }

        // Actualiza aqu� cualquier otro campo de texto que hayas a�adido
    }

    // Esta funci�n sigue siendo �til para saber qu� personaje est� seleccionado
    public int GetSelectedCharacterIndex() => currentIndex;

    // Opcional: Una funci�n para obtener todos los datos del personaje seleccionado
    public CharacterStats GetSelectedCharacterData()
    {
        if (characters != null && characters.Length > 0 && currentIndex >= 0 && currentIndex < characters.Length)
        {
            return characters[currentIndex];
        }
        // Devuelve un valor por defecto o lanza una excepci�n si prefieres manejar el error de otra forma
        Debug.LogError("CharacterSelector: No se pudieron obtener los datos del personaje seleccionado.");
        return default; // Devuelve una estructura CharacterStats vac�a/default
    }
}
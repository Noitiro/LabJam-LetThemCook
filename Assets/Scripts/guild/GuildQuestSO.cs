using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Guild/Quest Card")]
public class GuildQuestSO : ScriptableObject
{
    public string clientName; 
    [TextArea] public string requestDescription; 
    public RecipeSO requiredPotion;

    public int goldReward = 50;
    public float timeLimit = 30f;
}
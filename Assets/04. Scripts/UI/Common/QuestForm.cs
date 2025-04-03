using UnityEngine.Events;

public class QuestForm
{
    public UnityAction<PlayerUIManager> Contents { get; }

    public string Name { get; }
    public string Description { get; }
    public string ClueName { get; }
    public string ClueDescription { get; }
    public int Counter { get; private set; }
    public bool DidAccept { get; private set; }
    public bool DidClear { get; private set; }
    public bool DidGetAward { get; private set; }

    public QuestForm(UnityAction<PlayerUIManager> content, string name,
        string description, string clueName, string clueDescription,
        int counter = 0, bool didAccept = false, bool didClear = false, bool didGetAward = false)
    {
        Contents = content;
        Name = name;
        Description = description;
        ClueName = clueName;
        ClueDescription = clueDescription;
        Counter = counter;
        DidAccept = didAccept;
        DidClear = didClear;
        DidGetAward = didGetAward;
    }

    public void AddCounter()
    {
        Counter++;
    }

    public void AcceptQuest()
    {
        DidAccept = true;
    }

    public void ClearQuest()
    {
        DidClear = true;
    }

    public void GetAward()
    {
        DidGetAward = true;
    }
}
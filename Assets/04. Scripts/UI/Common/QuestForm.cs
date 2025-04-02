using UnityEngine.Events;

public class QuestForm
{
    public UnityAction<PlayerUIManager> Contents { get; }

    public string Name { get; }
    public string Description { get; }
    public int Counter { get; private set; }
    public bool DidAccept { get; private set; }
    public bool DidClear { get; private set; }

    public QuestForm(UnityAction<PlayerUIManager> content, string name,
        string description, int counter = 0, bool didAccept = false, bool didClear = false)
    {
        Contents = content;
        Name = name;
        Description = description;
        Counter = counter;
        DidAccept = didAccept;
        DidClear = didClear;
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
}
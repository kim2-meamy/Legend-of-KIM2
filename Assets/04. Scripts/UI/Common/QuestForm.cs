using UnityEngine.Events;

public class QuestForm
{
    public UnityAction<PlayerUIManager> Contents { get; }

    public string Name { get; }
    public string Description { get; }
    public int Counter { get; private set; }
    public bool DidClear { get; private set; }

    public QuestForm(UnityAction<PlayerUIManager> content, string name,
        string description, int counter = 0, bool didClear = false)
    {
        this.Contents = content;
        this.Name = name;
        this.Description = description;
        this.Counter = counter;
        this.DidClear = didClear;
    }

    public void AddCounter()
    {
        Counter++;
    }

    public void ClearQuest()
    {
        DidClear = true;
    }
}

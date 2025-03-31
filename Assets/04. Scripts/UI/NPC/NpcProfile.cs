using System.Collections.Generic;

public static class NpcProfile
{
    public const int MaxQuestCount = 100;
    
    public struct NpcProfileForm
    {
        public int Id { get; }
        public int QuestCount { get; private set; }

        public NpcProfileForm(int id, int questCount)
        {
            Id = id;
            QuestCount = questCount;
        }
    }
    
    public static Dictionary<string, NpcProfileForm> NpcIdList { get; } = new Dictionary<string, NpcProfileForm>()
    {
        { "Jaewoo", new NpcProfileForm(0, 1) },
    };
}

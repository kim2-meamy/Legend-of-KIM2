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
    
    public static Dictionary<string, NpcProfileForm> NpcProfileList { get; } = new Dictionary<string, NpcProfileForm>()
    {
        { "재우", new NpcProfileForm(0, 1) },
        { "우는 아이", new NpcProfileForm(1, 2) },
        { "두루마리 서신", new NpcProfileForm(2, 1) },
        {"우는 아이의 아버지", new NpcProfileForm(3, 2)},
        { "쿠쿠 대왕", new NpcProfileForm(4, 1) }
    };
}

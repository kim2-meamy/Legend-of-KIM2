using System.Collections.Generic;

public static class QuestId
{
    public static Dictionary<string, int> QuestIdList { get; } = new Dictionary<string, int>()
    {
        {"FirstStep", 1},
        {"SecondStep", 101},
        {"ThirdStep", 102},
        {"GetPaper", 201},
        {"FourthStep", 301},
        {"FinalStep", 302},
        {"BossFight", 401}
    };
}

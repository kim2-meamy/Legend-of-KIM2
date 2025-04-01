using System.Collections.Generic;

public class NpcConversationData
{
    public List<TreeNode> NpcConversationDataList { get; } = new List<TreeNode>();

    public NpcConversationData()
    {
        // Npc Id 0번
        {
            TreeNode root = new TreeNode()
        {
            Contents = "가...감사합니다……",
        };
        
        NpcConversationDataList.Add(root);
        
        TreeNode node1 = new TreeNode()
        {
            Contents = "저.. 정말.. 감사해요…. 슬라임들이 이렇지 않았는데…\n" +
                       "원래 나쁜 애들은 아니에요..",
            PlayerAnswer = "고맙긴요, 당연히 해야 할 일을 했을 뿐인걸요. 괜찮으세요?",
            IsQuestEntry = true,
            QuestId = 1,
            Parent = root
        };
        root.Children.Add(node1);
        
        TreeNode node2 = new TreeNode()
        {
            Contents = "원래 저희 쿠쿠 섬의 동물들은 온순하고 사람 말도 잘 들었는데, 갑자기 쿠쿠 대왕이 온 후부터 슬라임들이 날뛰기 시작했어요.",
            PlayerAnswer = "그게 무슨 말씀이세요?",
            Parent = node1
        };
        node1.Children.Add(node2);
        
        TreeNode node3 = new TreeNode()
        {
            Contents = "쿠쿠 섬의 마을 주민들과 동물들은 평화롭게 지내고 있었어요.\n" +
                       "그런데 어느 날 쿠쿠 대왕이 오고 쿠쿠 섬을 지배한 후부터 그의 영향으로 온 마을의 동물들이 포악해지기 시작했어요.",
            PlayerAnswer = "네?? 쿠쿠 대왕이요???",
            Parent = node2
        };
        node2.Children.Add(node3);
        
        TreeNode node4 = new TreeNode()
        {
            Contents = "네…..",
            PlayerAnswer = "그런 일이 있었군요.",
            Parent = node3
        };
        node3.Children.Add(node4);
        TreeNode node5 = new TreeNode()
        {
            Contents = "네…..",
            PlayerAnswer = "뭐 그런 말 같지도 않은 소리 하지 마세요.",
            Parent = node3
        };
        node3.Children.Add(node5);
        TreeNode node6 = new TreeNode()
        {
            Contents = "네…..",
            PlayerAnswer = "어… 안 물어봤는데요?",
            Parent = node3
        };
        node3.Children.Add(node6);
        
        TreeNode node7 = new TreeNode()
        {
            Contents = "맞을 거예요.",
            PlayerAnswer = "앗 저도 이 마을에 들어서자마자 몬스터와 마주치고 동생을 잃어버렸는데, 혹시 그 몬스터가 쿠쿠 대왕…?",
            Parent = node4
        };
        node4.Children.Add(node7);
        
        TreeNode node8 = new TreeNode()
        {
            Contents = "저희 마을의 가장 높은 곳에 그의 성이 있어요. 거기로 가보세요.",
            PlayerAnswer = "그럼 쿠쿠 대왕을 찾으려면 어디로 가야 하나요?",
            IsQuestRegister = true,
            QuestId = 1,
            Parent = node7
        };
        node7.Children.Add(node8);
        
        TreeNode node9 = new TreeNode()
        {
            PlayerAnswer = "수락한다.",
            Parent = node8
        };
        node8.Children.Add(node9);
        }
    }
}

using System.Collections.Generic;

public class NpcConversationData
{
    public const string PrevOptionImageText = "`를 눌러 뒤로가기";
    public List<TreeNode> NpcConversationDataList { get; } = new List<TreeNode>();

    public NpcConversationData()
    {
        // Npc Id 0번 - 첫 번째 퀘스트
        {
            TreeNode root = new TreeNode()
            {
                Contents = "가...감사합니다……"
            };
            
            NpcConversationDataList.Add(root);
            
            TreeNode node1 = new TreeNode()
            {
                Contents = "저.. 정말.. 감사해요…. 슬라임들이 이렇지 않았는데…\n" +
                           "원래 나쁜 애들은 아니에요..",
                PlayerAnswer =
                {
                    "고맙긴요, 당연히 해야 할 일을 했을 뿐인걸요.",
                    "괜찮으세요?"
                },
                IsQuestEntry = true,
                QuestId = 1,
                Parent = root
            };
            root.Children.Add(node1);
            
            TreeNode node2 = new TreeNode()
            {
                Contents = "원래 저희 쿠쿠 섬의 동물들은 온순하고 사람 말도 잘 들었는데, 갑자기 쿠쿠 대왕이 온 후부터 슬라임들이 날뛰기 시작했어요.",
                PlayerAnswer = { "그게 무슨 말씀이세요?" },
                Parent = node1
            };
            node1.Children.Add(node2);
            
            TreeNode node3 = new TreeNode()
            {
                Contents = "쿠쿠 섬의 마을 주민들과 동물들은 평화롭게 지내고 있었어요.\n" +
                           "그런데 어느 날 쿠쿠 대왕이 오고 쿠쿠 섬을 지배한 후부터 그의 영향으로 온 마을의 동물들이 포악해지기 시작했어요.",
                PlayerAnswer = { "네?? 쿠쿠 대왕이요???" },
                Parent = node2
            };
            node2.Children.Add(node3);
            
            TreeNode node4 = new TreeNode()
            {
                Contents = "네…..",
                PlayerAnswer = { "그런 일이 있었군요.", },
                Parent = node3
            };
            node3.Children.Add(node4);
            TreeNode node5 = new TreeNode()
            {
                Contents = "네…..",
                PlayerAnswer = { "뭐 그런 말 같지도 않은 소리 하지 마세요." },
                Parent = node3
            };
            node3.Children.Add(node5);
            TreeNode node6 = new TreeNode()
            {
                Contents = "네…..",
                PlayerAnswer = { "어… 안 물어봤는데요?" },
                Parent = node3
            };
            node3.Children.Add(node6);
            
            TreeNode node7 = new TreeNode()
            {
                Contents = "맞을 거예요.",
                PlayerAnswer =
                {
                    "앗 저도 이 마을에 들어서자마자 몬스터와 마주치고 동생을 잃어버렸는데,",
                    "혹시 그 몬스터가 쿠쿠 대왕…?"
                },
                Parent = node4
            };
            node4.Children.Add(node7);
            
            TreeNode node8 = new TreeNode()
            {
                Contents = "저희 마을의 가장 높은 곳에 그의 성이 있어요. 거기로 가보세요.",
                PlayerAnswer = { "그럼 쿠쿠 대왕을 찾으려면 어디로 가야 하나요?" },
                IsQuestRegister = true,
                QuestId = 1,
                Parent = node7
            };
            node7.Children.Add(node8);
            
            TreeNode node9 = new TreeNode()
            {
                PlayerAnswer = { "퀘스트를 수락한다." },
                Parent = node8
            };
            node8.Children.Add(node9);
        }
        
        // Npc Id 1번 - 첫 번째 퀘스트
        {
            TreeNode root = new TreeNode()
            {
                Contents = "엉엉…."
            };
            
            NpcConversationDataList.Add(root);
            
            TreeNode node1 = new TreeNode()
            {
                Contents = "엉어엉……ㅠㅠ",
                PlayerAnswer = { "아이야, 무슨 일이야, 왜 여기에서 울고 있어?" },
                IsQuestEntry = true,
                QuestId = 1,
                Parent = root
            };
            root.Children.Add(node1);
            
            TreeNode node2 = new TreeNode()
            {
                Contents = "아빠가 심부름을 맡기셨는데 오다 보니 없어졌어요ㅠㅠ",
                PlayerAnswer = { "왜 혼자 있니, 길을 잃었어?" },
                Parent = node1
            };
            node1.Children.Add(node2);
            
            TreeNode node3 = new TreeNode()
            {
                Contents = "집에서 이쪽으로 오고 있었는데…. \n" +
                           "아빠가 마을 촌장님께 서신을 전해달라고 하셨었어요. 두루마리 서신이에요.",
                PlayerAnswer =
                {
                    "내가 같이 찾아줄게.",
                    "어디서 찾을 수 있어? 그게 뭐야?"
                },
                IsQuestRegister = true,
                QuestId = 1,
                Parent = node2
            };
            node2.Children.Add(node3);
            
            TreeNode node4 = new TreeNode()
            {
                PlayerAnswer =
                {
                    "너희 집이 저쪽에 보이는 언덕 위의 집 맞아?",
                    "조금만 기다려…!"
                },
                Parent = node3
            };
            node3.Children.Add(node4);
        }
        
        // Npc Id 2번 - 첫 번째 퀘스트
        {
            TreeNode root = new TreeNode()
            {
                Contents = "( 우는 아이가 말했던 두루마리 서신이다. )",
                IsQuestEntry = true,
                IsQuestRegister = true,
                QuestId = 1,
            };
            
            NpcConversationDataList.Add(root);
            
            TreeNode node1 = new TreeNode()
            {
                PlayerAnswer = { "가져간다." },
                Parent = root
            };
            root.Children.Add(node1);
        }
        
        // Npc Id 3번 - 첫 번째 퀘스트
        {
            TreeNode root = new TreeNode()
            {
                Contents = "…."
            };

            NpcConversationDataList.Add(root);
            
            TreeNode node1 = new TreeNode()
            {
                Contents = "애야 내가 얼마나 걱정했는지 아니.\n" +
                           "너를 다시 만날 수 있어 정말 감사하구나.\n" +
                           "이제 가자.",
                PlayerAnswer =
                {
                    "( 우는 아이가 말하는 소리가 들린다. )",
                    "우는 아이: 아빠 제가 서신서를 전달하지 못했어요.",
                    "죄송해요."
                },
                IsQuestEntry = true,
                IsQuestRegister = true,
                QuestId = 1,
                Parent = root
            };
            root.Children.Add(node1);
            
            TreeNode node2 = new TreeNode()
            {
                PlayerAnswer = { "지켜본다." },
                Parent = node1
            };
            node1.Children.Add(node2);
        }
        
        // Npc Id 4번 - 첫 번째 퀘스트
        {
            TreeNode root = new TreeNode()
            {
                Contents = "기어이 여기까지 왔구나, 김투. 네 여동생을 되찾기 위해."
            };
            
            NpcConversationDataList.Add(root);
            
            TreeNode node1 = new TreeNode()
            {
                Contents = "변명…? 넌 아무것도 모른다.\n" +
                           "나는 이 섬을 지키고 있을 뿐이야. 희생 없이 평화는 존재하지 않아.",
                PlayerAnswer =
                {
                    "당연하지! 동생을 납치해놓고,",
                    "네가 무슨 변명을 하든 상관없어!"
                },
                IsQuestEntry = true,
                QuestId = 1,
                Parent = root
            };
            root.Children.Add(node1);
            
            TreeNode node2 = new TreeNode()
            {
                Contents = "그건 내가 감당해야 할 대가였다. 봉인이 약해지고 있어.\n" +
                           "너는 그 진실을 받아들일 준비가 되었느냐?",
                PlayerAnswer =
                { 
                    "거짓말! 네가 한 짓을 봐! 섬은 혼란에 빠졌고,",
                    "몬스터들은 사람들을 공격하고 있어!"
                },
                Parent = node1
            };
            node1.Children.Add(node2);
            
            TreeNode node3 = new TreeNode()
            {
                Contents = "더 이상 말로 설명할 필요는 없다.\n" +
                           "이 싸움에서 이긴다면, 네가 원하는 진실을 알려주지.자, 덤벼라!",
                PlayerAnswer = { "…봉인? 그게 무슨 말이야?" },
                IsQuestRegister = true,
                QuestId = 1,
                Parent = node2
            };
            node2.Children.Add(node3);
            
            TreeNode node4 = new TreeNode()
            {
                PlayerAnswer = { "전투를 시작한다." },
                Parent = node3
            };
            node3.Children.Add(node4);
        }
    }

    public void NpcId1ClearFirstQuest()
    {
        // Npc Id 1번 - 두 번째 퀘스트
        TreeNode secondNpcSecondQuest;
        {
            TreeNode root = new TreeNode()
            {
                Contents = "와 제가 찾고있던 서신서가 맞아요! 정말 감사합니다!"
            };
            
            secondNpcSecondQuest = root;
            
            TreeNode node1 = new TreeNode()
            {
                Contents = "저는 잘 몰라요. 아버지가 마을 촌장님께 전해달라고 하셨어요.",
                PlayerAnswer =
                {
                    "근데, 이 서신서의 내용을 나도 모르게 봐버렸는데..",
                    "아이야 이 서신서의 내용을 알고 있니?",
                    "원래 누구에게 전해주려는 것이었어?"
                },
                IsQuestEntry = true,
                QuestId = 2,
                Parent = root
            };
            root.Children.Add(node1);
            
            TreeNode node2 = new TreeNode()
            {
                Contents = "감사합니다. 이제 아버지께 갈 수 있겠어요",
                PlayerAnswer = { "그렇구나." },
                IsQuestRegister = true,
                QuestId = 2,
                Parent = node1
            };
            node1.Children.Add(node2);
            
            TreeNode node3 = new TreeNode()
            {
                PlayerAnswer = { "아이를 지켜본다." },
                Parent = node2
            };
            node2.Children.Add(node3);
        }
        
        NpcConversationDataList[NpcProfile.NpcProfileList["우는 아이"].Id] = secondNpcSecondQuest;
    }
    
    public void NpcId3ClearFirstQuest()
    {
        // Npc Id 1번 - 두 번째 퀘스트
        TreeNode thirdNpcSecondQuest;
        {
            TreeNode root = new TreeNode()
            {
                Contents = "( 성불하려는 듯 하다. )",
                IsQuestEntry = true,
                IsQuestRegister = true,
                QuestId = 2,
            };
            
            thirdNpcSecondQuest = root;
            
            TreeNode node1 = new TreeNode()
            {
                PlayerAnswer =
                {
                    "지켜본다.",
                },
                Parent = root
            };
            root.Children.Add(node1);
        }
        
        NpcConversationDataList[NpcProfile.NpcProfileList["우는 아이의 아버지"].Id] = thirdNpcSecondQuest;
    }
}

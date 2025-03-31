using System.Collections.Generic;

public class NpcConversationData
{
    public List<TreeNode> NpcConversationDataList { get; } = new List<TreeNode>();

    public NpcConversationData()
    {
        TreeNode root = new TreeNode()
        {
            Contents = "Hello!",
            PlayerAnswer = "First select.",
        };
        
        // Npc 0번 root 대화 노드
        NpcConversationDataList.Add(root);
        
        {
            {
                TreeNode node = new TreeNode()
                {
                    Contents = "Nice to meet you!",
                    PlayerAnswer = "Second select.",
                    Parent = root
                };
                node.Children.Add(new TreeNode()
                {
                    Contents = "^^",
                    PlayerAnswer = "Second select.",
                    Parent = node
                });
                
                root.Children.Add(node);
            }
            {
                TreeNode node = new TreeNode()
                {
                    Contents = "How are you?",
                    PlayerAnswer = "Third select.",
                    Parent = root
                };
                node.Children.Add(new TreeNode()
                {
                    Contents = "Good!",
                    PlayerAnswer = "Third select 1.",
                    Parent = node
                });
                
                root.Children.Add(node);
            }
            {
                TreeNode node = new TreeNode()
                {
                    Contents = "Talk to me for 5 time!",
                    PlayerAnswer = "Fourth select.",
                    IsQuestEntry = true,
                    QuestId = 1,
                    Parent = root
                };
                node.Children.Add(new TreeNode()
                {
                    Contents = "Ok?",
                    PlayerAnswer = "Fourth select 1.",
                    IsQuestRegister = true,
                    QuestId = 1,
                    Parent = node
                });
                
                root.Children.Add(node);
            }
        }
    }
}

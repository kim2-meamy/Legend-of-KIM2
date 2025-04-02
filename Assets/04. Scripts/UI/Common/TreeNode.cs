using System.Collections.Generic;

public class TreeNode
{
    public string Contents { get; set; } = string.Empty;
    public string PlayerAnswer { get; set; } = string.Empty;
    
    public bool IsQuestEntry { get; set; } = false;
    public bool IsQuestRegister { get; set; } = false;
    public int QuestId { get; set; } = 0;
    public bool DidGetAward { get; private set; } = false;
    
    public List<TreeNode> Children { get; set; } = new List<TreeNode>();
    public TreeNode Parent { get; set; } = null;
    
    public void GetAward()
    {
        DidGetAward = true;
    }
}

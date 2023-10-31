using System.Collections.Generic;

public class SelectorNode : BTNode
{
    private List<BTNode> nodes = new List<BTNode>();

    public SelectorNode(params BTNode[] nodes)
    {
        this.nodes.AddRange(nodes);
    }

    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.SUCCESS:
                    nodeState = NodeState.SUCCESS;
                    return nodeState;

                case NodeState.RUNNING:
                    nodeState = NodeState.RUNNING;
                    return nodeState;

                case NodeState.FAILURE:
                    continue;

                default:
                    continue;
            }
        }

        nodeState = NodeState.FAILURE;
        return nodeState;
    }
}
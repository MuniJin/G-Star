public enum NodeState
{
    RUNNING,
    SUCCESS,
    FAILURE
}

public abstract class BTNode
{
    protected NodeState nodeState;

    public NodeState GetNodeState()
    {
        return nodeState;
    }

    public abstract NodeState Evaluate();
}
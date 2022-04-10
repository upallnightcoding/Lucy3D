namespace Lucy3DLib
{
    class NodeCodeBlock : Node
    {
        public override NodeValue Evaluate(Context context)
        {
            foreach (Node node in GetNodeList()) {
                node.Evaluate(context);
            }

            return (null);
        }
    }
}

using ConstraintThingy;

public class QuantityConstraint : GUIConstraint
{
    public ContentType Content;

    public int Min = 0;
    public int Max = 10;

    public override void Apply(FiniteDomainVariable<ContentType>[] rooms)
    {
        Constraint.LimitOccurences(Content, Min, Max, rooms);
    }
}
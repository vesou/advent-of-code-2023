namespace AdventOfCode2023.Day19;

public class Problem19
{
    public const string Day  = "19";

    #region Task 2

    public static long Solve2()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        Data data = TranslateInput(inputLines);
        data.Parts = new List<PartRating>();

        return SumOfAllPossibleParts(data);
    }

    public static long SumOfAllPossibleParts(Data data)
    {
        // iterate through rules picking each rule true and false paths and limiting the possible values for each prop
        // then if reached A workflow, multiple the possible values for each prop
        List<RuleSet> allRulesLeadingToA = GetAllRulesLeadingToA(data);
        long result = 0;
        foreach (var ruleSet in allRulesLeadingToA)
        {
            result += ruleSet.GetCountOfAllPossibleValues();
        }

        return result;
    }

    private static List<RuleSet> GetAllRulesLeadingToA(Data data)
    {
        List<RuleSet> allRulesLeadingToA = new List<RuleSet>();
        WorkflowRule currentRule = new WorkflowRule(data.Workflows.First(x => x.Name == "in"), 0, new RuleSet());
        Queue<WorkflowRule> rulesToProcess = new Queue<WorkflowRule>();
        rulesToProcess.Enqueue(currentRule);
        while(rulesToProcess.Count > 0)
        {
            currentRule = rulesToProcess.Dequeue();
            switch (currentRule.Workflow.Name)
            {
                case "A":
                    allRulesLeadingToA.Add(currentRule.CurrentRuleSet);
                    continue;
                case "R":
                    continue;
            }

            var rule = currentRule.Workflow.Rules[currentRule.CurrentRuleIndex];
            // rule passes meaning workflow changes
            WorkflowRule newPassingRule = new WorkflowRule(data.Workflows.First(x => x.Name == rule.OutputWorkflowName), 0, currentRule.CurrentRuleSet);
            newPassingRule.CurrentRuleSet.AddRule(rule);
            rulesToProcess.Enqueue(newPassingRule);

            // a rule without a condition so we will not create an anti rule for it as it would definitely be rejected
            if (rule.ConditionChar == "")
            {
                continue;
            }

            WorkflowRule newAntiRule =
                new WorkflowRule(currentRule.Workflow, currentRule.CurrentRuleIndex + 1, currentRule.CurrentRuleSet);
            newAntiRule.CurrentRuleSet.AddAntiRule(rule);
            rulesToProcess.Enqueue(newAntiRule);
        }

        return allRulesLeadingToA;
    }

    #endregion

    #region Task 1

    public static long Solve1()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        Data data = TranslateInput(inputLines);

        return SumOfAcceptedParts(data);
    }

    public static long SumOfAcceptedParts(Data data)
    {
        long sum = 0;
        foreach (var part in data.Parts)
        {
            Workflow finalWorkflow = data.Process(part);
            if(finalWorkflow.Name == "A")
                sum += part.X + part.M + part.A + part.S;
            else if (finalWorkflow.Name == "R")
            {
                // reject
            }
            else
            {
                // should never happen
            }
        }

        return sum;
    }

    #endregion

    public static Data TranslateInput(List<string> inputLines)
    {
        return new Data(inputLines);
    }
}

public class Data
{
    public Data(List<string> inputLines)
    {
        Workflows = new HashSet<Workflow>();
        Parts = new List<PartRating>();

        Workflows.Add(Workflow.Accept());
        Workflows.Add(Workflow.Reject());

        foreach (var inputLine in inputLines)
        {
            if(string.IsNullOrWhiteSpace(inputLine)) continue;

            if (!inputLine.StartsWith('{'))
            {
                Workflows.Add(new Workflow(inputLine));
            }
            else
            {
                Parts.Add(new PartRating(inputLine));
            }
        }
    }

    public HashSet<Workflow> Workflows { get; set; }
    public List<PartRating> Parts { get; set; }

    public Workflow Process(PartRating part)
    {
        Workflow currentWorkflow = Workflows.First(x => x.Name == "in");
        int ruleIndex = 0;
        while(ruleIndex < currentWorkflow.Rules.Count)
        {
            var rule = currentWorkflow.Rules[ruleIndex];
            if (rule.Condition(part))
            {
                var test = Workflows.FirstOrDefault(x => x.Name == rule.OutputWorkflowName);

                if (test is null)
                {
                    // test
                }
                currentWorkflow = test;
                ruleIndex = 0;
            }
            else
            {
                ruleIndex++;
            }
        }

        return currentWorkflow;
    }
}

public class Workflow
{
    /// <summary>
    /// translates string px{a<2006:qkq,m>2090:A,rfg} to a Workflow where Name = px, and Rules = {a<2006:qkq,m>2090:A,rfg}
    /// </summary>
    /// <param name="data"></param>
    public Workflow(string data)
    {
        var parts = data.Split('{');
        Name = parts[0];
        Rules = new List<Rule>();
        foreach (var part in parts[1].Split(','))
        {
            Rules.Add(new Rule(part.TrimEnd('}')));
        }
    }

    public Workflow()
    {
        Name = "A";
        Rules = new List<Rule>();
    }

    public static Workflow Accept ()
    {
        return new Workflow();
    }

    public static Workflow Reject ()
    {
        return new Workflow()
        {
            Name = "R"
        };
    }

    public string Name { get; set; }
    public List<Rule> Rules { get; set; }
}

public class Rule
{
    /// <summary>
    /// translates a string like a<2006:qkq to a Rule where Condition = a<2006 and OutputWorkflowName = qkq
    /// another example m>2090:A
    /// </summary>
    /// <param name="data"></param>
    public Rule(string data)
    {
        var parts = data.Split(':');
        if(parts.Length == 1)
        {
            OutputWorkflowName = parts[0];
            Condition = _ => true;
            ConditionChar = "";
            return;
        }
        OutputWorkflowName = parts[1];
        Condition = GetPredicate(parts[0]);
        ConditionChar = parts[0].Split('<', '>')[0];
        ConditionValue = int.Parse(parts[0].Split('<', '>')[1]);
        ConditionIsLessThen = parts[0].Contains('<');
    }

    public bool ConditionIsLessThen { get; set; }

    public int ConditionValue { get; set; }

    /// <summary>
    /// translates a string like a<2006 to a Predicate<PartRating> where a is smaller than 2006 returns true
    /// or m>2090 to a Predicate<PartRating> where m is bigger than 2090 returns true
    /// and same for x and s values
    /// </summary>
    /// <param name="part"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private Predicate<PartRating> GetPredicate(string part)
    {
        var parts = part.Split('<', '>');
        bool lessThan = part.Contains('<');

        switch (parts[0])
        {
            case "a":
                return x => lessThan ? x.A == 0 || x.A < int.Parse(parts[1]) : x.A == 0 || x.A > int.Parse(parts[1]);
            case "m":
                return x => lessThan ? x.M == 0 || x.M < int.Parse(parts[1]) : x.M == 0 || x.M > int.Parse(parts[1]);
            case "x":
                return x => lessThan ? x.X == 0 || x.X < int.Parse(parts[1]) : x.X == 0 || x.X > int.Parse(parts[1]);
            case "s":
                return x => lessThan ? x.S == 0 || x.S < int.Parse(parts[1]) : x.S == 0 || x.S > int.Parse(parts[1]);
            default:
                throw new NotImplementedException();
        }
    }

    public Predicate<PartRating> Condition { get; set; }
    public string ConditionChar { get; set; }

    public string OutputWorkflowName { get; set; }
}

public class PartRating
{
    /// <summary>
    /// translates a string {x=787,m=2655,a=1222,s=2876} to a PartRating
    /// </summary>
    /// <param name="data"></param>
    public PartRating(string data)
    {
        data = data.Substring(1, data.Length - 2); // remove { and }
        var parts = data.Split(',');
        foreach (var part in parts)
        {
            var keyValue = part.Split('=');
            switch (keyValue[0])
            {
                case "x":
                    X = int.Parse(keyValue[1]);
                    break;
                case "m":
                    M = int.Parse(keyValue[1]);
                    break;
                case "a":
                    A = int.Parse(keyValue[1]);
                    break;
                case "s":
                    S = int.Parse(keyValue[1]);
                    break;
            }
        }
    }

    public PartRating(string propName, int propValue)
    {
        switch (propName)
        {
            case "x":
                X = propValue;
                break;
            case "m":
                M = propValue;
                break;
            case "a":
                A = propValue;
                break;
            case "s":
                S = propValue;
                break;
        }
    }

    public int X { get; set; }
    public int M { get; set; }
    public int A { get; set; }
    public int S { get; set; }
}

public class RuleSet
{
    public RuleSet()
    {
        XConditions = new List<Predicate<int>>();
        MConditions = new List<Predicate<int>>();
        AConditions = new List<Predicate<int>>();
        SConditions = new List<Predicate<int>>();
    }

    public void AddRule(Rule rule)
    {
        switch (rule.ConditionChar)
        {
            case "x":
                XConditions.Add(new Predicate<int>(x => rule.ConditionIsLessThen ? x < rule.ConditionValue : x > rule.ConditionValue));
                break;
            case "m":
                MConditions.Add(new Predicate<int>(x => rule.ConditionIsLessThen ? x < rule.ConditionValue : x > rule.ConditionValue));
                break;
            case "a":
                AConditions.Add(new Predicate<int>(x => rule.ConditionIsLessThen ? x < rule.ConditionValue : x > rule.ConditionValue));
                break;
            case "s":
                SConditions.Add(new Predicate<int>(x => rule.ConditionIsLessThen ? x < rule.ConditionValue : x > rule.ConditionValue));
                break;
        }
    }

    public void AddAntiRule(Rule rule)
    {
        switch (rule.ConditionChar)
        {
            case "x":
                XConditions.Add(new Predicate<int>(x => rule.ConditionIsLessThen ? x >= rule.ConditionValue : x <= rule.ConditionValue));
                break;
            case "m":
                MConditions.Add(new Predicate<int>(x => rule.ConditionIsLessThen ? x >= rule.ConditionValue : x <= rule.ConditionValue));
                break;
            case "a":
                AConditions.Add(new Predicate<int>(x => rule.ConditionIsLessThen ? x >= rule.ConditionValue : x <= rule.ConditionValue));
                break;
            case "s":
                SConditions.Add(new Predicate<int>(x => rule.ConditionIsLessThen ? x >= rule.ConditionValue : x <= rule.ConditionValue));
                break;
        }
    }

    public int GetCountOfAllPossibleValues()
    {
        return GetCountOfPossibleValue(XConditions) * GetCountOfPossibleValue(MConditions) * GetCountOfPossibleValue(AConditions) * GetCountOfPossibleValue(SConditions);
    }

    /// <summary>
    /// applies all conditions to the list of possible values and returns the count of possible values based on a integer value being between 1 and 4000
    /// </summary>
    /// <param name="conditions"></param>
    /// <returns></returns>
    private int GetCountOfPossibleValue(List<Predicate<int>> conditions)
    {
        int count = 0;
        for (int i = 1; i <= 4000; i++)
        {
            if (conditions.TrueForAll(x => x(i)))
            {
                count++;
            }
        }

        return count;
    }

    public List<Predicate<int>> XConditions { get; set; }
    public List<Predicate<int>> MConditions { get; set; }
    public List<Predicate<int>> AConditions { get; set; }
    public List<Predicate<int>> SConditions { get; set; }
}

public class WorkflowRule
{
    public WorkflowRule(Workflow workflow, int ruleIndex, RuleSet ruleSet)
    {
        Workflow = workflow;
        CurrentRuleIndex = ruleIndex;
        CurrentRuleSet = new RuleSet()
        {
            XConditions = ruleSet.XConditions.ToList(),
            MConditions = ruleSet.MConditions.ToList(),
            AConditions = ruleSet.AConditions.ToList(),
            SConditions = ruleSet.SConditions.ToList()
        };
    }

    public Workflow Workflow { get; set; }
    public int CurrentRuleIndex { get; set; }
    public RuleSet CurrentRuleSet { get; set; }
}

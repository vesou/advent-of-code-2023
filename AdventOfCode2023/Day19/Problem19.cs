namespace AdventOfCode2023.Day19;

public class Problem19
{
    public const string Day  = "19";

    #region Task 2

    public static long Solve2()
    {
        var inputLines = File.ReadAllLines($"Day{Day}/input1.txt").ToList();

        return 0;
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
            return;
        }
        OutputWorkflowName = parts[1];
        Condition = GetPredicate(parts[0]);
    }

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
                return x => lessThan ? x.A < int.Parse(parts[1]) : x.A > int.Parse(parts[1]);
            case "m":
                return x => lessThan ? x.M < int.Parse(parts[1]) : x.M > int.Parse(parts[1]);
            case "x":
                return x => lessThan ? x.X < int.Parse(parts[1]) : x.X > int.Parse(parts[1]);
            case "s":
                return x => lessThan ? x.S < int.Parse(parts[1]) : x.S > int.Parse(parts[1]);
            default:
                throw new NotImplementedException();
        }
    }

    public Predicate<PartRating> Condition { get; set; }

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

    public int X { get; set; }
    public int M { get; set; }
    public int A { get; set; }
    public int S { get; set; }
}

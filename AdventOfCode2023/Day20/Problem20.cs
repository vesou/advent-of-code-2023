using System.Text;

namespace AdventOfCode2023.Day20;

public class Problem20
{
    public const string Day  = "20";
    public const string FlipFlopModulePrefix  = "%";
    public const string ConjunctionModulePrefix  = "&";
    public const string BroadcastModule  = "broadcaster";
    public const bool LowPulse = false;
    public const bool HighPulse = true;

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

        return GetPulsePower(data, 1000);
    }

    public static long GetPulsePower(Data data, int numberOfCycles)
    {
        List<PulseContext> pulseContexts = new List<PulseContext>();
        int cycleLength = 0;
        int indexOfACycleStart = 0;
        int indexWhenWasCycleFound = 0;
        for (int i = 0; i < numberOfCycles; i++)
        {
            string stateOfAllModules = GetCurrentStateOfAllModules(data);
            // not needed for part 1
            // int index = pulseContexts.FindIndex(x => x.StateOfAllModules == stateOfAllModules);
            // if(index != -1)
            // {
            //     // found a cycle so can calculate the pulses for every remaining cycle outside
            //     cycleLength = i - index;
            //     indexOfACycleStart = index;
            //     indexWhenWasCycleFound = i;
            //     break;
            // }

            PulseContext pulse = new PulseContext(i, stateOfAllModules);
            (int numberOfHighPulses, int numberOfLowPulses) = GetNumberOfHighAndLowPulses(data);
            pulse.NumberOfHighPulses = numberOfHighPulses;
            pulse.NumberOfLowPulses = numberOfLowPulses;
            pulseContexts.Add(pulse);
        }

        return GetTotalPulsePower(pulseContexts, indexOfACycleStart, indexWhenWasCycleFound, cycleLength, numberOfCycles);
    }

    private static long GetTotalPulsePower(List<PulseContext> pulseContexts, int indexOfACycleStart, int indexWhenWasCycleFound, int cycleLength, int numberOfCycles)
    {
        long numberOfHighPulses = 0;
        long numberOfLowPulses = 0;
        if (indexOfACycleStart == 0)
        {
            numberOfHighPulses = pulseContexts.Sum(x => x.NumberOfHighPulses);
            numberOfLowPulses = pulseContexts.Sum(x => x.NumberOfLowPulses);

            return numberOfLowPulses * numberOfHighPulses;
        }

        return numberOfLowPulses * numberOfHighPulses;
    }

    public static (int numberOfHighPulses, int numberOfLowPulses) GetNumberOfHighAndLowPulses(Data data)
    {
        int numberOfHighPulses = 0;
        int numberOfLowPulses = 0;

        Queue<(Module, bool)> pulseQueue = new Queue<(Module, bool)>();
        var broadcaster = data.Modules[BroadcastModule];
        pulseQueue.Enqueue((broadcaster, LowPulse));
        while (pulseQueue.Count > 0)
        {
            (Module module, bool pulse) = pulseQueue.Dequeue();
            if (pulse == LowPulse)
            {
                numberOfLowPulses++;
            }
            else if (pulse == HighPulse)
            {
                numberOfHighPulses++;
            }

            module.SetState(pulse);
            if(module.Type == ModuleType.FlipFlop && pulse == HighPulse)
            {
                // flip flop module only sends a pulse when it receives a low pulse
                continue;
            }

            foreach (var outputModule in module.OutputModules)
            {
                pulseQueue.Enqueue((data.Modules[outputModule], module.GetState()));
            }
        }

        return (numberOfHighPulses, numberOfLowPulses);
    }

    private static string GetCurrentStateOfAllModules(Data data)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var module in data.Modules.Values)
        {
            sb.Append(module.Label + (module.GetState() ? "1" : "0") + "|");
        }

        return sb.ToString();
    }

    #endregion

    public static Data TranslateInput(List<string> inputLines)
    {
        return new Data(inputLines);
    }
}

public class Data
{
    /// <summary>
    /// translates input lines into a data object
    /// example of data
    /// broadcaster -> a, b, c
    /// %a -> b
    /// %b -> c
    /// %c -> inv
    /// &inv -> a
    /// </summary>
    /// <param name="inputLines"></param>
    public Data(List<string> inputLines)
    {
        Modules = new Dictionary<string, Module>();
        foreach (var inputLine in inputLines)
        {
            var split = inputLine.Split("->");
            var labelWithType = split[0].Trim();
            (ModuleType moduleType, string label) = GetModule(labelWithType);
            var outputModules = split[1].Split(",").Select(x => x.Trim()).ToList();
            var module = new Module(label, moduleType, outputModules);
            Modules.Add(label, module);
        }

        List<Module> missingModules = new List<Module>();
        // now that all modules are created, we can set the input modules
        foreach (var module in Modules.Values)
        {
            foreach (var outputModuleLabel in module.OutputModules)
            {
                bool moduleFound = Modules.TryGetValue(outputModuleLabel, out Module outputModule);
                if(moduleFound)
                    outputModule.InputModules.Add(module);
                else
                {
                    missingModules.Add(new Module(outputModuleLabel, ModuleType.Empty, new List<string>()));
                }
            }
        }

        missingModules.ForEach(x => Modules.Add(x.Label, x));
    }

    public static (ModuleType moduleType, string label) GetModule(string labelWithType)
    {
        if (labelWithType.StartsWith(Problem20.FlipFlopModulePrefix))
        {
            return (ModuleType.FlipFlop, labelWithType.Substring(1));
        }

        if (labelWithType.StartsWith(Problem20.ConjunctionModulePrefix))
        {
            return (ModuleType.Conjunction, labelWithType.Substring(1));
        }

        if (labelWithType == Problem20.BroadcastModule)
        {
            return (ModuleType.Broadcast, labelWithType);
        }

        return (ModuleType.Empty, labelWithType);
    }

    public Dictionary<string, Module> Modules { get; set; }
}

public class Module
{
    private bool _state;
    public Module(string label, ModuleType moduleType, List<string> outputModules)
    {
        Label = label;
        Type = moduleType;
        OutputModules = outputModules;
        switch (moduleType)
        {
            case ModuleType.FlipFlop:
                _state = false;
                break;
            case ModuleType.Conjunction:
                break;
            case ModuleType.Broadcast:
                SetState(false);
                break;
            case ModuleType.Empty:
                SetState(false);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(moduleType), moduleType, null);
        }

        InputModules = new List<Module>();
    }

    public ModuleType Type { get; set; }
    public string Label { get; set; }

    public void SetState(bool value)
    {
        switch (Type)
        {
            case ModuleType.FlipFlop:
                if (!value)
                    _state = !_state;
                break;
            case ModuleType.Conjunction:
                break;
            case ModuleType.Broadcast:
                _state = value;
                break;
            case ModuleType.Empty:
                _state = value;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public bool GetState()
    {
        if (Type != ModuleType.Conjunction)
        {
            return _state;
        }

        return !InputModules.TrueForAll(x => x.GetState());
    }

    public List<Module> InputModules { get; set; }
    public List<string> OutputModules { get; set; }
}

public enum ModuleType
{
    FlipFlop,
    Conjunction,
    Broadcast,
    Empty
}

public class PulseContext
{
    public PulseContext(int i, string stateOfAllModules)
    {
        Index = i;
        StateOfAllModules = stateOfAllModules;
        NumberOfHighPulses = 0;
        NumberOfLowPulses = 0;
    }

    public int Index { get; set; }
    public int NumberOfHighPulses { get; set; }
    public int NumberOfLowPulses { get; set; }
    public string StateOfAllModules { get; set; }
}

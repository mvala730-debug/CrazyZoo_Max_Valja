namespace CrazyZoo.Core;
public sealed record AnimalSnapshot(string Name,string TypeName,int Age,int Energy,int Happiness);
public enum ComputationState{Success,Error,Cancelled}
public sealed record ComputationReport(string AnimalName,ComputationState State,string Message,long DurationMs);
public sealed record BatchReport(IReadOnlyList<ComputationReport> Items,long TotalDurationMs);
public enum WorkloadKind{RouteComparison,GrowthForecast,RepeatedSimulation}
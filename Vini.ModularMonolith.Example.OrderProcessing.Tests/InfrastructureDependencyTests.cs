using ArchUnitNET.Domain;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.Loader;
using ArchUnitNET.xUnit;
using Xunit.Abstractions;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace Vini.ModularMonolith.Example.OrderProcessing.Tests;

public class InfrastructureDependencyTests
{
  private readonly ITestOutputHelper _outputHelper;

  public InfrastructureDependencyTests(ITestOutputHelper outputHelper)
  {
    _outputHelper = outputHelper;
  }

  private static readonly Architecture Architecture = new ArchLoader()
    .LoadAssemblies(typeof(OrderProcessing.AssemblyInfo).Assembly)
    .Build();

  [Fact]
  public void DomainTypesShouldNotReferenceInfrastructure()
  {
    var domainTypes = Types().That()
      .ResideInNamespace("Vini.ModularMonolith.Example.OrderProcessing.Domain.*", useRegularExpressions: true)
      .As("OrderProcessing Domain Types");

    var infrastructureTypes = Types().That()
      .ResideInNamespace("Vini.ModularMonolith.Example.OrderProcessing.Infrastructure.*", useRegularExpressions: true)
      .As("OrderProcessing Infrastructure Types");

    var rule = domainTypes.Should().NotDependOnAny(infrastructureTypes);

    //PrintTypes(domainTypes, infrastructureTypes);

    rule.Check(Architecture);
  }

  [Fact]
  public void DomainTypesShouldNotReferencePresentation()
  {
    var domainTypes = Types().That()
      .ResideInNamespace("Vini.ModularMonolith.Example.OrderProcessing.Domain.*", useRegularExpressions: true)
      .As("OrderProcessing Domain Types");

    var presentationTypes = Types().That()
      .ResideInNamespace("Vini.ModularMonolith.Example.OrderProcessing.Endpoints.*", useRegularExpressions: true)
      .As("OrderProcessing Presentation Types");

    var rule = domainTypes.Should().NotDependOnAny(presentationTypes);

    //PrintTypes(domainTypes, presentationTypes);

    rule.Check(Architecture);
  }

  private void PrintTypes(GivenTypesConjunctionWithDescription domainTypes, GivenTypesConjunctionWithDescription infrastructureTypes)
  {
    foreach (var domainClass in domainTypes.GetObjects(Architecture))
    {
      _outputHelper.WriteLine($"Domain Type: {domainClass.FullName}");
      foreach (var dependency in domainClass.Dependencies)
      {
        var targetType = dependency.Target;
        if (infrastructureTypes.GetObjects(Architecture).Any(infraClass => infraClass.Equals(targetType)))
        {
          _outputHelper.WriteLine($"   Depends on Infrastructure: {targetType}");
        }
      }
    }

    foreach (var iType in infrastructureTypes.GetObjects(Architecture))
    {
      _outputHelper.WriteLine($"Infrastructure Types: {iType.FullName}");
    }
  }
}

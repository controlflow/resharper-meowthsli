using System;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Daemon.CSharp.Stages;
using JetBrains.ReSharper.Daemon.Stages;
using JetBrains.ReSharper.Daemon.Stages.Dispatcher;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;

[assembly: RegisterConfigurableSeverity(
  ReSharper.Meowthsli.YouAreDoingItWrongWarning.SeverityId,
  null, HighlightingGroupIds.CodeSmell,
  "Meowthsli: you are doing it wrong!",
  "Meowthsli: you are doing it wrong!",
  Severity.ERROR, false)]

namespace ReSharper.Meowthsli
{
  [ElementProblemAnalyzer(typeof(IClassLikeDeclaration),
    HighlightingTypes = new[] { typeof(YouAreDoingItWrongWarning) })]
  public class TypeNamesEndsWithAnalysis/*without -er!*/ : ElementProblemAnalyzer<IClassLikeDeclaration>
  {
    protected override void Run(
      IClassLikeDeclaration element, ElementProblemAnalyzerData data, IHighlightingConsumer consumer)
    {
      var declaredName = element.DeclaredName;
      if (declaredName.EndsWith("er", StringComparison.OrdinalIgnoreCase))
      {
        if (declaredName.EndsWith("Builder", StringComparison.OrdinalIgnoreCase))
          return; // билдер это паттерн такой, с ясной ответственностью
        if (declaredName.EndsWith("Manager", StringComparison.OrdinalIgnoreCase))
          return; // Manager короче чем Repository, более прнятно и проактивно по сути самого слова
        if (declaredName.Equals("ProductOwner", StringComparison.OrdinalIgnoreCase))
          return; // продактовнер окей

        consumer.AddHighlighting(
          new YouAreDoingItWrongWarning(
            "@meowthsli: Если разработчик называет сущности в коде на -er " +
            "(Executor, Owner, Processor), то он лячкает говнокод ПРЯМО СЕЙЧАС, " +
            "есть шанс его остановить"),
            element.GetNameDocumentRange());
      }
    }
  }
}

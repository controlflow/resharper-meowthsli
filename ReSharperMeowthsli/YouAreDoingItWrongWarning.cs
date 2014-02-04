using JetBrains.Annotations;
using JetBrains.ReSharper.Daemon;
using JetBrains.ReSharper.Psi.CSharp;

namespace ReSharper.Meowthsli
{
  [ConfigurableSeverityHighlighting(SeverityId, CSharpLanguage.Name)]
  public class YouAreDoingItWrongWarning : IHighlighting
  {
    public const string SeverityId = "YouAreDoingItWrong";

    public YouAreDoingItWrongWarning([NotNull] string message)
    {
      ToolTip = message;
    }

    public bool IsValid() { return true; }
    public string ToolTip { get; private set; }
    public string ErrorStripeToolTip { get { return ToolTip; } }
    public int NavigationOffsetPatch { get { return 0; } }
  }
}
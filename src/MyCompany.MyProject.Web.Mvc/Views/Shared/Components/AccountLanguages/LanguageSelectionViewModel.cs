using Abp.Localization;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace MyCompany.MyProject.Web.Views.Shared.Components.AccountLanguages;

public class LanguageSelectionViewModel
{
    public LanguageInfo CurrentLanguage { get; set; }

    public IReadOnlyList<LanguageInfo> Languages { get; set; }

    public PathString CurrentUrl { get; set; }
}

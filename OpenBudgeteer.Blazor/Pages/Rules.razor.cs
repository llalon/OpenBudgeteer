using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using OpenBudgeteer.Blazor.Shared;
using OpenBudgeteer.Core.Common;
using OpenBudgeteer.Core.Data.Contracts.Services;
using OpenBudgeteer.Core.ViewModels.EntityViewModels;
using OpenBudgeteer.Core.ViewModels.PageViewModels;

namespace OpenBudgeteer.Blazor.Pages;

public partial class Rules : ComponentBase
{
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private IServiceManager ServiceManager { get; set; } = null!;

    private RulesPageViewModel _dataContext = null!;
    
    private HashSet<RuleSetViewModel> _selectedRuleSets = new();

    protected override async Task OnInitializedAsync()
    {
        _dataContext = new RulesPageViewModel(ServiceManager);

        await HandleResult(await _dataContext.LoadDataAsync());
    }

    private void RuleSet_SelectionChanged(HashSet<RuleSetViewModel> items)
    {
        _selectedRuleSets = items;
    }

    private async Task DeleteSelectedRuleSets()
    {
        var parameters = new DialogParameters<DeleteConfirmationDialog>
        {
            { x => x.Title, "Delete Rules" },
            { x => x.Message, "Do you really want to delete the selected Rules?" }
        };
        var dialog = await DialogService.ShowAsync<DeleteConfirmationDialog>("Delete Rules", parameters);
        var result = await dialog.Result;
        if (result is { Canceled: false })
        {
            var deletionResults = _selectedRuleSets
                .Select(i => i.DeleteRuleSet())
                .ToList();
            if (deletionResults.Any(i => !i.IsSuccessful))
            {
                await HandleResult(deletionResults.First(i => !i.IsSuccessful));
            }
            else
            {
                await HandleResult(deletionResults.First());
            }
            _selectedRuleSets.Clear();
        }
    }

    private async Task ShowEditRuleSetDialog(RuleSetViewModel? ruleSet)
    {
        var dialogDataContext = ruleSet ?? RuleSetViewModel.CreateEmpty(ServiceManager); 
        var parameters = new DialogParameters<EditRuleSetDialog>
        {
            { x => x.Title, ruleSet == null ? "Create Rule" : "Edit Rule" },
            { x => x.DataContext, dialogDataContext }
        };
        var dialog = await DialogService.ShowAsync<EditRuleSetDialog>(
            ruleSet == null ? "Create Rule" : "Edit Rule", parameters);
        var dialogResult = await dialog.Result;
        if (dialogResult is { Canceled: false })
        {
            await HandleResult(_dataContext.SaveRuleSetItem(dialogDataContext));
        }
      
        StateHasChanged();
    }

    private async Task HandleResult(ViewModelOperationResult result)
    {
        if (!result.IsSuccessful)
        {
            var parameters = new DialogParameters<ErrorMessageDialog>
            {
                { x => x.Title, "Rules" },
                { x => x.Message, result.Message }
            };
            await DialogService.ShowAsync<ErrorMessageDialog>("Rules", parameters);
        }
		if (result.ViewModelReloadRequired)
        {
            await _dataContext.LoadDataAsync();
            StateHasChanged();
        }
    }
}
using System;

public interface IResourceDialogModel : IDialogModel
{
    public IResourceCrafter Crafter { get; }
    public IItemSwitchViewModel ResourceModel { get; }
}

public class ResourceDialogModel : IResourceDialogModel, IDisposable
{
    public DialogsType Type => DialogsType.Resource;
    
    public IResourceCrafter Crafter { get; }
    public IItemSwitchViewModel ResourceModel => _resourceSwitchViewModel;
    private readonly ItemSwitchViewModel _resourceSwitchViewModel;

    public ResourceDialogModel(IResourceCrafter crafter, ICommandDispatcher commandDispatcher)
    {
        Crafter = crafter;
        _resourceSwitchViewModel = new ItemSwitchViewModel(crafter.Resource, commandDispatcher);
    }

    public void Dispose()
    {
        _resourceSwitchViewModel.Dispose();
    }
}
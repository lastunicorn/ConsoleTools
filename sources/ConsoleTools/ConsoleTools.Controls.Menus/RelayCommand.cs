using System;

namespace DustInTheWind.ConsoleTools.Controls.Menus;

public class RelayCommand : ICommand
{
    public Action ExecuteAction { get; set; }

    public Func<bool> IsActiveAction { get; set; }

    public bool IsActive => ExecuteAction != null && (IsActiveAction?.Invoke() ?? true);

    public void Execute()
    {
        ExecuteAction?.Invoke();
    }
}
$version = "0.5.7"

$packages = @(
	@{
		PackageId = "DustInTheWind.ConsoleTools.Core"
		ProjectPath = "..\sources\ConsoleTools\ConsoleTools.Core\ConsoleTools.Core.csproj"
	},
	@{
		PackageId = "DustInTheWind.ConsoleTools.Controls"
		ProjectPath = "..\sources\ConsoleTools\ConsoleTools.Controls\ConsoleTools.Controls.csproj"
	},
	@{
		PackageId = "DustInTheWind.ConsoleTools.Controls.Menus"
		ProjectPath = "..\sources\ConsoleTools\ConsoleTools.Controls.Menus\ConsoleTools.Controls.Menus.csproj"
	},
	@{
		PackageId = "DustInTheWind.ConsoleTools.Controls.Spinners"
		ProjectPath = "..\sources\ConsoleTools\ConsoleTools.Controls.Spinners\ConsoleTools.Controls.Spinners.csproj"
	},
	@{
		PackageId = "DustInTheWind.ConsoleTools.Controls.Tables"
		ProjectPath = "..\sources\ConsoleTools\ConsoleTools.Controls.Tables\ConsoleTools.Controls.Tables.csproj"
	},
	@{
		PackageId = "ConsoleTools"
		NuspecPath = "ConsoleTools.nuspec"
	}
)

$packagesDirectory = ".\packages-output"
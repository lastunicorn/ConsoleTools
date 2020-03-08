// ConsoleTools
// Copyright (C) 2017-2020 Dust in the Wind
// 
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

// --------------------------------------------------------------------------------
// Bugs or feature requests
// --------------------------------------------------------------------------------
// Note: For any bug or feature request please add a new issue on GitHub: https://github.com/lastunicorn/ConsoleTools/issues/new

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DustInTheWind.ConsoleTools.Controls;
using DustInTheWind.ConsoleTools.Controls.Spinners;
using DustInTheWind.ConsoleTools.Mvc.UseCases;
using DustInTheWind.ConsoleTools.Mvc.UserControls;

namespace DustInTheWind.ConsoleTools.Mvc
{
    public abstract class ConsoleApplicationBase
    {
        private readonly IServiceProvider serviceProvider;

        private readonly ApplicationHeader applicationHeader;
        private readonly UseCaseCollection useCases;
        private readonly ApplicationFooter applicationFooter;

        public bool ShowHeader { get; set; } = true;

        public bool ShowFooter { get; set; }

        public bool UseSpinner { get; set; }

        public bool PauseOnExit { get; set; }

        protected ConsoleApplicationBase()
        {
            serviceProvider = CreateServiceProvider();

            applicationHeader = CreateApplicationHeader();
            ConfigureApplicationHeader(applicationHeader);

            applicationFooter = CreateApplicationFooter();
            ConfigureApplicationFooter(applicationFooter);

            useCases = CreateUseCaseCollection() ?? new UseCaseCollection();
            CreateUseCases(useCases);
        }

        protected abstract IServiceProvider CreateServiceProvider();

        protected virtual ApplicationHeader CreateApplicationHeader()
        {
            return new ApplicationHeader();
        }

        protected virtual void ConfigureApplicationHeader(ApplicationHeader header)
        {
        }

        protected virtual ApplicationFooter CreateApplicationFooter()
        {
            return new ApplicationFooter();
        }

        protected virtual void ConfigureApplicationFooter(ApplicationFooter footer)
        {
        }

        protected virtual UseCaseCollection CreateUseCaseCollection()
        {
            return new UseCaseCollection();
        }

        protected virtual void CreateUseCases(UseCaseCollection useCaseCollection)
        {
            UseCaseCollectionItem helpUseCaseItem = CreateHelpCommand();

            if (helpUseCaseItem != null)
                useCases.Add(helpUseCaseItem);

            Assembly currentAssembly = Assembly.GetExecutingAssembly();

            IEnumerable<Assembly> assemblies = AppDomain.CurrentDomain.GetAssemblies()
                .Where(x => x != currentAssembly);

            foreach (Assembly assembly in assemblies)
            {
                IEnumerable<Type> useCasesTypes = assembly.GetTypes()
                    .Where(x => typeof(IUseCase).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

                foreach (Type useCasesType in useCasesTypes)
                {
                    string name = useCasesType.Name.EndsWith("UseCase", StringComparison.InvariantCultureIgnoreCase)
                        ? useCasesType.Name.Substring(0, useCasesType.Name.Length - "UseCase".Length).ToLower()
                        : useCasesType.Name;

                    IUseCase useCase = serviceProvider.GetService(useCasesType) as IUseCase;

                    useCaseCollection.Add(name, useCase);
                }
            }
        }

        protected virtual UseCaseCollectionItem CreateHelpCommand()
        {
            HelpUseCase helpUseCase = new HelpUseCase(useCases);
            return new UseCaseCollectionItem("help", helpUseCase);
        }

        public void Run(string[] args)
        {
            try
            {
                OnStart();

                if (ShowHeader)
                    applicationHeader?.Display();

                Arguments arguments = new Arguments(args);
                IUseCase useCase = useCases.SelectCommand(arguments.Command);

                if (UseSpinner)
                    Spinner.Run(() => useCase.Execute(arguments));
                else
                    useCase.Execute(arguments);
            }
            catch (Exception ex)
            {
                OnError(ex);
            }
            finally
            {
                OnExit();

                if (ShowFooter)
                    applicationFooter?.Display();

                if (PauseOnExit)
                    Pause.QuickDisplay();
            }
        }

        protected virtual void OnStart()
        {
        }

        protected virtual void OnExit()
        {
        }

        protected virtual void OnError(Exception ex)
        {
            CustomConsole.WriteLineError(ex);
        }
    }
}
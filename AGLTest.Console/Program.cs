using AutoMapper;
using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.CommandLineUtils;
using AGLTest.Common.Mappers;
using AGLTest.Common.Models;
using AGLTest.DataViewer.IO;
using AGLTest.DataViewer.Config;
using AGLTest.DataViewer.Types;
using AGLTest.DataViewer.CustomViews;
using AGLTest.DataViewer.View;

namespace AGLTest.ConsoleApp
{
    class Program
    {
        static int Main(string[] args)
        {
            // init of automapper to allow using static Mapper
            Mapper.Initialize(config =>
            {
                config.CreateMap<IEnumerable<Person>, GenderPetView>().ConvertUsing<GenderPetViewConverter>();
            });
            
            var app = new CommandLineApplication()
            {
                Name = "AGL.CodeTest Data Viewer",
                Description = "Console viewer for pets.",
            };

            var output = new OutputWriter(app);
            var viewerFactory = new DataViewFactory<IDataView>();        

            // help, and version options
            app.HelpOption("-?|-h|--help");
            app.VersionOption("-v|--version", () =>
            {
                return string.Format("Version {0}", Assembly.GetEntryAssembly().GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion);
            });

            // command line options
            var feedUrl = app.Option("-u|--url <feedurl>",
                                     "Datafeed URL",
                                     CommandOptionType.SingleValue);

            var viewName = app.Option("-n|--name <viewname>",
                                      "View name",
                                      CommandOptionType.SingleValue);

            var petType = app.Option("-p|--pet <pettype>",
                                     "Filter by type of pet",
                                     CommandOptionType.MultipleValue);

            // delegate execution handler
            app.OnExecute(() =>
            {
                try
                {
                    if (feedUrl.HasValue())
                    {
                        var config = new ViewerConfig();
                        var url = feedUrl.Value();
                        var name = viewName.HasValue() ? viewName.Value() : DefaultView.Name;

                        if (petType.HasValue())
                        {
                            config.AddRange(petType.Values);
                        };

                        var view = viewerFactory.Create(name, output, url);
                        return view.Execute(config);
                    }
                    else
                    {
                        output.WriteFail($"Missing value for {feedUrl.Description}");
                        return ExitCodes.ERROR;
                    }
                }
                catch (Exception ex)
                {
                    output.WriteFail((ex.InnerException == null) ? ex : ex.InnerException);
                    return ExitCodes.ERROR;
                }
            });

            // console command execution
            try
            {
                return app.Execute(args);
            }
            catch (CommandParsingException ex)
            {
                // command line argument error
                output.WriteFail(ex);
            }
            catch (Exception ex)
            {
                output.WriteFail(ex);
            }

            return ExitCodes.ERROR;
        }
    }
}

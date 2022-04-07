using AutoMapper;
using LiveMotion.Core.Interfaces;
using LiveMotion.Core.Repositories;
using LiveMotion.Core.Services;
using LiveMotion.WPFCliet.AutoMapper;
using LiveMotion.WPFCliet.Controls.PresentationList;
using LiveMotion.WPFCliet.Controls.Slide;
using LiveMotion.WPFCliet.Dialoges;
using LiveMotion.WPFCliet.ViewModels;
using SimpleInjector;
using SimpleInjector.Lifestyles;
using System;

namespace LiveMotion.WPFCliet
{
    static class Program
    {
        public static Container Container { get; private set; }

        [STAThread]
        static void Main()
        {
            Bootstrap();

            // Any additional other configuration, e.g. of your desired MVVM toolkit.

            RunApplication(Container);
        }

        private static Container Bootstrap()
        {
            // Create the container as usual.
            Container = new Container();

            Container.Options.DefaultScopedLifestyle = new ThreadScopedLifestyle();

            var mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });

            var mapper = mapperConfiguration.CreateMapper();

            Container.RegisterInstance(mapper);
            // Register your types, for instance:
            Container.Register<Core.DbConfiguration.LiveMotionContext>(Lifestyle.Scoped);
            Container.Register<IBaseRepository, BaseRepository>();
            Container.Register<SlideService>();
            Container.Register<PresentationService>();

            // Register your windows and view models:
            Container.Register<MainWindow>();
            Container.Register<MainWindowViewModel>();

            Container.Register<PresentationListViewModel>();
            Container.Register<SlidePlayerViewModel>();
            Container.Register<SlideListViewModel>();
            Container.Register<PresentationEditViewModel>();


            Container.Verify();

            return Container;
        }

        private static void RunApplication(Container container)
        {
            try
            {
                var app = new App();
                //app.InitializeComponent();
                var mainWindow = container.GetInstance<MainWindow>();
                app.Run(mainWindow);
            }
            catch (Exception ex)
            {
                //Log the exception and exit
            }
        }



    }
}

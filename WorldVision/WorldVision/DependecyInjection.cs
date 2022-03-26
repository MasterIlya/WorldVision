using Microsoft.Extensions.DependencyInjection;
using WorldVision.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldVision.Repositories;
using WorldVision.Repositories.Repositories;
using WorldVision.Services.Services;
using Elasticsearch.Net;
using Nest;
using Microsoft.AspNetCore.Http;

namespace WorldVision
{
    internal class DependencyInjection
    {
        private readonly ApplicationSettings _applicationSettings;
        private readonly IServiceCollection _container;
        public DependencyInjection(ApplicationSettings applicationSetting, IServiceCollection container)
        {
            _applicationSettings = applicationSetting;
            _container = container;
        }

        internal void Init()
        {
            _container.AddSingleton(typeof(IRepositoryContext), new RepositoryContext(_applicationSettings.ConnectionString));

            RegisterSingletons(typeof(UsersService), "Service");
            RegisterSingletons(typeof(UsersRepository), "Repository");
            RegisterElacticSearch();
        }

        private void RegisterSingletons(Type anyTypeFromAssembly, string typePostfix)
        {
            var implementationTypes = anyTypeFromAssembly.Assembly.GetExportedTypes().Where(x => x.IsClass && !x.IsAbstract && x.Name.EndsWith(typePostfix));
            foreach (var implementationType in implementationTypes)
            {
                RegisterSingleton(implementationType);
            }
        }

        private void RegisterSingleton(Type implementationType)
        {
            var interfaceType = GetDefaultInterface(implementationType);
            _container.AddSingleton(interfaceType, implementationType);
        }

        private static Type GetDefaultInterface(Type classType)
        {
            return classType.GetInterface("I" + classType.Name);
        }

        private void RegisterElacticSearch()
        {
            var pool = new SingleNodeConnectionPool(new Uri("http://localhost:9200"));
            var settings = new ConnectionSettings(pool).DefaultIndex("reviews");
            var client = new ElasticClient(settings);
            _container.AddSingleton(client);
        }
    }
}

using Microsoft.Extensions.DependencyInjection;
using QuickHelp.Application.Abstractions;
using QuickHelp.Domain;

namespace QuickHelp.Application.Stub
{
	public static class QuickHelpStubDependencies
	{
		public static void Register(IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IDocumentFilterer, StubbedDocumentFilterer>();
			serviceCollection.AddSingleton<IDocumentRepository, StubbedDocumentRepository>();
		}
	}
}
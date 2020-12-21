using System.Collections.Generic;
using QuickHelp.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace QuickHelp.Application.Stub
{
	public class DocumentFilterer : IDocumentFilterer
	{
		public List<Document> Filter( string searchString )
		{
			var documents = new List<Document>(Documents.Get());
			var searchTerms = searchString.ToLower().Split( ' ' );
			foreach ( var searchTerm in searchTerms )
				documents.RemoveAll( x => !x.Name.Value.ToLower().Contains( searchTerm ) );

			return documents;
		}
	}

	public static class Dependencies
	{
		public static void Register(IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<IDocumentFilterer, DocumentFilterer>();
			serviceCollection.AddSingleton<IDocumentRepository, StubbedDocumentRepository>();
		}
	}
}
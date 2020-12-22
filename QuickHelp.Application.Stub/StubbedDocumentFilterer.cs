using System.Collections.Generic;
using QuickHelp.Application.Abstractions;
using QuickHelp.Domain;

namespace QuickHelp.Application.Stub
{
	public class StubbedDocumentFilterer : IDocumentFilterer
	{
		public List<Document> Filter( string searchString )
		{
			var documents = new List<Document>( Documents.Get() );
			documents.RemoveAll( x => x.Deleted );

			var searchTerms = searchString.ToLower().Split( ' ' );
			foreach ( var searchTerm in searchTerms )
				documents.RemoveAll( x => !x.Name.Value.ToLower().Contains( searchTerm ) );

			return documents;
		}
	}
}
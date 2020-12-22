using System.Collections.Generic;
using QuickHelp.Domain;

namespace QuickHelp.Application.Abstractions
{
	public interface IDocumentFilterer
	{
		List<Document> Filter( string searchString );
	}
}
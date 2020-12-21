using System.Collections.Generic;
using QuickHelp.Domain;

namespace QuickHelp.Application.Stub
{
	public interface IDocumentFilterer
	{
		List<Document> Filter( string searchString );
	}
}
using System.Linq;
using QuickHelp.Domain;
using QuickHelp.Domain.ValueObjects;

namespace QuickHelp.Application.Stub
{
	public class StubbedDocumentRepository : IDocumentRepository
	{
		public bool Exists( DocumentName name )
		{
			return Documents.Get().Any( x => x.Name == name );
		}

		public void Save( Document document )
		{
			if (Documents.Get().Contains( document ) )
				return;

			Documents.Add( document );
		}

		public Document Get( DocumentId id )
		{
			return Documents.Get().SingleOrDefault( x => x.Id == id );
		}
	}
}
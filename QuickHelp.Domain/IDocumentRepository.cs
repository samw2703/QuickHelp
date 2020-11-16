using QuickHelp.Domain.ValueObjects;

namespace QuickHelp.Domain
{
	public interface IDocumentRepository
	{
		bool Exists(DocumentName name);
	}
}
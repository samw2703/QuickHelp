using QuickHelp.Domain.ValueObjects;

namespace QuickHelp.Domain
{
	public interface IDocumentRepository
	{
		bool Exists(DocumentName name);
		void Save(Document document);
		Document Get(DocumentId id);
	}
}
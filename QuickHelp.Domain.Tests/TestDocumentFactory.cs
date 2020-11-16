using System;
using QuickHelp.Domain.ValueObjects;

namespace QuickHelp.Domain.Tests
{
	internal static class TestDocumentFactory
	{
		public static Document CreateDocument(DocumentId id = null, DocumentName name = null, DocumentBody body = null)
			=> Document.CreateNew(
				id ?? DocumentId.From(Guid.NewGuid()),
				name ?? DocumentName.From("Name"),
				body ?? DocumentBody.From("body"),
				MockDocumentRepositoryFactory.CreateRepository());

		public static Document CreateEventlessDocument(DocumentId id = null, DocumentName name = null, DocumentBody body = null)
		{
			var document = CreateDocument(id, name, body);
			document.ClearEvents();
			return document;
		}

		public static Document CreateDeletedDocument(DocumentId id = null, DocumentName name = null, DocumentBody body = null)
		{
			var document = CreateDocument(id, name, body);
			document.Delete();
			document.ClearEvents();
			return document;
		}
	}
}
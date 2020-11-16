using NUnit.Framework;
using System;
using System.Linq;
using Moq;
using QuickHelp.Domain.Events;
using QuickHelp.Domain.Exceptions;
using QuickHelp.Domain.ValueObjects;

using static QuickHelp.Domain.Tests.MockDocumentRepositoryFactory;
using static QuickHelp.Domain.Tests.TestDocumentFactory;

namespace QuickHelp.Domain.Tests
{
	public class When_creating_new
	{
		public class And_name_clashes
		{
			[Test]
			public void Throws_DocumentNameClash()
			{

				Assert.Throws<DocumentNameClash>(() => Document.CreateNew(
					DocumentId.From(),
					DocumentName.From("Name"),
					DocumentBody.From("Body"),
					CreateRepository(true)));
			}
		}

		public class And_creating_is_valid
		{

			[Test]
			public void Creates_successfully()
			{
				var documentId = DocumentId.From();
				var documentName = DocumentName.From("Name");
				var documentBody = DocumentBody.From("Body");
				var document = Document.CreateNew(documentId, documentName, documentBody, CreateRepository());

				Assert.AreEqual(documentName, document.Name);
				Assert.AreEqual(documentBody, document.Body);
			}

			[Test]
			public void Raises_DocumentCreated()
			{
				var documentId = DocumentId.From();
				var documentName = DocumentName.From("Name");
				var documentBody = DocumentBody.From("Body");
				var document = Document.CreateNew(documentId, documentName, documentBody, CreateRepository());

				var @event = document.Events.Single() as NewDocumentCreated;
				Assert.AreEqual(documentId.Value, @event.Id);
				Assert.AreEqual(documentName.Value, @event.Name);
				Assert.AreEqual(documentBody.Value, @event.Body);
			}
		}
	}

	public class When_renaming
	{
		public class And_renaming_is_valid
		{
			[Test]
			public void Renaming_successful()
			{
				var newName = DocumentName.From("I'm a new name");
				var document = CreateEventlessDocument();
				document.Rename(newName, CreateRepository());

				Assert.AreEqual(newName.Value, document.Name.Value);
			}

			[Test]
			public void DocumentRenamed_raised()
			{
				var documentId = DocumentId.From();
				var newName = DocumentName.From("I'm a new name");
				var document = CreateEventlessDocument(id: documentId);
				document.Rename(newName, CreateRepository());

				var @event = document.Events.Single() as DocumentRenamed;
				Assert.AreEqual(newName.Value, @event.NewName);
				Assert.AreEqual(documentId.Value, @event.Id);
			}
		}

		public class And_Document_with_new_name_already_exists
		{
			[Test]
			public void Throws_DocumentNameClash()
			{
				var document = CreateEventlessDocument();
				Assert.Throws<DocumentNameClash>(
					() => document.Rename(DocumentName.From("new name please"), CreateRepository(exists: true)));
			}
		}

		public class And_new_name_is_identical_to_current_name
		{
			[Test]
			public void No_events_raised()
			{
				var name = DocumentName.From("a name");
				var document = CreateEventlessDocument(name: name);
				document.Rename(name, CreateRepository());

				Assert.IsEmpty(document.Events);
			}
		}

		public class And_document_is_deleted
		{
			[Test]
			public void Throws_DeletedDocument()
			{
				var document = CreateDeletedDocument();
				Assert.Throws<DeletedDocument>(
					() => document.Rename(DocumentName.From("name"), CreateRepository()));
			}
		}
	}

	public class When_editing
	{
		public class And_editing_is_valid
		{
			[Test]
			public void Editing_successful()
			{
				var newBody = DocumentBody.From("a new body");
				var document = CreateEventlessDocument();
				document.Edit(newBody);

				Assert.AreEqual(newBody, document.Body);
			}

			[Test]
			public void DocumentEdited_raised()
			{
				var documentId = DocumentId.From();
				var newBody = DocumentBody.From("a new body");
				var document = CreateEventlessDocument(id: documentId);
				document.Edit(newBody);

				var @event = document.Events.Single() as DocumentEdited;
				Assert.AreEqual(newBody.Value, @event.NewBody);
				Assert.AreEqual(documentId.Value, @event.Id);
			}
		}

		public class And_document_is_deleted
		{
			[Test]
			public void DeletedDocument_thrown()
			{
				var document = CreateDeletedDocument();
				Assert.Throws<DeletedDocument>(() => document.Edit(DocumentBody.From("Doesn't matter")));
			}
		}
	}

	public class When_deleting
	{
		public class And_deleting_is_valid
		{
			[Test]
			public void Successfully_deletes()
			{
				var document = CreateDocument();
				document.Delete();

				Assert.True(document.Deleted);
			}

			[Test]
			public void DocumentDeleted_raised()
			{
				var document = CreateDocument();
				document.Delete();

				Assert.True(document.Deleted);
			}
		}

		public class And_document_is_deleted
		{
			[Test]
			public void DeletedDocument_thrown()
			{
				var document = CreateDeletedDocument();
				Assert.Throws<DeletedDocument>(
					() => document.Delete());
			}
		}
	}

	public class When_pinning
	{
		public class And_pinning_is_valid
		{
			[Test]
			public void Pins_successfully()
			{
				var document = CreateEventlessDocument();
				document.Pin();

				Assert.True(document.Pinned);
			}

			[Test]
			public void Raises_DocumentPinned()
			{
				var documentId = DocumentId.From();
				var document = CreateEventlessDocument(id: documentId);
				document.Pin();

				var @event = document.Events.Single() as DocumentPinned;
				Assert.AreEqual(documentId.Value, @event.Id);
			}
		}

		public class And_document_is_deleted
		{
			[Test]
			public void Throw_DeletedDocument()
			{
				var document = CreateDeletedDocument();
				Assert.Throws<DeletedDocument>(
					() => document.Pin());
			}
		}

		public class And_document_is_pinned
		{
			[Test]
			public void Does_not_raise_an_event()
			{
				var document = CreateEventlessDocument();
				document.Pin();
				document.ClearEvents();
				document.Pin();

				Assert.IsEmpty(document.Events);
			}
		}
	}

	public class When_Unpinning
	{
		public class And_unpinning_is_valid
		{
			[Test]
			public void Successfully_unpins()
			{
				var document = CreatePinnedDocument();
				document.Unpin();

				Assert.False(document.Pinned);
			}

			[Test]
			public void DocumentUnpinned_raised()
			{
				var documentId = DocumentId.From();
				var document = CreatePinnedDocument(documentId);
				document.Unpin();

				var @event = document.Events.Single() as DocumentUnpinned;
				Assert.AreEqual(documentId.Value, @event.Id);
			}
		}

		public class And_document_is_deleted
		{
			[Test]
			public void Throws_DeletedDocument()
			{
				var document = CreatePinnedDocument();
				document.Delete();

				Assert.Throws<DeletedDocument>(
					() => document.Unpin());
			}
		}

		public class And_document_is_not_pinned
		{
			[Test]
			public void Does_not_raise_event()
			{
				var document = CreateEventlessDocument();
				document.Unpin();

				Assert.IsEmpty(document.Events);
			}
		}

		private static Document CreatePinnedDocument(DocumentId id = null)
		{
			var document = CreateEventlessDocument(id: id ?? DocumentId.From());
			document.Pin();
			document.ClearEvents();
			return document;
		}
	}
}
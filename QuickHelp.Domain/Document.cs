using System.Collections.Generic;
using QuickHelp.Domain.Events;
using QuickHelp.Domain.Exceptions;
using QuickHelp.Domain.ValueObjects;

namespace QuickHelp.Domain
{
	public class Document
	{
		internal  DocumentId Id { get; }
		public DocumentName Name { get; private set; }
		internal DocumentBody Body { get; private set; }
		internal bool Deleted { get; private set; }
		internal bool Pinned { get; private set; }

		public List<object> Events { get; } = new List<object>();

		private Document(DocumentId id, DocumentName name, DocumentBody body)
		{
			Id = id;
			Name = name;
			Body = body;
			Raise(new NewDocumentCreated(Id.Value, Name.Value, Body.Value));
		}

		public static Document CreateNew(DocumentId id, 
			DocumentName name, 
			DocumentBody body, 
			IDocumentRepository repository)
		{
			ValidateNameClash(name, repository);
			return new Document(id, name, body);
		}

		public void Rename(DocumentName newName, IDocumentRepository repository)
		{
			if (newName == Name)
				return;
			ValidateNameClash(newName, repository);
			ValidateDeleted(nameof(Rename));

			Name = newName;
			Raise(new DocumentRenamed(Id.Value, newName.Value));
		}

		public void Edit(DocumentBody newBody)
		{
			ValidateDeleted(nameof(Edit));
			Body = newBody;
			Raise(new DocumentEdited(Id.Value, newBody.Value));
		}

		public void Delete()
		{
			ValidateDeleted(nameof(Delete));
			Deleted = true;
			Raise(new DocumentDeleted(Id.Value));
		}

		public void Pin()
		{
			ValidateDeleted(nameof(Pin));
			if (Pinned)
				return;

			Pinned = true;
			Raise(new DocumentPinned(Id.Value));
		}

		public void Unpin()
		{
			ValidateDeleted(nameof(Unpin));
			if (!Pinned)
				return;

			Pinned = false;
			Raise(new DocumentUnpinned(Id.Value));
		}

		private void ValidateDeleted(string action)
		{
			if (Deleted)
				throw new DeletedDocument($"Cannot {action} a deleted document");
		}

		private static void ValidateNameClash(DocumentName name, IDocumentRepository repository)
		{
			if (repository.Exists(name))
				throw new DocumentNameClash(name);
		}

		private void Raise(object @event)
			=> Events.Add(@event);

		internal void ClearEvents()
			=> Events.Clear();
	}
}
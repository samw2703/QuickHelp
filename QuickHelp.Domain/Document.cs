using System.Collections.Generic;
using QuickHelp.Domain.Events;
using QuickHelp.Domain.Exceptions;
using QuickHelp.Domain.ValueObjects;

namespace QuickHelp.Domain
{
	public class Document
	{
		private readonly DocumentId _id;
		private readonly DocumentName _name;
		private readonly DocumentBody _body;

		public List<object> Events { get; } = new List<object>();

		private Document(DocumentId id, DocumentName name, DocumentBody body)
		{
			_id = id;
			_name = name;
			_body = body;
			Raise(new NewDocumentCreated(_id, _name, _body));
		}

		public static Document CreateNew(DocumentId id, 
			DocumentName name, 
			DocumentBody body, 
			IDocumentRepository repository)
		{
			if (repository.Exists(name))
				throw new DocumentNameClash(name);
			return new Document(id, name, body);
		}

		private void Raise(object @event)
			=> Events.Add(@event);
	}
}
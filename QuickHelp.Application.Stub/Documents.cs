using System;
using System.Collections.Generic;
using QuickHelp.Domain;
using QuickHelp.Domain.ValueObjects;

namespace QuickHelp.Application.Stub
{
	public static class Documents
	{
		private static readonly List<Document> _documents;

		static Documents()
		{
			_documents = new List<Document>();
			AddDocument( Guid.NewGuid(), "first doc", "something interesting" );
			AddDocument( Guid.NewGuid(), "second doc", " something else interesting" );
			AddDocument( Guid.NewGuid(), "one more doc", "boooooring" );
			AddDocument( Guid.NewGuid(), "something else", "body" );
			AddDocument( Guid.NewGuid(), "a mistake", "you weren't supposed to find out this way" );
			AddDocument( Guid.NewGuid(), "multi line", @"I am a multi line document

and therefore I have multiple lines you see..." );
		}

		public static List<Document> Get()
		{
			return _documents;
		}

		public static void Add( Document document )
		{
			_documents.Add( document );
		}

		private static void AddDocument( Guid documentId, string documentName, string documentBody )
		{
			var document = Document.CreateNew( DocumentId.From( documentId ),
				DocumentName.From( documentName ),
				DocumentBody.From( documentBody ),
				new StubbedDocumentRepository() );
			_documents.Add( document );
		}
	}
}
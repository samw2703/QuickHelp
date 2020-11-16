using NUnit.Framework;
using System;
using System.Linq;
using Moq;
using QuickHelp.Domain.Events;
using QuickHelp.Domain.Exceptions;
using QuickHelp.Domain.ValueObjects;

namespace QuickHelp.Domain.Tests
{
	public class When_creating_new
	{
		public class And_name_clashes
		{
			[Test]
			public void Throws_DocumentNameClash()
			{
				var mockDocumentRepository = new Mock<IDocumentRepository>();
				mockDocumentRepository
					.Setup(x => x.Exists(It.IsAny<DocumentName>()))
					.Returns(true);

				Assert.Throws<DocumentNameClash>(() => Document.CreateNew(
					DocumentId.From(Guid.NewGuid()),
					DocumentName.From("Name"),
					DocumentBody.From("Body"),
					mockDocumentRepository.Object));
			}
		}

		public class And_no_name_clash
		{
			private IDocumentRepository _mockDocumentRepository;

			[SetUp]
			public void Setup()
			{
				var mockDocumentRepository = new Mock<IDocumentRepository>();
				mockDocumentRepository
					.Setup(x => x.Exists(It.IsAny<DocumentName>()))
					.Returns(false);
				_mockDocumentRepository = mockDocumentRepository.Object;
			}

			[Test]
			public void Creates_successfully()
			{
				Assert.DoesNotThrow(() => Document.CreateNew(
					DocumentId.From(Guid.NewGuid()),
					DocumentName.From("Name"),
					DocumentBody.From("Body"),
					_mockDocumentRepository));
			}

			[Test]
			public void Raises_DocumentCreated()
			{
				var documentId = DocumentId.From(Guid.NewGuid());
				var documentName = DocumentName.From("Name");
				var documentBody = DocumentBody.From("Body");
				var document = Document.CreateNew(documentId, documentName, documentBody, _mockDocumentRepository);

				var @event = document.Events.Single() as NewDocumentCreated;
				Assert.AreEqual(documentId.Value, @event.Id);
				Assert.AreEqual(documentName.Value, @event.Name);
				Assert.AreEqual(documentBody.Value, @event.Body);
			}
		}
	}
}
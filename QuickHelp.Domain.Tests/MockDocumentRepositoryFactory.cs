using Moq;
using QuickHelp.Domain.ValueObjects;

namespace QuickHelp.Domain.Tests
{
	internal static class MockDocumentRepositoryFactory
	{
		public static IDocumentRepository CreateRepository(bool exists = false)
		{
			var mock = new Mock<IDocumentRepository>();
			mock
				.Setup(x => x.Exists(It.IsAny<DocumentName>()))
				.Returns(exists);

			return mock.Object;
		}
	}
}
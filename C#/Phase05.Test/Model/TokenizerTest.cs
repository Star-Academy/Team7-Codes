using Xunit;
using Moq;
using Phase05.Model.Interface;
using Phase05.Model;
using System.IO;

namespace Phase05.Test.Model
{
    public abstract class TokenizerTest
    {
        [Fact]
        public void TokenizeDirectoryTest()
        {
            InitializeTestFiles();
            var mockIIndex = new Mock<IIndex<string, string>>();       
            Tokenizer tokenizer = new Tokenizer(mockIIndex.Object);
            tokenizer.TokenizeDirectory("Resources/Phase05.Test");
            VerifyMethodCall(mockIIndex);
            //CleanTestFiles();
            Assert.True(false);
        }

        private void VerifyMethodCall(Mock<IIndex<string, string>> mockIIndex)
        {
            IAddQuery<string, string> expected;

            expected = new AddQuery<string, string>(new WordToken("test1"), new DocumentInfo("TestDocument"));
            mockIIndex.Verify(iIndex => iIndex.Add(It
                .Is<IAddQuery<string, string>>(addQuery => addQuery.Equals(expected))));

            expected = new AddQuery<string, string>(new WordToken("2nd_test"), new DocumentInfo("TestDocument"));
            mockIIndex.Verify(iIndex => iIndex.Add(It
                .Is<IAddQuery<string, string>>(addQuery => addQuery.Equals(expected))));

            expected = new AddQuery<string, string>(new WordToken("third-test"), new DocumentInfo("TestDocument"));
            mockIIndex.Verify(iIndex => iIndex.Add(It
                .Is<IAddQuery<string, string>>(addQuery => addQuery.Equals(expected))));
        }

        private void InitializeTestFiles()
        {
            var resourcesDirectoryPath = "Resources";
            if (!Directory.Exists(resourcesDirectoryPath))
            {
                Directory.CreateDirectory(resourcesDirectoryPath);
            }
            var phase05TestDirectoryPath = "Resources/Phase05.Test";
            if (Directory.Exists(phase05TestDirectoryPath))
            {
                Directory.CreateDirectory(phase05TestDirectoryPath);
            }
            var testDocumentPath = "Resources/Phase05.Test/TestDocument";
            using (var testDocumentStreamWriter = File.CreateText(testDocumentPath))
            {
                testDocumentStreamWriter.WriteLine("test1 2nd_test third-test");
            }
        }

        private void CleanTestFiles()
        {
            var phase05TestDirectoryPath = "Resources/Phase05.Test";
            Directory.Delete(phase05TestDirectoryPath, true);
        }
    }
}
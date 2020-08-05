import org.junit.Rule;
import org.junit.Test;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.MockitoJUnit;
import org.mockito.junit.MockitoRule;

public class FileReaderTest {

    @Mock private InvertedIndex invertedIndexMock;

    @Rule public MockitoRule mockitoRule = MockitoJUnit.rule();

    @Test
    public void readFileTest() {
        FileReader fileReader = new FileReader(invertedIndexMock);
        fileReader.readAllFiles("src/test/resources");
        Mockito.verify(invertedIndexMock).add("test1", "TestDocument");
        Mockito.verify(invertedIndexMock).add("2nd_test", "TestDocument");
        Mockito.verify(invertedIndexMock).add("third-test", "TestDocument");
        Mockito.verifyNoMoreInteractions(invertedIndexMock);

    }
}

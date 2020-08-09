import org.junit.Rule;
import org.junit.Test;
import org.mockito.InjectMocks;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.MockitoJUnit;
import org.mockito.junit.MockitoRule;

public class AppTest {

    @Mock private InvertedIndex invertedIndexMock;
    @Mock private CommandReader commandReaderMock;

    @InjectMocks private App app;

    @Rule public MockitoRule mockitoRule = MockitoJUnit.rule();

    @Test
    public void runCommandLineTest() {

        String userInput = "THIS SHOULD BE SENT TO INVERTED_INDEX AFTER NORMALIZATION";
        String userInputNormalized = "this should be sent to inverted_index after normalization";
        Mockito.when(commandReaderMock.readCommand()).thenReturn(userInput);
        app.runCommandLine();
        try {
            Mockito.verify(invertedIndexMock).advanceFind(userInputNormalized);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

}
import org.junit.Before;
import org.junit.Rule;
import org.junit.Test;
import org.mockito.Mock;
import org.mockito.Mockito;
import org.mockito.junit.MockitoJUnit;
import org.mockito.junit.MockitoRule;

import static org.mockito.Mockito.mock;

public class AppTest {

    @Mock private final InvertedIndex invertedIndexMock = mock(InvertedIndex.class);
    @Mock private final CommandReader commandReaderMock = mock(CommandReader.class);

    private App app;

    @Rule public MockitoRule mockitoRule = MockitoJUnit.rule();

    @Before
    public void init(){
        app = new App("src/test/resources/NotEmptyDir", commandReaderMock, invertedIndexMock);
    }


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

    @Test
    public void notFoundTest() throws Exception {
        String userInput = "not found query";
        String exceptionMessage = "not found message";
        Mockito.when(commandReaderMock.readCommand()).thenReturn(userInput);
        Mockito.when(invertedIndexMock.advanceFind(userInput)).thenThrow(new Exception(exceptionMessage));
        app.runCommandLine();
        Mockito.verify(commandReaderMock).sendResponse(exceptionMessage);
    }
}
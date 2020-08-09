import java.util.HashSet;

public class App {

    private final InvertedIndex invertedIndex;
    private final CommandReader commandReader;

    public App(CommandReader commandReader, InvertedIndex invertedIndex) {
        this.invertedIndex = invertedIndex;
        this.commandReader = commandReader;
    }

    public App(String path, CommandReader commandReader, InvertedIndex invertedIndex) {
        this(commandReader, invertedIndex);
        if (path != null)
            readFileToInvertedIndex(path);
    }

    private void readFileToInvertedIndex(String path) {
        FileReader fileReader = new FileReader(invertedIndex);
        fileReader.readAllFiles(path);
    }

    public void runCommandLine() {
        commandReader.sendResponse("Search : ");
        String words = commandReader.readCommand();
        words = Tokenizer.normalize(words);

        try {
            HashSet<String> result = invertedIndex.advanceFind(words);
            printOutput(result, words);
        } catch(Exception e) {
            System.out.println(e.getMessage());
        }

        commandReader.closeResources();
    }

    private void printOutput(HashSet<String> result, String words){
        commandReader.sendResponse(result.size() + " documents with \"" + words + "\" : ");
        result.forEach(singleResult -> commandReader.sendResponse("\t" + singleResult));
    }  
}

public interface CommandReader {
    String readCommand();
    void sendResponse(String response);
    void closeResources();
}

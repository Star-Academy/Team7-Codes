import java.util.Scanner;

public class TerminalCommandReader implements CommandReader {

    final Scanner scanner = new Scanner(System.in);

    @Override
    public String readCommand() {
        return scanner.nextLine();
    }

    @Override
    public void sendResponse(String response) {
        System.out.println(response);
    }

    @Override
    public void closeResources() {
        scanner.close();
    }
}

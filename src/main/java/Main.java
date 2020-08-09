import java.util.Scanner;

public class Main {
    
    public static void main(String[] args) {
        //String path = "/media/hassan/new part/code-star/EnglishData";  // linux path
        String path= "D:\\Downloads\\SampleEnglishData\\EnglishData"; // Windows path
        new App(path, implementCommandReader(), new InvertedIndex()).runCommandLine();
    }

    private static CommandReader implementCommandReader() {

        return new CommandReader() {

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
        };
    }
}
import org.junit.Test;

import static org.junit.Assert.*;

public class TokenizerTest {
    @Test
    public void normalizeTest() {
        String expected = "this_is_a_text";
        String actual = Tokenizer.normalize("tHIS_iS_A_teXT");
        assertEquals(expected, actual);
    }
}
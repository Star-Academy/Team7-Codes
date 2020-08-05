import org.junit.Test;

import static org.junit.Assert.assertEquals;

public class TokenizerTest {
    @Test
    public void normalizeTest() {
        String expected = "  this_is_a_    text and this is a number 4323";
        String actual = Tokenizer.normalize("  tHIS_iS_A_    teXT ANd tHiS IS a nuMBER 4323");
        assertEquals(expected, actual);
    }
}
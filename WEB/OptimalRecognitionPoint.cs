namespace WEB
{
    public static class OptimalRecognitionPoint
    {
        public static (string left, string middle, string right) Split(string? word)
        {
            if (string.IsNullOrEmpty(word)) return ("", "", "");

            int count = (word.Contains('.') || word.Contains(',')) ? word.Length - 1 : word.Length;

            int pivot = count switch
            {
                <= 3    => 0,
                <= 5    => 1,
                <= 9    => 2,
                _       => 3
            };

            return (
                word[..pivot],
                word[pivot].ToString(),
                word[(pivot + 1)..]
            );
        }
    }
}

public class CardSelectTypeAndRarityDialogRequest
{
    /// <summary>
    /// タイプ
    /// </summary>
    public Type type { get; set; }

    /// <summary>
    /// レアリティ
    /// </summary>
    public Rarity rarity { get; set; }
}

public class CardSelectTypeAndRarityDialogResponse
{
    /// <summary>
    /// レスポンスタイプ
    /// </summary>
    public CommonDialogResponseType responseType { get; set; }

    /// <summary>
    /// タイプ
    /// </summary>
    public Type type { get; set; }

    /// <summary>
    /// レアリティ
    /// </summary>
    public Rarity rarity { get; set; }
}

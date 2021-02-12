using UnityEngine;

public class CardSelectClassDialogRequest
{
    /// <summary>
    /// カードクラス
    /// </summary>
    public CardClass cardClass { get; set; }
}

public class CardSelectClassDialogResponse
{
    /// <summary>
    /// レスポンスタイプ
    /// </summary>
    public CommonDialogResponseType responseType { get; set; }

    /// <summary>
    /// カードクラス
    /// </summary>
    public CardClass cardClass { get; set; }
}
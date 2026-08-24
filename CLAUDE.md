# 開發流程：TDD（.NET + NUnit）

本專案採用嚴格 TDD。任何行為變更都必須依序經過 RED → GREEN → REFACTOR，
**不得跳過、不得合併階段**。使用 /red、/green、/refactor 指令逐步進行。

## RED
- 只寫測試，**不建立或修改任何實作程式碼**（含空 class / 方法骨架）
- 一次一個測試，涵蓋一個行為
- 寫完必須執行測試並回報失敗訊息原文
- 失敗必須是「預期的失敗」：斷言失敗，或型別不存在導致編譯錯誤
- 停下等我確認才進 GREEN

## GREEN
- **禁止修改測試檔案**。測試是規格。認為測試有誤請停下來問我
- 寫最小可行實作，不加測試未涵蓋的功能
- **禁止用比對測試輸入值的方式騙過測試**（hardcode、特例 if）
- 跑全部測試確認全綠，且未破壞既有測試
- 停下等我確認

## REFACTOR
- 不改變外部行為，不新增功能，不改測試斷言
- 沒有明顯壞味道就明說「不需要重構」，不要為改而改
- 每次重構後重跑全部測試

## 通用
- 每個綠燈後建議我 commit，一個 commit = 一個行為
- 一次只做一個功能循環

## NUnit 慣例
- 命名：`方法名_情境_預期結果`
  例：`CalculateDiscount_AmountOver1000_Returns10PercentOff`
- 斷言一律用 constraint model：`Assert.That(actual, Is.EqualTo(expected))`
  NUnit 4 已移除 `Assert.AreEqual`／`Assert.IsTrue` 等 classic 寫法，禁止使用
- 多斷言用 `Assert.Multiple(() => { ... })`
- 例外用 `Assert.That(() => sut.Do(), Throws.TypeOf<ArgumentException>())`
- 參數化用 `[TestCase]` / `[TestCaseSource]`，但不同「行為」不可擠進同一測試
- Arrange / Act / Assert 三段之間空一行

## 指令
- 全部測試：`dotnet test ConsoleApp1/ConsoleApp1.slnx --nologo -v q`
- 單一測試：`dotnet test ConsoleApp1/ConsoleApp1.slnx --nologo --filter "FullyQualifiedName~<名稱>"`
- 建置：`dotnet build ConsoleApp1/ConsoleApp1.slnx --nologo`

## 測試範例（目前專案尚無測試，請以此為風格基準）
```csharp
namespace ConsoleApp1.Tests;

[TestFixture]
public class DiscountCalculatorTests
{
    [Test]
    public void Calculate_AmountOver1000_Returns10PercentOff()
    {
        var sut = new DiscountCalculator();

        var result = sut.Calculate(1500m);

        Assert.That(result, Is.EqualTo(1350m));
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void Calculate_NonPositiveAmount_ThrowsArgumentOutOfRange(decimal amount)
    {
        var sut = new DiscountCalculator();

        Assert.That(() => sut.Calculate(amount),
            Throws.TypeOf<ArgumentOutOfRangeException>());
    }
}
```

## 專案結構
- git 根目錄：本目錄（`tdd20260824/`）
- solution：`ConsoleApp1/ConsoleApp1.slnx`
- 產品程式碼：`ConsoleApp1/ConsoleApp1/`（Exe，已設定 `InternalsVisibleTo`
  給測試專案，internal 型別可直接測試，**不要為了測試把型別改成 public**）
- 測試程式碼：`ConsoleApp1/ConsoleApp1.Tests/`（NUnit 4.3.2 + NUnit.Analyzers）
- NUnit.Analyzers 會在編譯期擋掉 classic assert，請直接使用 constraint model
- 所有 dotnet 指令請從 git 根目錄執行，路徑如上方「指令」段所示

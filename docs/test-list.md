# Tennis Kata — 測試清單

TDD 待辦清單。完成的項目打勾並補上 commit hash。
每項＝一個行為＝一個 RED→GREEN→REFACTOR 循環＝一個 commit。

分支：`tennis-kata`（分出點 `6987d2f`）

## 前提

### 假設的 API

```csharp
internal class TennisGame
{
    public string Score(int player1, int player2)
}
```

純函式、無狀態，與 `DiscountCalculator` 同風格。

**已定案採用此純函式版本**（2026-08-24）。

另一個選項是經典 Coding Dojo 的有狀態版本（`WonPoint(name)` / `GetScore()`）。
設計上更豐富，但「連續得分累積」本身會變成一個要測的行為。若日後要練
「面對既有爛程式碼如何安全重構」，該用原題的爛實作起手，而非從零 TDD。

### 為什麼第 1 項綁三組 TestCase

CLAUDE.md 禁止用 hardcode 或比對測試輸入值騙過測試，所以經典 kata 的
第一步（`0-0` → 直接 `return "Love-All"`）在本專案不可用。第 1 項一開始
就必須是一條「規則」，用三組資料逼出分數名稱表與 `-All` 的通則。

### 排序原則

依 CLAUDE.md 與 `/tdd` 階段 0 的規則：退化案例先、主要規則次之、
邊界值緊跟對應規則、例外處理靠後、最一般化的放最後。
本清單各項之間**沒有硬依賴**（API 為純函式），順序是偏好而非約束。

---

## 第 1 批（5 項，一批上限）— 已完成 5/5

commit：`df68553` → `043e121` → `67d4c43` → `9faa2df` → `b0f2347`
＋重構 `86bf4e4`。測試 24/24 綠（含 DiscountCalculator 的 10 項）。

執行中的兩處偏離，已記錄於各 commit 訊息：
- 第 5 項的資料除 4-0、4-2、5-3 外另加 3-5（player2 勝）。全是 player1 勝
  的話，最小實作可寫死 "Win for player1" 而不看領先者
- 第 5 項後另開 `refactor: 抽出 leader`，因為要消除的重複橫跨第 4、5 兩項
  的程式碼，非單一循環內部的重組

- [x] **1. 同分且雙方均未達 40 → `<分數>-All`**
  - 測試：`Score_EqualScoresBelowForty_ReturnsScoreAll`
  - 資料：`0-0`→`Love-All`、`1-1`→`Fifteen-All`、`2-2`→`Thirty-All`
  - 排序理由：退化案例
  - 預期 RED：`TennisGame` 型別不存在 → 編譯錯誤

- [x] **2. 比分不同且雙方均未達 40 → `<p1>-<p2>`**
  - 測試：`Score_DifferentScoresBelowForty_ReturnsBothScores`
  - 資料：`1-0`→`Fifteen-Love`、`2-1`→`Thirty-Fifteen`、`3-2`→`Forty-Thirty`
  - 排序理由：主要規則
  - 預期 RED：第 1 項的實作只處理同分，不同分會落空 → 斷言失敗

- [x] **3. 雙方同分且均達 40 以上 → `Deuce`**
  - 測試：`Score_EqualScoresAtFortyOrAbove_ReturnsDeuce`
  - 資料：`3-3`、`4-4`
  - 排序理由：邊界值，緊跟第 1 項的規則
  - 預期 RED：第 1 項的實作會把 `3-3` 報成 `Forty-All` → 斷言失敗

- [x] **4. 雙方均達 40 且差 1 分 → `Advantage <player>`**
  - 測試：`Score_BothAtFortyAndOnePointLead_ReturnsAdvantage`
  - 資料：`4-3`→`Advantage player1`、`3-4`→`Advantage player2`
  - 排序理由：承接 deuce 之後的狀態
  - 預期 RED：第 2 項的實作會拿 `4` 去查分數名稱表 → 索引越界例外

- [x] **5. 一方達 4 分以上且領先 2 分以上 → `Win for <player>`**
  - 測試：`Score_PlayerLeadsByTwoAtFourOrAbove_ReturnsWin`
  - 資料：`4-0`、`4-2`、`5-3`
  - 排序理由：終局條件，最一般化
  - 預期 RED：`4-0` 撞上分數名稱表越界

**每項的 RED 都已推演過**（給定前面各項完成後的實作狀態，確認會失敗且
失敗原因正確）。排清單時不驗算，很容易排出「寫下去直接綠」的假 RED。

---

## 第 2 批（候選，非 kata 原規則，做不做未定）

- [ ] **6. 負分 → `ArgumentOutOfRangeException`**
  - kata 原題沒有。與 `DiscountCalculator` 的守衛風格一致

- [ ] **7. 玩家名稱可指定（建構子注入）**
  - 目前 `player1` / `player2` 是寫死的字串

---

## 未定案，需要決定

1. ~~**`Advantage` / `Win` 的輸出字串格式。**~~ **已定案**：
   `Advantage player1`、`Win for player1`，玩家標識用 `player1` / `player2`
   （小寫、無空白）。沿用 Coding Dojo 慣例，便於與網路上的參考解答對照。

2. **不可能發生的分數要不要處理。**
   例如 `5-1` 在真實網球中不存在（`4-1` 就結束了）。第 5 項的實作會把它
   報成 `Win for player1`，不會出錯但也沒守著。要驗的話是第 3 批的事。

3. **`Forty-All` 這個字串永遠不該出現，但沒有測試表達這件事。**
   （第 3 項的 RED 階段親眼出現過此輸出，該項的 GREEN 消滅了它。
   但「永不出現」這個性質仍無測試守著。）
   第 3 項若把條件寫成 `> 3` 而非 `>= 3`，`3-3` 會漏回第 1 項變成
   `Forty-All`。要釘死的話是 `test:` guard commit，不是 RED。

---

## 接續方式

```
/tdd 依 docs/test-list.md 執行未打勾的前 5 項
```

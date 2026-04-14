import { useState } from "react";
import "./App.css";

const EMatchResult = {
  ExactMatch: "ExactMatch",
  CloseMatch: "CloseMatch",
  NoMatch: "NoMatch",
};

const initialData = {
  marketCode: "",
  price: "",
  quantity: "",
  amount: "",
};

const parseNumber = (value) => {
  const number = Number(value);
  return Number.isFinite(number) ? number : NaN;
};

const getMatchResult = (instruction, allegement) => {
  const instructionPrice = parseNumber(instruction.price);
  const instructionQuantity = parseNumber(instruction.quantity);
  const instructionAmount = parseNumber(instruction.amount);
  const allegementPrice = parseNumber(allegement.price);
  const allegementQuantity = parseNumber(allegement.quantity);
  const allegementAmount = parseNumber(allegement.amount);

  if (
    !instruction.marketCode ||
    !allegement.marketCode ||
    Number.isNaN(instructionPrice) ||
    Number.isNaN(instructionQuantity) ||
    Number.isNaN(instructionAmount) ||
    Number.isNaN(allegementPrice) ||
    Number.isNaN(allegementQuantity) ||
    Number.isNaN(allegementAmount)
  ) {
    return { result: null, message: "请输入所有字段，并确保数量/价格/金额为有效数字。" };
  }

  const sameMarketCode = instruction.marketCode.trim() === allegement.marketCode.trim();
  const samePrice = instructionPrice === allegementPrice;
  const sameQuantity = instructionQuantity === allegementQuantity;
  const sameAmount = instructionAmount === allegementAmount;

  if (sameMarketCode && samePrice && sameQuantity && sameAmount) {
    return { result: EMatchResult.ExactMatch, message: "ExactMatch" };
  }

  if (sameMarketCode && samePrice && sameQuantity && !sameAmount) {
    return { result: EMatchResult.CloseMatch, message: "CloseMatch" };
  }

  return { result: EMatchResult.NoMatch, message: "NoMatch" };
};

function App() {
  const [instruction, setInstruction] = useState(initialData);
  const [allegement, setAllegement] = useState(initialData);
  const [matchOutput, setMatchOutput] = useState(null);

  const handleInputChange = (section, field) => (event) => {
    const value = event.target.value;
    if (section === "instruction") {
      setInstruction((prev) => ({ ...prev, [field]: value }));
    } else {
      setAllegement((prev) => ({ ...prev, [field]: value }));
    }
  };

  const handleMatch = () => {
    const result = getMatchResult(instruction, allegement);
    setMatchOutput(result);
  };

  return (
    <div className="App">
      <header className="App-header">
        <h1>Instruction / Allegement Matcher</h1>
        <p>输入 Instruction 和 Allegement 的字段，然后点击 Match 查看匹配结果。</p>

        <div className="match-grid">
          <section className="match-card">
            <h2>Instruction</h2>
            <label>
              MarketCode
              <input
                type="text"
                value={instruction.marketCode}
                onChange={handleInputChange("instruction", "marketCode")}
                placeholder="例如 ABC"
              />
            </label>
            <label>
              Price
              <input
                type="number"
                step="0.01"
                value={instruction.price}
                onChange={handleInputChange("instruction", "price")}
                placeholder="例如 100"
              />
            </label>
            <label>
              Quantity
              <input
                type="number"
                step="1"
                value={instruction.quantity}
                onChange={handleInputChange("instruction", "quantity")}
                placeholder="例如 10"
              />
            </label>
            <label>
              Amount
              <input
                type="number"
                step="0.01"
                value={instruction.amount}
                onChange={handleInputChange("instruction", "amount")}
                placeholder="例如 1000"
              />
            </label>
          </section>

          <section className="match-card">
            <h2>Allegement</h2>
            <label>
              MarketCode
              <input
                type="text"
                value={allegement.marketCode}
                onChange={handleInputChange("allegement", "marketCode")}
                placeholder="例如 ABC"
              />
            </label>
            <label>
              Price
              <input
                type="number"
                step="0.01"
                value={allegement.price}
                onChange={handleInputChange("allegement", "price")}
                placeholder="例如 100"
              />
            </label>
            <label>
              Quantity
              <input
                type="number"
                step="1"
                value={allegement.quantity}
                onChange={handleInputChange("allegement", "quantity")}
                placeholder="例如 10"
              />
            </label>
            <label>
              Amount
              <input
                type="number"
                step="0.01"
                value={allegement.amount}
                onChange={handleInputChange("allegement", "amount")}
                placeholder="例如 1000"
              />
            </label>
          </section>
        </div>

        <button className="match-button" onClick={handleMatch}>
          Match
        </button>

        <div className="result-box">
          {matchOutput ? (
            <>
              <strong>Result:</strong> {matchOutput.message}
              <div className="result-detail">
                {matchOutput.result && `EMatchResult.${matchOutput.result}`}
              </div>
            </>
          ) : (
            <span>请填入数据后点击 Match。</span>
          )}
        </div>
      </header>
    </div>
  );
}

export default App;
